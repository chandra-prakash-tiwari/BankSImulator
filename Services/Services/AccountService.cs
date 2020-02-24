using BankSimulator.Models;
using BankSimulator.Services.Interfaces;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BankSimulator.Services.Services
{
    public class AccountService : IAccountService
    {
        public Bank CurrentBank { get; set; }

        public AccountService(string bankId)
        {
            this.CurrentBank = MasterBankService.GetBank(bankId);
        }

        public bool Deposit(string accountNumber, double amount)
        {
            var account = this.CurrentBank?.Accounts?.FirstOrDefault(c => c.Id == accountNumber);
            if (account != null)
            {
                account.FundBalance = account.FundBalance + amount;

                Transaction transaction = this.GetNewTransaction(TransactionType.Deposit, account.Id, amount);
                account.Transactions.Add(transaction);

                return true;
            }

            return false;
        }

        public bool CashWithdraw(string accountNumber, double amount)
        {
            var account = this.CurrentBank?.Accounts?.FirstOrDefault(c => c.Id == accountNumber);
            if (account != null && account.FundBalance - amount >= 0)
            {
                account.FundBalance = account.FundBalance - amount;
                Transaction transaction = this.GetNewTransaction(TransactionType.CashWithdraw, account.Id, amount);
                account.Transactions.Add(transaction);
                return true;
            }

            return false;
        }

        public double GetBalance(string accountId)
        {
            Account account = this.CurrentBank?.Accounts?.FirstOrDefault(a => a.Id == accountId);
            return account != null ? account.FundBalance : default(double);
        }

        public bool FundTransaction(string srcAccount, string descAccount, string srcBank, string descBank, double amount)
        {
            try
            {
                var sourceAcc = this.CurrentBank?.Accounts?.FirstOrDefault(src => src.Id == srcAccount);
                Account destAcc = srcBank == descBank ?
                    this.CurrentBank?.Accounts?.FirstOrDefault(desc => desc.Id == descAccount) :
                    MasterBankService.Banks.Where(a => a.Id == descBank).SelectMany(a => a.Accounts).FirstOrDefault(a => a.Id == descAccount);

                return this.FundTransfer(sourceAcc, destAcc, srcBank == descBank ? this.CurrentBank.RTGSSame : this.CurrentBank.RTGSOther, amount, srcBank, descBank);
            }
            catch (Exception)
            {
                return false;
            }
        }

        private bool FundTransfer(Account sourceAcc, Account destAccount, float rtgs, double amount, string srcBank, string descBank)
        {
            if (sourceAcc != null && destAccount != null && sourceAcc.FundBalance >= (amount + (amount * rtgs / 100)))
            {
                sourceAcc.FundBalance = sourceAcc.FundBalance - (amount + (amount * rtgs / 100));
                destAccount.FundBalance = destAccount.FundBalance + amount;
                Transaction transaction = this.GetNewTransaction(TransactionType.FundTransfer, sourceAcc.Id, destAccount.Id, descBank, amount);
                sourceAcc.Transactions.Add(transaction);
            }

            return true;
        }

        public Transaction GetNewTransaction(TransactionType type, string accountNumber, double amount)
        {
            return new Transaction()
            {
                Id = "TXN" + this.CurrentBank.Id + accountNumber + DateTime.Now.Day + DateTime.Now.Month + DateTime.Now.Year,
                SrcBankId = this.CurrentBank.Id,
                Date = DateTime.Now,
                Amount = amount,
                Mode = type,
                SrcAccountNumber = accountNumber
            };
        }

        public Transaction GetNewTransaction(TransactionType type, string srcAccountNumber, string descAccountnumber, string descBankId, double amount)
        {
            return new Transaction()
            {
                Id = "TXN" + this.CurrentBank.Id + srcAccountNumber + DateTime.Now.Day + DateTime.Now.Month + DateTime.Now.Year,
                SrcBankId = this.CurrentBank.Id,
                DestBankId=descBankId,
                SrcAccountNumber = srcAccountNumber,
                DescAccountNumber= descAccountnumber,
                Date = DateTime.Now,
                Amount = amount,
                Mode = type
            };
        }
    }
}


using Models;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Services
{
    public class AccountService : IAccountService
    {
        public Bank CurrentBank { get; set; }

        public AccountService(Bank bank)
        {
            this.CurrentBank = bank;
        }

        public bool Deposit(string accountNumber, double amount)
        {
            var account = CurrentBank.Accounts.FirstOrDefault(c => c.Id == accountNumber);
            if (account != null)
            {
                account.FundBalance = account.FundBalance + amount;

                Transaction transaction = this.GetTransaction(TransactionType.Deposit, account.Id, amount);
                account.Transactions.Add(transaction);

                return true;
            }

            return false;
        }

        public bool CashWithdraw(string accountNumber, double amount)
        {
            DateTime now = DateTime.Now;
            var account = this.CurrentBank.Accounts.FirstOrDefault(c => c.Id == accountNumber);
            if (account != null && account.FundBalance - amount >= 0)
            {
                account.FundBalance = account.FundBalance - amount;

                Transaction transaction = new Transaction
                {
                    SrcBankId = this.CurrentBank.Id,
                    AccountNumber = accountNumber
                };
                transaction = this.GetTransaction(TransactionType.CashWithdraw, account.Id, amount);
                account.Transactions.Add(transaction);
                return true;
            }

            return false;
        }

        public double GetBalance(string AccountId)
        {
            Account account = CurrentBank.Accounts.FirstOrDefault(a => a.Id == AccountId);
            return account.FundBalance;
        }

        public bool FundTransaction(string srcAccount, string descAccount, string srcBank, string descBank, double amount)
        {
            Transaction transaction = new Transaction();
            var source = this.CurrentBank.Accounts.FirstOrDefault(src => src.Id == srcAccount);
            if (srcBank == descBank)
            {
                var destination = this.CurrentBank.Accounts.FirstOrDefault(desc => desc.Id == descAccount);
                this.FundTransfer(source, destination, this.CurrentBank.RTGSSame, amount, srcBank, descBank);
            }
            else
            {
                var destination = MasterBankService.Banks.Where(a => a.Id == descBank).SelectMany(a => a.Accounts).FirstOrDefault(a => a.Id == descAccount);
                this.FundTransfer(source, destination, this.CurrentBank.RTGSOther, amount, srcBank, descBank);
            }

            return false;
        }

        private bool FundTransfer(Account sourceAcc, Account destAccount, float rtgs, double amount, string srcBank, string descBank)
        {
            Transaction transaction = new Transaction();
            if (sourceAcc != null && destAccount != null && sourceAcc.FundBalance >= (amount + (amount * rtgs / 100)))
            {
                sourceAcc.FundBalance = sourceAcc.FundBalance - (amount + (amount * rtgs / 100));
                destAccount.FundBalance = destAccount.FundBalance + amount;

                transaction.SrcBankId = srcBank;
                transaction.DestBankId = descBank;
                transaction.DescAccountNumber = sourceAcc.Id;
                transaction.AccountNumber = destAccount.Id;
                transaction = this.GetTransaction(TransactionType.FundTransfer, srcBank, amount);
                sourceAcc.Transactions.Add(transaction);
            }

            return true;
        }

        public Transaction GetTransaction(TransactionType type, string accountNumber, double amount)
        {
            DateTime now = DateTime.Now;
            return new Transaction()
            {
                Id = "TXN" + this.CurrentBank.Id + accountNumber + now.Day + now.Month + now.Year,
                Date = now.Date,
                Amount = amount,
                Mode = type,
                AccountNumber = accountNumber
            };
        }
    }
}


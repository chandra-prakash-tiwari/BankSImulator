using Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Services
{
    public class AccountService : IAccountService
    {
        public Bank CurrentBank { get; set; }
        public List<Bank> Banks { get; }

        public AccountService(Bank bank, List<Bank> Banks)
        {
            this.CurrentBank = bank;
            this.Banks = Banks;
        }

        public double? Deposit(string accountNumber, double amount)
        {
            var account = CurrentBank.Accounts.FirstOrDefault(c => c.Id == accountNumber);
            if (account != null)
            {
                account.FundBalance = account.FundBalance + amount;

                Transaction transaction = this.GetTransaction(TransactionType.Deposit, account.Id, amount);
                account.Transactions.Add(transaction);

                return account.FundBalance;
            }

            return null;
        }

        public double? CashWithdraw(string accountNumber, double amount)
        {
            DateTime now = DateTime.Now;
            var account = this.CurrentBank.Accounts.FirstOrDefault(c => c.Id == accountNumber);
            if (account != null && account.FundBalance - amount >= 0)
            {
                account.FundBalance = account.FundBalance - amount;

                Transaction transaction = new Transaction();
                transaction.SrcBankId = this.CurrentBank.Id;
                transaction.AccountNumber = accountNumber;
                transaction = this.GetTransaction(TransactionType.CashWithdraw, account.Id, amount);
                account.Transactions.Add(transaction);
                return account.FundBalance;
            }

            return null;
        }

        public double? ViewBalence(string accountNumber)
        {
            var account = this.CurrentBank.Accounts.FirstOrDefault(c => c.Id == accountNumber);
            if (account != null)
            {
                return account.FundBalance;
            }

            return null;
        }

        public bool FundTransaction(string srcAccount, string descAccount, string srcBank, string descBank, double amount)
        {
            Transaction transaction = new Transaction();
            var source = this.CurrentBank.Accounts.FirstOrDefault(src => src.Id == srcAccount);
            if (srcBank == descBank)
            {
                var destination = this.CurrentBank.Accounts.FirstOrDefault(desc => desc.Id == descAccount);
                this.FundTransfer(source, destination, this.CurrentBank.RtgsSame, amount, srcBank, descBank);
            }
            else
            {
                var destination = this.Banks.Where(a => a.Id == descBank).SelectMany(a => a.Accounts).FirstOrDefault(a => a.Id == descAccount);
                this.FundTransfer(source, destination, this.CurrentBank.RtgsOther, amount, srcBank, descBank);
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

        public bool RevertTransaction(string id)
        {
            Transaction transaction = this.CurrentBank.Accounts.SelectMany(a => a.Transactions).Single(a => a.Id == id);
            if (transaction != null)
            {
                Account srcAccount = this.CurrentBank.Accounts.SingleOrDefault(a => a.Id == transaction.AccountNumber);
                if (srcAccount != null)
                {
                    if (transaction.Mode == TransactionType.Deposit)
                    {
                        srcAccount.FundBalance = srcAccount.FundBalance - transaction.Amount;
                        srcAccount.Transactions.Remove(transaction);
                        return true;
                    }
                    else if (transaction.Mode == TransactionType.CashWithdraw)
                    {
                        srcAccount.FundBalance = srcAccount.FundBalance + transaction.Amount;
                        srcAccount.Transactions.Remove(transaction);
                        return true;
                    }
                    else if (transaction.Mode == TransactionType.FundTransfer)
                    {
                        if (transaction.SrcBankId == transaction.DestBankId)
                        {
                            Account destAccount = this.CurrentBank.Accounts.SingleOrDefault(a => a.Id == transaction.AccountNumber);
                            if (destAccount != null)
                            {
                                srcAccount.FundBalance = srcAccount.FundBalance + transaction.Amount;
                                destAccount.FundBalance = destAccount.FundBalance - transaction.Amount;
                                srcAccount.Transactions.Remove(transaction);
                                return true;
                            }
                        }
                        else
                        {
                            Account destAccount = this.Banks.Where(a => a.Id == transaction.DestBankId).SelectMany(a => a.Accounts).FirstOrDefault(a => a.Id == transaction.AccountNumber);
                            if (destAccount != null)
                            {
                                srcAccount.FundBalance = srcAccount.FundBalance + transaction.Amount;
                                destAccount.FundBalance = destAccount.FundBalance - transaction.Amount;
                                srcAccount.Transactions.Remove(transaction);
                                return true;
                            }
                        }
                    }
                }
            }

            return false;
        }
    }
}

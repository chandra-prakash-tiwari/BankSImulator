using Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Services
{
    public class AccountService : IAccountService
    {
        public Bank Bank { get; set; }
        public List<Bank> Banks { get; }

        public AccountService(Bank bank, List<Bank> Banks)
        {
            this.Bank = bank;
            this.Banks = Banks;
        }

        public double? Deposit(string accountNumber, double amount)
        {
            var account = Bank.Accounts.FirstOrDefault(c => c.Id == accountNumber);
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

            var account = this.Bank.Accounts.FirstOrDefault(c => c.Id == accountNumber);
            if (account != null && account.FundBalance - amount >= 0)
            {
                Transaction transaction = new Transaction();

                account.FundBalance = account.FundBalance - amount;

                transaction.BankId = this.Bank.Id;
                transaction.Account = accountNumber;
                transaction = this.GetTransaction(TransactionType.CashWithdraw, account.Id, amount);

                account.Transactions.Add(transaction);
                return account.FundBalance;
            }

            return null;
        }

        public double? ViewBalence(string accountNumber)
        {
            var account = this.Bank.Accounts.FirstOrDefault(c => c.Id == accountNumber);
            if(account != null)
            {
                return account.FundBalance;
            }

            return null;
        }

        public bool FundTransaction(string srcAccount, string descAccount, string srcBank, string descBank, double amount)
        {
            var source = this.Bank.Accounts.FirstOrDefault(src => src.Id == srcAccount);

            if (srcBank == descBank)
            {
                var destination = this.Bank.Accounts.FirstOrDefault(desc => desc.Id == descAccount);

                if (source != null && destination != null && source.FundBalance >= (amount + (amount * this.Bank.RtgsSame / 100)))
                {
                    Transaction transaction = new Transaction();
                    source.FundBalance = source.FundBalance - (amount + (amount * this.Bank.RtgsSame / 100));
                    destination.FundBalance = destination.FundBalance + amount;

                    transaction.SourceBank = srcBank;
                    transaction.DestinationBank = descBank;
                    transaction.SourceAccount = srcAccount;
                    transaction.DestinationAccount = descAccount;
                    transaction = this.GetTransaction(TransactionType.FundTransfer, source.Id, amount);

                    source.Transactions.Add(transaction);

                    return true;
                }
            }
            else
            {
                var destination = this.Banks.Where(a => a.Id == descBank).SelectMany(a => a.Accounts).FirstOrDefault(a => a.Id == descAccount);

                if (source != null && destination != null && source.FundBalance >= (amount + (amount * this.Bank.RtgsOther / 100)))
                {
                    Transaction transaction = new Transaction();
                    source.FundBalance = source.FundBalance - (amount + (amount * this.Bank.RtgsOther / 100));
                    destination.FundBalance = destination.FundBalance + amount;

                    transaction.SourceBank = srcBank;
                    transaction.DestinationBank = descBank;
                    transaction.SourceAccount = srcAccount;
                    transaction.DestinationAccount = descAccount;
                    transaction = this.GetTransaction(TransactionType.FundTransfer, source.Id, amount);

                    source.Transactions.Add(transaction);

                    return true;
                }
            }

            return false;
        }

        public Transaction GetTransaction(TransactionType type, string accountNumber, double amount)
        {
            DateTime now = DateTime.Now;

            return new Transaction()
            {
                Id = "TXN" + this.Bank.Id + accountNumber + now.Day + now.Month + now.Year,
                Date = now.Date,
                Amount = amount,
                Mode = type,
                Account=accountNumber
            };
        }

        public bool RevertTransaction(string id)
        {
            Transaction transaction = this.Bank.Accounts.SelectMany(a => a.Transactions).Single(a => a.Id==id);

            Account account = this.Bank.Accounts.SingleOrDefault(a => a.Id == transaction.Account);
            if (transaction.Mode == TransactionType.Deposit)
            {
                account.FundBalance = account.FundBalance - transaction.Amount;
                return true;
            }

            else if (transaction.Mode == TransactionType.CashWithdraw)
            {
                account.FundBalance = account.FundBalance + transaction.Amount;
                return true;
            }

            else if(transaction.Mode==TransactionType.FundTransfer)
            {
                Account sourceAccount = this.Bank.Accounts.SingleOrDefault(a => a.Id == transaction.SourceAccount);

                if (transaction.SourceBank==transaction.DestinationBank)
                {
                    Account destinationAccount = this.Bank.Accounts.SingleOrDefault(a => a.Id == transaction.DestinationAccount);

                    sourceAccount.FundBalance = sourceAccount.FundBalance + transaction.Amount;
                    destinationAccount.FundBalance = destinationAccount.FundBalance - transaction.Amount;
                }

                else
                {
                    Account destinationAccount = this.Banks.Where(a => a.Id == transaction.DestinationBank).SelectMany(a => a.Accounts).FirstOrDefault(a => a.Id == transaction.DestinationAccount);

                    sourceAccount.FundBalance = sourceAccount.FundBalance + transaction.Amount;
                    destinationAccount.FundBalance = destinationAccount.FundBalance - transaction.Amount;
                }
            }

            return true;
        }
    }
}

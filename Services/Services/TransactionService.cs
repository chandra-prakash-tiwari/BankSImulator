using BankSimulator.Models;
using BankSimulator.Services.Interfaces;
using System;
using System.Linq;

namespace Services.Services
{
    public class TransactionService: ITransactionService
    {
        public Bank CurrentBank { get; set; }

        public TransactionService(string bankId)
        {
            this.CurrentBank = MasterBankService.GetBank(bankId);
        }

        public double ViewBalence(string accountNumber)
        {
            var account = this.CurrentBank.Accounts.FirstOrDefault(c => c.Id == accountNumber);
            if (account != null)
            {
                return account.FundBalance;
            }

            return default(double);
        }

        public bool RevertTransaction(string id)
        {
            try
            {
                Transaction transaction = this.CurrentBank.Accounts.SelectMany(a => a.Transactions).Single(a => a.Id == id);
                if (transaction != null)
                {
                    Account srcAccount = this.CurrentBank.Accounts.FirstOrDefault(a => a.Id == transaction.SrcAccountNumber);
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
                                Account destAccount = this.CurrentBank.Accounts.FirstOrDefault(a => a.Id == transaction.SrcAccountNumber);
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
                                Account destAccount = MasterBankService.Banks.Where(a => a.Id == transaction.DestBankId).SelectMany(a => a.Accounts).FirstOrDefault(a => a.Id == transaction.SrcAccountNumber);
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
            catch(Exception)
            {
                return false;
            }
        }
    }
}

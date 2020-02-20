using Models;
using Services.Interfaces;
using System.Linq;

namespace Services.Services
{
    public class TransactionService: ITransactionService
    {
        public string BankId { get; set; }

        public TransactionService(string bankId)
        {
            this.BankId = bankId;
        }

        public double? ViewBalence(string accountNumber)
        {
            var account = MasterBankService.GetBank(this.BankId).Accounts.FirstOrDefault(c => c.Id == accountNumber);
            if (account != null)
            {
                return account.FundBalance;
            }

            return null;
        }

        public bool RevertTransaction(string id)
        {
            Transaction transaction = MasterBankService.GetBank(this.BankId).Accounts.SelectMany(a => a.Transactions).Single(a => a.Id == id);
            if (transaction != null)
            {
                Account srcAccount = MasterBankService.GetBank(this.BankId).Accounts.FirstOrDefault(a => a.Id == transaction.AccountNumber);
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
                            Account destAccount = MasterBankService.GetBank(this.BankId).Accounts.FirstOrDefault(a => a.Id == transaction.AccountNumber);
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
                            Account destAccount = MasterBankService.Banks.Where(a => a.Id == transaction.DestBankId).SelectMany(a => a.Accounts).FirstOrDefault(a => a.Id == transaction.AccountNumber);
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

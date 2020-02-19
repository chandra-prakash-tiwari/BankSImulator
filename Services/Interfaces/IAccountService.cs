namespace Services
{
    public interface IAccountService
    {
        bool Deposit(string accountNumber, double amount);

        bool CashWithdraw(string accountNumber, double amount);

        bool FundTransaction(string srcAccount, string destAccount, string srcBankId, string destBankId, double amount);
    }
}

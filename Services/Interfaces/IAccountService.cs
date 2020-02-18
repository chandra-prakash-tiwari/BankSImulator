namespace Services
{
    public interface IAccountService
    {
        double? Deposit(string accountNumber, double amount);

        double? CashWithdraw(string accountNumber, double amount);

        double? ViewBalence(string accountNumber);

        bool FundTransaction(string srcAccount, string destAccount, string srcBankId, string destBankId, double amount);
    }
}

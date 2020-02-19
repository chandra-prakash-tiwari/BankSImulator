
namespace Models
{
    public enum MainMenu
    {
        CreateBank = 1,
        Login,
        Exit
    };

    public enum UserType
    {
        Admin,
        Employee,
        Account,
    };

    public enum AdministratorMenu
    {
        AddEmployee = 1,
        RemoveEmployee,
        UpdateEmployee,
        AddAccount,
        RemoveAccount,
        UpdateAccount,
        Transaction,
        CurrencyExchange,
        SignOut,
        Exit
    };

    public enum EmployeeMenu
    {
        AddAccount = 1,
        RemoveAccount,
        UpdateAccount,
        Transaction,
        CurrencyExchange,
        SignOut,
        Exit
    };

    public enum TransactionMenu
    {
        Deposit = 1,
        CashWithdraw,
        FundTransfer,
        ViewAccountAccount,
        TransactionRevert,
        SignOut,
        Exit
    };

    public enum CustomerMenu
    {
        Deposit = 1,
        CashWithdraw,
        FundTransfer,
        ViewBalance,
        SignOut,
        Exit
    };

    public enum TransactionType
    {
        Deposit,
        CashWithdraw,
        FundTransfer,
    };

    public enum TransactionStatus
    {
        Pending,
        Success,
        Failed
    };

    public enum UpdateField
    {
        Name=1,
        Address,
        PhoneNumber,
        Password
    };
}

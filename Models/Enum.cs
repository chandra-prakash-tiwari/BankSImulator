
namespace Models
{
    public enum MainMenu
    {
        NewBank = 1,
        Login,
        Exit
    };

    public enum UserType
    {
        Admin = 0,
        Employee,
        Account,
    };

    public enum AdministratorOption
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

    public enum EmployeeOption
    {
        AddAccount = 1,
        RemoveAccount,
        UpdateAccount,
        Transaction,
        CurrencyExchange,
        SignOut,
        Exit
    };

    public enum EmployeeTransaction
    {
        Deposit = 1,
        CashWithdraw,
        FundTransfer,
        ViewAccountAccount,
        TransactionRevert,
        SignOut,
        Exit
    };

    public enum CustomerOption
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
    
}


using BankSimulator.GetData;
using Models;
using Services;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BankSimulator
{
    class Program
    {
        static void Main()
        {
            new BankSimulator().Initialize();
        }

        public static void CustomerMenu(AccountService accountService, User user)
        {
            Console.Clear();
            Account account = MasterBankService.GetBank(accountService.BankId).Accounts.FirstOrDefault(a => a.Holder.UserId == user.UserId);
            Console.Write(Constrants.UserMenu);
            CustomerMenu option = (CustomerMenu)Helper.GetValidInteger();
            Console.Clear();
            switch (option)
            {
                case Models.CustomerMenu.Deposit:
                    Console.Write(Constrants.TransactionAmount);
                    double amount = double.Parse(Console.ReadLine());

                    if (!accountService.Deposit(account.Id, amount))
                    {
                        Console.WriteLine(Constrants.AvailbleBalance + accountService.GetBalance(account.Id));
                        Console.ReadKey();
                    }
                    else
                    {
                        Console.WriteLine("Deposit Will Be not perform for some reason");
                        Console.ReadKey();
                    }

                    CustomerMenu(accountService, user);

                    break;

                case Models.CustomerMenu.CashWithdraw:
                    Console.Write(Constrants.TransactionAmount);
                    amount = Helper.GetValidDouble();
                    if (!accountService.CashWithdraw(account.Id, amount))
                    {
                        Console.WriteLine(Constrants.AvailbleBalance + accountService.GetBalance(account.Id));
                        Console.ReadKey();
                    }
                    else
                    {
                        Console.WriteLine("Fund Balance is not sufficient");
                        Console.ReadKey();
                    }

                    CustomerMenu(accountService, user);

                    break;

                case Models.CustomerMenu.FundTransfer:
                    Console.Write("Receiver Account Number :");
                    string accountNumber = Helper.GetValidString();
                    Console.Write("Receiver BankID :");
                    string Id = Helper.GetValidString();
                    Console.WriteLine(Constrants.TransactionAmount);
                    amount = Helper.GetValidDouble();

                    if (accountService.FundTransaction(account.Id, accountNumber, accountService.BankId, Id, amount))
                    {
                        Console.WriteLine("Fund transfer is not completed");
                        Console.ReadKey();
                    }

                    CustomerMenu(accountService, user);

                    break;

                case Models.CustomerMenu.ViewBalance:
                    TransactionService transactionService = new TransactionService(accountService.BankId);
                    transactionService.ViewBalence(account.Id);
                    Console.ReadLine();

                    CustomerMenu(accountService, user);

                    break;

                case Models.CustomerMenu.SignOut:
                    break;

                case Models.CustomerMenu.Exit:
                    Environment.Exit(0);

                    break;

                default:
                    CustomerMenu(accountService, user);
                    break;
            }
        }

        public static void AdministratorMenu(BankService bankService, AccountService accountService)
        {
            Console.Clear();
            Console.Write(Constrants.AdminMenu);
            AdministratorMenu option = (AdministratorMenu)Helper.GetValidInteger();
            Console.Clear();
            switch (option)
            {
                case Models.AdministratorMenu.AddEmployee:
                    Employee employee = bankService.CreateEmployee(UserInput.GetEmployeeDetails());
                    Console.WriteLine($"Employee Id : {employee.Id}");
                    Console.WriteLine($"Employee User Id : {employee.UserId}");
                    Console.ReadKey();
                    AdministratorMenu(bankService, accountService);

                    break;

                case Models.AdministratorMenu.RemoveEmployee:
                    Console.WriteLine("Enter Employee Id");
                    string employeeId = Helper.GetValidString();
                    int index = bankService.SearchEmployee(employeeId);
                    if ( index == -1)
                        Console.WriteLine(Constrants.UserNotFound);
                    else
                    {
                        bankService.RemoveEmployee(index);
                        Console.WriteLine("Employee Will be Removed Successfully");
                        Console.ReadKey();
                    }

                    Console.ReadKey();
                    AdministratorMenu(bankService, accountService);

                    break;

                case Models.AdministratorMenu.UpdateEmployee:
                    Console.Write("Enter Employee Id");
                    employeeId = Helper.GetValidString();
                    index = bankService.SearchEmployee(employeeId);
                    if (index == -1)
                    {
                        Console.WriteLine(Constrants.UserNotFound);
                    }
                    else
                    {
                        bankService.UpdateEmployee(UserInput.GetEmployeeDetails(), employeeId);
                        Console.WriteLine("Employee Detail will be updated");
                    }

                    Console.ReadKey();
                    AdministratorMenu(bankService, accountService);

                    break;

                case Models.AdministratorMenu.AddAccount:
                    Account account = bankService.CreateAccount(UserInput.GetAccountDetails());
                    Console.WriteLine(Constrants.AccountNumber + account.Id);
                    Console.WriteLine(Constrants.UserId + account.Holder.UserId);
                    Console.WriteLine(Constrants.Password + account.Holder.Password);
                    Console.WriteLine(Constrants.BankId + account.Holder.BankId);
                    Console.ReadKey();
                    AdministratorMenu(bankService, accountService);

                    break;

                case Models.AdministratorMenu.RemoveAccount:
                    Console.Write(Constrants.AccountNumber);
                    string AccountId = Helper.GetValidString();

                    index = bankService.SearchAccount(AccountId);
                    if (index == -1)
                        Console.WriteLine(Constrants.UserNotFound);
                    else
                    {
                        bankService.RemoveAccount(index);
                        Console.WriteLine("Account will be removed");
                    }

                    Console.ReadKey();
                    AdministratorMenu(bankService, accountService);

                    break;

                case Models.AdministratorMenu.UpdateAccount:
                    Console.Write(Constrants.AccountNumber);
                    AccountId = Helper.GetValidString();
                    index = bankService.SearchAccount(AccountId);
                    if (index == -1)
                        Console.WriteLine(Constrants.UserNotFound);
                    else
                    {
                        bankService.UpdateAccount(UserInput.GetAccountDetails(), AccountId);
                        Console.WriteLine("Account Detail will be updated");
                    }

                    Console.ReadKey();
                    AdministratorMenu(bankService, accountService);

                    break;

                case Models.AdministratorMenu.Transaction:
                    TransactionMenu(bankService, accountService);
                    AdministratorMenu(bankService, accountService);

                    break;

                case Models.AdministratorMenu.CurrencyExchange:
                    Console.Write(Constrants.TransactionAmount);
                    double amount = Helper.GetValidDouble();
                    Console.Write("Enter the currency Rate");
                    float currencyRate = Helper.GetValidFloat();

                    Console.WriteLine($"{amount * currencyRate} amount will be pay");
                    AdministratorMenu(bankService, accountService);

                    break;

                case Models.AdministratorMenu.SignOut:

                    break;

                case Models.AdministratorMenu.Exit:
                    Environment.Exit(0);

                    break;

                default:

                    AdministratorMenu(bankService, accountService);

                    break;
            }
        }

        public static void TransactionMenu(BankService bankService, AccountService accountService)
        {
            Console.Clear();

            TransactionService transactionService = new TransactionService(bankService.BankId);
            Console.Write(Constrants.TransactionMenu);

            TransactionMenu option = (TransactionMenu)Helper.GetValidInteger();
            Console.Clear();
            switch (option)
            {
                case Models.TransactionMenu.Deposit:
                    Console.Write(Constrants.AccountNumber);
                    string accountNumber = Helper.GetValidString();
                    int index = bankService.SearchAccount(accountNumber);
                    if (index == -1)
                        Console.WriteLine(Constrants.UserNotFound);
                    else
                    {
                        Console.Write(Constrants.TransactionAmount);
                        double payout = Helper.GetValidDouble();

                        Console.Write("Current Currency Rate  :");
                        double currencyValue = Helper.GetValidDouble();

                        payout = currencyValue * payout;

                        if (!accountService.Deposit(accountNumber, payout))
                            Console.WriteLine($"Your new Balance is : {accountService.GetBalance(accountNumber)}");

                        else
                            Console.WriteLine("Deposit operation is not performed");
                    }
                    Console.ReadKey();

                    break;

                case Models.TransactionMenu.CashWithdraw:
                    Console.Write(Constrants.AccountNumber);
                    accountNumber = Helper.GetValidString();

                    index = bankService.SearchAccount(accountNumber);
                    if (index == -1)
                        Console.WriteLine(Constrants.UserNotFound);
                    else
                    {
                        Console.Write(Constrants.TransactionAmount);
                        double payout = Helper.GetValidDouble();

                        if (accountService.CashWithdraw(accountNumber, payout))
                            Console.WriteLine($"Your new Balance is : {accountService.GetBalance(accountNumber)}");

                        else
                            Console.WriteLine("CashWithdraw operation is not performed");
                    }
                    
                    Console.ReadKey();

                    break;

                case Models.TransactionMenu.FundTransfer:
                    Console.Write($"Source {Constrants.AccountNumber}");
                    string SourceAccountNumber = Helper.GetValidString();

                    Console.Write($"Destination {Constrants.AccountNumber}");
                    string DestinationAccountNumber = Helper.GetValidString();

                    Console.Write("Enter Destination IFSC Code");
                    string DestinationIFSCCode = Helper.GetValidString();

                    Console.Write(Constrants.TransactionAmount);
                    double amount = Helper.GetValidDouble();
                    if (accountService.FundTransaction(SourceAccountNumber, DestinationAccountNumber, bankService.BankId, DestinationIFSCCode, amount))
                        Console.WriteLine("Fund Transfer will be not perform ");
                    Console.ReadKey();

                    break;

                case Models.TransactionMenu.ViewAccountAccount:
                    Console.Write(Constrants.AccountNumber);
                    string getAccountNumber = Helper.GetValidString();

                    double? fundBalance = transactionService.ViewBalence(getAccountNumber);
                    if (fundBalance != null)
                        Console.WriteLine($"Your Balance is {fundBalance}");
                    else
                        Console.WriteLine("this account number is not present in current bank");

                    Console.ReadKey();

                    break;

                case Models.TransactionMenu.TransactionRevert:
                    Console.Write("Enter the Transaction Id");
                    string Id = Helper.GetValidString();

                    if (!transactionService.RevertTransaction(Id))
                        Console.WriteLine("Transaction Revert will not be done");
                    else
                        Console.WriteLine("Transaction Revert completed");

                    Console.ReadKey();

                    break;

                case Models.TransactionMenu.SignOut:
                    break;

                case Models.TransactionMenu.Exit:
                    Environment.Exit(0);

                    break;

                default:
                    TransactionMenu(bankService, accountService);

                    break;
            }
        }

        public static void EmployeeMenu(BankService bankService, AccountService accountService)
        {
            Console.Clear();
            Console.Write(Constrants.EmployeeMenu);
            EmployeeMenu option = (EmployeeMenu)Helper.GetValidInteger();
            Console.Clear();
            switch (option)
            {
                case Models.EmployeeMenu.AddAccount:
                    bankService.CreateAccount(UserInput.GetAccountDetails());

                    EmployeeMenu(bankService, accountService);

                    break;

                case Models.EmployeeMenu.RemoveAccount:
                    Console.Write(Constrants.AccountNumber);
                    string AccountId = Helper.GetValidString();

                    int index = bankService.SearchAccount(AccountId);
                    if (index == -1)
                        Console.WriteLine(Constrants.UserNotFound);
                    else
                    {
                        bankService.RemoveAccount(index);
                        Console.WriteLine("Account will be removed");
                    }

                    Console.ReadKey();
                    EmployeeMenu(bankService, accountService);

                    break;

                case Models.EmployeeMenu.UpdateAccount:
                    Console.Write("Enter Account Id");
                    AccountId = Helper.GetValidString();

                    index = bankService.SearchAccount(AccountId);
                    if (index == -1)
                        Console.WriteLine(Constrants.UserNotFound);
                    else
                    {
                        bankService.UpdateAccount(UserInput.GetAccountDetails(), AccountId);
                        Console.WriteLine("Account will be removed");
                    }

                    Console.ReadKey();
                    EmployeeMenu(bankService, accountService);

                    break;

                case Models.EmployeeMenu.Transaction:
                    TransactionMenu(bankService, accountService);
                    EmployeeMenu(bankService, accountService);

                    break;

                case Models.EmployeeMenu.CurrencyExchange:
                    Console.Write("Enter the Amount");
                    double amount = Helper.GetValidDouble();
                    Console.Write("Enter the currency Rate");
                    float currencyRate = Helper.GetValidFloat();
                    Console.Write($"{amount * currencyRate} amount will be pay");
                    EmployeeMenu(bankService, accountService);

                    break;

                case Models.EmployeeMenu.SignOut:

                    break;

                case Models.EmployeeMenu.Exit:
                    Environment.Exit(0);

                    break;

                default:
                    EmployeeMenu(bankService, accountService);

                    break;
            }
        }
    }

    public class BankSimulator
    {
        //public BankService BankService { get; set; }

        public User CurrentUser { get; set; }

        public void Initialize()
        {
            Console.Clear();
            Console.Write(Constrants.MainMenu);
            MainMenu option = (MainMenu)Helper.GetValidInteger();
            Console.Clear();
            switch (option)
            {
                case MainMenu.CreateBank:
                    Bank bank = UserInput.GetBankDetails();
                    bank.Admin = UserInput.GetAdminDetails();
                    bank = MasterBankService.CreateBank(bank);
                    Console.WriteLine(Constrants.BankId + bank.Id);
                    Console.WriteLine("Admin Credentials");
                    Console.WriteLine(Constrants.UserId + bank.Admin.UserId);
                    Console.WriteLine(Constrants.Password + bank.Admin.Password);
                    Console.ReadKey();
                    Initialize();

                    break;

                case MainMenu.Login:
                    this.CurrentUser = this.Login(Login());
                    NavigateUser(this.CurrentUser);

                    break;

                case MainMenu.Exit:
                    Environment.Exit(0);

                    break;

                default:
                    this.Initialize();

                    break;
            }
        }

        public static Login Login()
        {
            Login login = new Login();

            Console.Write(Constrants.UserId);
            login.UserName = Helper.GetValidString();
            Console.Write(Constrants.Password);
            login.Password = Helper.GetValidString();

            return login;
        }

        public User Login(Login login)
        {
            string bankId = login.UserName.Substring(3);
            Bank bank = MasterBankService.Banks.FirstOrDefault(b => b.Id == bankId);

            if (bank != null)
            {
                if (bank.Admin.UserId == login.UserName && bank.Admin.Password == login.Password)
                {
                    return bank.Admin;
                }
                else
                {
                    Employee employee = bank.Employees.FirstOrDefault(e => e.UserId == login.UserName);
                    Account customer = bank.Accounts.FirstOrDefault(c => c.Holder.UserId == login.UserName);

                    if (employee != null && employee.Password == login.Password)
                    {
                        return employee;
                    }

                    if (customer != null && customer.Holder.Password == login.Password)
                    {
                        return customer.Holder;
                    }
                }
                return null;
            }
            else
            {
                return null;
            }
        }

        public void NavigateUser(User CurrentUser)
        {
            if (this.CurrentUser != null)
            {
                Bank bank = MasterBankService.Banks.FirstOrDefault(c => c.Id == CurrentUser.UserId.Substring(3));
                AccountService accountService = new AccountService(bank.Id);
                var bankService = new BankService(bank.Id);

                switch (this.CurrentUser.UserType)
                {
                    case UserType.Admin:
                        Program.AdministratorMenu(bankService, accountService);
                        break;

                    case UserType.Employee:
                        Program.EmployeeMenu(bankService, accountService);
                        break;

                    case UserType.Account:

                        Program.CustomerMenu(accountService, CurrentUser);
                        break;

                    default:
                        Console.WriteLine("User Can't Find");
                        Console.Read();
                        break;
                }
            }
            else
            {
                Console.WriteLine(Constrants.UserNotFound);
                this.Initialize();
            }
            this.Initialize();
        }
    }
}

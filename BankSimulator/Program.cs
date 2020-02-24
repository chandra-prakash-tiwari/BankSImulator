
using BankSimulator.Models;
using BankSimulator.Services.Services;
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
            Account account = accountService.CurrentBank.Accounts.FirstOrDefault(a => a.Holder.UserId == user.UserId);
            Console.Write(Constant.UserMenu);
            CustomerMenu option = (CustomerMenu)Helper.GetValidInteger();
            Console.Clear();
            switch (option)
            {
                case Models.CustomerMenu.Deposit:
                    Console.Write(Constant.TransactionAmount);
                    double amount = double.Parse(Console.ReadLine());

                    if (!accountService.Deposit(account.Id, amount))
                    {
                        Console.WriteLine(Constant.AvailbleBalance + accountService.GetBalance(account.Id));
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
                    Console.Write(Constant.TransactionAmount);
                    amount = Helper.GetValidDouble();
                    if (!accountService.CashWithdraw(account.Id, amount))
                    {
                        Console.WriteLine(Constant.AvailbleBalance + accountService.GetBalance(account.Id));
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
                    Console.WriteLine(Constant.TransactionAmount);
                    amount = Helper.GetValidDouble();

                    if (accountService.FundTransaction(account.Id, accountNumber, accountService.CurrentBank.Id, Id, amount))
                    {
                        Console.WriteLine("Fund transfer is not completed");
                        Console.ReadKey();
                    }

                    CustomerMenu(accountService, user);

                    break;

                case Models.CustomerMenu.ViewBalance:
                    TransactionService transactionService = new TransactionService(accountService.CurrentBank.Id);
                    Console.WriteLine("Available balance in your account is: ", transactionService.ViewBalence(account.Id));
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
            Console.Write(Constant.AdminMenu);
            AdministratorMenu option = (AdministratorMenu)Helper.GetValidInteger();
            Console.Clear();
            switch (option)
            {
                case Models.AdministratorMenu.AddEmployee:
                    string employeeId = bankService.CreateEmployee(UserInput.GetEmployeeDetails());
                    Employee employee = bankService.GetEmployee(employeeId);
                    Console.WriteLine($"Employee Id : {employeeId}");
                    Console.WriteLine($"Employee User Id : {employee.UserId}");
                    Console.WriteLine($"Employee Password : {employee.Password}");
                    Console.ReadKey();
                    AdministratorMenu(bankService, accountService);

                    break;

                case Models.AdministratorMenu.RemoveEmployee:
                    Console.WriteLine("Enter Employee Id");
                    employeeId = Helper.GetValidString();

                    if (!bankService.HasEmployee(employeeId))
                        Console.WriteLine(Constant.UserNotFound);
                    else
                    {
                        bankService.RemoveEmployee(employeeId);
                        Console.WriteLine("Employee Will be Removed Successfully");
                        Console.ReadKey();
                    }

                    Console.ReadKey();
                    AdministratorMenu(bankService, accountService);

                    break;

                case Models.AdministratorMenu.UpdateEmployee:
                    Console.Write("Enter Employee Id");
                    employeeId = Helper.GetValidString();

                    if (!bankService.HasEmployee(employeeId))
                    {
                        Console.WriteLine(Constant.UserNotFound);
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
                    string accountId = bankService.CreateAccount(UserInput.GetAccountDetails());
                    Account account = bankService.GetAccount(accountId);
                    Console.WriteLine(Constant.AccountNumber + account.Id);
                    Console.WriteLine(Constant.UserId + account.Holder.UserId);
                    Console.WriteLine(Constant.Password + account.Holder.Password);
                    Console.WriteLine(Constant.BankId + account.Holder.BankId);
                    Console.ReadKey();
                    AdministratorMenu(bankService, accountService);

                    break;

                case Models.AdministratorMenu.RemoveAccount:
                    Console.Write(Constant.AccountNumber);
                    accountId = Helper.GetValidString();

                    if (!bankService.HasAccount(accountId))
                        Console.WriteLine(Constant.UserNotFound);
                    else
                    {
                        bankService.RemoveAccount(accountId);
                        Console.WriteLine("Account will be removed");
                    }

                    Console.ReadKey();
                    AdministratorMenu(bankService, accountService);

                    break;

                case Models.AdministratorMenu.UpdateAccount:
                    Console.Write(Constant.AccountNumber);
                    accountId = Helper.GetValidString();

                    if (bankService.HasAccount(accountId))
                        Console.WriteLine(Constant.UserNotFound);
                    else
                    {
                        bankService.UpdateAccount(UserInput.GetAccountDetails(), accountId);
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
                    Console.Write(Constant.TransactionAmount);
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

            TransactionService transactionService = new TransactionService(bankService.CurrentBank.Id);
            Console.Write(Constant.TransactionMenu);

            TransactionMenu option = (TransactionMenu)Helper.GetValidInteger();
            Console.Clear();
            switch (option)
            {
                case Models.TransactionMenu.Deposit:
                    Console.Write(Constant.AccountNumber);
                    string accountNumber = Helper.GetValidString();

                    if (bankService.HasAccount(accountNumber))
                        Console.WriteLine(Constant.UserNotFound);
                    else
                    {
                        Console.Write(Constant.TransactionAmount);
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
                    Console.Write(Constant.AccountNumber);
                    accountNumber = Helper.GetValidString();

                    if (bankService.HasAccount(accountNumber))
                        Console.WriteLine(Constant.UserNotFound);
                    else
                    {
                        Console.Write(Constant.TransactionAmount);
                        double payout = Helper.GetValidDouble();

                        if (accountService.CashWithdraw(accountNumber, payout))
                            Console.WriteLine($"Your new Balance is : {accountService.GetBalance(accountNumber)}");

                        else
                            Console.WriteLine("CashWithdraw operation is not performed");
                    }

                    Console.ReadKey();

                    break;

                case Models.TransactionMenu.FundTransfer:
                    Console.Write($"Source {Constant.AccountNumber}");
                    string SourceAccountNumber = Helper.GetValidString();

                    Console.Write($"Destination {Constant.AccountNumber}");
                    string DestinationAccountNumber = Helper.GetValidString();

                    Console.Write("Enter Destination IFSC Code");
                    string DestinationIFSCCode = Helper.GetValidString();

                    Console.Write(Constant.TransactionAmount);
                    double amount = Helper.GetValidDouble();
                    if (accountService.FundTransaction(SourceAccountNumber, DestinationAccountNumber, bankService.CurrentBank.Id, DestinationIFSCCode, amount))
                        Console.WriteLine("Fund Transfer will be not perform ");
                    Console.ReadKey();

                    break;

                case Models.TransactionMenu.ViewAccountAccount:
                    Console.Write(Constant.AccountNumber);
                    string getAccountNumber = Helper.GetValidString();

                    double? fundBalance = transactionService.ViewBalence(getAccountNumber);
                    if (fundBalance != null)
                        Console.WriteLine($"Your balance is {fundBalance}");
                    else
                        Console.WriteLine("this account number is not present in current bank");

                    Console.ReadKey();

                    break;

                case Models.TransactionMenu.TransactionRevert:
                    Console.Write("Enter the Transaction Id");
                    string Id = Helper.GetValidString();

                    if (!transactionService.RevertTransaction(Id))
                        Console.WriteLine("Transaction revert will not be done");
                    else
                        Console.WriteLine("Transaction revert completed");

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
            Console.Write(Constant.EmployeeMenu);
            EmployeeMenu option = (EmployeeMenu)Helper.GetValidInteger();
            Console.Clear();
            switch (option)
            {
                case Models.EmployeeMenu.AddAccount:
                    bankService.CreateAccount(UserInput.GetAccountDetails());

                    EmployeeMenu(bankService, accountService);

                    break;

                case Models.EmployeeMenu.RemoveAccount:
                    Console.Write(Constant.AccountNumber);
                    string accountId = Helper.GetValidString();

                    Account account = bankService.GetAccount(accountId);
                    if (account == null)
                        Console.WriteLine(Constant.UserNotFound);
                    else
                    {
                        bankService.RemoveAccount(accountId);
                        Console.WriteLine("Account will be removed");
                    }

                    Console.ReadKey();

                    Console.ReadKey();
                    EmployeeMenu(bankService, accountService);

                    break;

                case Models.EmployeeMenu.UpdateAccount:
                    Console.Write("Enter Account Id");
                    accountId = Helper.GetValidString();

                    account = bankService.GetAccount(accountId);
                    if (account == null)
                        Console.WriteLine(Constant.UserNotFound);
                    else
                    {
                        bankService.UpdateAccount(UserInput.GetAccountDetails(), accountId);
                        Console.WriteLine("Account Detail will be updated");
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
        public User CurrentUser { get; set; }

        public void Initialize()
        {
            Console.Clear();
            Console.Write(Constant.MainMenu);
            MainMenu option = (MainMenu)Helper.GetValidInteger();
            switch (option)
            {
                case MainMenu.BankSetup:
                    string bankId = MasterBankService.CreateBank(UserInput.GetBankDetails());
                    Console.WriteLine(Constant.BankId + bankId);

                    BankService.CreateAdmin(UserInput.GetAdminDetails(), bankId);
                    Console.WriteLine(Constant.AdminCredentials);
                    Console.WriteLine(Constant.UserId + MasterBankService.GetBank(bankId)?.Admin?.UserId);
                    Console.ReadKey();

                    Initialize();
                    break;

                case MainMenu.Login:
                    this.CurrentUser = MasterBankService.Authentication(UserInput.GetCredentials());
                    if (this.CurrentUser != null)
                    {
                        this.NavigateUser(CurrentUser);
                    }
                    else
                    {
                        Console.WriteLine(Constant.UserNotFound);
                        this.Initialize();
                    }

                    break;

                case MainMenu.Exit:
                    Environment.Exit(0);

                    break;

                default:
                    this.Initialize();

                    break;
            }

            Initialize();
        }

        public void NavigateUser(User CurrentUser)
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
    }
}


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
            Account account = accountService.CurrentBank.Accounts.FirstOrDefault(a => a.Holder.UserId == user.UserId);
            Console.Write(Constant.UserMenu);
            CustomerMenu option = (CustomerMenu)Helper.GetValidInteger();
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
                        Console.WriteLine(Constant.DepositDecline);
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
                        Console.WriteLine(Constant.CashWithDrawDecline);
                        Console.ReadKey();
                    }
                    CustomerMenu(accountService, user);

                    break;

                case Models.CustomerMenu.FundTransfer:
                    Console.Write(Constant.DestinationAccountNumber);
                    string accountNumber = Helper.GetValidString();
                    Console.Write(Constant.DestinationBankId);
                    string Id = Helper.GetValidString();
                    Console.WriteLine(Constant.TransactionAmount);
                    amount = Helper.GetValidDouble();

                    if (accountService.FundTransaction(account.Id, accountNumber, accountService.CurrentBank.Id, Id, amount))
                    {
                        Console.WriteLine(Constant.FundTranserDecline);
                        Console.ReadKey();
                    }
                    CustomerMenu(accountService, user);

                    break;

                case Models.CustomerMenu.ViewBalance:
                    TransactionService transactionService = new TransactionService(accountService.CurrentBank.Id);
                    Console.WriteLine(Constant.AvailbleBalance, transactionService.ViewBalence(account.Id));
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
            Console.Write(Constant.AdminMenu);
            AdministratorMenu option = (AdministratorMenu)Helper.GetValidInteger();
            switch (option)
            {
                case Models.AdministratorMenu.AddEmployee:
                    string employeeId = bankService.CreateEmployee(UserInput.GetEmployeeDetails());
                    Employee employee = bankService.GetEmployee(employeeId);
                    Console.WriteLine(Constant.EmployeeId + employeeId);
                    Console.WriteLine(Constant.EmployeeUserName + employee.UserId);
                    Console.WriteLine(Constant.EmployeePassword + employee.Password);
                    Console.ReadKey();
                    AdministratorMenu(bankService, accountService);

                    break;

                case Models.AdministratorMenu.RemoveEmployee:
                    Console.WriteLine(Constant.EmployeeId);
                    employeeId = Helper.GetValidString();

                    if (!bankService.HasEmployee(employeeId))
                        Console.WriteLine(Constant.UserNotFound);
                    else
                    {
                        bankService.RemoveEmployee(employeeId);
                        Console.WriteLine(Constant.RemoveEmployee);
                        Console.ReadKey();
                    }

                    Console.ReadKey();
                    AdministratorMenu(bankService, accountService);

                    break;

                case Models.AdministratorMenu.UpdateEmployee:
                    Console.Write(Constant.EmployeeId);
                    employeeId = Helper.GetValidString();

                    if (!bankService.HasEmployee(employeeId))
                    {
                        Console.WriteLine(Constant.UserNotFound);
                    }
                    else
                    {
                        bankService.UpdateEmployee(UserInput.GetEmployeeDetails(), employeeId);
                        Console.WriteLine(Constant.UpdateEmployee);
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
                        Console.WriteLine(Constant.RemoveAccount);
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
                        Console.WriteLine(Constant.UpdateAccount);
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
                    Console.Write(Constant.CurrencyRate);
                    float currencyRate = Helper.GetValidFloat();

                    Console.WriteLine(amount * currencyRate + Constant.PayableAmount);
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
            TransactionService transactionService = new TransactionService(bankService.CurrentBank.Id);
            Console.Write(Constant.TransactionMenu);

            TransactionMenu option = (TransactionMenu)Helper.GetValidInteger();
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

                        Console.Write(Constant.CurrencyRate);
                        double currencyValue = Helper.GetValidDouble();

                        payout = currencyValue * payout;

                        if (!accountService.Deposit(accountNumber, payout))
                            Console.WriteLine(Constant.CurrencyRate, accountService.GetBalance(accountNumber));

                        else
                            Console.WriteLine(Constant.DepositDecline);
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
                            Console.WriteLine(Constant.NewBalance, accountService.GetBalance(accountNumber));

                        else
                            Console.WriteLine(Constant.CashWithDrawDecline);
                    }

                    Console.ReadKey();

                    break;

                case Models.TransactionMenu.FundTransfer:
                    Console.Write(Constant.SourceAccountNumber);
                    string SourceAccountNumber = Helper.GetValidString();

                    Console.Write(Constant.DestinationAccountNumber);
                    string DestinationAccountNumber = Helper.GetValidString();

                    Console.Write(Constant.DestinationBankId);
                    string DestinationIFSCCode = Helper.GetValidString();

                    Console.Write(Constant.TransactionAmount);
                    double amount = Helper.GetValidDouble();
                    if (accountService.FundTransaction(SourceAccountNumber, DestinationAccountNumber, bankService.CurrentBank.Id, DestinationIFSCCode, amount))
                        Console.WriteLine(Constant.FundTranserDecline);
                    Console.ReadKey();

                    break;

                case Models.TransactionMenu.ViewAccountAccount:
                    Console.Write(Constant.AccountNumber);
                    string getAccountNumber = Helper.GetValidString();

                    double? fundBalance = transactionService.ViewBalence(getAccountNumber);
                    if (fundBalance != null)
                        Console.WriteLine(Constant.NewBalance,fundBalance);
                    else
                        Console.WriteLine(Constant.AcoountNotFound);

                    Console.ReadKey();

                    break;

                case Models.TransactionMenu.TransactionRevert:
                    Console.Write(Constant.TransactionId);
                    string Id = Helper.GetValidString();

                    if (!transactionService.RevertTransaction(Id))
                    {
                        Console.WriteLine(Constant.RevertDecline);
                    }
                    else
                    {
                        Console.WriteLine(Constant.RevertDone);
                    }
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
            Console.Write(Constant.EmployeeMenu);
            EmployeeMenu option = (EmployeeMenu)Helper.GetValidInteger();
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
                        Console.WriteLine(Constant.RemoveAccount);
                    }

                    Console.ReadKey();

                    Console.ReadKey();
                    EmployeeMenu(bankService, accountService);

                    break;

                case Models.EmployeeMenu.UpdateAccount:
                    Console.Write(Constant.AccountNumber);
                    accountId = Helper.GetValidString();

                    account = bankService.GetAccount(accountId);
                    if (account == null)
                        Console.WriteLine(Constant.UserNotFound);
                    else
                    {
                        bankService.UpdateAccount(UserInput.GetAccountDetails(), accountId);
                        Console.WriteLine(Constant.UpdateAccount);
                    }

                    Console.ReadKey();
                    EmployeeMenu(bankService, accountService);

                    break;

                case Models.EmployeeMenu.Transaction:
                    TransactionMenu(bankService, accountService);
                    EmployeeMenu(bankService, accountService);

                    break;

                case Models.EmployeeMenu.CurrencyExchange:
                    Console.Write(Constant.TransactionAmount);
                    double amount = Helper.GetValidDouble();
                    Console.Write(Constant.CurrencyRate);
                    float currencyRate = Helper.GetValidFloat();
                    Console.Write(amount * currencyRate+Constant.PayableAmount);
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
            Console.Write(Constant.MainMenu);
            MainMenu option = (MainMenu)Helper.GetValidInteger();
            switch (option)
            {
                case MainMenu.BankSetup:
                    string bankId = MasterBankService.CreateBank(UserInput.GetBankDetails());
                    if (bankId == null)
                    {
                        Console.WriteLine(Constant.BankIdNotAvailable);
                    }
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
                        Console.ReadKey();
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
            }
        }
    }
}

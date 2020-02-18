
using BankSimulator.GetData;
using Models;
using Services;
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
            Account account = accountService.CurrentBank.Accounts.SingleOrDefault(a => a.Holder.UserId == user.UserId);

            CustomerMenu option = (CustomerMenu)Helper.GetInt("\n 1. Deposit\n 2. Cash Withdraw\n 3. Fund Transfer\n 4. View Balence\n 5. SignOut\n 6. Exit");
            switch (option)
            {
                case Models.CustomerMenu.Deposit:
                    Console.Clear();

                    Console.Write("Amount :");
                    double amount = double.Parse(Console.ReadLine());
                    double? fundBalance = accountService.Deposit(account.Id, amount);
                    if (fundBalance != null)
                    {
                        Console.WriteLine($"Your new balance is : {fundBalance}");
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
                    Console.Clear();

                    amount = Helper.GetDouble("Amount  :");
                    fundBalance = accountService.CashWithdraw(account.Id, amount);
                    if (fundBalance != null)
                    {
                        Console.WriteLine($"Your new balance is : {fundBalance}");
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
                    Console.Clear();

                    string accountNumber = Helper.GetString("Receiver Account Number :");
                    string Id = Helper.GetString("Receiver BankID :");
                    amount = Helper.GetDouble("Amount  :");

                    if (accountService.FundTransaction(account.Id, accountNumber, accountService.CurrentBank.Id, Id, amount))
                    {
                        Console.WriteLine("Fund transfer is not completed");
                        Console.ReadKey();
                    }

                    CustomerMenu(accountService, user);

                    break;

                case Models.CustomerMenu.ViewBalance:
                    Console.Clear();

                    accountService.ViewBalence(account.Id);
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
            AdministratorMenu option = (AdministratorMenu)Helper.GetInt(" 1. Add New Employee\n 2. Remove Employee\n 3. Update Employee Detail\n 4. Add new Account\n 5. Remove Account\n 6. Update Account\n 7. Account Transaction\n 8. Currency Exchange\n 9. SignOut\n 10.Exit");
            switch (option)
            {
                case Models.AdministratorMenu.AddEmployee:
                    string EmployeeId = bankService.CreateEmployee(UserInput.GetEmployeeDetails(bankService.Bank.Id));
                    Console.WriteLine($"Employee Id : {EmployeeId}");
                    AdministratorMenu(bankService, accountService);

                    break;

                case Models.AdministratorMenu.RemoveEmployee:
                    string employeeId = Helper.GetString("Enter Employee Id");

                    if (!bankService.RemoveEmployee(employeeId))
                    {
                        Console.WriteLine("Invalid employee");
                    }

                    AdministratorMenu(bankService, accountService);

                    break;

                case Models.AdministratorMenu.UpdateEmployee:
                    employeeId = Helper.GetString("Enter Employee Id");

                    if (!bankService.UpdateEmployee(UserInput.GetEmployeeDetails(bankService.Bank.Id), employeeId))
                    {
                        Console.WriteLine("Invalid employee");
                    }

                    AdministratorMenu(bankService, accountService);

                    break;

                case Models.AdministratorMenu.AddAccount:
                    string accountNumber = bankService.CreateAccount(UserInput.GetAccountDetails(bankService.Bank.Id));
                    Console.WriteLine($"Account Number : {accountNumber}");

                    AdministratorMenu(bankService, accountService);

                    break;

                case Models.AdministratorMenu.RemoveAccount:
                    string AccountId = Helper.GetString("Enter Account Id");

                    bankService.RemoveAccount(AccountId);
                    AdministratorMenu(bankService, accountService);

                    break;

                case Models.AdministratorMenu.UpdateAccount:
                    AccountId = Helper.GetString("Enter Account Id");
                    bankService.UpdateAccount(UserInput.GetAccountDetails(bankService.Bank.Id), AccountId);
                    AdministratorMenu(bankService, accountService);

                    break;

                case Models.AdministratorMenu.Transaction:
                    TransactionMenu(bankService, accountService);

                    AdministratorMenu(bankService, accountService);

                    break;

                case Models.AdministratorMenu.CurrencyExchange:
                    double amount = Helper.GetDouble("Enter the Amount");
                    float currencyRate = Helper.GetFloat("Enter the currency Rate");

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
            Console.WriteLine();
            EmployeeTransaction option = (EmployeeTransaction)Helper.GetInt(" 1. Deposit\n 2. Cash Withdraw\n 3. Fund Transfer\n 4. View Account Balence\n 5. Revert Transaction\n 6. SignOut\n 7. Exit");
            switch (option)
            {
                case EmployeeTransaction.Deposit:
                    Console.Clear();
                    string accountNumber = Helper.GetString("Enter Account Account Number  :");
                    double currencyValue = Helper.GetDouble("Current Currency Rate  :");
                    double amount = Helper.GetDouble("Amount ");
                    amount = currencyValue * amount;
                    double? fundBalance = accountService.Deposit(accountNumber, amount);

                    if (fundBalance != null)
                        Console.WriteLine($"Your new Balance is : {fundBalance}");

                    else
                        Console.WriteLine("Deposit operation is not performed");
                    Console.ReadKey();

                    break;

                case EmployeeTransaction.CashWithdraw:
                    Console.Clear();
                    accountNumber = Helper.GetString("Enter Account Account Number  :");
                    amount = Helper.GetDouble("Amount ");
                    fundBalance = accountService.CashWithdraw(accountNumber, amount);
                    if (fundBalance != null)
                        Console.WriteLine($"Your new Balance is : {fundBalance}");

                    else
                        Console.WriteLine("CashWithdraw operation is not performed");
                    Console.ReadKey();

                    break;

                case EmployeeTransaction.FundTransfer:
                    string SourceAccountNumber = Helper.GetString("Enter Source Account Number");
                    string DestinationAccountNumber = Helper.GetString("Enter Destination Account Number");
                    string DestinationIFSCCode = Helper.GetString("Enter Destination IFSC Code");
                    amount = Helper.GetDouble("Amount");

                    if (accountService.FundTransaction(SourceAccountNumber, DestinationAccountNumber, bankService.Bank.Id, DestinationIFSCCode, amount))
                        Console.WriteLine("Fund Transfer will be not perform ");
                    Console.ReadKey();

                    break;

                case EmployeeTransaction.ViewAccountAccount:
                    Console.Clear();
                    string getAccountNumber = Helper.GetString("Enter Account Account Number  :");
                    fundBalance = accountService.ViewBalence(getAccountNumber);
                    if (fundBalance != null)
                        Console.WriteLine($"Your Balance is {fundBalance}");
                    else
                        Console.WriteLine("this account number is not present in current bank");

                    Console.ReadKey();

                    break;

                case EmployeeTransaction.TransactionRevert:
                    string Id = Helper.GetString("Enter the Transaction Id");

                    if (accountService.RevertTransaction(Id))
                        Console.WriteLine("Transaction Revert will not be done");
                    else
                        Console.WriteLine("Transaction Revert completed");

                    Console.ReadKey();

                    break;

                case EmployeeTransaction.SignOut:
                    break;

                case EmployeeTransaction.Exit:
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
            EmployeeMenu option = (EmployeeMenu)Helper.GetInt(" 1. Add New Account\n 2. Remove Account\n 3 .Update Account\n 4. Transaction\n 5. Currency Exchange\n 6. SignOut\n 7. Exit");

            switch (option)
            {
                case Models.EmployeeMenu.AddAccount:
                    bankService.CreateAccount(UserInput.GetAccountDetails(bankService.Bank.Id));

                    EmployeeMenu(bankService, accountService);

                    break;

                case Models.EmployeeMenu.RemoveAccount:
                    string AccountId = Helper.GetString("Enter Account Id");

                    if (!bankService.RemoveAccount(AccountId))
                    {
                        Console.WriteLine("Account Not Found");
                    }
                    Console.ReadKey();
                    EmployeeMenu(bankService, accountService);

                    break;

                case Models.EmployeeMenu.UpdateAccount:
                    AccountId = Helper.GetString("Enter Account Id");

                    if (!bankService.UpdateAccount(UserInput.GetAccountDetails(bankService.Bank.Id), AccountId))
                    {
                        Console.WriteLine("Account Not Found");
                    }
                    Console.ReadKey();
                    EmployeeMenu(bankService, accountService);

                    break;

                case Models.EmployeeMenu.Transaction:
                    TransactionMenu(bankService, accountService);
                    EmployeeMenu(bankService, accountService);

                    break;

                case Models.EmployeeMenu.CurrencyExchange:
                    double amount = Helper.GetDouble("Enter the Amount");
                    float currencyRate = Helper.GetFloat("Enter the currency Rate");
                    Console.WriteLine($"{amount * currencyRate} amount will be pay");
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
        public BankService BankService { get; set; }

        public List<Bank> Banks { get; set; }

        public User CurrentUser { get; set; }

        public BankSimulator()
        {
            Banks = new List<Bank>();
        }

        public void Initialize()
        {
            MainMenu option;
            try
            {
                option = (MainMenu)Helper.GetInt(" 1. Create a New Bank\n 2. Login\n 3. Exit");
                switch (option)
                {
                    case MainMenu.CreateBank:
                        Console.Clear();

                        Bank bank = UserInput.GetBankDetails();
                        bank.Admin = UserInput.GetAdminDetails(bank.Id);
                        this.CreateBank(bank);
                        Initialize();

                        break;

                    case MainMenu.Login:
                        Console.Clear();

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

            catch (Exception ex)
            {
                Console.Write(ex);
                Console.Read();
            }
        }

        public static Login Login()
        {
            Login login = new Login
            {
                UserName = Helper.GetString(" User Name  :"),
                Password = Helper.GetString("Password  :")
            };

            return login;
        }

        public User Login(Login login)
        {
            string bankId = login.UserName.Substring(3);
            Bank bank = this.Banks.FirstOrDefault(b => b.Id == bankId);

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

        public Bank CreateBank(Bank bank)
        {
            DateTime now = DateTime.Now;
            bank.Id = bank.Name.Substring(0, 3) + now.Day + now.Month + now.Year;
            this.Banks.Add(bank);
            return bank;
        }
        public void NavigateUser(User CurrentUser)
        {
            if (this.CurrentUser != null)
            {
                Bank bank = this.Banks.FirstOrDefault(c => c.Id == CurrentUser.UserId.Substring(3));
                AccountService accountService = new AccountService(bank, this.Banks);
                this.BankService = new BankService(bank);

                switch (this.CurrentUser.UserType)
                {
                    case UserType.Admin:
                        Program.AdministratorMenu(this.BankService, accountService);
                        break;

                    case UserType.Employee:
                        Program.EmployeeMenu(this.BankService, accountService);
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
                Console.WriteLine("Invalid User");
                this.Initialize();
            }
            this.Initialize();
        }
    }
}

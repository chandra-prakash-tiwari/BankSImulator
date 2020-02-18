using Models;
using System;

namespace BankSimulator.GetData
{
    public class UserInput
    {
        public static Admin GetAdminDetails(string bankCode)
        {
            Admin admin = new Admin();

            Console.WriteLine("Enter Admin Detail");
            admin.Name = Helper.GetString("Name :");
            admin.PhoneNumber = Helper.GetLong("Phone Number :");
            admin.Address = Helper.GetString("Address :");
            admin.BankId = bankCode;
            admin.UserId = admin.Name.Substring(0, 3) + admin.BankId;
            Console.WriteLine(admin.UserId);
            admin.Password = Helper.GetString("Password :");

            admin.UserType = UserType.Admin;

            return admin;
        }

        public static Account GetAccountDetails(string bankCode)
        {
            Account account = new Account();

            Console.WriteLine("Enter Account Detail");
            account.Holder.Name = Helper.GetString("Name :");
            DateTime now = DateTime.Now;           
            account.Holder.PhoneNumber = Helper.GetLong("Phone Number :");
            account.Holder.Address = Helper.GetString("Address :");
            account.Holder.Password = Helper.GetString("Password :"); ;
            Console.WriteLine("Customer Bank Id :" + account.Holder.BankId);
            account.Holder.UserType = UserType.Account;
            account.FundBalance = 0;

            account.Holder.UserType = UserType.Account;
            return account;
        }

        public static Employee GetEmployeeDetails(string bankId)
        {
            Employee employee = new Employee();

            DateTime now = DateTime.Now;

            Console.WriteLine(" Enter Employee Detail");
            employee.Name = Helper.GetString("Name: ");
            employee.PhoneNumber = Helper.GetLong("Phone Number :");
            employee.Address = Helper.GetString("Address :");
            employee.Password = Helper.GetString("Password :");
            employee.UserType = UserType.Employee;
            Console.WriteLine("Customer Bank Id :" + employee.BankId);

            return employee;
        }

        public static Bank GetBankDetails()
        {
            Bank bank = new Bank();

            DateTime now = DateTime.Now;
            Console.WriteLine("Enter Bank Detail");
            bank.Name = Helper.GetString("Name :");
            bank.PhoneNumber = Helper.GetLong("Phone Number : ");
            bank.RtgsSame = Helper.GetPercentage("RTGS for same Bank : ");
            bank.RtgsOther = Helper.GetPercentage("RTGS for other Bank : ");
            bank.ItgsSame = Helper.GetPercentage("ITGS for same Bank  : ");
            bank.ItgsOther = Helper.GetPercentage("ITGS for Other Bank : ");
            bank.Id = bank.Name.Substring(0, 3) + now.Day + now.Month + now.Year;
            Console.WriteLine($"UserName :{bank.Id}");
            bank.Currency = Helper.GetString("Default Currency  :");

            return bank;
        }
    }
}

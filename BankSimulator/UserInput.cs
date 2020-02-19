using Models;
using System;

namespace BankSimulator.GetData
{
    public class UserInput
    {
        public static User GetAdminDetails()
        {
            User admin = new User();

            Console.WriteLine("Enter Admin Detail");

            Console.Write(Constrants.Name);
            admin.Name = Helper.GetValidString();

            Console.Write(Constrants.PhoneNumber);
            admin.PhoneNumber = Helper.GetValidLong();

            Console.Write(Constrants.Address);
            admin.Address = Helper.GetValidString();

            Console.Write(Constrants.Password);
            admin.Password = Helper.GetValidString();

            admin.UserType = UserType.Admin;

            return admin;
        }

        public static Account GetAccountDetails()
        {
            Account account = new Account();

            Console.WriteLine("Enter Account Detail");

            Console.Write(Constrants.Name);
            account.Holder.Name = Helper.GetValidString();

            Console.Write(Constrants.PhoneNumber);
            account.Holder.PhoneNumber = Helper.GetValidLong();

            Console.Write(Constrants.Address);
            account.Holder.Address = Helper.GetValidString();

            Console.Write(Constrants.Password);
            account.Holder.Password = Helper.GetValidString(); ;

            account.Holder.UserType = UserType.Account;
            account.FundBalance = 0;

            return account;
        }

        public static Employee GetEmployeeDetails()
        {
            Employee employee = new Employee();

            Console.WriteLine(" Enter Employee Detail");

            Console.Write(Constrants.Name);
            employee.Name = Helper.GetValidString();

            Console.Write(Constrants.PhoneNumber);
            employee.PhoneNumber = Helper.GetValidLong();

            Console.Write(Constrants.Address);
            employee.Address = Helper.GetValidString();

            Console.Write(Constrants.Password);
            employee.Password = Helper.GetValidString();

            employee.UserType = UserType.Employee;

            return employee;
        }

        public static Bank GetBankDetails()
        {
            Bank bank = new Bank();

            Console.WriteLine("Enter Bank Detail");

            Console.Write(Constrants.Name);
            bank.Name = Helper.GetValidString();

            Console.Write(Constrants.PhoneNumber);
            bank.PhoneNumber = Helper.GetValidLong();

            Console.Write("RTGS for same Bank : ");
            bank.RTGSSame = Helper.GetValidPercentage();

            Console.Write("RTGS for other Bank : ");
            bank.RTGSOther = Helper.GetValidPercentage();

            Console.Write("ITGS for same Bank  : ");
            bank.ITGSSame = Helper.GetValidPercentage();

            Console.Write("ITGS for Other Bank : ");
            bank.ITGSOther = Helper.GetValidPercentage();

            Console.Write("Default Currency  : ");
            bank.Currency = Helper.GetValidString();

            return bank;
        }
    }
}

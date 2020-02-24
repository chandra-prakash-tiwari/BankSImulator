using BankSimulator.Models;
using System;

namespace BankSimulator
{
    public class UserInput
    {
        public static User GetAdminDetails()
        {
            User admin = new User();

            Console.WriteLine("Enter Admin Detail");

            Console.Write(Constant.Name);
            admin.Name = Helper.GetValidString();

            Console.Write(Constant.PhoneNumber);
            admin.PhoneNumber = Helper.GetValidLong();

            Console.Write(Constant.Address);
            admin.Address = Helper.GetValidString();

            Console.Write(Constant.Password);
            admin.Password = Helper.GetValidString();

            admin.UserType = UserType.Admin;

            return admin;
        }

        public static Account GetAccountDetails()
        {
            Account account = new Account();

            Console.WriteLine("Enter Account Detail");

            Console.Write(Constant.Name);
            account.Holder.Name = Helper.GetValidString();

            Console.Write(Constant.PhoneNumber);
            account.Holder.PhoneNumber = Helper.GetValidLong();

            Console.Write(Constant.Address);
            account.Holder.Address = Helper.GetValidString();

            Console.Write(Constant.Password);
            account.Holder.Password = Helper.GetValidString(); ;

            account.Holder.UserType = UserType.Account;
            account.FundBalance = 0;

            return account;
        }

        public static Employee GetEmployeeDetails()
        {
            Employee employee = new Employee();

            Console.WriteLine(" Enter Employee Detail");

            Console.Write(Constant.Name);
            employee.Name = Helper.GetValidString();

            Console.Write(Constant.PhoneNumber);
            employee.PhoneNumber = Helper.GetValidLong();

            Console.Write(Constant.Address);
            employee.Address = Helper.GetValidString();

            Console.Write(Constant.Password);
            employee.Password = Helper.GetValidString();

            employee.UserType = UserType.Employee;

            return employee;
        }

        public static Bank GetBankDetails()
        {
            Bank bank = new Bank();

            Console.WriteLine("Enter Bank Detail");

            Console.Write(Constant.Name);
            bank.Name = Helper.GetValidString();

            Console.Write(Constant.PhoneNumber);
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

        public static Login GetCredentials()
        {
            Login login = new Login();

            Console.Write(Constant.UserId);
            login.UserName = Helper.GetValidString();
            Console.Write(Constant.Password);
            login.Password = Helper.GetValidString();

            return login;
        }
    }
}

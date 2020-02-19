using Models;
using System;

namespace BankSimulator.GetData
{
    public class UserInput
    {
        public static Admin GetAdminDetails()
        {
            Admin admin = new Admin();

            Console.WriteLine("Enter Admin Detail");
            admin.Name = Helper.GetValidString(Constrants.Name);
            admin.PhoneNumber = Helper.GetValidLong(Constrants.PhoneNumber);
            admin.Address = Helper.GetValidString(Constrants.Address);
            admin.Password = Helper.GetValidString(Constrants.Password);
            admin.UserType = UserType.Admin;

            return admin;
        }

        public static Account GetAccountDetails()
        {
            Account account = new Account();

            Console.WriteLine("Enter Account Detail");
            account.Holder.Name = Helper.GetValidString(Constrants.Name);          
            account.Holder.PhoneNumber = Helper.GetValidLong(Constrants.PhoneNumber);
            account.Holder.Address = Helper.GetValidString(Constrants.Address);
            account.Holder.Password = Helper.GetValidString(Constrants.Password); ;
            account.Holder.UserType = UserType.Account;
            account.FundBalance = 0;

            return account;
        }

        public static Employee GetEmployeeDetails()
        {
            Employee employee = new Employee();

            Console.WriteLine(" Enter Employee Detail");
            employee.Name = Helper.GetValidString(Constrants.Name);
            employee.PhoneNumber = Helper.GetValidLong(Constrants.PhoneNumber);
            employee.Address = Helper.GetValidString(Constrants.Address);
            employee.Password = Helper.GetValidString(Constrants.Password);
            employee.UserType = UserType.Employee;

            return employee;
        }

        public static Bank GetBankDetails()
        {
            Bank bank = new Bank();

            Console.WriteLine("Enter Bank Detail");
            bank.Name = Helper.GetValidString(Constrants.Name);
            bank.PhoneNumber = Helper.GetValidLong(Constrants.PhoneNumber);
            bank.RtgsSame = Helper.GetValidPercentage("RTGS for same Bank : ");
            bank.RtgsOther = Helper.GetValidPercentage("RTGS for other Bank : ");
            bank.ItgsSame = Helper.GetValidPercentage("ITGS for same Bank  : ");
            bank.ItgsOther = Helper.GetValidPercentage("ITGS for Other Bank : ");
            bank.Currency = Helper.GetValidString("Default Currency  :");

            return bank;
        }
    }
}

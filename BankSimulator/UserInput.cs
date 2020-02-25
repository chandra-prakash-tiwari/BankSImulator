using BankSimulator.Models;
using System;

namespace BankSimulator
{
    public class UserInput
    {
        public static User GetAdminDetails()
        {
            User admin = new User();

            Console.WriteLine(Constant.AdminDetail);

            Console.Write(Constant.Name);
            admin.Name = Helper.GetValidName();

            Console.Write(Constant.PhoneNumber);
            admin.PhoneNumber = Helper.GetValidLong();

            Console.Write(Constant.Email);
            admin.Email = Helper.GetValidEmail();

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

            Console.WriteLine(Constant.AccountHolderDetail);

            Console.Write(Constant.Name);
            account.Holder.Name = Helper.GetValidAccountName();

            Console.Write(Constant.PhoneNumber);
            account.Holder.PhoneNumber = Helper.GetValidLong();

            Console.Write(Constant.Email);
            account.Holder.Email = Helper.GetValidEmail();

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

            Console.WriteLine(Constant.EmployeeDetail);

            Console.Write(Constant.Name);
            employee.Name = Helper.GetValidEmployeeName();

            Console.Write(Constant.PhoneNumber);
            employee.PhoneNumber = Helper.GetValidLong();

            Console.Write(Constant.Email);
            employee.Email = Helper.GetValidEmail();

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

            Console.WriteLine(Constant.BankDetail);

            Console.Write(Constant.Name);
            bank.Name = Helper.GetValidBankName();

            Console.Write(Constant.PhoneNumber);
            bank.PhoneNumber = Helper.GetValidLong();

            Console.Write(Constant.RTGSRateForSame);
            bank.RTGSSame = Helper.GetValidPercentage();

            Console.Write(Constant.RTGSRateForOther);
            bank.RTGSOther = Helper.GetValidPercentage();

            Console.Write(Constant.ITGSRateForSame);
            bank.ITGSSame = Helper.GetValidPercentage();

            Console.Write(Constant.ITGSRateForOther);
            bank.ITGSOther = Helper.GetValidPercentage();

            Console.Write(Constant.DefaultCurrency);
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

        public static ConfirmationOption Confirmation()
        {
            ConfirmationOption confirm = (ConfirmationOption)Helper.GetValidInteger();
            switch (confirm)
            {
                case ConfirmationOption.Yes:
                    return ConfirmationOption.Yes;

                case ConfirmationOption.No:
                    return ConfirmationOption.No;

                default:
                    Console.WriteLine(Constant.Invalid);
                    confirm = Confirmation();
                    return confirm;

            }
        }
    }
}

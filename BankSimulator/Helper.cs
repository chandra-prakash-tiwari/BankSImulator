using BankSimulator.Models;
using Services.Services;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace BankSimulator
{
    public class Helper
    {
        public static int GetValidInteger()
        {
            if (!Int32.TryParse(Console.ReadLine(), out int number))
            {
                Console.WriteLine(Constant.Invalid);
                number = GetValidInteger();
            }

            return number;
        }

        public static double GetValidDouble()
        {
            if (!double.TryParse(Console.ReadLine(), out double number))
            {
                Console.WriteLine(Constant.Invalid);
                number = GetValidDouble();
            }

            return number;
        }

        public static float GetValidPercentage()
        {
            if (!(float.TryParse(Console.ReadLine(), out float rate) && rate < 100))
            {
                Console.WriteLine(Constant.Invalid);
                rate = GetValidPercentage();
            }

            return rate;
        }

        public static float GetValidFloat()
        {
            if (!float.TryParse(Console.ReadLine(), out float number))
            {
                Console.WriteLine(Constant.Invalid);
                number = GetValidFloat();
            }

            return number;
        }

        public static long GetValidLong()
        {
            if (!long.TryParse(Console.ReadLine(), out long number))
            {
                Console.WriteLine(Constant.Invalid);
                number = GetValidLong();
            }

            return number;
        }

        public static string GetValidString()
        {
            string userString = Console.ReadLine();
            if (string.IsNullOrEmpty(userString))
            {
                Console.WriteLine(Constant.Invalid);
                userString = GetValidString();
            }

            return userString;
        }

        public static string GetValidEmail()
        {
            string email = Console.ReadLine();
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(email);
            if (string.IsNullOrEmpty(email)|| !match.Success)
            {
                Console.WriteLine(Constant.Invalid);
                email = GetValidEmail();
            }

            return email;
        }

        public static string GetValidBankName()
        {
            string validBankName = Console.ReadLine();
            if (string.IsNullOrEmpty(validBankName) && validBankName.Length>3)
            {
                Console.WriteLine(Constant.Invalid);
                validBankName = GetValidBankName();
            }
            else if (!MasterBankService.BankIdVerification(validBankName))
            {
                Console.WriteLine(Constant.UserNameNotAvailable);
                validBankName = GetValidBankName();
            }

            return validBankName;
        }

        public static string GetValidAccountName()
        {
            string UserName = Console.ReadLine();
            if (string.IsNullOrEmpty(UserName))
            {
                Console.WriteLine(Constant.Invalid);
                UserName = GetValidAccountName();
            }
            else if (!MasterBankService.AccountUserNameVerification(UserName))
            {
                Console.WriteLine(Constant.UserNameNotAvailable);
                UserName = GetValidAccountName();
            }

            return UserName;
        }

        public static string GetValidEmployeeName()
        {
            string UserName = Console.ReadLine();
            if (string.IsNullOrEmpty(UserName))
            {
                Console.WriteLine(Constant.Invalid);
                UserName = GetValidEmployeeName();
            }
            else if (!MasterBankService.EmployeeUserNameVerification(UserName))
            {
                Console.WriteLine(Constant.UserNameNotAvailable);
                UserName = GetValidEmployeeName();
            }

            return UserName;
        }

        public static string GetValidName()
        {
            string UserName = Console.ReadLine();
            if (string.IsNullOrEmpty(UserName))
            {
                Console.WriteLine(Constant.Invalid);
                UserName = GetValidName();
            }

            return UserName;
        }
    }
}

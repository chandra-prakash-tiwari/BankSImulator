using BankSimulator.Models;
using System;

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
    }
}

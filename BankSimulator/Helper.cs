using Models;
using System;

namespace BankSimulator
{
    public class Helper
    {
        public static int GetValidInteger()
        {
            if (!Int32.TryParse(Console.ReadLine(), out int number))
            {
                Console.WriteLine(Constrants.Invalid);
                number = GetValidInteger();
            }

            return number;
        }

        public static double GetValidDouble()
        {
            if (!double.TryParse(Console.ReadLine(), out double number))
            {
                Console.WriteLine(Constrants.Invalid);
                number = GetValidDouble();
            }

            return number;
        }

        public static float GetValidPercentage()
        {
            if (!(float.TryParse(Console.ReadLine(), out float number) && number < 100))
            {

                Console.WriteLine(Constrants.Invalid);
                number = GetValidPercentage();
            }

            return number;
        }

        public static float GetValidFloat()
        {
            if (!float.TryParse(Console.ReadLine(), out float number))
            {
                Console.WriteLine(Constrants.Invalid);
                number = GetValidFloat();
            }

            return number;
        }

        public static long GetValidLong()
        {
            if (!long.TryParse(Console.ReadLine(), out long number))
            {
                Console.WriteLine(Constrants.Invalid);
                number = GetValidLong();
            }

            return number;
        }

        public static string GetValidString()
        {
            string str = Console.ReadLine();
            if (str == "")
            {
                Console.WriteLine(Constrants.Invalid);
                str = GetValidString();
            }

            return str;
        }
    }
}

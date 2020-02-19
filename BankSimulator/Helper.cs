using Models;
using System;

namespace BankSimulator
{
    public class Helper
    {
        public static int GetValidInteger(string text)
        {
            Console.Write(text);
            if (!Int32.TryParse(Console.ReadLine(), out int number))
            {
                Console.WriteLine(Constrants.Invalid);
                number = GetValidInteger(text);
            }

            return number;
        }

        public static double GetValidDouble(string text)
        {
            Console.Write(text);
            if (!double.TryParse(Console.ReadLine(), out double number))
            {
                Console.WriteLine(Constrants.Invalid);
                number = GetValidDouble(text);
            }

            return number;
        }

        public static float GetValidPercentage(string text)
        {
            Console.Write(text);
            if (!(float.TryParse(Console.ReadLine(), out float number) && number < 100))
            {

                Console.WriteLine(Constrants.Invalid);
                number = GetValidPercentage(text);
            }

            return number;
        }

        public static float GetValidFloat(string text)
        {
            Console.Write(text);
            if (!float.TryParse(Console.ReadLine(), out float number))
            {
                Console.WriteLine(Constrants.Invalid);
                number = GetValidFloat(text);
            }

            return number;
        }

        public static long GetValidLong(string text)
        {
            Console.Write(text);
            if (!long.TryParse(Console.ReadLine(), out long number))
            {
                Console.WriteLine(Constrants.Invalid);
                number = GetValidLong(text);
            }

            return number;
        }

        public static string GetValidString(string text)
        {
            Console.Write(text);
            string str = Console.ReadLine();
            if (str == "")
            {
                Console.WriteLine(Constrants.Invalid);
                str = GetValidString(text);
            }

            return str;
        }
    }
}

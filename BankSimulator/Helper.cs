using System;

namespace BankSimulator
{
    public class Helper
    {
        public static int GetInt(string text)
        {
            Console.Write(text);

            bool success = Int32.TryParse(Console.ReadLine(), out int number);
            if (!success)
            {
                Console.WriteLine("Please Enter Correct Value");
                number = GetInt(text);
            }

            return number;
        }

        public static double GetDouble(string text)
        {
            Console.Write(text);

            bool success = double.TryParse(Console.ReadLine(), out double number);
            if (!success)
            {
                Console.WriteLine("Please Enter Correct Value");
                number = GetDouble(text);
            }

            return number;
        }

        public static float GetPercentage(string text)
        {
            Console.Write(text);

            bool success = float.TryParse(Console.ReadLine(), out float number);
            if (!(success && number < 100))
            {

                Console.WriteLine("Please Enter Correct Value");
                number = GetPercentage(text);
            }

            return number;
        }

        public static float GetFloat(string text)
        {
            Console.Write(text);

            bool success = float.TryParse(Console.ReadLine(), out float number);
            if (!success)
            {
                Console.WriteLine("Please Enter Correct Value");
                number = GetFloat(text);
            }

            return number;
        }

        public static long GetLong(string text)
        {
            Console.Write(text);

            bool success = long.TryParse(Console.ReadLine(), out long number);
            if (!success)
            {
                Console.WriteLine("Please Enter Correct Value");
                number = GetLong(text);
            }

            return number;
        }

        public static string GetString(string text)
        {
            Console.Write(text);

            string str = Console.ReadLine();
            if (str == "")
            {
                Console.WriteLine("Please Enter Correct Value");
                str = GetString(text);
            }

            return str;
        }
    }
}

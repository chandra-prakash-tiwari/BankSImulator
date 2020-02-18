using System;

namespace BankSimulator
{
    public class Helper
    {
        public static int GetInt(string display)
        {
            Console.Write($"{display}");

            bool success = Int32.TryParse(Console.ReadLine(), out int number);
            if (!success)
            {
                Console.WriteLine("Please Enter Correct Value");
                number = GetInt(display);
            }

            return number;
        }

        public static double GetDouble(string display)
        {
            Console.Write($"{display}");

            bool success = double.TryParse(Console.ReadLine(), out double number);
            if (!success)
            {
                Console.WriteLine("Please Enter Correct Value");
                number = GetDouble(display);
            }

            return number;
        }

        public static float GetPercentage(string display)
        {
            Console.Write($"{display}");

            bool success = float.TryParse(Console.ReadLine(), out float number);
            if (!(success && number < 100))
            {

                Console.WriteLine("Please Enter Correct Value");
                number = GetPercentage(display);
            }

            return number;
        }

        public static float GetFloat(string display)
        {
            Console.Write($"{display}");

            bool success = float.TryParse(Console.ReadLine(), out float number);
            if (!success)
            {
                Console.WriteLine("Please Enter Correct Value");
                number = GetFloat(display);
            }

            return number;
        }

        public static long GetLong(string display)
        {
            Console.Write($"{display}");

            bool success = long.TryParse(Console.ReadLine(), out long number);
            if (!success)
            {
                Console.WriteLine("Please Enter Correct Value");
                number = GetLong(display);
            }

            return number;
        }

        public static string GetString(string display)
        {
            Console.Write(display);

            string str = Console.ReadLine();
            if (str == "")
            {
                Console.WriteLine("Please Enter Correct Value");
                str = GetString(display);
            }

            return str;
        }
    }
}

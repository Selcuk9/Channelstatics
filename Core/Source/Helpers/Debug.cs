using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Core.Source.Helpers
{
    public static class Debug
    {
        /// <summary>
        /// Вывести сообщение в консоль
        /// </summary>
        /// <param name="message"></param>
        public static void Log(string message)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"{DateTime.Now.ToShortTimeString()} [log] : {message}");
        }

        /// <summary>
        /// Вывести сообщение в консоль
        /// </summary>
        /// <param name="message"></param>
        public static void Success(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{DateTime.Now.ToShortTimeString()} [log] : {message}");
        }

        /// <summary>
        /// Считываем данные из консоли.
        /// </summary>
        /// <returns></returns>
        public static string InputData(string IntroductionString)
        {
            Console.ForegroundColor = ConsoleColor.White;
            if (string.IsNullOrWhiteSpace(IntroductionString))
            {
                Console.WriteLine(IntroductionString);
            }
            return Console.ReadLine();
        } 
    }
}

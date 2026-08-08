using System;
using System.Linq; 
namespace MyShellCommand.Services
{
    internal class ConsoleTextColor
    {
        private static Dictionary<string, ConsoleColor> colorMap = Enum.GetValues(typeof(ConsoleColor)).Cast<ConsoleColor>().ToDictionary(c => c.ToString().ToLower(), c => c);
        public static void Set(string color)
        {
            if (colorMap.TryGetValue(color.ToLower(), out ConsoleColor newcolor))
            {
                Console.ForegroundColor = newcolor; 
            }
        }

        public static void Reset()
        {
            Console.ResetColor();
        }
    }
}

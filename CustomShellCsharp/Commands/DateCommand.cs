using MyShellCommand.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyShellCommand.Commands
{
    internal class DateCommand : ICommand
    {
        public string Name => "date";

        public string Description => "Prints the current date";

        public void Execute(string arguments)
        {
            if (string.IsNullOrEmpty(arguments))
            {
                ConsoleTextColor.Set("red");
                Console.WriteLine("No arguments were given!");
                ConsoleTextColor.Reset();
            }
            else
            {
                if (CountryTimeZones.TryGetTimeZoneId(arguments, out string zoneId))
                {
                    TimeZoneInfo zone = TimeZoneInfo.FindSystemTimeZoneById(zoneId);
                    DateTime targetTime = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.Local, zone);
                    // DATE
                    ConsoleTextColor.Set("yellow");
                    Console.WriteLine("DATE");
                    Console.WriteLine(new string('-', 70));
                    ConsoleTextColor.Reset();
                    Console.WriteLine(targetTime.ToString("yyyy-MM-dd"));
                    ConsoleTextColor.Reset();
                    // TIME 24H
                    Console.WriteLine();
                    ConsoleTextColor.Set("yellow");
                    Console.WriteLine("TIME 24HR");
                    Console.WriteLine(new string('-', 70));
                    ConsoleTextColor.Reset();
                    Console.WriteLine(targetTime.ToString("HH:mm:ss"));

                    // TIME 12HR 
                    Console.WriteLine();
                    ConsoleTextColor.Set("yellow");
                    Console.WriteLine("TIME 12HR");
                    Console.WriteLine(new string('-', 70));
                    ConsoleTextColor.Reset();
                    Console.WriteLine(targetTime.ToString("hh: mm:ss tt"));
                }
                else
                {
                    ConsoleTextColor.Set("red");
                    Console.WriteLine($"'{arguments}' is not a valid time zone, if you want to see the actual time zones use the 'ptimezones' command!");
                    ConsoleTextColor.Reset();
                } 
                    
            }
        }
    }
}

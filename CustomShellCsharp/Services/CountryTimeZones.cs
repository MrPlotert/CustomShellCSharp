using System.Collections.Generic;

namespace MyShellCommand.Services
{
    internal static class CountryTimeZones
    {
        private static readonly Dictionary<string, string> timeZones = new Dictionary<string, string>
        {
            { "za", "South Africa Standard Time" },
            { "us", "Eastern Standard Time" },
            { "gb", "GMT Standard Time" },
            { "ie", "GMT Standard Time" },
            { "fr", "Romance Standard Time" },
            { "de", "W. Europe Standard Time" },
            { "es", "Romance Standard Time" },
            { "it", "W. Europe Standard Time" },
            { "pt", "GMT Standard Time" },
            { "nl", "W. Europe Standard Time" },
            { "be", "Romance Standard Time" },
            { "ch", "W. Europe Standard Time" },
            { "at", "W. Europe Standard Time" },
            { "se", "W. Europe Standard Time" },
            { "no", "W. Europe Standard Time" },
            { "dk", "Romance Standard Time" },
            { "fi", "FLE Standard Time" },
            { "pl", "Central European Standard Time" },
            { "ru", "Russian Standard Time" },
            { "ua", "FLE Standard Time" },
            { "gr", "GTB Standard Time" },
            { "tr", "Turkey Standard Time" },
            { "eg", "Egypt Standard Time" },
            { "ng", "W. Central Africa Standard Time" },
            { "ke", "E. Africa Standard Time" },
            { "ma", "Morocco Standard Time" },
            { "cn", "China Standard Time" },
            { "jp", "Tokyo Standard Time" },
            { "kr", "Korea Standard Time" },
            { "in", "India Standard Time" },
            { "pk", "Pakistan Standard Time" },
            { "bd", "Bangladesh Standard Time" },
            { "id", "SE Asia Standard Time" },
            { "th", "SE Asia Standard Time" },
            { "vn", "SE Asia Standard Time" },
            { "ph", "Singapore Standard Time" },
            { "sg", "Singapore Standard Time" },
            { "my", "Singapore Standard Time" },
            { "ae", "Arabian Standard Time" },
            { "sa", "Arab Standard Time" },
            { "il", "Israel Standard Time" },
            { "au", "AUS Eastern Standard Time" },
            { "nz", "New Zealand Standard Time" },
            { "ca", "Eastern Standard Time" },
            { "mx", "Central Standard Time (Mexico)" },
            { "br", "E. South America Standard Time" },
            { "ar", "Argentina Standard Time" },
            { "cl", "Pacific SA Standard Time" },
            { "co", "SA Pacific Standard Time" },
            { "pe", "SA Pacific Standard Time" }
        };

        public static void PrintTimezones()
        {
            Console.WriteLine();
            ConsoleTextColor.Set("yellow");
            Console.WriteLine("TIME ZONES");
            Console.WriteLine(new string('-', 70));
            ConsoleTextColor.Reset();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"{"NAME",-20} {"REFERENCE",-10}");
            Console.WriteLine(new string('-', 32));
            Console.ResetColor();

            foreach (var kvp in timeZones)
            {
                Console.WriteLine($"{kvp.Key,-20} {kvp.Value,-10}");
            }
        }

        public static bool TryGetTimeZoneId(string countryCode, out string timeZoneId)
        {
            return timeZones.TryGetValue(countryCode.ToLower(), out timeZoneId);
        }
    }
}
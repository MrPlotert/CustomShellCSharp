using System;
using System.IO;
using System.Runtime.InteropServices;
using MyShellCommand.Services;

namespace MyShellCommand.Commands
{
    internal class SysInfoCommand : ICommand
    {
        public string Name => "sysinfo";

        public string Description => "Prints system and device information";

        public void Execute(string arguments)
        {
            if (!string.IsNullOrWhiteSpace(arguments))
            {
                ConsoleTextColor.Set("red");
                Console.WriteLine($"'{Name}' does NOT accept any arguments!");
                ConsoleTextColor.Reset();
                return;
            }

            PrintSectionHeader("OPERATING SYSTEM");
            PrintLine("OS Version", Environment.OSVersion.ToString());
            PrintLine("OS Description", RuntimeInformation.OSDescription);
            PrintLine("Architecture", RuntimeInformation.OSArchitecture.ToString());
            PrintLine("64-bit OS", Environment.Is64BitOperatingSystem.ToString());
            PrintLine("64-bit Process", Environment.Is64BitProcess.ToString());
            Console.WriteLine();

            PrintSectionHeader("MACHINE");
            PrintLine("Machine Name", Environment.MachineName);
            PrintLine("User Name", Environment.UserName);
            PrintLine("Processor Count", Environment.ProcessorCount.ToString());
            PrintLine("System Uptime", FormatUptime(Environment.TickCount64));
            Console.WriteLine();

            PrintSectionHeader(".NET RUNTIME");
            PrintLine("Framework", RuntimeInformation.FrameworkDescription);
            PrintLine("Runtime Identifier", RuntimeInformation.RuntimeIdentifier);
            Console.WriteLine();

            PrintSectionHeader("STORAGE");
            foreach (DriveInfo drive in DriveInfo.GetDrives())
            {
                if (!drive.IsReady)
                {
                    continue;
                }

                string totalSize = FormatBytes(drive.TotalSize);
                string freeSpace = FormatBytes(drive.AvailableFreeSpace);

                Console.WriteLine("{0,-10} {1,-10} {2,-15} {3}",
                    drive.Name, drive.DriveType, $"{freeSpace} free", $"of {totalSize}");
            }
        }

        private static void PrintSectionHeader(string title)
        {
            ConsoleTextColor.Set("yellow");
            Console.WriteLine(title);
            Console.WriteLine(new string('-', 50));
            ConsoleTextColor.Reset();
        }

        private static void PrintLine(string label, string value)
        {
            Console.WriteLine("{0,-20} {1}", label, value);
        }

        private static string FormatUptime(long milliseconds)
        {
            TimeSpan uptime = TimeSpan.FromMilliseconds(milliseconds);
            return $"{uptime.Days}d {uptime.Hours}h {uptime.Minutes}m {uptime.Seconds}s";
        }

        private static string FormatBytes(long bytes)
        {
            if (bytes >= 1024L * 1024 * 1024)
            {
                return $"{bytes / (1024.0 * 1024 * 1024):F2} GB";
            }
            if (bytes >= 1024 * 1024)
            {
                return $"{bytes / (1024.0 * 1024):F2} MB";
            }
            return $"{bytes / 1024.0:F2} KB";
        }
    }
}
using System;
using System.IO;
using MyShellCommand.Services;

namespace MyShellCommand.Commands
{
    internal class FindCommand : ICommand
    {
        public string Name => "find";

        public string Description => "Searches the current directory and subfolders for files/directories matching a name.";

        public void Execute(string arguments)
        {
            if (string.IsNullOrWhiteSpace(arguments))
            {
                ConsoleTextColor.Set("red");
                Console.WriteLine($"'{Name}' has received no arguments!");
                ConsoleTextColor.Reset();
                return;
            }

            string startDirectory = Directory.GetCurrentDirectory();
            int matchCount = 0;

            SearchDirectory(startDirectory, arguments, ref matchCount);

            if (matchCount == 0)
            {
                ConsoleTextColor.Set("red");
                Console.WriteLine($"No files or directories matching '{arguments}' were found.");
                ConsoleTextColor.Reset();
            }
            else
            {
                ConsoleTextColor.Set("cyan");
                Console.WriteLine($"{matchCount} match(es) found.");
                ConsoleTextColor.Reset();
            }
        }

        private void SearchDirectory(string currentDirectory, string searchTerm, ref int matchCount)
        {
            try
            {
                foreach (string file in Directory.GetFiles(currentDirectory))
                {
                    if (Path.GetFileName(file).Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                    {
                        ConsoleTextColor.Set("green");
                        Console.WriteLine($"File: '{file}'");
                        ConsoleTextColor.Reset();
                        matchCount++;
                    }
                }

                foreach (string dir in Directory.GetDirectories(currentDirectory))
                {
                    if (Path.GetFileName(dir).Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                    {
                        ConsoleTextColor.Set("yellow");
                        Console.WriteLine($"Directory: '{dir}'");
                        ConsoleTextColor.Reset();
                        matchCount++;
                    }

                    SearchDirectory(dir, searchTerm, ref matchCount);
                }
            }
            catch (UnauthorizedAccessException)
            {
                // Skip folders we don't have permission to read, continue searching elsewhere
            }
            catch (IOException)
            {
                // Skip folders that error out for other I/O reasons
            }
        }
    }
}
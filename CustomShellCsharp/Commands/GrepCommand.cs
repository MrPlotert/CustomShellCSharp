using System;
using System.IO;
using MyShellCommand.Services;

namespace MyShellCommand.Commands
{
    internal class GrepCommand : ICommand
    {
        public string Name => "grep";

        public string Description => "Finds content inside a file.";

        public void Execute(string arguments)
        {
            if (string.IsNullOrWhiteSpace(arguments))
            {
                ConsoleTextColor.Set("red");
                Console.WriteLine($"Error: The '{Name}' command requires a search term and a file. Usage: grep <term> <file>");
                ConsoleTextColor.Reset();
                return;
            }

            string[] args = arguments.Split(new[] { ' ' }, 2, StringSplitOptions.RemoveEmptyEntries);

            if (args.Length < 2)
            {
                ConsoleTextColor.Set("red");
                Console.WriteLine($"Error: The '{Name}' command requires both a search term and a file. Usage: grep <term> <file>");
                ConsoleTextColor.Reset();
                return;
            }

            string searchTerm = args[0].Trim();
            string filePath = args[1].Trim();

            string finishedPath = Path.GetFullPath(filePath, Directory.GetCurrentDirectory());

            if (!File.Exists(finishedPath))
            {
                ConsoleTextColor.Set("red");
                Console.WriteLine($"'{finishedPath}' is not a valid path!");
                ConsoleTextColor.Reset();
                return;
            }

            try
            {
                string[] lines = File.ReadAllLines(finishedPath);
                int matchCount = 0;

                for (int i = 0; i < lines.Length; i++)
                {
                    if (lines[i].Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                    {
                        Console.Write($"{i + 1}: ");

                        string line = lines[i];
                        int searchStart = 0;
                        int matchIndex;

                        while ((matchIndex = line.IndexOf(searchTerm, searchStart, StringComparison.OrdinalIgnoreCase)) != -1)
                        {
                            ConsoleTextColor.Set("green");
                            Console.Write(line.Substring(searchStart, matchIndex - searchStart));

                            ConsoleTextColor.Set("yellow");
                            Console.Write(line.Substring(matchIndex, searchTerm.Length));

                            searchStart = matchIndex + searchTerm.Length;
                        }

                        ConsoleTextColor.Set("green");
                        Console.WriteLine(line.Substring(searchStart));
                        ConsoleTextColor.Reset();

                        matchCount++;
                    }
                }

                if (matchCount == 0)
                {
                    ConsoleTextColor.Set("red");
                    Console.WriteLine($"No matches found for '{searchTerm}'.");
                    ConsoleTextColor.Reset();
                }
            }
            catch (UnauthorizedAccessException)
            {
                ConsoleTextColor.Set("red");
                Console.WriteLine($"Permission denied reading '{finishedPath}'.");
                ConsoleTextColor.Reset();
            }
            catch (IOException)
            {
                ConsoleTextColor.Set("red");
                Console.WriteLine($"An I/O error occurred while reading '{finishedPath}' (file may be in use).");
                ConsoleTextColor.Reset();
            }
        }
    }
}
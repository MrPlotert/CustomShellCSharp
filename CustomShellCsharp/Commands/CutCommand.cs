using System;
using System.IO;
using MyShellCommand.Services;

namespace MyShellCommand.Commands
{
    internal class CutCommand : ICommand
    {
        public string Name => "cut";
        private const string seperator = " to ";
        public string Description => "Cuts a file/directory from a source to destination";

        public void Execute(string arguments)
        {
            if (string.IsNullOrWhiteSpace(arguments))
            {
                ConsoleTextColor.Set("red");
                Console.WriteLine($"'{Name}' requires arguments!");
                ConsoleTextColor.Reset();
                return;
            }

            int seperatorIndex = arguments.IndexOf(seperator, StringComparison.OrdinalIgnoreCase);

            if (seperatorIndex == -1)
            {
                ConsoleTextColor.Set("red");
                Console.WriteLine("Usage: <source> to <destination>");
                ConsoleTextColor.Reset();
                return;
            }

            string source = arguments.Substring(0, seperatorIndex).Trim();
            string destination = arguments.Substring(seperatorIndex + seperator.Length).Trim();

            if (!Directory.Exists(source) && !File.Exists(source))
            {
                ConsoleTextColor.Set("red");
                Console.WriteLine($"Error: '{source}' does not exist.");
                ConsoleTextColor.Reset();
                return;
            }

            try
            {
                if (Directory.Exists(source))
                {
                    string parentDir = Path.GetDirectoryName(destination);

                    if (!string.IsNullOrEmpty(parentDir) && !Directory.Exists(parentDir))
                    {
                        Directory.CreateDirectory(parentDir);
                    }

                    Directory.Move(source, destination);
                    ConsoleTextColor.Set("green");
                    Console.WriteLine($"Directory '{source}' moved to '{destination}'.");
                    ConsoleTextColor.Reset();
                }
                else if (File.Exists(source))
                {
                    if (Directory.Exists(destination))
                    {
                        string fileName = Path.GetFileName(source);
                        destination = Path.Combine(destination, fileName);
                    }
                    else
                    {
                        string parentDir = Path.GetDirectoryName(destination);

                        if (!string.IsNullOrEmpty(parentDir) && !Directory.Exists(parentDir))
                        {
                            Directory.CreateDirectory(parentDir);
                        }
                    }

                    File.Move(source, destination);
                    ConsoleTextColor.Set("green");
                    Console.WriteLine($"File '{source}' moved to '{destination}'.");
                    ConsoleTextColor.Reset();
                }
            }
            catch (DirectoryNotFoundException)
            {
                ConsoleTextColor.Set("red");
                Console.WriteLine($"Part of the path for '{destination}' could not be found.");
                ConsoleTextColor.Reset();
            }
            catch (PathTooLongException)
            {
                ConsoleTextColor.Set("red");
                Console.WriteLine("One of the specified paths is too long.");
                ConsoleTextColor.Reset();
            }
            catch (IOException)
            {
                ConsoleTextColor.Set("red");
                Console.WriteLine("An I/O error occurred during the move (destination may already exist).");
                ConsoleTextColor.Reset();
            }
            catch (UnauthorizedAccessException)
            {
                ConsoleTextColor.Set("red");
                Console.WriteLine($"Permission denied while moving '{source}'.");
                ConsoleTextColor.Reset();
            }
            catch (ArgumentException)
            {
                ConsoleTextColor.Set("red");
                Console.WriteLine("One of the specified paths contains invalid characters.");
                ConsoleTextColor.Reset();
            }
        }
    }
}
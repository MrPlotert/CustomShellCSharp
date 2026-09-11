using System;
using System.IO;
using MyShellCommand.Services;

namespace MyShellCommand.Commands
{
    internal class RenameCommand : ICommand
    {
        public string Name => "rename";

        public string Description => "Renames a file or directory.";

        public void Execute(string arguments)
        {
            if (string.IsNullOrWhiteSpace(arguments))
            {
                ConsoleTextColor.Set("red");
                Console.WriteLine("Error: No arguments provided. Usage: rename <old_name> <new_name>");
                ConsoleTextColor.Reset();
                return;
            }

            string[] args = arguments.Split(new[] { ' ' }, 2, StringSplitOptions.RemoveEmptyEntries);

            if (args.Length < 2)
            {
                ConsoleTextColor.Set("red");
                Console.WriteLine("Error: Insufficient arguments. Usage: rename <old_name> <new_name>");
                ConsoleTextColor.Reset();
                return;
            }

            string oldName = args[0].Trim();
            string newName = args[1].Trim();

            if (!File.Exists(oldName) && !Directory.Exists(oldName))
            {
                ConsoleTextColor.Set("red");
                Console.WriteLine($"Error: '{oldName}' does not exist.");
                ConsoleTextColor.Reset();
                return;
            }

            try
            {
                if (File.Exists(oldName))
                {
                    File.Move(oldName, newName);
                    ConsoleTextColor.Set("green");
                    Console.WriteLine($"File '{oldName}' renamed to '{newName}'.");
                    ConsoleTextColor.Reset();
                }
                else if (Directory.Exists(oldName))
                {
                    Directory.Move(oldName, newName);
                    ConsoleTextColor.Set("green");
                    Console.WriteLine($"Directory '{oldName}' renamed to '{newName}'.");
                    ConsoleTextColor.Reset();
                }
            }
            catch (UnauthorizedAccessException)
            {
                ConsoleTextColor.Set("red");
                Console.WriteLine($"Permission denied while renaming '{oldName}'.");
                ConsoleTextColor.Reset();
            }
            catch (DirectoryNotFoundException)
            {
                ConsoleTextColor.Set("red");
                Console.WriteLine($"Part of the path for '{newName}' could not be found.");
                ConsoleTextColor.Reset();
            }
            catch (PathTooLongException)
            {
                ConsoleTextColor.Set("red");
                Console.WriteLine("One of the specified names is too long.");
                ConsoleTextColor.Reset();
            }
            catch (IOException)
            {
                ConsoleTextColor.Set("red");
                Console.WriteLine($"'{newName}' already exists, or an I/O error occurred.");
                ConsoleTextColor.Reset();
            }
            catch (ArgumentException)
            {
                ConsoleTextColor.Set("red");
                Console.WriteLine("One of the specified names contains invalid characters.");
                ConsoleTextColor.Reset();
            }
        }
    }
}
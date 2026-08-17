using System;
using System.IO;
using MyShellCommand.Services;

namespace MyShellCommand.Commands
{
    internal class MkdirCommand : ICommand
    {
        public string Name => "mkdir";

        public string Description => "Creates a new directory";

        public void Execute(string arguments)
        {
            if (string.IsNullOrWhiteSpace(arguments))
            {
                ConsoleTextColor.Set("red");
                Console.WriteLine($"No arguments given for '{Name}' command!");
                ConsoleTextColor.Reset();
                return;
            }

            string newPath;

            try
            {
                newPath = Path.GetFullPath(arguments, Directory.GetCurrentDirectory());
            }
            catch (ArgumentException)
            {
                ConsoleTextColor.Set("red");
                Console.WriteLine($"'{arguments}' contains invalid path characters.");
                ConsoleTextColor.Reset();
                return;
            }
            catch (PathTooLongException)
            {
                ConsoleTextColor.Set("red");
                Console.WriteLine("The specified path is too long.");
                ConsoleTextColor.Reset();
                return;
            }
            catch (NotSupportedException)
            {
                ConsoleTextColor.Set("red");
                Console.WriteLine($"'{arguments}' contains an invalid format (e.g. a stray colon).");
                ConsoleTextColor.Reset();
                return;
            }

            if (Directory.Exists(newPath) || File.Exists(newPath))
            {
                ConsoleTextColor.Set("red");
                Console.WriteLine($"{newPath} already exists!");
                ConsoleTextColor.Reset();
                return;
            }

            try
            {
                Directory.CreateDirectory(newPath);
                ConsoleTextColor.Set("yellow");
                Console.WriteLine($"Successfully created path: {newPath}");
                ConsoleTextColor.Reset();
            }
            catch (UnauthorizedAccessException)
            {
                ConsoleTextColor.Set("red");
                Console.WriteLine($"Permission denied to create '{newPath}'.");
                ConsoleTextColor.Reset();
            }
            catch (DirectoryNotFoundException)
            {
                ConsoleTextColor.Set("red");
                Console.WriteLine($"Part of the path '{newPath}' could not be found.");
                ConsoleTextColor.Reset();
            }
            catch (IOException)
            {
                ConsoleTextColor.Set("red");
                Console.WriteLine($"An I/O error occurred while creating '{newPath}'.");
                ConsoleTextColor.Reset();
            }
        }
    }
}
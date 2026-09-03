using System;
using System.IO;
using MyShellCommand.Services;

namespace MyShellCommand.Commands
{
    internal class CatCommand : ICommand
    {
        public string Name => "cat";

        public string Description => "Prints all content within a file.";

        public void Execute(string arguments)
        {
            if (string.IsNullOrWhiteSpace(arguments))
            {
                ConsoleTextColor.Set("red");
                Console.WriteLine("Error: No file specified.");
                ConsoleTextColor.Reset();
                return;
            }

            string targetPath = Path.GetFullPath(arguments, Directory.GetCurrentDirectory());

            if (!File.Exists(targetPath))
            {
                ConsoleTextColor.Set("red");
                Console.WriteLine($"Error: File not found - {targetPath}");
                ConsoleTextColor.Reset();
                return;
            }

            try
            {
                string content = File.ReadAllText(targetPath);
                Console.WriteLine(content);
            }
            catch (UnauthorizedAccessException)
            {
                ConsoleTextColor.Set("red");
                Console.WriteLine($"Permission denied reading '{targetPath}'.");
                ConsoleTextColor.Reset();
            }
            catch (IOException)
            {
                ConsoleTextColor.Set("red");
                Console.WriteLine($"An I/O error occurred while reading '{targetPath}' (file may be in use).");
                ConsoleTextColor.Reset();
            }
        }
    }
}
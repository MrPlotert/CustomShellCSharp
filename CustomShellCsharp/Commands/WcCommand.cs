using System;
using System.IO;
using MyShellCommand.Services;

namespace MyShellCommand.Commands
{
    internal class WcCommand : ICommand
    {
        public string Name => "wc";

        public string Description => "Prints the total amount of Lines/Characters/Words in a file.";

        public void Execute(string arguments)
        {
            if (string.IsNullOrWhiteSpace(arguments))
            {
                ConsoleTextColor.Set("red");
                Console.WriteLine($"'{Name}' requires arguments!");
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
                string[] lines = File.ReadAllLines(targetPath);
                string[] words = content.Split(new[] { ' ', '\t', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

                ConsoleTextColor.Set("yellow");
                Console.WriteLine($"Lines: {lines.Length}");
                Console.WriteLine($"Words: {words.Length}");
                Console.WriteLine($"Characters: {content.Length}");
                ConsoleTextColor.Reset();
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
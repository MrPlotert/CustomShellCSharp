using System;
using System.IO;
using MyShellCommand.Core;
using MyShellCommand.Services;

namespace MyShellCommand.Commands
{
    internal class CDCommand : ICommand
    {
        public string Name => "cd";

        public string Description => "Changes the current directory";

        public void Execute(string arguments)
        {
            if (string.IsNullOrWhiteSpace(arguments))
            {
                ConsoleTextColor.Set("red");
                Console.WriteLine("Error: No directory specified.");
                ConsoleTextColor.Reset();
                return;
            }

            try
            {
                string newPath = Path.GetFullPath(
                    arguments,
                    Directory.GetCurrentDirectory()
                );

                if (Directory.Exists(newPath))
                {
                    Directory.SetCurrentDirectory(newPath);

                    ConsoleTextColor.Set("green");
                    ConsoleTextColor.Reset();
                }
                else
                {
                    ConsoleTextColor.Set("red");
                    Console.WriteLine($"Error: Directory '{newPath}' does not exist.");
                    ConsoleTextColor.Reset();
                }
            }
            catch (Exception ex)
            {
                ConsoleTextColor.Set("red");
                Console.WriteLine($"Error: {ex.Message}");
                ConsoleTextColor.Reset();
            }
        }
    }
}
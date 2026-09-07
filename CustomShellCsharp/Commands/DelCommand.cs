using System;
using System.IO;
using MyShellCommand.Services;

namespace MyShellCommand.Commands
{
    internal class DelCommand : ICommand
    {
        public string Name => "del";

        public string Description => "Deletes a file";

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
                File.Delete(targetPath);
                ConsoleTextColor.Set("cyan");
                Console.WriteLine($"Deleted file: {targetPath}");
                ConsoleTextColor.Reset();
            }
            catch (UnauthorizedAccessException)
            {
                ConsoleTextColor.Set("red");
                Console.WriteLine("Access denied. You don't have permission to delete this file.");
                ConsoleTextColor.Reset();
            }
            catch (DirectoryNotFoundException)
            {
                ConsoleTextColor.Set("red");
                Console.WriteLine($"Part of the path '{targetPath}' could not be found.");
                ConsoleTextColor.Reset();
            }
            catch (PathTooLongException)
            {
                ConsoleTextColor.Set("red");
                Console.WriteLine("The specified path is too long.");
                ConsoleTextColor.Reset();
            }
            catch (IOException)
            {
                ConsoleTextColor.Set("red");
                Console.WriteLine("The file could not be deleted because of an I/O error (file may be in use).");
                ConsoleTextColor.Reset();
            }
        }
    }
}
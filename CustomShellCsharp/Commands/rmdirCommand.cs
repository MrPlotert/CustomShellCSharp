using System;
using System.IO;
using Microsoft.VisualBasic.FileIO;
using MyShellCommand.Services;

namespace MyShellCommand.Commands
{
    internal class RmdirCommand : ICommand
    {
        private const string PermanentOption = "-p";

        public string Name => "rmdir";

        public string Description => "Removes a folder/directory";

        public void Execute(string arguments)
        {
            if (string.IsNullOrWhiteSpace(arguments))
            {
                ConsoleTextColor.Set("red");
                Console.WriteLine($"No arguments given for '{Name}' command!");
                ConsoleTextColor.Reset();
                return;
            }

            bool permanent = false;
            string path = arguments.Trim();

            if (path.StartsWith(PermanentOption, StringComparison.OrdinalIgnoreCase))
            {
                permanent = true;
                path = path.Substring(PermanentOption.Length).Trim();
            }

            if (string.IsNullOrWhiteSpace(path))
            {
                ConsoleTextColor.Set("red");
                Console.WriteLine("No directory specified.");
                ConsoleTextColor.Reset();
                return;
            }

            string targetPath;

            try
            {
                targetPath = Path.GetFullPath(path, Directory.GetCurrentDirectory());
            }
            catch (ArgumentException)
            {
                ConsoleTextColor.Set("red");
                Console.WriteLine($"'{path}' contains invalid path characters.");
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
                Console.WriteLine($"'{path}' contains an invalid format (e.g. a stray colon).");
                ConsoleTextColor.Reset();
                return;
            }

            if (!Directory.Exists(targetPath))
            {
                ConsoleTextColor.Set("red");
                Console.WriteLine($"Directory '{targetPath}' does not exist.");
                ConsoleTextColor.Reset();
                return;
            }

            try
            {
                FileSystem.DeleteDirectory(
                    targetPath,
                    UIOption.AllDialogs,
                    permanent ? RecycleOption.DeletePermanently : RecycleOption.SendToRecycleBin
                );

                ConsoleTextColor.Set("green");
                Console.WriteLine($"Successfully removed: {targetPath}");
                ConsoleTextColor.Reset();
            }
            catch (OperationCanceledException)
            {
                ConsoleTextColor.Set("cyan");
                Console.WriteLine("Deletion cancelled.");
                ConsoleTextColor.Reset();
            }
            catch (UnauthorizedAccessException)
            {
                ConsoleTextColor.Set("red");
                Console.WriteLine($"Permission denied to remove '{targetPath}'.");
                ConsoleTextColor.Reset();
            }
            catch (DirectoryNotFoundException)
            {
                ConsoleTextColor.Set("red");
                Console.WriteLine($"Directory '{targetPath}' could not be found.");
                ConsoleTextColor.Reset();
            }
            catch (IOException)
            {
                ConsoleTextColor.Set("red");
                Console.WriteLine($"An I/O error occurred while removing '{targetPath}' (folder may be in use).");
                ConsoleTextColor.Reset();
            }
        }
    }
}
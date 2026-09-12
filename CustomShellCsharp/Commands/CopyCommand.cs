using System;
using System.IO;
using MyShellCommand.Services;

namespace MyShellCommand.Commands
{
    internal class CopyCommand : ICommand
    {
        public string Name => "copy";
        private const string seperator = " to ";
        public string Description => "Copies files/folders from source to destination (WARNING: OVERWRITES EXISTING FILES)";

        public void Execute(string arguments)
        {
            if (string.IsNullOrWhiteSpace(arguments))
            {
                ConsoleTextColor.Set("red");
                Console.WriteLine($"Error: The '{Name}' command requires source and destination arguments.");
                ConsoleTextColor.Reset();
                return;
            }

            int separatorIndex = arguments.IndexOf(seperator, StringComparison.OrdinalIgnoreCase);

            if (separatorIndex == -1)
            {
                ConsoleTextColor.Set("red");
                Console.WriteLine($"Error: Usage: {Name} <source> to <destination>");
                ConsoleTextColor.Reset();
                return;
            }

            string source = arguments.Substring(0, separatorIndex).Trim();
            string destination = arguments.Substring(separatorIndex + seperator.Length).Trim();

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
                    CopyDirectory(source, destination);
                    ConsoleTextColor.Set("green");
                    Console.WriteLine($"Directory '{source}' copied to '{destination}'.");
                    ConsoleTextColor.Reset();
                }
                else if (File.Exists(source))
                {
                    if (Directory.Exists(destination))
                    {
                        string fileName = Path.GetFileName(source);
                        destination = Path.Combine(destination, fileName);
                    }

                    File.Copy(source, destination, true);
                    ConsoleTextColor.Set("green");
                    Console.WriteLine($"File '{source}' copied to '{destination}'.");
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
                Console.WriteLine("An I/O error occurred during the copy.");
                ConsoleTextColor.Reset();
            }
            catch (UnauthorizedAccessException)
            {
                ConsoleTextColor.Set("red");
                Console.WriteLine($"Permission denied while copying '{source}'.");
                ConsoleTextColor.Reset();
            }
            catch (ArgumentException)
            {
                ConsoleTextColor.Set("red");
                Console.WriteLine("One of the specified paths contains invalid characters.");
                ConsoleTextColor.Reset();
            }
        }

        private void CopyDirectory(string sourceDir, string destinationDir)
        {
            Directory.CreateDirectory(destinationDir);

            foreach (string file in Directory.GetFiles(sourceDir))
            {
                string destFile = Path.Combine(destinationDir, Path.GetFileName(file));
                File.Copy(file, destFile, true);
            }

            foreach (string dir in Directory.GetDirectories(sourceDir))
            {
                string destSubDir = Path.Combine(destinationDir, Path.GetFileName(dir));
                CopyDirectory(dir, destSubDir);
            }
        }
    }
}
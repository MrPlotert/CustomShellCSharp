using System;
using System.IO;
using MyShellCommand.Core;
using MyShellCommand.Services;

namespace MyShellCommand.Commands
{
    internal class LSCommand : ICommand
    {
        public string Name => "ls";

        public string Description => "Prints all files and folders in the current directory";
        private int folders_amount = 0;
        private int files_amount = 0;
        public void Execute(string arguments)
        {
            if (!string.IsNullOrWhiteSpace(arguments))
            {
                ConsoleTextColor.Set("red");
                Console.WriteLine($"The '{Name}' command does not accept any arguments!");
                ConsoleTextColor.Reset();
                return;
            }

            string currentDirectory = Directory.GetCurrentDirectory();
            string separator = new string('-', 80);

            DirectoryInfo[] folders = new DirectoryInfo(currentDirectory).GetDirectories();
            FileInfo[] files = new DirectoryInfo(currentDirectory).GetFiles();

            ConsoleTextColor.Set("yellow");
            Console.WriteLine("FOLDERS");
            Console.WriteLine(separator);
            ConsoleTextColor.Reset();

            foreach (DirectoryInfo folder in folders)
            {
                if (folder.Exists)
                {
                    folders_amount++;
                    Console.WriteLine();
                    Console.WriteLine("{0,-30} {1,-10} {2}", folder.Name, "Folder", folder.CreationTime);
                }
            }
            Console.WriteLine($"Total Folders: {folders_amount}");
            Console.WriteLine();
            ConsoleTextColor.Set("green");
            Console.WriteLine("FILES");
            Console.WriteLine(separator);
            ConsoleTextColor.Reset();

            foreach (FileInfo file in files)
            {
                if (file.Exists)
                {
                    string size = FormatSize(file.Length);
                    Console.WriteLine();
                    Console.WriteLine("{0,-30} {1,-10} {2,-10} {3}", file.Name, file.Extension, size, file.CreationTime);
                    files_amount++; 
                }
            }
            Console.WriteLine($"Total Files: {files_amount}");


            // Reset the Folders and files amount
            folders_amount = 0;
            files_amount = 0;
        }

        private static string FormatSize(long bytes)
        {
            if (bytes >= 1024 * 1024)
            {
                return $"{bytes / (1024.0 * 1024.0):F2} MB";
            }
            if (bytes >= 1024)
            {
                return $"{bytes / 1024.0:F2} KB";
            }
            return $"{bytes} B";
        }
    }
}
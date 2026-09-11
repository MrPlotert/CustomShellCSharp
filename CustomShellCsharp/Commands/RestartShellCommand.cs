using MyShellCommand.Services;
using System;
using System.Diagnostics; 
using System.Collections.Generic;
using System.Text;
using System.IO.Enumeration;

namespace MyShellCommand.Commands
{
    internal class RestartShellCommand : ICommand
    {
        public string Name => "restart";

        public string Description => "Restarts the shell command application.";

        public void Execute(string arguments)
        {
            if (!string.IsNullOrWhiteSpace(arguments))
            {
                ConsoleTextColor.Set("red");
                Console.WriteLine($"Error: The '{Name}' command does not accept any arguments.");
                ConsoleTextColor.Reset();
                return;
            }

            string? currentProcess = Environment.ProcessPath; 

            if (string.IsNullOrEmpty(currentProcess))
            {
                ConsoleTextColor.Set("red");
                Console.WriteLine("Error: Unable to determine the current process path.");
                ConsoleTextColor.Reset();
                return;
            }


            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = currentProcess,
                UseShellExecute = false
            };

            try
            {
                Console.Clear();
                Process.Start(startInfo);
                Environment.Exit(0);
            }
            catch (Exception ex)
            {
                ConsoleTextColor.Set("red");
                Console.WriteLine($"Error: Failed to restart. {ex.GetType().Name}: {ex.Message}");
                ConsoleTextColor.Reset();
            }

        }
    }
}

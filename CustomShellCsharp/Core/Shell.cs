using MyShellCommand.Commands;
using MyShellCommand.Services;
using System;
using System.IO;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace MyShellCommand.Core
{
    internal class Shell
    {
        private bool isRunning = true;
        private CommandRegistry commandRegistry;

        public Shell()
        {
            ConsoleTextColor.Set("YELLOW");
            Console.Write("Not sure what to do? Type 'help' for a list of commands.\n");
            ConsoleTextColor.Reset();

            commandRegistry = new CommandRegistry();

            RegisterCommands(); // Register all commands
            Directory.SetCurrentDirectory(
                Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)
            );
        }

        public void Run()
        {
            while (isRunning)
            {
                Console.Write($"{Directory.GetCurrentDirectory()}> ");
                string input = GetInput();
                ProcessCommand(input);
            }
        }

        public void Stop()
        {
            ConsoleTextColor.Set("green");
            Console.WriteLine("Exiting the shell command...");
            ConsoleTextColor.Reset();

            isRunning = false;
        }

        private void ProcessCommand(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return;
            }

            string[] parts = input.Split(' ', 2);
            string command = parts[0].ToLower();
            string arguments = parts.Length > 1 ? parts[1] : string.Empty;

            ICommand? commandInstance = commandRegistry.GetCommand(command);

            if (commandInstance != null)
            {
                commandInstance.Execute(arguments);
            }
            else
            {
                List<ICommand> commands = commandRegistry.GetAllCommands();
                string relevantCommand = CommandSuggestion.FindClosest(command, commands); 

                if (!string.IsNullOrEmpty(relevantCommand))
                {
                    ConsoleTextColor.Set("red");
                    Console.WriteLine($"'{command}' is not a real command!");

                    ConsoleTextColor.Set("yellow");
                    Console.WriteLine($"Did you mean '{relevantCommand}' ?");

                    ConsoleTextColor.Reset();
                }
                else
                {
                    ConsoleTextColor.Set("red");
                    Console.WriteLine($"'{command}' is not a real command!");

                    ConsoleTextColor.Reset();
                }

               
            }
        }

        private void RegisterCommands()
        {
            // Register commands here.
            commandRegistry.RegisterCommand(new ExitCommand(this));
            commandRegistry.RegisterCommand(new EchoCommand());
            commandRegistry.RegisterCommand(new HelpCommand(commandRegistry));
            commandRegistry.RegisterCommand(new ClearShellCommand(this));
            commandRegistry.RegisterCommand(new PWDCommand());
            commandRegistry.RegisterCommand(new CDCommand());
            commandRegistry.RegisterCommand(new LSCommand());
            commandRegistry.RegisterCommand(new MkdirCommand());
            commandRegistry.RegisterCommand(new RmdirCommand());
        }

        public void ClearShell()
        {
            Console.Clear();
        }

        private string GetInput()
        {
            string? temp_input = Console.ReadLine();

            if (string.IsNullOrEmpty(temp_input))
            {
                return string.Empty;
            }
            else
            {
                return temp_input;
            }
        }
    }
}
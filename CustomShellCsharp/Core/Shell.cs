using MyShellCommand.Commands;
using MyShellCommand.Services; 
using System;
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
            commandRegistry = new CommandRegistry();

            RegisterCommands(); // Register all commands
        }
        public void Run()
        {
            while (isRunning)
            {
                Console.Write("LeoShell> ");
                string input = GetInput();
                ProcessCommand(input);
      

            }
        }

        public void Stop()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Exiting the shell command...");
            Console.ResetColor(); 
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
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Command '{command}' not found.");
                Console.ResetColor();
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

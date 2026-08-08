using System;
using MyShellCommand.Core;
using System.Collections.Generic;
using MyShellCommand.Services;

namespace MyShellCommand.Commands
{
    internal class HelpCommand : ICommand
    {
        public string Name => "help";

        public string Description => "Displays help information for available commands";

        private CommandRegistry commandRegistry;

        public HelpCommand(CommandRegistry commandRegistry)
        {
            this.commandRegistry = commandRegistry;
        }

        public void Execute(string arguments)
        {
            ConsoleTextColor.Set("yellow");

            int num = 0;
            List<ICommand> commands = commandRegistry.GetAllCommands();

            Console.WriteLine("{0,-10} {1,-20} {2}",
                "Command # |", "Command Name |", "Description");

            Console.WriteLine(new string('-', 70));

            foreach (var command in commands)
            {
                num++;

                Console.WriteLine("{0,-10} {1,-20} {2}",
                    num, command.Name, command.Description);
            }

            ConsoleTextColor.Reset();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Text;
using MyShellCommand.Commands; 

namespace MyShellCommand.Services
{
    internal class CommandRegistry
    {
        private List<ICommand> commands; 


        public CommandRegistry()
        {
            commands = new List<ICommand>(); 
        }

        public void RegisterCommand(ICommand command)
        {
            commands.Add(command);
        }

        public ICommand? GetCommand(string name)
        {
            foreach (var command in commands)
            {
                if (command.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                {
                    return command; 
                }
            }
            return null;
        }
    }
}

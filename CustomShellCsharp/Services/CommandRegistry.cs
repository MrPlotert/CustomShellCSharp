using System.Collections.Generic;
using MyShellCommand.Commands;

namespace MyShellCommand.Services
{
    internal class CommandRegistry
    {
        private Dictionary<string, ICommand> commands;

        public CommandRegistry()
        {
            commands = new Dictionary<string, ICommand>();
        }

        public List<ICommand> GetAllCommands()
        {
            return new List<ICommand>(new HashSet<ICommand>(commands.Values));
        }

        public void RegisterCommand(ICommand command)
        {
            commands[command.Name.ToLower()] = command;

            foreach (string alias in command.Aliases)
            {
                commands[alias.ToLower()] = command;
            }
        }

        public ICommand? GetCommand(string name)
        {
            commands.TryGetValue(name.ToLower(), out ICommand? command);
            return command;
        }
    }
}
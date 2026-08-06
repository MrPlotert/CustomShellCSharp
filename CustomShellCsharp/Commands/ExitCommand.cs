using MyShellCommand.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyShellCommand.Commands
{
    internal class ExitCommand : ICommand
    {
        private Shell shell;
        public string Name => "exit";

        public string Description => "Exits the shell command.";

    public ExitCommand(Shell shell)
        {
            this.shell = shell; 

        }

        public void Execute(string arguments)
        {
            shell.Stop();
        }
    }
}

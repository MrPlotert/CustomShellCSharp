using MyShellCommand.Core;
using MyShellCommand.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyShellCommand.Commands
{
    internal class HistoryCommand : ICommand
    {
        public string Name => "history";

        public string Description => "Prints all of the last 20 commands used (INCLUDES the history command you typed)";

        private readonly Shell shell; 

        public HistoryCommand(Shell shell)
        {
            this.shell = shell; 
        }

        public void Execute(string arguments)
        {
            shell.PrintHistory(); 
        }
    }
}

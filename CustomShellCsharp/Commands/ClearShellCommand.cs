using System;
using MyShellCommand.Core; 
using System.Collections.Generic;
using System.Text;

namespace MyShellCommand.Commands
{
    internal class ClearShellCommand : ICommand
    {
        public string Name => "clear";

        public string Description => "Clears the console screen";

        private readonly Shell shell; 

        public ClearShellCommand(Shell shell)
        {
            this.shell = shell;
        }

        public void Execute(string arguments)
        {
            shell.ClearShell(); 
        }
    }
}

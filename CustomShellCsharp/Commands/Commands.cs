using System;
using System.Collections.Generic;
using System.Text;

namespace MyShellCommand.Commands
{
    internal interface ICommand
    {
        string Name { get; } 
        string Description { get; }

        void Execute(string arguments);
    }
}

using System;
using System.IO; 
using MyShellCommand.Core; 
using System.Collections.Generic;
using System.Text;

namespace MyShellCommand.Commands
{
    internal class PWDCommand : ICommand
    {
        public string Name => "pwd"; 

        public string Description => "Displays the current working directory.";

        public void Execute(string arguments)
        {
            if (!string.IsNullOrWhiteSpace(arguments))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("The 'pwd' command does not accept any arguments.");
                Console.ResetColor();
                return; 
            }
            Console.WriteLine(Directory.GetCurrentDirectory());
        }
    }
}

using System;
using System.IO;
using MyShellCommand.Services; 
using MyShellCommand.Core; 
using System.Collections.Generic;

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
                ConsoleTextColor.Set("red"); 
                Console.WriteLine("The 'pwd' command does not accept any arguments.");
                ConsoleTextColor.Reset();
                return; 
            }
            Console.WriteLine(Directory.GetCurrentDirectory());
        }
    }
}

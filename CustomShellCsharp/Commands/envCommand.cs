using MyShellCommand.Services;
using System;
using System.Collections;

namespace MyShellCommand.Commands
{
    internal class envCommand : ICommand
    {
        public string Name => "env";

        public string Description => "Prints all environment variables.";

        public void Execute(string arguments)
        {
            if (!string.IsNullOrEmpty(arguments))
            {
                ConsoleTextColor.Set("red");
                Console.WriteLine($"'{Name}' does not accept any arguments!");
                ConsoleTextColor.Reset();
                return;
            }

            ConsoleTextColor.Set("yellow");
            Console.WriteLine("Environment Variables");
            Console.WriteLine("---------------------");
            ConsoleTextColor.Reset();

            foreach (DictionaryEntry variable in Environment.GetEnvironmentVariables())
            {
                Console.WriteLine();
                Console.WriteLine($"{variable.Key,-35} = {variable.Value}");
            }

            Console.WriteLine();

            ConsoleTextColor.Set("yellow");
            Console.WriteLine("---------------------");
            Console.WriteLine("End of environment variables.");
            ConsoleTextColor.Reset();
        }
    }
}
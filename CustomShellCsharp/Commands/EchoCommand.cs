using System;
using MyShellCommand.Core;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;

namespace MyShellCommand.Commands
{
    internal class EchoCommand : ICommand
    {
        public string Name => "echo";

        public string Description => "Prints the provided arguments to the console.";

        // Command options
        private const string RepeatOption = "-n";
        private const string UppercaseOption = "-u";

        public void Execute(string arguments)
        {
            // Default option values
            int repeat = 1;
            bool uppercase = false;

            // Split the user's input into individual words/options
            string[] parts = arguments.Split(' ');

            // Stores where the actual message begins
            int messageStartIndex = -1;

            // Parse command options (-n, -u, etc.)
            for (int i = 0; i < parts.Length; i++)
            {
                // Handle the repeat option
                if (parts[i] == RepeatOption && (i + 1) < parts.Length)
                {
                    // Validate and store the repeat count
                    if (int.TryParse(parts[i + 1], out int parsedRepeat))
                    {
                        repeat = parsedRepeat;
                        i++; // Skip the number since we've already processed it
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"Invalid repeat value: {parts[i + 1]}");
                        Console.ResetColor();
                        return;
                    }
                }
                // Enable uppercase output
                else if (parts[i] == UppercaseOption)
                {
                    uppercase = true;
                }
                else
                {
                    // First non-option marks the beginning of the message
                    messageStartIndex = i;
                    break;
                }
            }

            // Only continue if a message was found
            if (messageStartIndex > -1)
            {
                string message = "";

                // Build the message from the remaining words
                for (int i = messageStartIndex; i < parts.Length; i++)
                {
                    if (!string.IsNullOrEmpty(parts[i]))
                    {
                        message += parts[i] + " ";
                    }
                }

                // Remove the extra space added to the end
                message = message.TrimEnd();

                // Convert the message to uppercase if requested
                if (uppercase)
                {
                    message = message.ToUpper();
                }

                // Print the message the requested number of times
                for (int j = 0; j < repeat; j++)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine(message);
                    Console.ResetColor();
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No message provided to echo.");
                Console.ResetColor();
            }
        }
    }
}
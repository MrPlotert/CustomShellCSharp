using System;
using MyShellCommand.Core; 
using System.Collections.Generic;
using MyShellCommand.Commands;
using System.Reflection.Metadata.Ecma335;


namespace MyShellCommand.Services
{
    internal static class CommandSuggestion 
    {
         public static string FindClosest(string input, List<ICommand> commands)
        {
            int matches = 0;
            string closestCommand = string.Empty;
            int highestMatches = 0; 
            
            foreach (ICommand command in commands)
            {
                matches = 0; 
                string commandName = command.Name; 

                foreach (var Char in input)
                {
                    if (matches <= commandName.Length - 1)
                    {
                        if (Char == commandName[matches])
                        {
                            matches++;
                        }
                    }
                }

                if (matches > highestMatches)
                {
                    closestCommand = commandName; 
                    highestMatches = matches;
                }

            }


            return closestCommand; 
        }
    }
}

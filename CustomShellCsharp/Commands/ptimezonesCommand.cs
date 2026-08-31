using MyShellCommand.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyShellCommand.Commands
{
    internal class ptimezonesCommand : ICommand
    {
        public string Name => "ptimezones";

        public string Description => "Prints all the available time zones";

        public void Execute(string arguments)
        {
            CountryTimeZones.PrintTimezones();
        }
    }
}

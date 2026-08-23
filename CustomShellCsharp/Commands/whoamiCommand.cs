using MyShellCommand.Services;



namespace MyShellCommand.Commands
{
    internal class whoamiCommand : ICommand
    {
        public string Name => "whoami";

        public string Description => "Displays your system's name information and user information";

        public void Execute(string arguments)
        {
            ConsoleTextColor.Set("cyan");

            // Print the info
            Console.WriteLine($"Machine Name: {Environment.MachineName}");
            Console.WriteLine($"User Name: {Environment.UserName}");
            Console.WriteLine($"User Domain Name: {Environment.UserDomainName}");

            ConsoleTextColor.Reset();
        }
    }
}

using System;
using System.IO;
using System.Security.Principal;
using MyShellCommand.Services;

namespace MyShellCommand.Commands
{
    internal class ClearBinCommand : ICommand
    {
        public string Name => "clearbin";

        public string Description => "Clears the recycle bin on your current machine. (NOTE: DELETES ON ALL DRIVES)";

        public void Execute(string arguments)
        {
            if (!string.IsNullOrWhiteSpace(arguments))
            {
                ConsoleTextColor.Set("red");
                Console.WriteLine($"'{Name}' command does not accept any arguments!");
                ConsoleTextColor.Reset();
                return;
            }

            string sid = WindowsIdentity.GetCurrent().User?.Value;

            if (string.IsNullOrEmpty(sid))
            {
                ConsoleTextColor.Set("red");
                Console.WriteLine("Could not determine current user SID.");
                ConsoleTextColor.Reset();
                return;
            }

            int deletedCount = 0;
            int failedCount = 0;

            foreach (DriveInfo drive in DriveInfo.GetDrives())
            {
                if (drive.DriveType != DriveType.Fixed || !drive.IsReady)
                {
                    continue;
                }

                string binPath = Path.Combine(drive.RootDirectory.FullName, "$Recycle.Bin", sid);

                if (!Directory.Exists(binPath))
                {
                    continue;
                }

                foreach (string entry in Directory.GetFileSystemEntries(binPath))
                {
                    try
                    {
                        if (Path.GetFileName(entry).Equals("desktop.ini", StringComparison.OrdinalIgnoreCase))
                        {
                            continue;
                        }

                        if (Directory.Exists(entry))
                        {
                            Console.WriteLine(entry);
                            Directory.Delete(entry, true);
                        }
                        else
                        {
                            Console.WriteLine(entry);
                            File.Delete(entry);
                        }

                        deletedCount++;
                    }
                    catch (UnauthorizedAccessException)
                    {
                        failedCount++;
                    }
                    catch (IOException)
                    {
                        failedCount++;
                    }
                }
            }

            ConsoleTextColor.Set("green");
            Console.WriteLine($"Recycle bin cleared: {deletedCount} item(s) deleted.");
            ConsoleTextColor.Reset();

            if (failedCount > 0)
            {
                ConsoleTextColor.Set("red");
                Console.WriteLine($"{failedCount} item(s) could not be deleted (in use or access denied).");
                ConsoleTextColor.Reset();
            }
        }
    }
}
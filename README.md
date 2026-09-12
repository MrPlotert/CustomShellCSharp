# CustomShellCSharp

A modular command-line shell written in **C#** and built completely from scratch as a learning project. The goal of this project is to create an extensible shell where every command is its own class, making it easy to maintain and expand over time.

---

## Features

* Modular command architecture
* `ICommand` interface for all commands
* Command registry for automatic command lookup
* Dynamic shell prompt showing the current directory
* Shell starts in the user's home directory
* Support for command aliases (multiple names per command)
* Built-in `echo`, `exit`, `help`, `clear`, `pwd`, `cd`, `ls` commands
* Built-in `mkdir`, `rmdir`, `del`, `rename`, `touch`, `copy`, `cut` file/directory commands
* Built-in `cat` command for printing file contents
* Built-in `clearbin` command
* Built-in `sysinfo` command
* Built-in `date` command with per-country time zone lookup
* Built-in `restart` command
* Built-in `matrix` command for a console rain effect
* Support for command options
* Input validation and error handling
* Command suggestion system for mistyped commands
* Centralized console text color service
* Easy to extend with new commands

---

## Current Commands

| Command    | Aliases | Description                                                     |
| ---------- | ------- | ---------------------------------------------------------------- |
| `echo`     |         | Prints text to the console with optional modifiers.             |
| `exit`     |         | Closes the shell.                                                |
| `help`     | `?`     | Displays help information for available commands.                |
| `clear`    |         | Clears the console screen.                                       |
| `pwd`      |         | Displays the current working directory.                          |
| `cd`       |         | Changes the current directory.                                   |
| `ls`       |         | Lists files and folders in the current directory.                |
| `mkdir`    |         | Creates a new directory.                                         |
| `rmdir`    |         | Removes a directory, with a native confirmation dialog.          |
| `del`      |         | Deletes a file.                                                  |
| `rename`   |         | Renames a file or directory.                                     |
| `touch`    |         | Creates a new empty file.                                        |
| `copy`     |         | Copies a file or directory from source to destination.           |
| `cut`      |         | Moves a file or directory from source to destination.            |
| `cat`      |         | Prints the contents of a file.                                   |
| `clearbin` |         | Clears the Recycle Bin across all fixed drives.                  |
| `sysinfo`  |         | Prints system and device information.                            |
| `date`     |         | Prints the date and time for a given country code.               |
| `restart`  |         | Restarts the shell application.                                  |
| `matrix`   |         | Displays an animated Matrix-style character rain effect.         |

### Echo Options

| Option        | Description                                       |
| ------------- | ------------------------------------------------- |
| `-n <number>` | Repeats the message a specified number of times.  |
| `-u`          | Converts the output to uppercase before printing. |

Example:

```text
echo -u -n 3 Hello World
```

Output:

```text
HELLO WORLD
HELLO WORLD
HELLO WORLD
```

### `ls` Details

`ls` does not accept any arguments — it only lists the contents of the current directory (no browsing into other folders).

For each entry it displays:

* **Folders** — name and creation time.
* **Files** — name, extension, size (formatted as B/KB/MB), and creation time.

Folders and files are printed in separate, color-coded sections for readability.

### `mkdir` Details

`mkdir` creates a new directory at the given path, resolved relative to the current directory (absolute paths are also supported, similar to `cd`).

* Rejects empty/whitespace arguments.
* Reports an error if a file or directory already exists at the target path.
* Validates the resolved path and reports invalid characters, overly long paths, or unsupported formats.
* Handles permission and I/O errors during creation.

### `rmdir` Details

`rmdir` removes a directory at the given path, resolved relative to the current directory (like `cd`), and always triggers Windows' native confirmation and progress dialogs before deleting anything.

| Option | Description                                                          |
| ------ | ---------------------------------------------------------------------|
| `-p`   | Deletes the directory permanently instead of using the Recycle Bin.  |

By default (no `-p`), the directory is sent to the Recycle Bin rather than permanently deleted.

Example:

```text
rmdir OldFolder
rmdir -p OldFolder
```

### `del` Details

`del` deletes a single file at the given path, resolved relative to the current directory.

* Rejects empty/whitespace arguments.
* Reports an error if the file does not exist.
* Handles permission, path length, and I/O errors during deletion.

### `rename` Details

`rename` renames or relocates a file or directory.

Usage:

```text
rename <old_name> <new_name>
```

* Works on both files and directories.
* Reports an error if the source does not exist, or if the new name is already taken.

### `touch` Details

`touch` creates a new, empty file at the given path.

### `copy` Details

`copy` copies a file or directory from a source to a destination. Directories are copied recursively, including all nested subfolders and files.

Usage:

```text
copy <source> to <destination>
```

The word `to` must appear as its own standalone word between the source and destination. If the destination is an existing folder, the source's file name is automatically appended to it.

**Warning:** existing files at the destination are overwritten without confirmation.

Example:

```text
copy notes.txt to C:\Users\Leo\Desktop
```

### `cut` Details

`cut` behaves the same as `copy`, but moves the file or directory instead of duplicating it — the source is removed once the move completes.

Usage:

```text
cut <source> to <destination>
```

Missing destination folders are created automatically, matching `copy`'s behavior.

### `cat` Details

`cat` prints the full contents of a text file to the console.

* Rejects empty/whitespace arguments.
* Reports an error if the file does not exist.
* Handles permission and I/O errors while reading.

### `clearbin` Details

`clearbin` empties the Recycle Bin without a confirmation prompt, using plain `System.IO` (no external DLLs or interop).

* Does not accept any arguments.
* Resolves the current user's SID and only touches that user's Recycle Bin contents.
* Iterates every fixed, ready drive, targeting each drive's `$Recycle.Bin\<SID>` folder.
* Deletes files and folders individually, skipping `desktop.ini`.
* Reports a summary count of deleted and failed items; a locked or in-use item does not stop the rest of the operation.

### `sysinfo` Details

`sysinfo` prints a summary of the current system, organized into sections:

* **Operating System** — OS version, description, architecture, 64-bit OS/process.
* **Machine** — machine name, user name, processor count, system uptime.
* **.NET Runtime** — framework description, runtime identifier.
* **Storage** — free/total space for each ready, fixed drive.

### `date` Details

`date` prints the current date and time for a given country code, using a built-in country-to-timezone lookup.

Usage:

```text
date <country_code>
```

Displays the date, 24-hour time, 12-hour time, and the resolved time zone's display name. Countries spanning multiple time zones (e.g. the US, Russia) resolve to one representative zone.

### `restart` Details

`restart` relaunches the shell as a fresh process and closes the current one.

* Does not accept any arguments.
* Resolves the currently running executable's path automatically, regardless of where it's located on disk.
* Clears the console before launching the new instance so the restart appears clean rather than showing leftover output from the previous session.

### `matrix` Details

`matrix` fills the console with an animated, falling-character rain effect inspired by *The Matrix*, with a fading green trail behind a bright white leading character in each column.

* Does not accept any arguments.
* Runs until any key is pressed, then cleans up and returns to the shell normally.

---

## Command Suggestions

The shell includes a basic command suggestion system for commands that are not recognized.

For example, if the user enters:

```text
ech
```

The shell can suggest:

```text
ech not found. Did you mean 'echo'?
```

The suggestion system compares the user's input against the commands registered with the `CommandRegistry` and determines which command is the closest match.

This functionality is handled by the `CommandSuggestion` service.

---

## Console Text Colors

Console text colors are handled through a dedicated `ConsoleTextColor` service instead of changing `Console.ForegroundColor` directly throughout the project.

For example:

```csharp
ConsoleTextColor.Set("red");
Console.WriteLine("Error message");
ConsoleTextColor.Reset();
```

The service uses a dictionary to match color names to the corresponding `ConsoleColor` values.

This keeps color handling centralized and makes it easier to change or expand later.

---

## Command Aliases

Commands can optionally declare multiple names through an `Aliases` property on `ICommand`. Aliases are registered alongside a command's primary name, so typing any of them runs the same command.

For example, `help` can also be invoked as `?`.

`HelpCommand` displays a command's aliases next to its primary name in the help table.

---

## Examples

A quick reference showing one example usage for every command.

```text
echo -u -n 3 Hello World
exit
help
?
clear
pwd
cd Documents
ls
mkdir NewFolder
rmdir OldFolder
rmdir -p OldFolder
del notes.txt
rename notes.txt backup.txt
touch newfile.txt
copy notes.txt to C:\Users\Leo\Desktop
cut notes.txt to C:\Users\Leo\Desktop
cat notes.txt
clearbin
sysinfo
date za
restart
matrix
```

---

## Planned Features

* Improve command suggestion accuracy
* Command history
* Better argument parsing
* Configuration support
* Additional built-in commands
* More advanced shell functionality

---

## Project Structure

```text
CustomShellCSharp/
│
├── Commands/
│   ├── ICommand.cs
│   ├── EchoCommand.cs
│   ├── ExitCommand.cs
│   ├── HelpCommand.cs
│   ├── ClearShellCommand.cs
│   ├── PWDCommand.cs
│   ├── CDCommand.cs
│   ├── LSCommand.cs
│   ├── MkdirCommand.cs
│   ├── RmdirCommand.cs
│   ├── DelCommand.cs
│   ├── RenameCommand.cs
│   ├── TouchCommand.cs
│   ├── CopyCommand.cs
│   ├── CutCommand.cs
│   ├── CatCommand.cs
│   ├── ClearBinCommand.cs
│   ├── SysInfoCommand.cs
│   ├── DateCommand.cs
│   ├── RestartShellCommand.cs
│   └── MatrixCommand.cs
│
├── Core/
│   └── Shell.cs
│
├── Services/
│   ├── CommandRegistry.cs
│   ├── ConsoleTextColor.cs
│   ├── CommandSuggestion.cs
│   └── CountryTimeZones.cs
│
├── Program.cs
└── CustomShellCSharp.csproj
```

---

## Technologies

* C#
* .NET
* Visual Studio
* Git
* GitHub

---

## Why I Made This

This project was created to improve my understanding of:

* Object-Oriented Programming (OOP)
* Interfaces
* Classes and objects
* Command parsing
* Collections
* Dictionaries
* Error handling
* String manipulation
* File system operations
* Clean code principles
* Software architecture
* Building modular applications

Rather than creating everything inside one large `switch` statement, every command is implemented as its own class and registered with the shell. This makes adding new commands straightforward and keeps the project organized as it grows.

The project is also being used as a way to learn C# by actually building something rather than only following tutorials.

---

## Future Goals

The long-term goal is to continue expanding this project into a more capable shell while learning new C# concepts along the way.

Every new feature is an opportunity to improve both the shell and my programming skills.

---

## Author

Created by **@MrPlotert** on Github.
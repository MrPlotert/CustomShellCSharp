# CustomShellCSharp

A modular command-line shell written in **C#** and built completely from scratch as a learning project. The goal of this project is to create an extensible shell where every command is its own class, making it easy to maintain and expand over time.

---

## Features

* Modular command architecture
* `ICommand` interface for all commands
* Command registry for automatic command lookup
* Dynamic shell prompt showing the current directory
* Shell starts in the user's home directory
* Built-in `echo` command
* Built-in `exit` command
* Built-in `help` command
* Built-in `clear` command
* Built-in `pwd` command
* Built-in `cd` command
* Built-in `ls` command
* Built-in `mkdir` command
* Built-in `rmdir` command
* Support for command options
* Input validation and error handling
* Command suggestion system for mistyped commands
* Centralized console text color service
* Easy to extend with new commands

---

## Current Commands

| Command | Description                                         |
| ------- | --------------------------------------------------- |
| `echo`  | Prints text to the console with optional modifiers. |
| `exit`  | Closes the shell.                                   |
| `help`  | Displays help information for available commands.   |
| `clear` | Clears the console screen.                          |
| `pwd`   | Displays the current working directory.             |
| `cd`    | Changes the current directory.                      |
| `ls`    | Lists files and folders in the current directory.   |
| `mkdir` | Creates a new directory.                             |
| `rmdir` | Removes a directory, with a native confirmation dialog. |

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

## Planned Features

* Improve command suggestion accuracy
* Command aliases
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
│   └── RmdirCommand.cs
│
├── Core/
│   └── Shell.cs
│
├── Services/
│   ├── CommandRegistry.cs
│   ├── ConsoleTextColor.cs
│   └── CommandSuggestion.cs
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

Created by **@MrPlotter5557**.
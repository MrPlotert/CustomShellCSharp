# CustomShellCSharp

A modular command-line shell written in **C#** and built completely from scratch as a learning project. The goal of this project is to create an extensible shell where every command is its own class, making it easy to maintain, understand, and expand over time.

---

## Features

* Modular command architecture
* `ICommand` interface for all commands
* Centralized `CommandRegistry`
* Automatic command lookup
* Custom shell prompt (`LeoShell>`)
* Built-in help system
* Command-specific argument validation
* Support for command options
* Consistent colored console output
* Easy to extend with new commands

---

## Current Commands

| Command | Description                                                         |
| ------- | ------------------------------------------------------------------- |
| `echo`  | Prints text to the console with optional modifiers.                 |
| `exit`  | Closes the shell.                                                   |
| `help`  | Displays a table of all registered commands and their descriptions. |
| `clear` | Clears the console window.                                          |
| `pwd`   | Displays the current working directory.                             |

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

---

## Planned Features

* Change directory (`cd`)
* Directory listing (`dir`)
* File creation
* File deletion
* File copying
* File moving
* Command aliases
* Command history
* Improved argument parser
* Configuration support
* Command usage information
* Additional built-in commands

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
│   └── PwdCommand.cs
│
├── Core/
│   └── Shell.cs
│
├── Services/
│   └── CommandRegistry.cs
│
├── Program.cs
└── CustomShellCSharp.csproj
```

---

## Architecture

The shell follows a modular architecture where every command is implemented as its own class.

```text
User Input
     │
     ▼
   Shell
     │
     ▼
CommandRegistry
     │
     ▼
Find matching ICommand
     │
     ▼
Execute()
```

Adding a new command only requires three steps:

1. Create a class that implements `ICommand`.
2. Register it with the `CommandRegistry`.
3. The command automatically becomes available to the shell and appears in the `help` command.

---

## Technologies

* C#
* .NET
* Visual Studio
* Git
* GitHub

---

## Why I Made This

This project was created to strengthen my understanding of:

* Object-Oriented Programming (OOP)
* Interfaces
* Classes and objects
* Dependency Injection
* Collections (`List<T>`)
* Command parsing
* Input validation
* Clean code principles
* Software architecture
* Building modular applications

Rather than placing all command logic inside one large `switch` statement, every command is implemented as its own class and registered with the shell. This makes the project easier to maintain, easier to extend, and closer to how larger software projects are structured.

---

## Future Goals

The long-term goal is to continue expanding this project into a more capable shell while learning new C# concepts and .NET APIs. Every new command is an opportunity to practice writing clean, maintainable code and to explore how operating systems and command-line applications work.

Ultimately, I want this project to grow into a fully featured custom shell that demonstrates solid software design while documenting my progress as I continue learning C#.

---

## Author

Created by **@MrPlotter5557**.

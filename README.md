# CustomShellCSharp

A modular command-line shell written in **C#** and built completely from scratch as a learning project. The goal of this project is to create an extensible shell where every command is its own class, making it easy to maintain and expand over time.

---

## Features

* Modular command architecture
* `ICommand` interface for all commands
* Command registry for automatic command lookup
* Custom shell prompt (`LeoShell>`)
* Built-in `echo` command
* Built-in `exit` command
* Support for command options
* Input validation and error handling
* Easy to extend with new commands

---

## Current Commands

| Command | Description                                         |
| ------- | --------------------------------------------------- |
| `echo`  | Prints text to the console with optional modifiers. |
| `exit`  | Closes the shell.                                   |

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

* Help command
* Clear screen command
* Current directory command
* Change directory command
* Directory listing
* Command aliases
* Command history
* Better argument parsing
* Configuration support
* Additional built-in commands

---

## Project Structure

```text
CustomShellCSharp/
│
├── Commands/
│   ├── ICommand.cs
│   ├── EchoCommand.cs
│   └── ExitCommand.cs
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
* Clean code principles
* Software architecture
* Building modular applications

Rather than creating everything inside one large `switch` statement, every command is implemented as its own class and registered with the shell. This makes adding new commands straightforward and keeps the project organized as it grows.

---

## Future Goals

The long-term goal is to continue expanding this project into a more capable shell while learning new C# concepts along the way. Every new feature is an opportunity to improve both the shell and my programming skills.

---

## Author

Created by **@MrPlotter5557**.

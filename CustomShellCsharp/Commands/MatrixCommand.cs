using MyShellCommand.Services;
using System;
using System.Threading;

namespace MyShellCommand.Commands
{
    internal class MatrixCommand : ICommand
    {
        public string Name => "matrix";

        public string Description => "Does a cool matrix effect inside of the console.";

        public void Execute(string arguments)
        {
            int screenHeight = Console.WindowHeight;
            int screenWidth = Console.WindowWidth;
            Random rng = new Random();
            int trailLength = 8;
            int[] headPositions = new int[screenWidth];

            for (int i = 0; i < headPositions.Length; i++)
            {
                headPositions[i] = rng.Next(-40, 0);
            }

            Console.CursorVisible = false;

            while (true)
            {
                for (int x = 0; x < screenWidth; x++)
                {
                    char randomChar = (char)rng.Next(33, 127);

                    if (headPositions[x] >= 0 && headPositions[x] < screenHeight)
                    {
                        Console.SetCursorPosition(x, headPositions[x]);
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write(randomChar);
                    }

                    for (int trailStep = 1; trailStep <= trailLength; trailStep++)
                    {
                        int trailPosition = headPositions[x] - trailStep;

                        if (trailPosition >= 0 && trailPosition < screenHeight)
                        {
                            Console.SetCursorPosition(x, trailPosition);
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write(randomChar);
                        }
                    }

                    int clearPosition = headPositions[x] - trailLength - 1;

                    if (clearPosition >= 0 && clearPosition < screenHeight)
                    {
                        Console.SetCursorPosition(x, clearPosition);
                        Console.Write(' ');
                    }

                    headPositions[x]++;

                    if (headPositions[x] >= screenHeight)
                    {
                        for (int clearStep = 0; clearStep <= trailLength; clearStep++)
                        {
                            int leftoverPosition = screenHeight - 1 - clearStep;

                            if (leftoverPosition >= 0)
                            {
                                Console.SetCursorPosition(x, leftoverPosition);
                                Console.Write(' ');
                            }
                        }

                        headPositions[x] = rng.Next(-40, 0);
                    }
                }

                Thread.Sleep(30);

                if (Console.KeyAvailable)
                {
                    Console.ReadKey(true);
                    break;
                }
            }

            ConsoleTextColor.Reset();
            Console.Clear();
            Console.CursorVisible = true;
        }
    }
}
using System;
using AdventOfCode2021.Puzzles;

namespace AdventOfCode2021
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length > 1)
            {
                var puzzleNumber = int.Parse(args[0]);
                
                var puzzle = GetPuzzle(puzzleNumber, args);
                puzzle.Solve();
            }
            else
            {
                Console.WriteLine("Missing arguments");
            }
        }

        static IPuzzle GetPuzzle(int puzzleNumber, string[] args)
        {
            switch (puzzleNumber)
            {
                case 1:
                    return new Puzzle1(args[1]);
                case 2:
                    return new Puzzle2(args[1]);
                default:
                    throw new ArgumentException("Unknown puzzle number", nameof(puzzleNumber));
            }
        }
    }
}

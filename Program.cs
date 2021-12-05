using System;
using AdventOfCode2021.Puzzles;

namespace AdventOfCode2021
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                var challenge = new Puzzle1(args[0]);
                challenge.Solve();
            }
            else
            {
                Console.WriteLine("No arguments found!");
            }
        }
    }
}

using System;
using System.IO;
using System.Linq;

namespace AdventOfCode2021.Puzzles
{
    public class Puzzle1 : IPuzzle
    {
        private readonly string inputPath;

        public Puzzle1(string inputPath)
        {
            this.inputPath = inputPath;
        }

        public void Solve()
        {
            try
            {
                int[] measurements = File.ReadAllLines(inputPath)
                    .Select(line => int.Parse(line)) // could throw exception...
                    .ToArray();
                Solve(measurements);
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Could not find input file at : {0}", inputPath);
            }
        }

        private void Solve(int[] measurements)
        {
            int previous = measurements[0];
            var increasedCount = measurements
                .Skip(1)
                .Aggregate(0, (count, next) => 
                {
                    if (next > previous)
                    {
                        count++;
                    }
                    previous = next;
                    return count;
                });

            Console.WriteLine("Solution: {0}", increasedCount);
        }
    }
}
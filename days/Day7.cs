using System;
using System.IO;
using System.Linq;

namespace AdventOfCode2021.Days
{
    public class Day7 : IDay
    {
        private readonly string inputPath = "inputs/day7.txt";

        public void Part1()
        {
            FindBestPosition((src, dst) => Math.Abs(dst-src));
        }

        public void Part2()
        {
            FindBestPosition((src, dst) => {
                int n = Math.Abs(dst-src);
                return n*(n+1)/2;
            });
        }

        private void FindBestPosition(Func<int, int, int> costFunction)
        {
            var crabs = File.ReadAllLines(inputPath)[0]
                .Split(",")
                .Select(s => int.Parse(s))
                .ToArray();

            var maxPosition = crabs.Max();

            var lowestFuel = int.MaxValue;
            var bestPosition = -1;
            for (int i = 0; i <= maxPosition; i++)
            {
                int fuel = crabs.Aggregate(0, (total, pos) => total + costFunction(pos, i));
                if (fuel < lowestFuel)
                {
                    lowestFuel = fuel;
                    bestPosition = i;
                }
            }

            Console.WriteLine("Solution : {0}", lowestFuel);
        }
    }
}

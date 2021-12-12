using System;
using System.IO;
using System.Linq;

namespace AdventOfCode2021.Days
{
    public class Day7 : Day
    {
        public override ulong Part1()
        {
            return FindBestPosition((src, dst) => Math.Abs(dst-src));
        }

        public override ulong Part2()
        {
            return FindBestPosition((src, dst) => {
                int n = Math.Abs(dst-src);
                return n*(n+1)/2;
            });
        }

        private ulong FindBestPosition(Func<int, int, int> costFunction)
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

            return (ulong) lowestFuel;
        }
    }
}

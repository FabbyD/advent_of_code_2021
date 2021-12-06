using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2021.Days
{
    public class Day3 : IDay
    {
        private const string inputPath = "inputs/day3.txt";

        public void Part1()
        {
            var lines = File.ReadAllLines(inputPath);
            var counts = new List<int>();
            foreach (var line in lines)
            {
                for (int i = 0; i < line.Length; i++)
                {
                    if (counts.Count <= i)
                    {
                        counts.Add(0);
                    }

                    if (line[i] == '1')
                    {
                        counts[i]++;
                    }
                }
            }

            int mask = 0;
            int gammaRate = 0;
            for (int i = 0; i < counts.Count; i++)
            {
                int count = counts[i];
                if (count > lines.Length / 2)
                {
                    gammaRate |= 1 << i;
                }
                else if (count == lines.Length)
                {
                    throw new InvalidOperationException("Derp.");
                }

                mask |= 1 << i;
            }

            int epsilonRate = ~gammaRate & mask;

            Console.WriteLine("Solution: {0}", gammaRate * epsilonRate);
        }

        public void Part2()
        {

        }
    }
}
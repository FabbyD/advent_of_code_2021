using System;
using System.IO;

namespace AdventOfCode2021.Days
{
    public class Day3 : IDay
    {
        private const string inputPath = "inputs/day3.txt";

        public void Part1()
        {
            var lines = File.ReadAllLines(inputPath);
            var bitCounts = CountBits(lines);
            int numBits = bitCounts.Length;
            int gammaRate = ComputeGammaRate(bitCounts, lines.Length);
            int epsilonRate = ComputeEpsilonRate(gammaRate, numBits);

            Console.WriteLine("Solution: {0}", gammaRate * epsilonRate);
        }

        public void Part2()
        {
            
        }

        private int[] CountBits(string[] lines)
        {
            // assuming all binary numbers are of the same length
            var numBits = lines[0].Length;
            var counts = new int[numBits];

            foreach (var line in lines)
            {
                for (int i = 0; i < line.Length; i++)
                {
                    if (line[i] == '1')
                    {
                        counts[i]++;
                    }
                }
            }

            return counts;
        }

        private int ComputeGammaRate(int[] bitCounts, int reportSize)
        {
            int gammaRate = 0;
            for (int i = 0; i < bitCounts.Length; i++)
            {
                int count = bitCounts[i];
                if (count > reportSize / 2)
                {
                    gammaRate |= 1 << i;
                }
                else if (count == reportSize)
                {
                    throw new InvalidOperationException("Derp.");
                }
            }

            return gammaRate;
        }

        private int ComputeEpsilonRate(int gammaRate, int numBits)
        {
            int mask = 0;
            for (int i = 0; i < numBits; i++)
            {
                mask |= 1 << i;
            }

            return ~gammaRate & mask;
        }
    }
}
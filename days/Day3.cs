using System;
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
            int gammaRate = ComputeGammaRate(lines);
            int numBits = lines[0].Length;
            int epsilonRate = ComputeEpsilonRate(gammaRate, numBits);

            Console.WriteLine("Solution: {0}", gammaRate * epsilonRate);
        }

        public void Part2()
        {
            var lines = File.ReadAllLines(inputPath);
            
            int oxygenRating = FindOxygenRating(lines);
            int co2Rating = FindCO2Rating(lines);

            Console.WriteLine("Solution: {0}", oxygenRating * co2Rating);
        }

        private bool IsEven(int number) => number % 2 == 0;

        private int FindOxygenRating(string[] lines)
        {
            return FindRating(lines, 0, (lines, bitPosition) => {
                var onesCount = CountBit(lines, bitPosition);

                int half = lines.Length / 2;
                if (IsEven(lines.Length))
                {
                    return onesCount >= half ? '1' : '0';
                }
                else
                {
                    return onesCount > half ? '1' : '0';
                }
            });
        }

        private int FindCO2Rating(string[] lines)
        {
            return FindRating(lines, 0, (lines, bitPosition) => {
                var onesCount = CountBit(lines, bitPosition);

                float half = lines.Length / 2;
                if (IsEven(lines.Length))
                {
                    return onesCount >= half ? '0' : '1';
                }
                else
                {
                    return onesCount > half ? '0' : '1';
                }
            });
        }

        private int FindRating(string[] lines, int bitPosition, Func<string[], int, char> findBit)
        {
            if (lines.Length == 1)
            {
                return Convert.ToInt32(lines[0], 2);
            }

            if (bitPosition >= lines[0].Length)
            {
                throw new InvalidOperationException("Derp.");
            }

            char comparedBit = findBit(lines, bitPosition);

            var filteredLines = lines
                .Where(line => line[bitPosition] == comparedBit)
                .ToArray();
            
            return FindRating(filteredLines, bitPosition + 1, findBit);
        }

        private int[] CountBits(string[] lines)
        {
            // assuming all binary numbers are of the same length
            var numBits = lines[0].Length;
            var counts = new int[numBits];

            for (int bitPosition = 0; bitPosition < numBits; bitPosition++)
            {
                counts[bitPosition] = CountBit(lines, bitPosition);
            }

            return counts;
        }

        private int CountBit(string[] lines, int bitPosition)
        {
            int count = 0;
            foreach (var line in lines)
            {
                if (line[bitPosition] == '1')
                {
                    count++;
                }
            }

            return count;
        }
        
        private int ComputeGammaRate(string[] lines)
        {
            int[] bitCounts = CountBits(lines);
            int reportSize = lines.Length;
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
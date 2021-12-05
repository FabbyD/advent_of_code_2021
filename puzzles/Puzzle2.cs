using System;
using System.IO;
using System.Linq;

namespace AdventOfCode2021.Puzzles
{
    public class Puzzle2 : IPuzzle
    {
        private readonly string inputPath;

        public Puzzle2(string inputPath)
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
            const int windowSize = 3;
            int previousSum = WindowSum(measurements, 0, windowSize);
            int count = 0;
            for(int start = 1; start <= measurements.Length - windowSize; start++)
            {
                int currentSum = WindowSum(measurements, start, windowSize);

                if (currentSum > previousSum)
                {
                    count++;
                }

                previousSum = currentSum;
            }

            Console.WriteLine("Solution: {0}", count);
        }

        private int WindowSum(int[] measurements, int start, int size)
        {
            int sum = 0;
            for(int i = start; i < start+size; i++)
            {
                sum += measurements[i];
            }

            return sum;
        }
    }
}
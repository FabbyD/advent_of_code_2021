using System;
using System.IO;
using System.Linq;

namespace AdventOfCode2021.Days
{
    public class Day1 : Day
    {
        public override void Part1()
        {
            int[] measurements = ReadMeasurements();

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

        public override void Part2()
        {
            int[] measurements = ReadMeasurements();
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

        private int[] ReadMeasurements()
        {
            return File.ReadAllLines(inputPath)
                    .Select(line => int.Parse(line)) // could throw exception...
                    .ToArray();
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
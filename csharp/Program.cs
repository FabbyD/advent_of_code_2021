using System;
using AdventOfCode2021.Days;

namespace AdventOfCode2021
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length > 1)
            {
                bool useExample = args.Length > 2 && args[2] == "-t";
                int dayNumber = int.Parse(args[0]);
                var day = GetDay(dayNumber, useExample);
                
                ulong solution;
                int partNumber = int.Parse(args[1]);
                switch (partNumber)
                {
                    case 1:
                        solution = day.Part1();
                        break;
                    case 2:
                        solution = day.Part2();
                        break;
                    default :
                        throw new ArgumentOutOfRangeException(nameof(args), "There are only 2 parts to each day.");
                }

                Console.WriteLine($"Day {dayNumber} part {partNumber}");
                Console.WriteLine($"Solution : {solution}");
            }
            else
            {
                Console.WriteLine("Missing arguments");
            }
        }

        static Day GetDay(int dayNumber, bool useExample)
        {
            var dayType = Type.GetType($"AdventOfCode2021.Days.Day{dayNumber}");

            if (dayType == null)
            {
                throw new ArgumentException($"Could not find day {dayNumber}", nameof(dayNumber));
            }
            else
            {
                var day = (Day)Activator.CreateInstance(dayType);
                day.UseExample = useExample;
                return day;
            }
        }
    }
}

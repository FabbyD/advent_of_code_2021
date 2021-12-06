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
                var day = GetDay(int.Parse(args[0]));
                
                switch (int.Parse(args[1]))
                {
                    case 1:
                        day.Part1();
                        break;
                    case 2:
                        day.Part2();
                        break;
                }
            }
            else
            {
                Console.WriteLine("Missing arguments");
            }
        }

        static IDay GetDay(int dayNumber)
        {
            switch (dayNumber)
            {
                case 1:
                    return new Day1();
                default:
                    throw new ArgumentException("Unknown day number", nameof(dayNumber));
            }
        }
    }
}

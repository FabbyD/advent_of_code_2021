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

        static Day GetDay(int dayNumber)
        {
            var dayType = Type.GetType($"AdventOfCode2021.Days.Day{dayNumber}");

            if (dayType == null)
            {
                throw new ArgumentException($"Could not find day {dayNumber}", nameof(dayNumber));
            }
            else
            {
                return (Day)Activator.CreateInstance(dayType);
            }
        }
    }
}

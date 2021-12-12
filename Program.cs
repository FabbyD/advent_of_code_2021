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
                var day = GetDay(int.Parse(args[0]), useExample);
                
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

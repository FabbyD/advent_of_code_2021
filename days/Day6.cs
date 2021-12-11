using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace AdventOfCode2021.Days
{
    public class Day6 : Day
    {
        class School
        {
            private List<long> fishes;

            private const int TimeToReproduce = 7;
            private const int Delay = 2;

            public long Count => fishes.Sum();

            public School()
            {
                fishes = new List<long>(TimeToReproduce+Delay);
                for (int i = 0; i < TimeToReproduce+Delay; i++)
                {
                    fishes.Add(0);
                }
            }

            public void AddFish(int timer)
            {
                fishes[timer]++;
            }

            public void Day()
            {
                // keep track of new fishes but only add them at the end of the day
                var newFishCount = fishes[0];

                for (int i = 0; i < TimeToReproduce + Delay - 1; i++)
                {
                    fishes[i] = fishes[i+1];
                }
                
                fishes[TimeToReproduce+Delay-1] = newFishCount;
                fishes[TimeToReproduce-1] += newFishCount;
            }
        }

        public override void Part1()
        {
            Simulate(80);
        }

        public override void Part2()
        {
            Simulate(256);
        }

        private void Simulate(int dayCount)
        {
            var school = new School();
            var timers = File.ReadAllLines(inputPath)[0].Split(",");
            foreach (var timer in timers)
            {
                school.AddFish(int.Parse(timer));
            }

            for (int i = 0; i < dayCount; i++)
            {
                school.Day();
            }

            Console.WriteLine("Solution : {0}", school.Count);
        }
    }
}

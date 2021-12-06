using System;
using System.IO;
using System.Linq;

namespace AdventOfCode2021.Days
{
    public class Day2 : IDay
    {
        private const string inputPath = "inputs/day2.txt";

        public void Part1()
        {
            var finalState = File.ReadAllLines(inputPath)
                .Aggregate(new State(), (state, line) => Process(line, state));

            Console.WriteLine("Solution: {0}", finalState.depth * finalState.horizontal);
        }

        public void Part2()
        {

        }

        private State Process(string line, State state)
        {
            var tokens = line.Split(' ');
            var command = tokens[0];
            var distance = int.Parse(tokens[1]);

            switch(command)
            {
                case "forward":
                    state.horizontal += distance;
                    break;
                case "down":
                    state.depth += distance;
                    break;
                case "up":
                    state.depth -= distance;
                    break;
                default:
                    break;
            }

            return state;
        }

        private struct State
        {
            public int horizontal;
            public int depth;
        }
    }
}
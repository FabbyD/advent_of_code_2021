using System;
using System.IO;
using System.Linq;

namespace AdventOfCode2021.Days
{
    public class Day2 : Day
    {
        public override ulong Part1()
        {
            var finalState = File.ReadAllLines(inputPath)
                .Aggregate(new State(), (state, line) => Process(Parse(line), ref state));

            return (ulong) (finalState.depth * finalState.horizontal);
        }

        public override ulong Part2()
        {
            var finalState = File.ReadAllLines(inputPath)
                .Aggregate(new State(), (state, line) => Process2(Parse(line), ref state));

            return (ulong) (finalState.depth * finalState.horizontal);
        }

        private Command Parse(string line)
        {
            var tokens = line.Split(' ');
            var command = tokens[0];
            var argument = int.Parse(tokens[1]);
            return new Command{ Name = command, Argument = argument};
        }

        private State Process(Command command, ref State state)
        {
            switch(command.Name)
            {
                case "forward":
                    state.horizontal += command.Argument;
                    break;
                case "down":
                    state.depth += command.Argument;
                    break;
                case "up":
                    state.depth -= command.Argument;
                    break;
                default:
                    break;
            }

            return state;
        }

        private State Process2(Command command, ref State state)
        {
            switch(command.Name)
            {
                case "forward":
                    state.horizontal += command.Argument;
                    state.depth += state.aim * command.Argument;
                    break;
                case "down":
                    state.aim += command.Argument;
                    break;
                case "up":
                    state.aim -= command.Argument;
                    break;
                default:
                    break;
            }

            return state;
        }

        private struct Command 
        {
            public string Name;
            public int Argument;

        }

        private struct State
        {
            public int horizontal;
            public int depth;
            public int aim;
        }
    }
}
using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode2021.Days
{
    public class Day10 : Day
    {
        Dictionary<char, int> scoresPart1 = new Dictionary<char, int>
            { { ')', 3}, { ']', 57}, { '}', 1197}, { '>', 25137}};

        Dictionary<char, uint> scoresPart2 = new Dictionary<char, uint>
            { { '(', 1}, { '[', 2}, { '{', 3}, { '<', 4}};

        public override ulong Part1()
        {
            Stack<char> stack = new Stack<char>();
            int sum = 0;
            foreach (var line in File.ReadLines(inputPath))
            {
                stack.Clear();
                int score = 0;
                foreach (var c in line)
                {
                    switch (c)
                    {
                        case '(' :
                        case '[' :
                        case '{' :
                        case '<' :
                            stack.Push(c);
                            break;
                        case ')' :
                            var chunkStart = stack.Pop();
                            if (chunkStart != '(')
                            {
                                score = scoresPart1[c];
                            }
                            break;
                        case ']' :
                            chunkStart = stack.Pop();
                            if (chunkStart != '[')
                            {
                                score = scoresPart1[c];
                            }
                            break;
                        case '}' :
                            chunkStart = stack.Pop();
                            if (chunkStart != '{')
                            {
                                score = scoresPart1[c];
                            }
                            break;
                        case '>' :
                            chunkStart = stack.Pop();
                            if (chunkStart != '<')
                            {
                                score = scoresPart1[c];
                            }
                            break;
                        default:
                            break;
                    }
                    if (score > 0)
                    {
                        break;
                    }
                }

                sum += score;
            }

            return (ulong)sum;
        }

        public override ulong Part2()
        {
            var stack = new Stack<char>();
            var scores = new List<ulong>();
            foreach (var line in File.ReadLines(inputPath))
            {
                stack.Clear();
                bool isCorrupted = false;
                foreach (var c in line)
                {
                    switch (c)
                    {
                        case '(' :
                        case '[' :
                        case '{' :
                        case '<' :
                            stack.Push(c);
                            break;
                        case ')' :
                            var chunkStart = stack.Pop();
                            isCorrupted = chunkStart != '(';
                            break;
                        case ']' :
                            chunkStart = stack.Pop();
                            isCorrupted = chunkStart != '[';
                            break;
                        case '}' :
                            chunkStart = stack.Pop();
                            isCorrupted = chunkStart != '{';
                            break;
                        case '>' :
                            chunkStart = stack.Pop();
                            isCorrupted = chunkStart != '<';
                            break;
                        default:
                            break;
                    }

                    if (isCorrupted)
                    {
                        break;
                    }
                }

                if (!isCorrupted && stack.Count != 0)
                {
                    // incomplete line
                    var score = CompleteLine(stack);
                    scores.Add(score);
                }
            }

            scores.Sort();
            int middle = scores.Count/2;

            return scores[middle];
        }

        private ulong CompleteLine(Stack<char> stack)
        {
            ulong score = 0;
            while (stack.Count > 0)
            {
                var c = stack.Pop();
                score = score*5 + scoresPart2[c];
            }

            return score;
        }
    }
}

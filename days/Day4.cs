using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2021.Days
{
    public class Day4 : IDay
    {
        private class Board
        {
            int[,] grid = new int[5,5];
            bool[,] marks = new bool[5,5];

            public int Score => ComputeScore();

            public void SetRow(int row, int[] numbers)
            {
                int rowSize = grid.GetLength(0);
                if (numbers.Length != rowSize)
                {
                    throw new InvalidOperationException($"Row size is {numbers.Length} but it should be {rowSize}");
                }

                for (int i = 0; i < numbers.Length; i++)
                {
                    grid[row,i] = numbers[i];
                }
            }

            public bool Mark(int number)
            {
                int rowSize = grid.GetLength(0);
                int columnSize = grid.GetLength(1);
                for (int row = 0; row < rowSize; row++)
                {
                    for (int column = 0; column < columnSize; column++)
                    {
                        if (grid[row,column] == number)
                        {
                            marks[row,column] = true;
                            if (IsRowOrColumnComplete(row, column))
                            {
                                return true;
                            }
                        }
                    }
                }

                return false;
            }

            private bool IsRowOrColumnComplete(int row, int column)
            {
                return IsRowComplete(row) || IsColumnComplete(column);
            }

            private bool IsRowComplete(int row)
            {
                bool isComplete = true;
                int columnSize = grid.GetLength(1);
                for (int i = 0; i < columnSize; i++)
                {
                    isComplete &= marks[row,i];
                    if (!isComplete)
                    {
                        break;
                    }
                }

                return isComplete;
            }

            private bool IsColumnComplete(int column)
            {
                bool isComplete = true;
                int rowSize = grid.GetLength(0);
                for (int i = 0; i < rowSize; i++)
                {
                    isComplete &= marks[i,column];
                    if (!isComplete)
                    {
                        break;
                    }
                }

                return isComplete;
            }

            private int ComputeScore()
            {
                int score = 0;
                for (int row = 0; row < grid.GetLength(0); row++)
                {
                    for (int column = 0; column < grid.GetLength(1); column++)
                    {
                        if (!marks[row,column])
                        {
                            score += grid[row,column];
                        }
                    }
                }

                return score;
            }
        }

        private class Bingo
        {
            private int[] draws;
            private int drawIndex;
            private List<Board> boards = new List<Board>();

            public int LastDraw => draws[drawIndex-1];
            public Board Winner { get; private set; }

            public Bingo(string inputPath)
            {
                Board currentBoard = null;
                int currentRow = 0;
                foreach (var line in File.ReadLines(inputPath))
                {
                    if (draws == null)
                    {
                        // parse draws on the first line
                        draws = line.Split(',')
                            .Select(s => int.Parse(s))
                            .ToArray();
                    }
                    else if (string.IsNullOrWhiteSpace(line))
                    {
                        // empty line delimits each board
                        if (currentBoard != null)
                        {
                            boards.Add(currentBoard);
                        }

                        currentBoard = new Board();
                        currentRow = 0;
                    }
                    else 
                    {
                        // currently parsing a board
                        int[] numbers = line.Split(' ')
                            .Where(s => !string.IsNullOrWhiteSpace(s))
                            .Select(s => int.Parse(s))
                            .ToArray();
                        currentBoard.SetRow(currentRow, numbers);
                        currentRow++;
                    }
                } 

                // add the last board
                boards.Add(currentBoard);
            }

            public void Draw()
            {
                int draw = draws[drawIndex++];

                var winningBoards = new List<Board>();
                foreach (var board in boards)
                {
                    if (board.Mark(draw))
                    {
                        Winner = board;
                        winningBoards.Add(board);
                    }
                }

                foreach (var board in winningBoards)
                {
                    boards.Remove(board);
                }
            }

            public bool IsDone()
            {
                return drawIndex >= draws.Length || Winner != null;
            }

            public bool IsDone2()
            {
                return drawIndex >= draws.Length || boards.Count == 0;
            }
        }

        private readonly string inputPath = "inputs/day4.txt";

        public void Part1()
        {
            var bingo = new Bingo(inputPath);

            while (!bingo.IsDone())
            {
                bingo.Draw();
            }

            var winner = bingo.Winner;
            var boardScore = winner.Score;
            var lastDraw = bingo.LastDraw;

            Console.WriteLine("Solution: {0}", boardScore * lastDraw);
        }

        public void Part2()
        {
            var bingo = new Bingo(inputPath);

            while (!bingo.IsDone2())
            {
                bingo.Draw();
            }

            var winner = bingo.Winner;
            var boardScore = winner.Score;
            var lastDraw = bingo.LastDraw;

            Console.WriteLine("Solution: {0}", boardScore * lastDraw);
        }
    }
}
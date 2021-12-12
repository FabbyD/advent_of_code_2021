using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;

namespace AdventOfCode2021.Days
{
    public class Day5 : Day
    {
        private struct Vent
        {
            public struct Point : IEquatable<Vent.Point>
            {
                public int x;
                public int y;

                public bool Equals([AllowNull] Point other)
                {
                    return x == other.x && y == other.y;
                }
            }

            public Point Start;
            public Point End;

            public Point GetNextPoint(Point current) 
            {
                Point nextPoint = new Point();
                if (Start.x == End.x)
                {
                    nextPoint.x = current.x;
                }
                else if (Start.x < End.x)
                {
                    nextPoint.x = current.x + 1;
                }
                else
                {
                    nextPoint.x = current.x - 1;
                }

                if (Start.y == End.y)
                {
                    nextPoint.y = current.y;
                }
                else if (Start.y < End.y)
                {
                    nextPoint.y = current.y + 1;
                }
                else
                {
                    nextPoint.y = current.y - 1;
                }

                return nextPoint;
            }

            public override string ToString()
            {
                return $"{Start.x},{Start.y} -> {End.x},{End.y}";
            }
        }

        private class Environment
        {
            private List<List<int>> points = new List<List<int>>();

            public void ApplyVent(Vent vent, bool allowDiagonal)
            {
                if (vent.Start.x == vent.End.x)
                {
                    ApplyVertical(vent);
                }
                else if (vent.Start.y == vent.End.y)
                {
                    ApplyHorizontal(vent);
                }
                else if (allowDiagonal)
                {
                    ApplyDiagonal(vent);
                }
            }

            public int CountDangerZones()
            {
                int count = 0;
                for (int x = 0; x < points.Count; x++)
                {
                    var row = points[x];
                    for (int y = 0; y < row.Count; y++)
                    {
                        if (row[y] >= 2)
                        {
                            count++;
                        }
                    }
                }

                return count;
            }

            private void Ensure(int x, int y)
            {
                for (int i = 0; i <= x; i++)
                {
                    if (points.Count <= i)
                    {
                        points.Add(new List<int>(y+1));
                    }

                    EnsureRow(i, y);
                }
            }

            private void EnsureRow(int x, int y)
            {
                var row = points[x];

                for (int j = row.Count; j <= y; j++)
                {
                    row.Add(0);
                }
            }

            private void ApplyHorizontal(Vent vent)
            {
                var y = vent.Start.y;
                var left = Math.Min(vent.Start.x, vent.End.x);
                var right = Math.Max(vent.Start.x, vent.End.x); 
                Ensure(right, y);
                for (int x = left; x <= right; x++)
                {
                    points[x][y]++;
                }
            }

            private void ApplyVertical(Vent vent)
            {
                var x = vent.Start.x;
                var up = Math.Min(vent.Start.y, vent.End.y);
                var down = Math.Max(vent.Start.y, vent.End.y);
                Ensure(x, down);
                for (int y = up; y <= down; y++)
                {
                    points[x][y]++;
                }
            }

            private void ApplyDiagonal(Vent vent)
            {
                var left = Math.Min(vent.Start.x, vent.End.x);
                var right = Math.Max(vent.Start.x, vent.End.x);
                var up = Math.Min(vent.Start.y, vent.End.y);
                var down = Math.Max(vent.Start.y, vent.End.y);
                Ensure(right, down);

                var currentPoint = vent.Start;
                var endPoint = vent.End;
                while (!currentPoint.Equals(endPoint))
                {
                    points[currentPoint.x][currentPoint.y]++;
                    currentPoint = vent.GetNextPoint(currentPoint);
                }

                // add end point
                points[currentPoint.x][currentPoint.y]++;
            }

        }

        public override ulong Part1()
        {
            var environment = new Environment();
            foreach (var line in File.ReadLines(inputPath))
            {
                var vent = ParseVent(line);
                environment.ApplyVent(vent, false);
            }

            return (ulong) environment.CountDangerZones();
        }

        public override ulong Part2()
        {
            var environment = new Environment();
            foreach (var line in File.ReadLines(inputPath))
            {
                var vent = ParseVent(line);
                environment.ApplyVent(vent, true);
            }

            return (ulong) environment.CountDangerZones();
        }

        private Vent ParseVent(string line)
        {
            var points = line.Split(" -> ")
                .Select(ParsePoint)
                .ToArray();
            
            return new Vent
            {
                Start = points[0],
                End = points[1]
            };
        }
        
        private Vent.Point ParsePoint(string s)
        {
            var coords = s.Split(",");
            return new Vent.Point {
                x = int.Parse(coords[0]),
                y = int.Parse(coords[1])
            };
        }
    }
}

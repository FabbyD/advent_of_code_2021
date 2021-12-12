using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2021.Days
{
    public class Day9 : Day
    {
        class Basin
        {
            private List<(int I, int J)> points = new List<(int I, int J)>();

            public void Add(int i, int j)
            {
                points.Add((i,j));
            }

            public int Size => points.Count;
        }

        class HeightMap
        {
            List<uint[]> map = new List<uint[]>();
            List<bool[]> visited = new List<bool[]>();

            public void AddRow(uint[] row)
            {
                map.Add(row);
                visited.Add(new bool[row.Length]);
            }

            public uint GetRiskLevel()
            {
                uint sum = 0;
                for (int i = 0; i < map.Count; i++)
                {
                    var row = map[i];
                    for (int j = 0; j < row.Length; j++)
                    {
                        if (IsLowPoint(i,j))
                        {
                            sum += GetRiskLevel(i,j);
                        }
                    }
                }

                return sum;
            }

            public List<Basin> GetBasins()
            {
                List<Basin> basins = new List<Basin>();
                for (int i = 0; i < map.Count; i++)
                {
                    var row = map[i];
                    for (int j = 0; j < row.Length; j++)
                    {
                        if (IsLowPoint(i,j))
                        {
                            basins.Add(GetBasin(i,j));
                        }
                    }
                }

                return basins;
            }

            private bool IsLowPoint(int i, int j)
            {
                uint height = map[i][j];

                // up
                if (i > 0)
                {
                    if (map[i-1][j] <= height)
                    {
                        return false;
                    }
                }

                // down
                if (i < map.Count-1)
                {
                    if (map[i+1][j] <= height)
                    {
                        return false;
                    }
                }

                // left
                if (j > 0)
                {
                    if (map[i][j-1] <= height)
                    {
                        return false;
                    }
                }

                // right
                if (j < map[i].Length-1)
                {
                    if (map[i][j+1] <= height)
                    {
                        return false;
                    }
                }

                return true;
            }

            private uint GetRiskLevel(int i, int j)
            {
                return map[i][j] + 1;
            }

            private Basin GetBasin(int i, int j)
            {
                var basin = new Basin();
                TraverseBasin(basin, i, j);
                return basin;
            }

            private void TraverseBasin(Basin basin, int i, int j)
            {
                if (visited[i][j] || map[i][j] == 9)
                {
                    return;
                }

                basin.Add(i, j);
                visited[i][j] = true;
                uint height = map[i][j];

                // up
                if (i > 0)
                {
                    if (map[i-1][j] > height)
                    {
                        TraverseBasin(basin, i-1, j);
                    }
                }

                // down
                if (i < map.Count-1)
                {
                    if (map[i+1][j] > height)
                    {
                        TraverseBasin(basin, i+1, j);
                    }
                }

                // left
                if (j > 0)
                {
                    if (map[i][j-1] > height)
                    {
                        TraverseBasin(basin, i, j-1);
                    }
                }

                // right
                if (j < map[i].Length-1)
                {
                    if (map[i][j+1] > height)
                    {
                        TraverseBasin(basin, i, j+1);
                    }
                }
            }
        }

        public override ulong Part1()
        {
            var heightMap = new HeightMap();
            foreach (var line in File.ReadLines(inputPath))
            {
                heightMap.AddRow(line.Select(c => uint.Parse(new []{c})).ToArray());
            }

            return (ulong) heightMap.GetRiskLevel();
        }

        public override ulong Part2()
        {
            var heightMap = new HeightMap();
            foreach (var line in File.ReadLines(inputPath))
            {
                heightMap.AddRow(line.Select(c => uint.Parse(new []{c})).ToArray());
            }

            var basins = heightMap.GetBasins();
            basins.Sort((a, b) => b.Size.CompareTo(a.Size));
            
            return (ulong) (basins[0].Size * basins[1].Size * basins[2].Size);
        }
    }
}

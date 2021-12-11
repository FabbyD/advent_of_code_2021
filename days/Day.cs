namespace AdventOfCode2021.Days
{
    public abstract class Day
    {
        protected virtual string inputPath => $"inputs/{GetType().Name.ToLower()}.txt";

        public abstract void Part1();
        public abstract void Part2();
    }
}
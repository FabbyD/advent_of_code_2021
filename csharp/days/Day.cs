namespace AdventOfCode2021.Days
{
    public abstract class Day
    {
        public bool UseExample { get; set;}

        protected virtual string inputPath => $"../inputs/{GetType().Name.ToLower()}{(UseExample ? "_example" : string.Empty)}.txt";

        public abstract ulong Part1();
        public abstract ulong Part2();
    }
}
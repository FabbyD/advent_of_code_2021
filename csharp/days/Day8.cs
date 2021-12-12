using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2021.Days
{
    public class Day8 : Day
    {
        readonly struct Digit
        {
            public static readonly Digit Zero = new Digit(a : true, b : true, c : true, e : true, f : true, g : true);
            public static readonly Digit One = new Digit(c : true, f : true);
            public static readonly Digit Two = new Digit(a : true, c : true, d : true, e : true, f : true);
            public static readonly Digit Three = new Digit(a : true, c : true, d : true, f : true, g : true);
            public static readonly Digit Four = new Digit(b : true, c : true, d : true, f : true);
            public static readonly Digit Five = new Digit(a : true, b : true, d : true, f : true, g : true);
            public static readonly Digit Six = new Digit(a : true, b : true, d : true, e : true, f : true, g : true);
            public static readonly Digit Seven = new Digit(a : true, c : true, f : true);
            public static readonly Digit Eight = new Digit(a : true, b : true, c : true, d : true, e : true, f : true, g : true);
            public static readonly Digit Nine = new Digit(a : true, b : true, c : true, d : true, f : true, g : true);

            private readonly bool[] segments;

            public Digit(bool a = false, bool b = false, bool c = false, bool d = false, bool e = false, bool f = false, bool g = false)
            {
                segments = new bool[7];
                segments[0] = a;
                segments[1] = b;
                segments[2] = c;
                segments[3] = d;
                segments[4] = e;
                segments[5] = f;
                segments[6] = g;
            }
        }

        class Signal
        {
            private readonly string signal;

            public Signal(string signal)
            {
                char[] characters = signal.ToArray();
                Array.Sort(characters);
                this.signal = new String(characters);
            }

            public int WireCount => signal.Length;

            public static Signal operator -(Signal a, Signal b)
            {
                var newSignal = new String(a.signal.Except(b.signal).ToArray());
                return new Signal(newSignal);
            }

            internal bool Includes(Signal other)
            {
                var intersection = signal.Intersect(other.signal);
                return intersection.Count() == other.signal.Length;
            }

            public override int GetHashCode()
            {
                return signal.GetHashCode();
            }

            public override bool Equals(object obj)
            {
                return Equals(obj as Signal);
            }

            public bool Equals(Signal other)
            {
                return signal == other.signal;
            }
        }
        
        class Decoding
        {
            private readonly Signal[] signals;

            public Decoding(Signal[] signals)
            {
                this.signals = signals;
            }

            private int Decode(Signal signal) => Array.IndexOf(signals, signal);

            public int Decode(IEnumerable<Signal> signals)
                => signals
                    .Select((signal, i) => (int)Math.Pow(10,3-i) * Decode(signal))
                    .Sum();
        }

        static class SegmentCounts
        {
            public const int One = 2;
            public const int Four = 4;
            public const int Seven = 3;
            public const int Eight = 7;
            public const int TwoThreeFive = 5;
            public const int ZeroSixNine = 6;
        }

        public override ulong Part1()
        {
            int count = 0;
            foreach (var line in File.ReadLines(inputPath))
            {
                var split = line.Split(" | ");
                var digits = split[1].Split(" ");
                foreach (var digit in digits)
                {
                    switch (digit.Length)
                    {
                        case SegmentCounts.One:
                        case SegmentCounts.Four:
                        case SegmentCounts.Seven:
                        case SegmentCounts.Eight:
                            count++;
                            break;
                    }
                }
            }

            return (ulong) count;
        }

        public override ulong Part2()
        {
            int sum = 0;
            foreach (var line in File.ReadLines(inputPath))
            {
                var split = line.Split(" | ");
                var signals = split[0]
                    .Split(" ")
                    .Select(signal => new Signal(signal));
                var decoding = FindDecoding(signals);
                
                var output = split[1]
                    .Split(" ")
                    .Select(o => new Signal(o));
                sum += decoding.Decode(output);
            }

            return (ulong) sum;
        }

        private Decoding FindDecoding(IEnumerable<Signal> signals)
        {
            var signalGroups = signals
                .GroupBy(signal => signal.WireCount)
                .ToDictionary(group => group.Key, group => group.ToList());

            var signal1 = signalGroups[SegmentCounts.One][0];
            var signal4 = signalGroups[SegmentCounts.Four][0];
            var signal7 = signalGroups[SegmentCounts.Seven][0];
            var signal8 = signalGroups[SegmentCounts.Eight][0];
            var signal3 = signalGroups[SegmentCounts.TwoThreeFive]
                .Where(signal => signal.Includes(signal1))
                .Single();
            signalGroups[SegmentCounts.TwoThreeFive].Remove(signal3);
            var signal6 = signalGroups[SegmentCounts.ZeroSixNine]
                .Where(signal => !signal.Includes(signal1))
                .Single();
            signalGroups[SegmentCounts.ZeroSixNine].Remove(signal6);
            var signal9 = signalGroups[SegmentCounts.ZeroSixNine]
                .Where(signal => signal.Includes(signal4))
                .Single();
            signalGroups[SegmentCounts.ZeroSixNine].Remove(signal9);
            var signal0 = signalGroups[SegmentCounts.ZeroSixNine].Single();
            var signalBd = signal4 - signal1;
            var signal5 = signalGroups[SegmentCounts.TwoThreeFive]
                .Where(signal => signal.Includes(signalBd))
                .Single();
            signalGroups[SegmentCounts.TwoThreeFive].Remove(signal5);
            var signal2 = signalGroups[SegmentCounts.TwoThreeFive].Single();

            Signal[] decoding = new Signal[10];
            decoding[0] = signal0;
            decoding[1] = signal1;
            decoding[2] = signal2;
            decoding[3] = signal3;
            decoding[4] = signal4;
            decoding[5] = signal5;
            decoding[6] = signal6;
            decoding[7] = signal7;
            decoding[8] = signal8;
            decoding[9] = signal9;

            return new Decoding(decoding);
        }
    }
}

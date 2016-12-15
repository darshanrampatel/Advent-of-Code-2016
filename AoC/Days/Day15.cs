using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AoC.Days
{
    class Day15
    {
        static string input = File.ReadAllText(AppContext.BaseDirectory + "/Inputs/Day15.txt");

        class Disc
        {
            public int ID { get; set; }
            public int Position { get; set; }
            public int PositionCount { get; set; }
            public int PositionAtTime { get; set; } = -1;
        }

        public string Part1()
        {
            var discs = new List<Disc>();
            var rawDiscs = input.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

            foreach (var rawDisc in rawDiscs)
            {
                var parts = rawDisc.Split(' ');
                var disc = new Disc()
                {
                    ID = Int32.Parse(parts[1].Replace("#", "")),
                    Position = Int32.Parse(parts[11].Replace(".", "")),
                    PositionCount = Int32.Parse(parts[3])
                };
                discs.Add(disc);
            }

            var time = 0;
            while (discs.Any(d => d.PositionAtTime != 0))
            {
                foreach (var disc in discs)
                {
                    disc.PositionAtTime = (disc.Position + (disc.ID - 1) + time) % disc.PositionCount;
                }
                time++;
            }

            return $"Starting time: {time - 2}";
        }

        public string Part2()
        {
            var discs = new List<Disc>();
            var rawDiscs = input.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

            foreach (var rawDisc in rawDiscs)
            {
                var parts = rawDisc.Split(' ');
                var disc = new Disc()
                {
                    ID = Int32.Parse(parts[1].Replace("#", "")),
                    Position = Int32.Parse(parts[11].Replace(".", "")),
                    PositionCount = Int32.Parse(parts[3])
                };
                discs.Add(disc);
            }

            discs.Add(new Disc() { ID = discs.Count + 1, Position= 0, PositionCount = 11 });

            var time = 0;
            while (discs.Any(d => d.PositionAtTime != 0))
            {
                foreach (var disc in discs)
                {
                    disc.PositionAtTime = (disc.Position + (disc.ID - 1) + time) % disc.PositionCount;
                }
                time++;
            }

            return $"Starting time: {time - 2}";
        }
    }
}

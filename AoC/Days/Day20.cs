using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AoC.Days
{
    class Day20
    {
        static string input = File.ReadAllText(AppContext.BaseDirectory + "/Inputs/Day20.txt");

        public string Part1()
        {
            var blacklist = input.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            var sortedBlacklist = new List<Tuple<uint, uint>>();
            foreach (var block in blacklist)
            {
                var parts = block.Split('-');
                var lowerBound = UInt32.Parse(parts[0]);
                var upperBound = UInt32.Parse(parts[1]);
                sortedBlacklist.Add(new Tuple<uint, uint>(lowerBound, upperBound));
            }

            sortedBlacklist = sortedBlacklist.OrderBy(e => e.Item1).ThenBy(e => e.Item2).ToList();
            var maxValue = UInt32.MaxValue;
            for (uint i = 0; i <= maxValue; i++)
            {
                bool allowed = true;
                foreach (var block in sortedBlacklist)
                {
                    if (i >= block.Item1 && i <= block.Item2)
                    {
                        allowed = false;
                        break;
                    }
                }
                if (allowed)
                {
                    return $"Lowest IP: {i}";
                }
            }

            return "Unknown";
        }

        public string Part2()
        {
            return $"Allowed IPs: {0}";
        }
    }
}

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
            var sortedBlacklist = new List<Tuple<long, long>>();
            foreach (var block in blacklist)
            {
                var parts = block.Split('-');
                var lowerBound = long.Parse(parts[0]);
                var upperBound = long.Parse(parts[1]);
                sortedBlacklist.Add(new Tuple<long, long>(lowerBound, upperBound));
            }

            sortedBlacklist = sortedBlacklist.OrderBy(e => e.Item1).ThenBy(e => e.Item2).ToList();
            var maxValue = UInt32.MaxValue;
            for (long i = 0; i <= maxValue; i++)
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
            var blacklist = input.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            var sortedBlacklist = new List<Tuple<long, long>>();
            foreach (var block in blacklist)
            {
                var parts = block.Split('-');
                var lowerBound = long.Parse(parts[0]);
                var upperBound = long.Parse(parts[1]);
                sortedBlacklist.Add(new Tuple<long, long>(lowerBound, upperBound));
            }
            var validIPAddresses = 0;
            sortedBlacklist = sortedBlacklist.OrderBy(e => e.Item1).ThenBy(e => e.Item2).ToList();
            var maxValue = UInt32.MaxValue;
            long i = 0;
            var currentIp = sortedBlacklist.FirstOrDefault(block => i >= block.Item1 && i <= block.Item2);

            while (i <= maxValue)
            {
                while (currentIp != null)
                {
                    i = currentIp.Item2 + 1;
                    currentIp = sortedBlacklist.FirstOrDefault(block => i >= block.Item1 && i <= block.Item2);
                }

                if (i <= maxValue)
                {
                    validIPAddresses++;
                    i++;
                    currentIp = sortedBlacklist.FirstOrDefault(block => i >= block.Item1 && i <= block.Item2);
                }
            }

            return $"Allowed IPs: {validIPAddresses}";
        }
    }
}

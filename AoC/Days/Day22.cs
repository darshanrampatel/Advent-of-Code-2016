using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace AoC.Days
{
    class Day22
    {
        static string input = File.ReadAllText(AppContext.BaseDirectory + "/Inputs/Day22.txt");

        class Node
        {
            public int x { get; set; }
            public int y { get; set; }
            public int Size { get; set; }
            public int Used { get; set; }
            public int Avail { get; set; }
        }

        public string Part1()
        {
            var df = input.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            int viablePairs = 0;
            var nodes = new List<Node>();
            foreach (var line in df)
            {
                if (line[0].Equals('/'))
                {
                    ///dev/grid/node-x33-y21  506T  491T    15T   97%
                    var parts = line.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                    var s = Int32.Parse(parts[1].Remove(parts[1].Length - 1));
                    var u = Int32.Parse(parts[2].Remove(parts[2].Length - 1));
                    var a = Int32.Parse(parts[3].Remove(parts[3].Length - 1));
                    var disk = parts[0].Split('-');
                    var x = Int32.Parse(disk[1].Substring(1));
                    var y = Int32.Parse(disk[2].Substring(1));

                    var node = new Node()
                    {
                        x = x,
                        y = y,
                        Size = s,
                        Used = u,
                        Avail = a
                    };
                    nodes.Add(node);
                }

            }

            foreach (var node1 in nodes)
            {
                foreach (var node2 in nodes)
                {
                    if (IsViablePair(node1, node2))
                    {
                        viablePairs++;
                    }
                }
            }

            return $"Viable Pairs: {viablePairs}";
        }

        private bool IsViablePair(Node node1, Node node2)
        {
            if (!node1.Equals(node2))
            {
                if (node1.Used != 0)
                {
                    if (node1.Used <= node2.Avail)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public string Part2()
        {
            var answer = "Unknown";
            return answer;
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AoC.Days
{
    class Day18
    {
        static string input = File.ReadAllText(AppContext.BaseDirectory + "/Inputs/Day18.txt");

        string GetNextRow(string row)
        {
            var newRow = string.Empty;
            for (int i = 0; i < row.Length; i++)
            {
                bool isTrap = false;
                bool leftTileTrap;
                bool centerTileTrap;
                bool rightTileTrap;

                if (i == 0)
                {
                    leftTileTrap = false;
                }
                else
                {
                    leftTileTrap = IsTrap(row[i - 1]);
                }
                if (i == row.Length - 1)
                {
                    rightTileTrap = false;
                }
                else
                {
                    rightTileTrap = IsTrap(row[i + 1]);
                }
                centerTileTrap = IsTrap(row[i]);

                if (leftTileTrap && centerTileTrap && !rightTileTrap)
                {
                    isTrap = true;
                }

                if (centerTileTrap && rightTileTrap && !leftTileTrap)
                {
                    isTrap = true;
                }

                if (leftTileTrap && !centerTileTrap && !rightTileTrap)
                {
                    isTrap = true;
                }

                if (rightTileTrap && !leftTileTrap && !centerTileTrap)
                {
                    isTrap = true;
                }

                newRow += isTrap ? '^': '.';
            }
            return newRow;
        }

        bool IsTrap(char tile)
        {
            if (tile.Equals('^'))
            {
                return true;
            }
            return false;
        }

        public string Part1()
        {
            int rows = 40;

            var map = new List<string>();
            map.Add(input);

            for (int i = 0; i < rows - 1; i++)
            {
                var row = map.LastOrDefault();
                var nextRow = GetNextRow(row);
                map.Add(nextRow);
            }

            int safeTiles = 0;
            foreach (var row in map)
            {
                safeTiles += row.Count(c => c.Equals('.'));
            }

            return $"Safe tiles: {safeTiles}";
        }



        public string Part2()
        {
            int rows = 400000;

            var map = new List<string>();
            map.Add(input);

            for (int i = 0; i < rows - 1; i++)
            {
                var row = map.LastOrDefault();
                var nextRow = GetNextRow(row);
                map.Add(nextRow);
            }

            int safeTiles = 0;
            foreach (var row in map)
            {
                safeTiles += row.Count(c => c.Equals('.'));
            }

            return $"Safe tiles: {safeTiles}";
        }
    }
}

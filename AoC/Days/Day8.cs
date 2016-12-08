using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AoC.Days
{
    class Day8
    {
        static string input = File.ReadAllText(AppContext.BaseDirectory + "/Inputs/Day8.txt");

        List<List<bool>> grid = new List<List<bool>>();

        public string Part1()
        {
            for (int i = 0; i < 6; i++)
            {
                var col = new List<bool>(new bool[50]);
                grid.Add(col);
            }
            DrawGrid();
            var operations = input.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            foreach (var operation in operations)
            {
                if (operation.StartsWith("rect"))
                {
                    var parts = operation.Split('x');
                    var width = Int32.Parse(parts[0].Replace("rect ", ""));
                    var height = Int32.Parse(parts[1].Substring(0));
                    DrawRect(width, height);
                }
                else if (operation.Contains("row"))
                {
                    var parts = operation.Split('=')[1].Split(new[] { " by " }, StringSplitOptions.None);
                    var row = Int32.Parse(parts[0]);
                    var pixels = Int32.Parse(parts[1]);
                    RotateRow(row, pixels);
                }
                else
                {
                    var parts = operation.Split('=')[1].Split(new[] { " by " }, StringSplitOptions.None);
                    var col = Int32.Parse(parts[0]);
                    var pixels = Int32.Parse(parts[1]);
                    RotateColumn(col, pixels);
                }
                //DrawGrid();
            }
            DrawGrid();
            return "Lit pixels: " + LitPixels();
        }

        void DrawRect(int width, int height)
        {
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    grid[i][j] = true;
                }
            }
        }

        int LitPixels()
        {
            var count = 0;
            foreach (var row in grid)
            {
                count += row.Where(x => x).Count();
            }
            return count;
        }

        void RotateColumn(int col, int pixels)
        {
            var oldCol = new List<bool>();

            for (int i = 0; i < grid.Count; i++)
            {
                oldCol.Add(grid[i][col]);
            }

            for (int i = 0; i < pixels; i++)
            {
                bool item = oldCol[oldCol.Count - 1];
                oldCol.RemoveAt(oldCol.Count - 1);
                oldCol.Insert(0, item);
            }

            for (int i = 0; i < oldCol.Count; i++)
            {
                grid[i][col] = oldCol[i];
            }
        }

        void RotateRow(int row, int pixels)
        {
            var oldRow = grid[row];
            for (int i = 0; i < pixels; i++)
            {
                bool item = oldRow[oldRow.Count - 1];
                oldRow.RemoveAt(oldRow.Count - 1);
                oldRow.Insert(0, item);
            }
            grid[row] = oldRow;
        }

        void DrawGrid()
        {
            foreach (var row in grid)
            {
                System.Diagnostics.Debug.Write(String.Join("", row.Select(x => x ? "#" : ".")));
                System.Diagnostics.Debug.Write(Environment.NewLine);
            }
            System.Diagnostics.Debug.Write(Environment.NewLine);
        }

        public string Part2()
        {
            Part1();
            var answer = string.Empty;
            foreach (var row in grid)
            {
                answer += String.Join("", row.Select(x => x ? "#" : "."));
                answer += Environment.NewLine;
            }
            return answer;
        }
    }
}

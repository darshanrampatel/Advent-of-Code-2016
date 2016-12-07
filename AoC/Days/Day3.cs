using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC.Days
{
    public class Day3
    {
        static string input = File.ReadAllText(AppContext.BaseDirectory + "/Inputs/Day3.txt");

        public string Part1()
        {
            var triangles = input.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            int possibleTriangles = 0;
            foreach (string rawTriangle in triangles)
            {
                string triangle = rawTriangle.Trim();
                var sides = triangle.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                if (IsValidTriangle(Int32.Parse(sides[0]), Int32.Parse(sides[1]), Int32.Parse(sides[2])))
                {
                    possibleTriangles++;
                }
            }
            return "Possible Triangles: " + possibleTriangles + " out of " + triangles.Length + " triangles";
        }

        private bool IsValidTriangle(int x, int y, int z)
        {
            return x + y > z && x + z > y && y + z > x;
        }

        public string Part2()
        {
            var triangles = input.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            int possibleTriangles = 0;
            var x = new List<int>();
            var y = new List<int>();
            var z = new List<int>();
            foreach (string rawTriangle in triangles)
            {
                string triangle = rawTriangle.Trim();
                var sides = triangle.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                x.Add(Int32.Parse(sides[0]));
                y.Add(Int32.Parse(sides[1]));
                z.Add(Int32.Parse(sides[2]));
            }
            for (int i = 0; i < x.Count; i = i + 3)
            {
                if (IsValidTriangle(x[i], x[i+1], x[i+2]))
                {
                    possibleTriangles++;
                }
                if (IsValidTriangle(y[i], y[i + 1], y[i + 2]))
                {
                    possibleTriangles++;
                }
                if (IsValidTriangle(z[i], z[i + 1], z[i + 2]))
                {
                    possibleTriangles++;
                }
            }
            return "Possible Triangles: " + possibleTriangles + " out of " + x.Count.ToString() + " triangles";
        }
    }

}

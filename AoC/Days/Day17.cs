using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Windows.Foundation;

namespace AoC.Days
{
    class Day17
    {
        static string input = File.ReadAllText(AppContext.BaseDirectory + "/Inputs/Day17.txt");

        static Point startingPosition = new Point(0, 0);
        static Point goal = new Point(3, 3);
        static int cols = 4;
        static int rows = 4;

        enum State
        {
            Unknown,
            CurrentlyAt,
            Goal
        }

        class Node
        {
            public int X { get; set; }
            public int Y { get; set; }
            public string History { get; set; }
        }

        string BFS(Point startingPoint, Point goal, bool part1)
        {
            var graph = new Node[rows, cols];
            for (int x = 0; x < rows; x++)
            {
                for (int y = 0; y < cols; y++)
                {
                    graph[x, y] = new Node()
                    {
                        X = x,
                        Y = y,
                        History = string.Empty
                    };

                }
            }

            var q = new Queue<Node>();
            q.Enqueue(graph[(int)startingPoint.Y, (int)startingPoint.X]);

            List<string> possibleRoutes = new List<string>();
            int shortestRoute = Int32.MaxValue;

            while (q.Count() > 0)
            {
                var current = q.Dequeue();
                if (current.X == 3 && current.Y == 3)
                {
                    if (part1)
                    {
                        return current.History;
                    }
                    else
                    {
                        possibleRoutes.Add(current.History);
                    }

                }
                foreach (var adjNode in GetAdjacentNodes(current.Y, current.X, current.History))
                {
                    if (current.History.Length < shortestRoute)
                    {
                        adjNode.History = current.History + adjNode.History;
                        q.Enqueue(adjNode);
                    }
                }
            }

            if (!part1)
            {
                return possibleRoutes.OrderByDescending(r => r.Length).FirstOrDefault().Length.ToString();
            }

            return "Unknown";
        }

        public string CalculateMD5Hash(string input)
        {
            MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("x2"));
            }
            return sb.ToString().Substring(0, 4);
        }

        private List<Node> GetAdjacentNodes(int X, int Y, string history)
        {
            var adjacentNodes = new List<Node>();

            if (X == 3 && Y == 3)
            {
                return adjacentNodes;
            }

            var hash = input + history;
            var newHash = CalculateMD5Hash(hash);

            if (X + 1 < cols)
            {
                if (IsDoorOpen(newHash[3]))
                {
                    var newNode = new Node()
                    {
                        X = Y,
                        Y = X + 1,
                        History = "R"
                    };
                    adjacentNodes.Add(newNode);
                }
            }

            if (X - 1 >= 0)
            {
                if (IsDoorOpen(newHash[2]))
                {
                    var newNode = new Node()
                    {
                        X = Y,
                        Y = X - 1,
                        History = "L"
                    };
                    adjacentNodes.Add(newNode);
                }
            }

            if (Y + 1 < rows)
            {
                if (IsDoorOpen(newHash[1]))
                {
                    var newNode = new Node()
                    {
                        X = Y + 1,
                        Y = X,
                        History = "D"
                    };
                    adjacentNodes.Add(newNode);
                }
            }

            if (Y - 1 >= 0)
            {
                if (IsDoorOpen(newHash[0]))
                {
                    var newNode = new Node()
                    {
                        X = Y - 1,
                        Y = X,
                        History = "U"
                    };
                    adjacentNodes.Add(newNode);
                }
            }
            return adjacentNodes;
        }

        bool IsDoorOpen(char c)
        {
            if (c.Equals('b') || c.Equals('c') || c.Equals('d') || c.Equals('e') || c.Equals('f'))
            {
                return true;
            }
            return false;
        }

        public string Part1()
        {
            return "Shortest Path: " + BFS(startingPosition, goal, true);
        }

        public string Part2()
        {
            return "Longest Path: " + BFS(startingPosition, goal, false);
        }
    }
}

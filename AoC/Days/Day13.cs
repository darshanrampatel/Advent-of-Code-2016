using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;

namespace AoC.Days
{
    class Day13
    {
        static string input = File.ReadAllText(AppContext.BaseDirectory + "/Inputs/Day13.txt");
        static int officeDesignersFavouriteNumber = Int32.Parse(input);
        static Point startingPosition = new Point(1, 1);
        State[,] grid = new State[rows, cols];
        static int cols = 51;
        static int rows = 51;
        enum State
        {
            Unknown,
            Wall,
            OpenSpace,
            CurrentlyAt,
            Goal
        }


        public string Part1()
        {
            var goal = new Point(31, 39);

            for (int x = 0; x < rows; x++)
            {
                for (int y = 0; y < cols; y++)
                {
                    grid[x, y] = IsOpenSpace(new Point(y, x));
                }
            }

            grid[(int)startingPosition.Y, (int)startingPosition.X] = State.CurrentlyAt;
            grid[(int)goal.Y, (int)goal.X] = State.Goal;
            DrawGrid();

            return "Steps: " + BFS(startingPosition, goal, true);
        }

        class Node
        {
            public int X { get; set; }
            public int Y { get; set; }
            public int Distance { get; set; }
            public Node Parent { get; set; }
        }

        string BFS(Point startingPoint, Point goal, bool Part1)
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
                        Distance = Int32.MaxValue,
                        Parent = null
                    };

                }
            }

            graph[(int)startingPoint.Y, (int)startingPoint.X].Distance = 0;
            graph[(int)startingPoint.Y, (int)startingPoint.X].Parent = null;

            var q = new Queue<Node>();
            q.Enqueue(graph[(int)startingPoint.Y, (int)startingPoint.X]);

            while (q.Count() > 0)
            {
                var current = q.Dequeue();

                foreach (var adjNode in GetAdjacentNodes(graph, current.Y, current.X))
                {
                    if (adjNode.Distance == Int32.MaxValue)
                    {
                        adjNode.Distance = current.Distance + 1;
                        adjNode.Parent = current;
                        q.Enqueue(adjNode);
                    }
                }
            }
            //DrawGraph(graph);
            if (Part1)
            {
                //System.Diagnostics.Debug.WriteLine(graph[(int)goal.Y, (int)goal.X].Distance);
                return graph[(int)goal.Y, (int)goal.X].Distance == Int32.MaxValue ? "Unknown" : graph[(int)goal.Y, (int)goal.X].Distance.ToString();
            }
            else
            {
                var locations = 0;
                for (int x = 0; x < rows; x++)
                {
                    for (int y = 0; y < cols; y++)
                    {
                        if (graph[x,y].Distance <= 50)
                        {
                            locations++;
                        }
                    }
                }
                return locations.ToString();
            }

        }

        private List<Node> GetAdjacentNodes(Node[,] graph, int X, int Y)
        {
            var adjacentNodes = new List<Node>();
            //System.Diagnostics.Debug.WriteLine("current: " + X + "," + Y + ": " + StateToChar(grid[Y, X]));

            if (X + 1 < cols)
            {
                if (!grid[Y, X + 1].Equals(State.Wall))
                {
                    //System.Diagnostics.Debug.WriteLine("right: " + (X + 1) + "," + Y + ": " + StateToChar(grid[Y, X + 1]));
                    adjacentNodes.Add(graph[Y, X + 1]);
                }
            }

            if (X - 1 >= 0)
            {
                if (!grid[Y, X - 1].Equals(State.Wall))
                {
                    //System.Diagnostics.Debug.WriteLine("left: " + (X - 1) + "," + Y + ": " + StateToChar(grid[Y, X - 1]));
                    adjacentNodes.Add(graph[Y, X - 1]);
                }
            }

            if (Y + 1 < rows)
            {
                if (!grid[Y + 1, X].Equals(State.Wall))
                {
                    //System.Diagnostics.Debug.WriteLine("below: " + X + "," + (Y + 1) + ": " + StateToChar(grid[Y + 1, X]));
                    adjacentNodes.Add(graph[Y + 1, X]);
                }
            }

            if (Y - 1 >= 0)
            {
                if (!grid[Y - 1, X].Equals(State.Wall))
                {
                    //System.Diagnostics.Debug.WriteLine("above: " + X + "," + (Y - 1) + ": " + StateToChar(grid[Y - 1, X]));
                    adjacentNodes.Add(graph[Y - 1, X]);
                }
            }
            //System.Diagnostics.Debug.WriteLine("");
            return adjacentNodes;
        }

        char StateToChar(State state)
        {
            char result = '?';
            switch (state)
            {
                case State.OpenSpace:
                    result = '.';
                    break;
                case State.Wall:
                    result = '#';
                    break;
                case State.CurrentlyAt:
                    result = 'O';
                    break;
                case State.Goal:
                    result = 'G';
                    break;
            }
            return result;
        }

        void DrawGrid()
        {
            System.Diagnostics.Debug.Write(" ");
            for (int y = 0; y < cols; y++)
            {
                System.Diagnostics.Debug.Write(y);
            }
            System.Diagnostics.Debug.Write(Environment.NewLine);
            for (int x = 0; x < rows; x++)
            {
                System.Diagnostics.Debug.Write(x);
                for (int y = 0; y < cols; y++)
                {
                    System.Diagnostics.Debug.Write(StateToChar(grid[x, y]));
                }
                System.Diagnostics.Debug.Write(Environment.NewLine);
            }
            System.Diagnostics.Debug.Write(Environment.NewLine);
        }

        void DrawGraph(Node[,] graph)
        {
            System.Diagnostics.Debug.Write(" ");
            for (int y = 0; y < cols; y++)
            {
                System.Diagnostics.Debug.Write(y);
            }
            System.Diagnostics.Debug.Write(Environment.NewLine);
            for (int x = 0; x < rows; x++)
            {
                System.Diagnostics.Debug.Write(x);
                for (int y = 0; y < cols; y++)
                {
                    System.Diagnostics.Debug.Write(graph[x, y].Distance == Int32.MaxValue ? " " : graph[x, y].Distance.ToString());
                }
                System.Diagnostics.Debug.Write(Environment.NewLine);
            }
            System.Diagnostics.Debug.Write(Environment.NewLine);
        }

        State IsOpenSpace(Point point)
        {
            var calc = (point.X * point.X) + (3 * point.X) + (2 * point.X * point.Y) + (point.Y) + (point.Y * point.Y) + officeDesignersFavouriteNumber;
            string binary = Convert.ToString((int)calc, 2);
            int countBits = binary.Count(c => c == '1');
            //System.Diagnostics.Debug.WriteLine(point.X + "," + point.Y + ": " + calc + " => " + binary + ", " + countBits);
            return countBits % 2 == 0 ? State.OpenSpace : State.Wall;
        }

        public string Part2()
        {
            var goal = new Point(31, 39);

            for (int x = 0; x < rows; x++)
            {
                for (int y = 0; y < cols; y++)
                {
                    grid[x, y] = IsOpenSpace(new Point(y, x));
                }
            }

            grid[(int)startingPosition.Y, (int)startingPosition.X] = State.CurrentlyAt;
            grid[(int)goal.Y, (int)goal.X] = State.Goal;
            DrawGrid();

            return "Locations: " + BFS(startingPosition, goal, false);
        }
    }
}

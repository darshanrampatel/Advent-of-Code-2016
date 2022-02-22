using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;

namespace AoC.Days
{
    class Day11
    {
        static string input = File.ReadAllText(AppContext.BaseDirectory + "/Inputs/Day11.txt");

        class FloorItems
        {
            public List<string> Generators { get; set; }
            public List<string> Microchips { get; set; }

            public override string ToString()
            {
                return "Generators: " + String.Join(",", Generators) + " Microchips: " + String.Join(",", Microchips);
            }
        }

        class Node
        {
            public int Floor { get; set; }
            public bool Elevator { get; set; }
            public int Distance { get; set; }
            public Node Parent { get; set; }
            public FloorItems FloorItems { get; set; }

            public override string ToString()
            {
                return $"F{Floor} " + (Elevator ? "E " : "  ") + FloorItems.ToString();
            }
        }

        class Building
        {
            public Node[] Floors { get; set; }
            public override string ToString()
            {
                return Floors.ToString();
            }

            public int Elevator
            {
                get
                {
                    return Floors.FirstOrDefault(f => f.Elevator).Floor;
                }
            }
        }

        string BFS()
        {
            var g = new Building() { Floors = new Node[4] };

            var graph = new Node[4];
            for (int x = 0; x < 4; x++)
            {
                g.Floors[x] = new Node()
                {
                    Floor = x,
                    Elevator = false,
                    Distance = Int32.MaxValue,
                    Parent = null
                };
            }

            g.Floors[0].Elevator = true;
            g.Floors[0].FloorItems = new FloorItems()
            {
                Generators = new List<string>(),
                Microchips = new List<string>() { "hydrogen-M", "lithum-M" }
            };
            g.Floors[1].FloorItems = new FloorItems()
            {
                Generators = new List<string>() { "hydrogen-G" },
                Microchips = new List<string>()
            };
            g.Floors[2].FloorItems = new FloorItems()
            {
                Generators = new List<string>() { "lithium-G" },
                Microchips = new List<string>()
            };
            g.Floors[3].FloorItems = new FloorItems()
            {
                Generators = new List<string>(),
                Microchips = new List<string>()
            };

            g.Floors[0].Distance = 0;
            g.Floors[0].Parent = null;

            System.Diagnostics.Debug.WriteLine("Starting...");
            for (int x = 0; x < 4; x++)
            {
                System.Diagnostics.Debug.WriteLine(g.Floors[x].ToString());
            }

            int solution = int.MaxValue;
            int count = 0;

            var Visited = new HashSet<Node[]>();

            var q = new Queue<Node[]>();
            q.Enqueue(g.Floors);

            while (q.Count() > 0)
            {
                var current = q.Dequeue();
                var floor = current.FirstOrDefault(f => f.Elevator).Floor;
                count++;
                System.Diagnostics.Debug.WriteLine(count);


                if (Visited.Contains(current))
                {
                    continue;
                }
                else
                {
                    Visited.Add(current);
                }

                //System.Diagnostics.Debug.WriteLine("current: " + current.ToString());

                if (IsGoalFloor(current))
                {
                    System.Diagnostics.Debug.WriteLine("Goal: " + current.ToString());
                    //if (solution > current.Distance)
                    //{
                    //    solution = current.Distance;
                    //    System.Diagnostics.Debug.WriteLine("SOLUTION FOUND: " + solution);
                    //    break;
                    //}
                }

                var adjFloors = new List<Node>();
                foreach (var adjNode in GetAdjacentFloors(current[floor], floor))
                {
                    if (adjNode.Distance == Int32.MaxValue)
                    {
                        adjNode.Distance = current[floor].Distance + 1;
                        adjNode.Parent = current[floor];
                        adjFloors.Add(adjNode);
                    }
                }
                q.Enqueue(adjFloors.ToArray());
            }
            //DrawGraph(graph);
            //System.Diagnostics.Debug.WriteLine(graph[(int)goal.Y, (int)goal.X].Distance);
            System.Diagnostics.Debug.WriteLine("Final...");
            for (int x = 0; x < 4; x++)
            {
                System.Diagnostics.Debug.WriteLine(graph[x].ToString());
            }
            return "";
            //return graph[(int)goal.Y, (int)goal.X].Distance == Int32.MaxValue ? "Unknown" : graph[(int)goal.Y, (int)goal.X].Distance.ToString();
        }

        private bool IsGoalFloor(Node[] current)
        {
            var topFloor = current.OrderByDescending(f => f.Floor).FirstOrDefault();

            if (topFloor.Floor == 3 && IsComplete(topFloor.FloorItems))
            {
                return true;
            }
            return false;
        }

        private bool IsComplete(FloorItems floor)
        {
            return IsFloorValid(floor) && floor.Microchips.Count > 0 && floor.Generators.Count > 0;
        }

        private Node[] GetAdjacentFloors(Node currentFloor, int floor)
        {
            var adjacentNodes = new List<Node>();
            //System.Diagnostics.Debug.WriteLine("Possible moves for " + currentFloor.ToString());
            foreach (var itemsToMove in GetPossibleMoves(currentFloor))
            {
                //System.Diagnostics.Debug.WriteLine(itemsToMove);
                //either up or down
                if (floor > 0)
                {
                    //we can go down
                    var t = GetNextFloor(currentFloor, itemsToMove, floor - 1);
                    if (t != null)
                    {
                        //System.Diagnostics.Debug.WriteLine("Valid down floor: " + t.ToString());
                        adjacentNodes.Add(t);
                    }
                }

                if (floor < 3)
                {
                    //we can go up
                    var t = GetNextFloor(currentFloor, itemsToMove, floor + 1);
                    if (t != null)
                    {
                        //System.Diagnostics.Debug.WriteLine("Valid up floor: " + t.ToString());
                        adjacentNodes.Add(t);
                    }

                }
            }
            return adjacentNodes.ToArray();
        }

        Node GetNextFloor(Node floor, Tuple<string, string> itemsToMove, int newFloor)
        {
            var newNode = new Node()
            {
                Floor = newFloor,
                Elevator = true,
                Distance = Int32.MaxValue,
                Parent = null,
                FloorItems = new FloorItems()
                {
                    Generators = new List<string>(),
                    Microchips = new List<string>()
                }
            };

            if (itemsToMove.Item1 != null)
            {
                if (itemsToMove.Item1.EndsWith("G"))
                {
                    newNode.FloorItems.Generators.Add(itemsToMove.Item1);
                    floor.FloorItems.Generators.Remove(itemsToMove.Item1);
                }
                else if (itemsToMove.Item1.EndsWith("M"))
                {
                    newNode.FloorItems.Microchips.Add(itemsToMove.Item1);
                    floor.FloorItems.Microchips.Remove(itemsToMove.Item1);
                }
            }
            if (itemsToMove.Item2 != null)
            {
                if (itemsToMove.Item2.EndsWith("G"))
                {
                    newNode.FloorItems.Generators.Add(itemsToMove.Item2);
                    floor.FloorItems.Generators.Remove(itemsToMove.Item2);
                }
                else if (itemsToMove.Item2.EndsWith("M"))
                {
                    newNode.FloorItems.Microchips.Add(itemsToMove.Item2);
                    floor.FloorItems.Microchips.Remove(itemsToMove.Item1);
                }
            }

            floor.Elevator = false;

            return IsFloorValid(floor.FloorItems) && IsFloorValid(newNode.FloorItems) ? newNode : null;
        }

        bool IsFloorValid(FloorItems floorItems)
        {
            return floorItems.Microchips.TrueForAll(c => floorItems.Generators.Contains(c.Replace("-C", "-G")) || floorItems.Generators.Count == 0);
        }

        private List<Tuple<string, string>> GetPossibleMoves(Node node)
        {
            var floorItems = node.FloorItems;
            var possibleMoves = new List<Tuple<string, string>>();
            var allFloorItems = new List<string>();
            allFloorItems.AddRange(node.FloorItems.Generators);
            allFloorItems.AddRange(node.FloorItems.Microchips);

            var t = allFloorItems
                .SelectMany(x => allFloorItems, (x, y) => Tuple.Create(x, y))
                .Where(tuple => tuple.Item1 != tuple.Item2)
                .Where(items => CanMoveTogether(items));
            possibleMoves.AddRange(t);
            possibleMoves.AddRange(allFloorItems.Select(x => Tuple.Create<string, string>(x, null)));
            return possibleMoves;
        }

        private bool CanMoveTogether(Tuple<string, string> items)
        {
            var item1 = items.Item1.Split('-');
            var item2 = items.Item2.Split('-');
            return (item1.First() == item2.First()) || (item1.Last() == item2.Last());
        }

        public string Part1()
        {
            BFS();


            var answer = "Unknown";
            return answer;
        }

        public string Part2()
        {
            var answer = "Unknown";
            return answer;
        }
    }
}

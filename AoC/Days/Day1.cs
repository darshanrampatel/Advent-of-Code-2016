using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;

namespace AoC.Days
{

    public class Day1
    {
        static string input = File.ReadAllText(AppContext.BaseDirectory + "/Inputs/Day1.txt");

        enum Direction
        {
            North,
            East,
            South,
            West,
            Undefined
        };
        public string Part1()
        {
            var position = new Point(0, 0);
            var facingDirection = Direction.North;
            var steps = input.Split(',');
            foreach (string rawStep in steps)
            {
                var step = rawStep.Trim();
                var direction = step.First();
                var s = step.Skip(1).ToString();
                var distance = Int32.Parse(step.Substring(1));
                switch (direction)
                {
                    case 'L':
                        facingDirection = ChangeDirection(facingDirection, true);
                        position = Move(position, facingDirection, distance);
                        break;
                    case 'R':
                        facingDirection = ChangeDirection(facingDirection, false);
                        position = Move(position, facingDirection, distance);
                        break;
                    default:
                        break;
                }
            }
            return position.ToString() + ": " + (Math.Abs(position.X) + Math.Abs(position.Y)) + " blocks away";
        }

        public List<Point> visitedPositions = new List<Point>();

        public string Part2()
        {
            var position = new Point(0, 0);
            visitedPositions.Add(position);
            var facingDirection = Direction.North;
            var steps = input.Split(',');
            foreach (string rawStep in steps)
            {
                var step = rawStep.Trim();
                var direction = step.First();
                var s = step.Skip(1).ToString();
                var distance = Int32.Parse(step.Substring(1));
                switch (direction)
                {
                    case 'L':
                        facingDirection = ChangeDirection(facingDirection, true);
                        position = Move(position, facingDirection, distance);
                        break;
                    case 'R':
                        facingDirection = ChangeDirection(facingDirection, false);
                        position = Move(position, facingDirection, distance);
                        break;
                    default:
                        break;
                }
            }
            var firstDuplicatePosition = visitedPositions
                .GroupBy(i => i)
                .Where(g => g.Count() > 1)
                .Select(g => g.Key)
                .FirstOrDefault();
            return "First location visited twice: " + firstDuplicatePosition.ToString() + ": " + (Math.Abs(firstDuplicatePosition.X) + Math.Abs(firstDuplicatePosition.Y)) + " blocks away"; ;
        }

        private Direction ChangeDirection(Direction oldDirection, bool TurnLeft)
        {
            switch (oldDirection)
            {
                case Direction.North:
                    return TurnLeft ? Direction.West : Direction.East;
                case Direction.East:
                    return TurnLeft ? Direction.North : Direction.South;
                case Direction.South:
                    return TurnLeft ? Direction.East : Direction.West;
                case Direction.West:
                    return TurnLeft ? Direction.South : Direction.North;
                default:
                    return Direction.Undefined;
            }
        }

        private Point Move(Point oldPosition, Direction direction, int Distance)
        {
            switch (direction)
            {
                case Direction.North:
                    for (var i = 1; i <= Distance; i++)
                    {
                        visitedPositions.Add(new Point(oldPosition.X, oldPosition.Y + i));
                    }
                    return new Point(oldPosition.X, oldPosition.Y + Distance);
                case Direction.East:
                    for (var i = 1; i <= Distance; i++)
                    {
                        visitedPositions.Add(new Point(oldPosition.X + i, oldPosition.Y));
                    }
                    return new Point(oldPosition.X + Distance, oldPosition.Y);
                case Direction.South:
                    for (var i = 1; i <= Distance; i++)
                    {
                        visitedPositions.Add(new Point(oldPosition.X, oldPosition.Y - i));
                    }
                    return new Point(oldPosition.X, oldPosition.Y - Distance);
                case Direction.West:
                    for (var i = 1; i <= Distance; i++)
                    {
                        visitedPositions.Add(new Point(oldPosition.X - i, oldPosition.Y));
                    }
                    return new Point(oldPosition.X - Distance, oldPosition.Y);
                default:
                    return oldPosition;
            }
        }
    }


}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Day15
{
    public class RepairRobot2 : IOutput, IInput
    {
        private Dictionary<Point, Tile> _panels = new Dictionary<Point, Tile>();
        private Dictionary<Point, int> _visited = new Dictionary<Point, int>();
        private Stack<Direction> _pathUntilNow = new Stack<Direction>();
        private Point _currentLocation;
        private Point _requestedLocation;
        private Direction _previousDirection = Direction.North;

        public RepairRobot2()
        {
            _panels[new Point(0, 0)] = new Empty(new Point(0, 0));
            _panels[new Point(0, 0)].DrawAsCurrent();
        }

        public void WriteOutput(long value)
        {
            Tile tile;
            var oldLocation = _currentLocation;
            //if (!_panels.TryGetValue(_requestedLocation, out tile))
            {
                switch (value)
                {
                    case 0:
                        tile = new Wall(_requestedLocation);
                        _pathUntilNow.Pop();
                        break;
                    case 1:
                        tile = new Empty(_requestedLocation);
                        _currentLocation = _requestedLocation;
                        break;
                    case 2:
                        tile = new OxygenSystem(_requestedLocation);
                        _currentLocation = _requestedLocation;

                        break;
                    default:
                        return;//not known
                }
                _panels[_requestedLocation] = tile;
            }
            tile.Draw();
            _panels[oldLocation].Draw();
            _panels[_currentLocation].DrawAsCurrent();
        }
        RobotMode _mode = RobotMode.Automatic;
        public long ReadInput()
        {
            Direction direction;
            if (_mode == RobotMode.Manual)
            {
                var key = Console.ReadKey(true);
                switch (key.Key)
                {
                    case ConsoleKey.LeftArrow:
                        direction = Direction.West;
                        break;
                    case ConsoleKey.RightArrow:
                        direction = Direction.East;
                        break;
                    case ConsoleKey.UpArrow:
                        direction = Direction.North;
                        break;
                    case ConsoleKey.DownArrow:
                        direction = Direction.South;
                        break;
                    default:
                        return 0;
                }
                if (_pathUntilNow.Count() > 0 && _pathUntilNow.Peek() == GetOpposite(direction))
                {
                    _pathUntilNow.Pop();
                }

            }
            else
            {
                direction = FindNextDirection();
            }
            _requestedLocation = _currentLocation.Next(direction, 1);
            _previousDirection = direction;
            _pathUntilNow.Push(direction);
            return (long)direction;
        }

        public Queue<Direction> _path = new Queue<Direction>();

        private Direction FindNextDirection()
        {
            if (_path.Count == 0)
            {
                return FindNewPath();
            }
            return _path.Dequeue();
            /*
            if (!_panels.TryGetValue(_currentLocation.Next(Direction.North, 1), out var north))
            {
                return Direction.North;
            }
            if (!_panels.TryGetValue(_currentLocation.Next(Direction.South, 1), out var south))
            {
                return Direction.South;
            }
            if (!_panels.TryGetValue(_currentLocation.Next(Direction.East, 1), out var east))
            {
                return Direction.East;
            }
            if (!_panels.TryGetValue(_currentLocation.Next(Direction.West, 1), out var west))
            {
                return Direction.West;
            }
            var tilesByDirection = new Dictionary<Direction, Tile> { { Direction.North, north }, { Direction.South, south }, { Direction.East, east }, { Direction.West, west } };
            if (tilesByDirection[_previousDirection] is Empty)
            {
                return _previousDirection;
            }
            Direction opposite = _previousDirection;
            switch (_previousDirection)
            {
                case Direction.North:
                    opposite = Direction.South;
                    break;
                case Direction.South:
                    opposite = Direction.North;
                    break;
                case Direction.West:
                    opposite = Direction.East;
                    break;
                case Direction.East:
                    opposite = Direction.West;
                    break;
            }

            var emptyTiles = tilesByDirection.Where(_ => _.Value is Empty && _.Key != opposite);
            if (emptyTiles.Any())
            {
                return emptyTiles.First().Key;
            }
            Console.WriteLine("I don't know");

            return opposite;*/
        }
        public Dictionary<Direction, Tile> GetNeighbors(Point p)
        {
            _panels.TryGetValue(p.Next(Direction.North, 1), out var north);
            _panels.TryGetValue(p.Next(Direction.South, 1), out var south);
            _panels.TryGetValue(p.Next(Direction.East, 1), out var east);
            _panels.TryGetValue(p.Next(Direction.West, 1), out var west);
            return new Dictionary<Direction, Tile> { { Direction.North, north }, { Direction.South, south }, { Direction.East, east }, { Direction.West, west } };
        }



        private Direction FindNewPath()
        {
            var surrounding = GetNeighbors(_currentLocation);

            if (surrounding.Count(_ => _.Value is Wall) == 3) // dead end
            {
                return surrounding.Where(_ => !(_.Value is Wall)).First().Key;
            }
            else if (surrounding.Count(_ => _.Value is Wall) < 2)
            {
                _mode = RobotMode.Automatic;
            }

            switch (_mode)
            {
                case RobotMode.Automatic:
                    var next = _previousDirection;
                    while (!(surrounding[next] is null))
                    {
                        next = TurnRight(next);
                    }
                    return next;

                case RobotMode.BackTracking:
                    return GetOpposite(_pathUntilNow.Pop());
                default:
                    return Direction.South;
            }
        }

        private Direction GetOpposite(Direction direction)
        {
            switch (direction)
            {
                case Direction.North:
                    return Direction.South;
                case Direction.South:
                    return Direction.North;
                case Direction.West:
                    return Direction.East;
                case Direction.East:
                    return Direction.West;
                default: return (Direction)0;
            }
        }
        private Direction TurnRight(Direction direction)
        {
            switch (direction)
            {
                case Direction.North:
                    return Direction.East;
                case Direction.South:
                    return Direction.West;
                case Direction.West:
                    return Direction.North;
                case Direction.East:
                    return Direction.South;
                default: return (Direction)0;
            }
        }
    }
    public enum RobotMode
    {
        Manual,
        Automatic,
        BackTracking
    }
}

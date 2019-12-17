using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Day15
{
    public struct Point : IEquatable<Point>
    {
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public readonly int X;
        public readonly int Y;

        public override bool Equals(object obj)
        {
            return obj is Point point && Equals(point);
        }

        public bool Equals(Point other)
        {
            return X == other.X &&
                   Y == other.Y;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }

        public static bool operator ==(Point left, Point right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Point left, Point right)
        {
            return !(left == right);
        }

        internal Point Next(Direction d, int distance)
        {
            var x = X;
            var y = Y;
            switch (d)
            {
                case Direction.North:
                    y += distance;
                    break;
                case Direction.South:
                    y -= distance;
                    break;
                case Direction.East:
                    x += distance;
                    break;
                case Direction.West:
                    x -= distance;
                    break;
            }
            return new Point(x, y);
        }
    }
}
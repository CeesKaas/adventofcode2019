using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Day3
{
    public class Point : IEquatable<Point>
    {
        public Point(int X, int Y)
        {
            this.X = X;
            this.Y = Y;
        }

        public int X { get; private set; }
        public int Y { get; private set; }

        public Point CalculateNext(Vector v)
        {
            var nextPoint = new Point(X, Y);
            switch (v.Direction)
            {
                case Direction.U:
                    nextPoint.Y += v.Distance;
                    break;
                case Direction.D:
                    nextPoint.Y -= v.Distance;
                    break;
                case Direction.R:
                    nextPoint.X += v.Distance;
                    break;
                case Direction.L:
                    nextPoint.X -= v.Distance;
                    break;
            }
            return nextPoint;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Point);
        }

        public bool Equals([AllowNull] Point other)
        {
            return other != null &&
                   X == other.X &&
                   Y == other.Y;
        }

        public int DistanceTo(Point other)
        {
            return Math.Abs(X + other.X) + Math.Abs(Y + other.Y);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }

        public static bool operator ==(Point left, Point right)
        {
            return EqualityComparer<Point>.Default.Equals(left, right);
        }

        public static bool operator !=(Point left, Point right)
        {
            return !(left == right);
        }
    }
}
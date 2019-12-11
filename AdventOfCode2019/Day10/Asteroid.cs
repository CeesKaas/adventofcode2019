using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Day10
{
    public class Asteroid
    {
        public Asteroid()
        {
            Location = new Point(0, 0);
        }
        public Asteroid(int x, int y)
        {
            Location = new Point(x, y);
        }
        public Point Location { get; }

        public double GetAngleTo(Asteroid other)
        {
            var a = Location;
            var b = other.Location;
            var dX = a.X - b.X;
            var dY = a.Y - b.Y;
            double v = ((Math.Atan2(dY, dX) * (180.0 / Math.PI) + 270) % 360);
            return v;
        }

        internal double DistanceTo(Asteroid other)
        {
            var a = Location;
            var b = other.Location;
            var dX = Math.Max(a.X, b.X) - Math.Min(a.X, b.X);
            var dY = Math.Max(a.Y, b.Y) - Math.Min(a.Y, b.Y);
            return Math.Sqrt(dX * dX + dY * dY);
        }
    }
    public class Point : IEquatable<Point>
    {
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; }
        public int Y { get; }

        public override string ToString()
        {
            return $"{X,4},{Y,4}";
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Point);
        }

        public bool Equals(Point other)
        {
            return other != null &&
                   X == other.X &&
                   Y == other.Y;
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
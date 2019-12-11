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


        public ICollection<Point> GetPointsBetween(Point other)
        {
            return GetPointsBetween(Location, other);
        }
        public static ICollection<Point> GetPointsBetween(Point a, Point b)
        {
            List<Point> points = new List<Point>();
            if (a.X == b.X)
            {
                var max = Math.Max(a.Y, b.Y);
                var min = Math.Min(a.Y, b.Y);
                for (int i = min + 1; i < max; i++)
                {
                    points.Add(new Point(a.X, i));
                }
                return points;
            }
            if (a.Y == b.Y)
            {
                var max = Math.Max(a.X, b.X);
                var min = Math.Min(a.X, b.X);
                for (int i = min + 1; i < max; i++)
                {
                    points.Add(new Point(i, a.Y));
                }
                return points;
            }
            var leftMost = a.X < b.X ? a : b;
            var rightMost = a.X > b.X ? a : b;

            var slope = (leftMost.Y - (double)rightMost.Y) / (leftMost.X - (double)rightMost.X);
            var offset = leftMost.Y - (leftMost.X * slope);

            for (int x = leftMost.X + 1; x < rightMost.X; x++)
            {
                double yDouble = (x * slope) + offset;
                int y = (int)yDouble;
                if (Math.Abs(y - yDouble) < 0.01)
                {
                    points.Add(new Point(x, y));
                }
            }
            return points;
        }
        [Test]
        public void GetPointsBetweenTest()
        {
            var p1 = GetPointsBetween(new Point(0, 3), new Point(3, 0));
            Assert.That(p1, Is.EquivalentTo(new[] { new Point(1, 2), new Point(2, 1) }));

            var p2 = GetPointsBetween(new Point(0, 3), new Point(4, 1));
            Assert.That(p2, Is.EquivalentTo(new[] { new Point(2, 2) }));
        }

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

        public double GetSlopeTo(Asteroid other)
        {
            var a = Location;
            var b = other.Location;
            if (a.X == b.X)
            {
                return a.Y < b.Y ? double.PositiveInfinity : double.NegativeInfinity;
            }
            if (a.Y == b.Y)
            {
                return a.X < b.X ? 0.0 : double.MaxValue;
            }

            var slope = (a.Y - (double)b.Y) / (a.X - (double)b.X);
            return slope;
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
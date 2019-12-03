using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Day3
{
    public class Point
    {
        public Point(int x, int y, int stepsTakenFromOrigin)
        {
            X = x;
            Y = y;
            StepsTakenFromOrigin = stepsTakenFromOrigin;
        }

        public int X { get; private set; }
        public int Y { get; private set; }
        public int StepsTakenFromOrigin { get; }

        public Point CalculateNext(Vector v)
        {
            var nextPoint = new Point(X, Y, StepsTakenFromOrigin + v.Distance);
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

        public bool IsOnLine(Line line)
        {
            if (line.A.X == X || line.B.X == X)
            {
                var yMin = Math.Min(line.A.Y, line.B.Y);
                var yMax = Math.Max(line.A.Y, line.B.Y);
                if (Y >= yMin && Y <= yMax)
                    return true;
            }
            else if (line.A.Y == Y || line.B.Y == Y)
            {
                var xMin = Math.Min(line.A.X, line.B.X);
                var xMax = Math.Max(line.A.X, line.B.X);
                if (X >= xMin && X <= xMax)
                    return true;
            }
            return false;
        }


        public int DistanceTo(Point other)
        {
            int xDistance, yDistance;
            if (X > other.X)
            {
                xDistance = X - other.X;
            }
            else
            {
                xDistance = other.X - X;
            }
            if (Y > other.Y)
            {
                yDistance = Y - other.Y;
            }
            else
            {
                yDistance = other.Y - Y;
            }
            return xDistance + yDistance;
        }

        public override string ToString()
        {
            return $"{X,4},{Y,4}";
        }
    }
}
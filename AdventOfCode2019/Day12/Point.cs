using System;
using System.Collections.Generic;
using System.Text;

namespace Day12
{
    struct Point
    {
        public readonly int X;
        public readonly int Y;
        public readonly int Z;
        public Point(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Point Apply(Vector v)
        {
            return new Point(X + v.X, Y + v.Y, Z + v.Z);
        }
        public int GetPotentialEnergy()
        {
            return Math.Abs(X) + Math.Abs(Y) + Math.Abs(Z);
        }
        public override string ToString()
        {
            return $"<x={X}, y={Y}, z={Z}>";
        }
    }
}

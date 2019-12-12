using System;
using System.Collections.Generic;
using System.Text;

namespace Day12
{
    struct Vector
    {
        public readonly int X;
        public readonly int Y;
        public readonly int Z;

        public Vector(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }
        public int GetKineticEnergy()
        {
            return Math.Abs(X) + Math.Abs(Y) + Math.Abs(Z);
        }

        public override string ToString()
        {
            return $"<x={X}, y={Y}, z={Z}>";
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Day12
{
    class Moon : IEquatable<Moon>
    {
        public Point Position { get; set; }
        public Vector Velocity { get; set; }

        public int GetCombinedEnergy()
        {
            return Position.GetPotentialEnergy() * Velocity.GetKineticEnergy();
        }

        public void Step()
        {
            Position = Position.Apply(Velocity);
        }

        public override string ToString()
        {
            return $"pos={Position}, vel={Velocity}";
        }

        public static Moon Parse(string s)
        {
            var parser = new Regex("<x=(?<X>-?[0-9]*), y=(?<Y>-?[0-9]*), z=(?<Z>-?[0-9]*)>", RegexOptions.ExplicitCapture);
            var match = parser.Match(s);
            var x = int.Parse(match.Groups["X"].Value);
            var y = int.Parse(match.Groups["Y"].Value);
            var z = int.Parse(match.Groups["Z"].Value);

            return new Moon { Position = new Point(x, y, z) };
        }

        public static void UpdateVelocities(Moon a, Moon b)
        {
            int aX = a.Velocity.X;
            int aY = a.Velocity.Y;
            int aZ = a.Velocity.Z;
            int bX = b.Velocity.X;
            int bY = b.Velocity.Y;
            int bZ = b.Velocity.Z;

            if (a.Position.X > b.Position.X)
            {
                aX -= 1;
                bX += 1;
            }else if (a.Position.X < b.Position.X)
            {
                aX += 1;
                bX -= 1;
            }
            if (a.Position.Y > b.Position.Y)
            {
                aY -= 1;
                bY += 1;
            }
            else if (a.Position.Y < b.Position.Y)
            {
                aY += 1;
                bY -= 1;
            }
            if (a.Position.Z > b.Position.Z)
            {
                aZ -= 1;
                bZ += 1;
            }
            else if (a.Position.Z < b.Position.Z)
            {
                aZ += 1;
                bZ -= 1;
            }

            a.Velocity = new Vector(aX, aY, aZ);
            b.Velocity = new Vector(bX, bY, bZ);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Moon);
        }

        public bool Equals(Moon other)
        {
            return other != null &&
                   EqualityComparer<Point>.Default.Equals(Position, other.Position) &&
                   EqualityComparer<Vector>.Default.Equals(Velocity, other.Velocity);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Position, Velocity);
        }

        public static bool operator ==(Moon left, Moon right)
        {
            return EqualityComparer<Moon>.Default.Equals(left, right);
        }

        public static bool operator !=(Moon left, Moon right)
        {
            return !(left == right);
        }
    }
}

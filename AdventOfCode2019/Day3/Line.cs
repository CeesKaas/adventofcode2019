using NUnit.Framework;

namespace Day3
{
    public class Line
    {
        private static readonly Point _origin = new Point(0, 0, 0);

        public Line(Point a, Point b)
        {
            A = a;
            B = b;
        }

        public bool Crosses(Line other, out Point crossingPoint)
        {
            crossingPoint = null;
            var vertical = A.X == B.X ? this : other.A.X == other.B.X ? other : null;
            var horizontal = A.Y == B.Y ? this : other.A.Y == other.B.Y ? other : null;
            if (vertical == null || horizontal == null)
                return false; //lines are parallel
            int y = horizontal.A.Y;
            int x = vertical.A.X;
            var possibleCrossingPoint = new Point(x, y, 0);
            int stepsTakenFromOriginFollowingPath = horizontal.A.StepsTakenFromOrigin + horizontal.A.DistanceTo(possibleCrossingPoint) + vertical.A.StepsTakenFromOrigin + vertical.A.DistanceTo(possibleCrossingPoint);
            if (possibleCrossingPoint.IsOnLine(this) && possibleCrossingPoint.IsOnLine(other))
            {
                crossingPoint = new Point(x, y, stepsTakenFromOriginFollowingPath);
                return true;
            }
            return false;
        }

        public Point A { get; set; }
        public Point B { get; set; }
        public int Length => A.DistanceTo(B);

        public override string ToString()
        {
            return $"A [{A}] - B [{B}]";
        }
    }
    public class LineTest
    {
        [Test]
        public void CrossesTest()
        {
            var line1 = new Line(new Point(1, 1, 0), new Point(3, 1, 0));
            var line2 = new Line(new Point(2, 0, 0), new Point(2, 2, 0));
            Assert.That(line1.Crosses(line2, out _), Is.True);
        }
    }
}
using NUnit.Framework;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace Day3
{
    public class Day3
    {
        private static Point _origin = new Point(0, 0, 0);
        public static void Execute()
        {
            var input = InputGetter.GetSplitInputForDay(3, new[] { '\n' }).ToArray();
            (var closest, var shortest) = Run(input[0], input[1]);
            Console.WriteLine($"closest {closest.DistanceTo(_origin)}, shortest {shortest.StepsTakenFromOrigin}");
        }

        [Test]
        [TestCase("R75, D30, R83, U83, L12, D49, R71, U7, L72", "U62, R66, U55, R34, D71, R55, D58, R83", 159, 610)]
        [TestCase("R98,U47,R26,D63,R33,U87,L62,D20,R33,U53,R51", "U98, R91, D20, R16, D67, R40, U7, R15, U6, R7", 135, 410)]
        public void Test(string a, string b, int expectedDistance, int expectedSteps)
        {
            (var closest, var shortest) = Run(a, b);
            Assert.That(closest.DistanceTo(_origin), Is.EqualTo(expectedDistance));
            Assert.That(shortest.StepsTakenFromOrigin, Is.EqualTo(expectedSteps));
        }

        private static (Point, Point) Run(string a, string b)
        {
            var vectorsA = InputTransformDay3.ParseLines(a);
            var vectorsB = InputTransformDay3.ParseLines(b);
            var linesA = CalculateLines(vectorsA);
            var linesB = CalculateLines(vectorsB);
            var crossings = FindCrossingPoints(linesA, linesB);
            return (FindClosestToOrigin(crossings), FindShortestToOrigin(crossings));
        }

        public static Line[] CalculateLines(Vector[] vectors)
        {
            var p = new Point(0, 0, 0);
            var lines = new List<Line>(vectors.Length);

            foreach (Vector v in vectors)
            {
                var other = p.CalculateNext(v);
                Line item = new Line(p, other);
                lines.Add(item);
                p = other;
            }
            return lines.ToArray();
        }
        public static Point[] FindCrossingPoints(Line[] linesA, Line[] linesB)
        {
            var points = new List<Point>();
            foreach (var a in linesA)
            {
                foreach (var b in linesB)
                {
                    if (a.Crosses(b, out var p) && p.X != 0 && p.Y != 0)
                    {
                        points.Add(p);
                    }
                }
            }
            return points.ToArray();
        }
        public static Point FindClosestToOrigin(Point[] points)
        {
            return points.OrderBy(p => p.DistanceTo(_origin)).First();
        }
        public static Point FindShortestToOrigin(Point[] points)
        {
            return points.OrderBy(p => p.StepsTakenFromOrigin).First();
        }
    }
}

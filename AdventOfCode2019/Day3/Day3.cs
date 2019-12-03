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
        public static void Execute()
        {
            var input = InputGetter.GetTransformedSplitInputForDay(3, new[] { '\n' }, InputTransformDay3.ParseLines).ToArray();
            Part1((Vector[][])input.Clone());
            Part2((Vector[][])input.Clone());
        }

        private static void Part1(Vector[][] input)
        {
            var points = input.Select(CalculateTurningPoints).Select(FindAllPointsOnPath).ToArray();

            var crossings = FindCrossingPoints(points[0], points[1]);

            var distanceOfClosest = FindClosestToOrigin(crossings);

            Console.WriteLine(distanceOfClosest);
        }

        private static void Part2(Vector[][] input)
        {
        }

        [Test]
        [TestCase("R75, D30, R83, U83, L12, D49, R71, U7, L72", "U62, R66, U55, R34, D71, R55, D58, R83", 159)]
        [TestCase("R98,U47,R26,D63,R33,U87,L62,D20,R33,U53,R51", "U98, R91, D20, R16, D67, R40, U7, R15, U6, R7", 135)]
        public void Test(string a, string b, int expectedDistance)
        {
            var vectorsA = InputTransformDay3.ParseLines(a);
            var vectorsB = InputTransformDay3.ParseLines(b);
            var pointsA = FindAllPointsOnPath(CalculateTurningPoints(vectorsA));
            var pointsB = FindAllPointsOnPath(CalculateTurningPoints(vectorsB));
            var crossings = FindCrossingPoints(pointsA, pointsB);
            var distanceOfClosest = FindClosestToOrigin(crossings);
            Assert.That(distanceOfClosest, Is.EqualTo(expectedDistance));
        }

        public static Point[] CalculateTurningPoints(Vector[] vectors)
        {
            var p = new Point(0, 0);
            var points = new List<Point>(vectors.Length);
            foreach (Vector v in vectors)
            {
                p = p.CalculateNext(v);
                points.Add(p);
            }
            return points.ToArray();
        }
        public static Point[] FindAllPointsOnPath(Point[] turningPoints)
        {
            Point a = turningPoints[0];
            List<Point> points = new List<Point>();
            foreach (var b in turningPoints)
            {
                points.Add(a);
                if (a.X > b.X)
                {
                    for (int x = a.X; x > b.X; x--)
                    {
                        points.Add(new Point(x, a.Y));
                    }
                }
                if (a.X < b.X)
                {
                    for (int x = a.X; x < b.X; x++)
                    {
                        points.Add(new Point(x, a.Y));
                    }
                }
                if (a.Y > b.Y)
                {
                    for (int y = a.Y; y > b.Y; y--)
                    {
                        points.Add(new Point(a.X, y));
                    }
                }
                if (a.Y < b.Y)
                {
                    for (int y = a.Y; y < b.Y; y++)
                    {
                        points.Add(new Point(a.X, y));
                    }
                }
                a = b;
            }
            return points.ToArray();
        }
        public static Point[] FindCrossingPoints(Point[] a, Point[] b)
        {
            return a.Where(b.Contains).ToArray();
        }
        public static int FindClosestToOrigin(Point[] points)
        {
            var origin = new Point(0, 0);
            return points.Min(p => p.DistanceTo(origin));
        }
    }
}

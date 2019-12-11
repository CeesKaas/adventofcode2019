using NUnit.Framework;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Day10
{
    public class Day10
    {
        public static void Execute()
        {
            var input = InputGetter.GetInputForDay(10);
            var grid = PrepareGrid(input);

            var best = FindAsteroidWithMostVisibleOthers(grid);
            Console.WriteLine(best.VisibleOthers);
            var shotsFired = DestroyAsteroidsInOrder(grid, best.Asteroid);
            Console.WriteLine(shotsFired[199].Location);
        }

        private static Dictionary<Point, Asteroid> PrepareGrid(string input)
        {
            Dictionary<Point, Asteroid> asteroids = new Dictionary<Point, Asteroid>();
            string[] lines = input.Split('\n');
            for (int i = 0; i < lines.Length; i++)
            {
                for (int j = 0; j < lines[i].Length; j++)
                {
                    switch (lines[i][j])
                    {
                        case '#':
                            Asteroid item = new Asteroid(j, i);
                            asteroids.Add(item.Location, item);
                            break;
                        case '.':
                            break;
                    }
                }
            }
            return asteroids;
        }

        public static (Asteroid Asteroid, int VisibleOthers) FindAsteroidWithMostVisibleOthers(Dictionary<Point, Asteroid> asteroids)
        {
            (Asteroid Asteroid, int VisibleOthers) best = ((Asteroid)null, 0);
            foreach (var asteroid in asteroids.Values)
            {
                var uniqueSlopes = asteroids.Values.Except(new[] { asteroid }).Select(_ => asteroid.GetAngleTo(_)).Distinct().Count();

                if (best.VisibleOthers < uniqueSlopes)
                {
                    best.Asteroid = asteroid;
                    best.VisibleOthers = uniqueSlopes;
                }
            }
            return best;
        }
        public static List<Asteroid> DestroyAsteroidsInOrder(Dictionary<Point, Asteroid> asteroids, Asteroid firingLocation)
        {
            var angles = asteroids.Values.Except(new[] { firingLocation }).Select<Asteroid, (Asteroid Asteroid, double Angle)>(_ => (_, firingLocation.GetAngleTo(_))).OrderBy(_ => _.Angle).GroupBy(_ => _.Angle);
            Dictionary<double, List<Asteroid>> firingSequence = new Dictionary<double, List<Asteroid>>();
            foreach (var a in angles)
            {
                firingSequence.Add(a.Key, a.Select(_ => _.Asteroid).OrderBy(_ => _.DistanceTo(firingLocation)).ToList());
            }
            List<Asteroid> destroyedAsteroids = new List<Asteroid>();
            bool shotFired;
            do
            {
                shotFired = false;
                foreach (var item in firingSequence)
                {
                    if (item.Value.Any())
                    {
                        Console.WriteLine($"{item.Key}");
                        var target = item.Value.First();
                        item.Value.Remove(target);
                        destroyedAsteroids.Add(target);
                        shotFired = true;
                    }
                }
            } while (shotFired);
            return destroyedAsteroids;
        }

        [Test]
        [TestCase(".#..#\n.....\n#####\n....#\n...##", 3, 4, 8)]
        [TestCase("......#.#.\n#..#.#....\n..#######.\n.#.#.###..\n.#..#.....\n..#....#.#\n#..#....#.\n.##.#..###\n##...#..#.\n.#....####", 5, 8, 33)]
        [TestCase("#.#...#.#.\n.###....#.\n.#....#...\n##.#.#.#.#\n....#.#.#.\n.##..###.#\n..#...##..\n..##....##\n......#...\n.####.###.", 1, 2, 35)]
        [TestCase(".#..#..###\n####.###.#\n....###.#.\n..###.##.#\n##.##.#.#.\n....###..#\n..#.#..#.#\n#..#.#.###\n.##...##.#\n.....#.#..", 6, 3, 41)]
        [TestCase(".#..##.###...#######\n##.############..##.\n.#.######.########.#\n.###.#######.####.#.\n#####.##.#.##.###.##\n..#####..#.#########\n####################\n#.####....###.#.#.##\n##.#################\n#####.##.###..####..\n..######..##.#######\n####.##.####...##..#\n.#####..#.######.###\n##...#.##########...\n#.##########.#######\n.####.#.###.###.#.##\n....##.##.###..#####\n.#.#.###########.###\n#.#.#.#####.####.###\n###.##.####.##.#..##", 11, 13, 210)]
        public void Test1(string input, int expectedX, int expectedY, int expectedVisible)
        {
            var grid = PrepareGrid(input);

            var best = FindAsteroidWithMostVisibleOthers(grid);

            Assert.That(best.Asteroid.Location, Is.EqualTo(new Point(expectedX, expectedY)), "Wrong Location found");

            Assert.That(best.VisibleOthers, Is.EqualTo(expectedVisible), "Wrong VisibleOthers found");
        }

        [Test]
        public void TestPart2()
        {
            var grid = PrepareGrid(".#....#####...#..\n##...##.#####..##\n##...#...#.#####.\n..#.....#...###..\n..#.#.....#....##");
            var location = grid[new Point(8, 3)];

            var order = DestroyAsteroidsInOrder(grid, location);

            Assert.That(order[0].Location, Is.EqualTo(new Point(8, 1)));
            Assert.That(order[1].Location, Is.EqualTo(new Point(9, 0)));
            Assert.That(order[2].Location, Is.EqualTo(new Point(9, 1)));
            Assert.That(order[3].Location, Is.EqualTo(new Point(10, 0)));
            Assert.That(order[4].Location, Is.EqualTo(new Point(9, 2)));
            Assert.That(order[5].Location, Is.EqualTo(new Point(11, 1)));
            Assert.That(order[6].Location, Is.EqualTo(new Point(12, 1)));
            Assert.That(order[7].Location, Is.EqualTo(new Point(11, 2)));
            Assert.That(order[8].Location, Is.EqualTo(new Point(15, 1)));
            Assert.That(order[9+0].Location, Is.EqualTo(new Point(12, 2)));
            Assert.That(order[9+1].Location, Is.EqualTo(new Point(13, 2)));
            Assert.That(order[9+2].Location, Is.EqualTo(new Point(14, 2)));
            Assert.That(order[9+3].Location, Is.EqualTo(new Point(15, 2)));
            Assert.That(order[9+4].Location, Is.EqualTo(new Point(12, 3)));
            Assert.That(order[9+5].Location, Is.EqualTo(new Point(16, 4)));
            Assert.That(order[9+6].Location, Is.EqualTo(new Point(15, 4)));
            Assert.That(order[9+7].Location, Is.EqualTo(new Point(10, 4)));
            Assert.That(order[9+8].Location, Is.EqualTo(new Point(4, 4)));
        }
        [Test]
        public void TestPart2Bigger()
        {
            var grid = PrepareGrid(".#..##.###...#######\n##.############..##.\n.#.######.########.#\n.###.#######.####.#.\n#####.##.#.##.###.##\n..#####..#.#########\n####################\n#.####....###.#.#.##\n##.#################\n#####.##.###..####..\n..######..##.#######\n####.##.####...##..#\n.#####..#.######.###\n##...#.##########...\n#.##########.#######\n.####.#.###.###.#.##\n....##.##.###..#####\n.#.#.###########.###\n#.#.#.#####.####.###\n###.##.####.##.#..##");
            var location = grid[new Point(11,13)];

            var order = DestroyAsteroidsInOrder(grid, location);

            Assert.That(order[0].Location, Is.EqualTo(new Point(11, 12)));
            Assert.That(order[1].Location, Is.EqualTo(new Point(12, 1)));
            Assert.That(order[2].Location, Is.EqualTo(new Point(12, 2)));
            Assert.That(order[9].Location, Is.EqualTo(new Point(12, 8)));
            Assert.That(order[19].Location, Is.EqualTo(new Point(16, 0)));
            Assert.That(order[49].Location, Is.EqualTo(new Point(16, 9)));
            Assert.That(order[99].Location, Is.EqualTo(new Point(10, 16)));
            Assert.That(order[198].Location, Is.EqualTo(new Point(9, 6)));
            Assert.That(order[199].Location, Is.EqualTo(new Point(8, 2)));
            Assert.That(order[200].Location, Is.EqualTo(new Point(10, 9)));
            Assert.That(order[298].Location, Is.EqualTo(new Point(11, 1)));
        }
    }
}
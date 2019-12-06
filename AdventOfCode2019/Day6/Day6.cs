using NUnit.Framework;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Day6
{
    public class Day6
    {
        public static void Execute()
        {
            var instance = new Day6();
            var input = InputGetter.GetTransformedSplitInputForDay(6, new[] { '\n' }, InputTransformDay6.ParseLines).ToArray();
            Dictionary<string, Planet> orbits = BuildOrbitalMap(input);
            Console.WriteLine("Part 1:" + Part1(orbits).ToString());
            Console.WriteLine("Part 2:" + Part2(orbits).ToString());
        }

        private static int Part1(Dictionary<string, Planet> orbits)
        {
            var sum = 0;
            foreach (var item in orbits.Values)
            {
                sum += RecursiveSum(item);
            }
            return sum;
        }
        private static int Part2(Dictionary<string, Planet> orbits)
        {
            var you = orbits["YOU"];
            var santa = orbits["SAN"];

            var commonPlanet = FirstCommonPlanet(you, santa);

            return commonPlanet.StepsTo(you) + commonPlanet.StepsTo(santa);
        }

        private static Planet FirstCommonPlanet(Planet you, Planet santa)
        {
            List<Planet> seen = new List<Planet>();
            var current = you;
            while (current.Orbits != null)
            {
                seen.Add(current.Orbits);
                current = current.Orbits;
            }
            current = santa;
            while (current.Orbits != null)
            {
                if (seen.Any(_ => _.Name == current.Orbits.Name))
                {
                    return current.Orbits;
                }
                current = current.Orbits;
            }
            return null; //shouldn't happen since everything orbits COM
        }

        private static Dictionary<string, Planet> BuildOrbitalMap((string Obj, string Orbitter)[] input)
        {
            var orbits = new Dictionary<string, Planet>();

            foreach (var item in input)
            {
                if (!orbits.TryGetValue(item.Obj, out var planet))
                {
                    planet = new Planet(item.Obj);
                    orbits.Add(item.Obj, planet);
                }
                if (!orbits.TryGetValue(item.Orbitter, out var orbitter))
                {
                    orbitter = new Planet(item.Orbitter);
                    orbits.Add(item.Orbitter, orbitter);
                }
                if (orbitter.Orbits != null)
                {
                    Console.WriteLine($"{orbitter.Name} already orbits {orbitter.Orbits.Name}");
                }
                orbitter.Orbits = planet;
                planet.Orbiters.Add(orbitter);
            }

            return orbits;
        }

        private static int RecursiveSum(Planet planet)
        {
            var sum = 0;
            foreach (var item in planet.Orbiters)
            {
                sum++;
                sum += RecursiveSum(item);
            }
            return sum;
        }

        [Test]
        public void Part1Test()
        {
            var inputString = "COM)B\nB)C\nC)D\nD)E\nE)F\nB)G\nG)H\nD)I\nE)J\nJ)K\nK)L";
            var input = inputString.Split(new[] { '\n' }).Select(InputTransformDay6.ParseLines).ToArray();
            var result = Part1(BuildOrbitalMap(input));

            Assert.That(result, Is.EqualTo(42));
        }
        [Test]
        public void Part2Test()
        {
            var inputString = "COM)B\nB)C\nC)D\nD)E\nE)F\nB)G\nG)H\nD)I\nE)J\nJ)K\nK)LCOM)B\nK)YOU\nI)SAN";
            var input = inputString.Split(new[] { '\n' }).Select(InputTransformDay6.ParseLines).ToArray();
            var result = Part2(BuildOrbitalMap(input));

            Assert.That(result, Is.EqualTo(4));
        }
    }
    class Planet
    {
        public List<Planet> Orbiters { get; } = new List<Planet>();
        public Planet Orbits { get; set; }
        public string Name { get; }

        public Planet(string name)
        {
            Name = name;
        }

        internal int StepsTo(Planet you)
        {
            if (Orbiters.Any(_ => _.Name == you.Name))
            {
                return 0;
            }
            foreach (var orbiter in Orbiters)
            {
                var stepsToNext = orbiter.StepsTo(you);
                if (stepsToNext >= 0)
                    return stepsToNext + 1;
            }
            return -1;
        }
    }
}
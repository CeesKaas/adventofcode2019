using NUnit.Framework;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace Day12
{
    public class Day12
    {
        public static void Execute()
        {
            var moons = InputGetter.GetTransformedSplitInputForDay(12, new[] { '\n' }, Moon.Parse).ToArray();
            var original = InputGetter.GetTransformedSplitInputForDay(12, new[] { '\n' }, Moon.Parse).ToArray();
            long xPeriod = 0, yPeriod = 0, zPeriod = 0;

            for (long count = 1; xPeriod == 0 || yPeriod == 0 || zPeriod == 0; count++)
            {
                Step(moons);
                if (count == 1000)
                {
                    Console.WriteLine(GetTotalEnergy(moons));
                }
                if (xPeriod == 0 &&
                    moons[0].Velocity.X == 0 && moons[0].Position.X == original[0].Position.X
                    && moons[1].Velocity.X == 0 && moons[1].Position.X == original[1].Position.X
                    && moons[2].Velocity.X == 0 && moons[2].Position.X == original[2].Position.X
                    && moons[3].Velocity.X == 0 && moons[3].Position.X == original[3].Position.X)
                {
                    xPeriod = count;
                    Console.WriteLine("X period = " + count);
                }
                if (yPeriod == 0 &&
                    moons[0].Velocity.Y == 0 && moons[0].Position.Y == original[0].Position.Y
                    && moons[1].Velocity.Y == 0 && moons[1].Position.Y == original[1].Position.Y
                    && moons[2].Velocity.Y == 0 && moons[2].Position.Y == original[2].Position.Y
                    && moons[3].Velocity.Y == 0 && moons[3].Position.Y == original[3].Position.Y)
                {
                    yPeriod = count;
                    Console.WriteLine("Y period = " + count);
                }
                if (zPeriod == 0 &&
                    moons[0].Velocity.Z == 0 && moons[0].Position.Z == original[0].Position.Z
                    && moons[1].Velocity.Z == 0 && moons[1].Position.Z == original[1].Position.Z
                    && moons[2].Velocity.Z == 0 && moons[2].Position.Z == original[2].Position.Z
                    && moons[3].Velocity.Z == 0 && moons[3].Position.Z == original[3].Position.Z)
                {
                    zPeriod = count;
                    Console.WriteLine("Z period = " + count);
                }
            }
            var lcmXY = lcm(xPeriod, yPeriod);
            Console.WriteLine(lcm(lcmXY, zPeriod));
        }

        static long gcf(long a, long b)
        {
            while (b != 0)
            {
                long temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }

        static long lcm(long a, long b)
        {
            return (a / gcf(a, b)) * b;
        }


        static void UpdateAllVelocities(Moon[] moons)
        {
            for (int i = 0; i < moons.Length; i++)
            {
                Moon a = moons[i];
                for (int j = i + 1; j < moons.Length; j++)
                {
                    Moon b = moons[j];
                    Moon.UpdateVelocities(a, b);
                }
            }
        }
        static void Step(Moon[] moons)
        {
            UpdateAllVelocities(moons);
            foreach (Moon moon in moons)
            {
                moon.Step();
            }
        }

        static int GetTotalEnergy(Moon[] moons)
        {
            return moons.Sum(_ => _.GetCombinedEnergy());
        }

        [Test]
        public void part1test1()
        {
            var moons = new[]{ Moon.Parse("<x=-1, y=0, z=2>"),
            Moon.Parse("<x=2, y=-10, z=-7>"),
            Moon.Parse("<x=4, y=-8, z=8>"),
            Moon.Parse("<x=3, y=5, z=-1>")};

            Assert.That(moons[0].ToString(), Is.EqualTo("pos=<x=-1, y=0, z=2>, vel=<x=0, y=0, z=0>"));
            Assert.That(moons[1].ToString(), Is.EqualTo("pos=<x=2, y=-10, z=-7>, vel=<x=0, y=0, z=0>"));
            Assert.That(moons[2].ToString(), Is.EqualTo("pos=<x=4, y=-8, z=8>, vel=<x=0, y=0, z=0>"));
            Assert.That(moons[3].ToString(), Is.EqualTo("pos=<x=3, y=5, z=-1>, vel=<x=0, y=0, z=0>"));

            Step(moons);
            Assert.That(moons[0].ToString(), Is.EqualTo("pos=<x=2, y=-1, z=1>, vel=<x=3, y=-1, z=-1>"));
            Assert.That(moons[1].ToString(), Is.EqualTo("pos=<x=3, y=-7, z=-4>, vel=<x=1, y=3, z=3>"));
            Assert.That(moons[2].ToString(), Is.EqualTo("pos=<x=1, y=-7, z=5>, vel=<x=-3, y=1, z=-3>"));
            Assert.That(moons[3].ToString(), Is.EqualTo("pos=<x=2, y=2, z=0>, vel=<x=-1, y=-3, z=1>"));

            Step(moons);
            //After 2 steps:
            Assert.That(moons[0].ToString(), Is.EqualTo("pos=<x=5, y=-3, z=-1>, vel=<x=3, y=-2, z=-2>"));
            Assert.That(moons[1].ToString(), Is.EqualTo("pos=<x=1, y=-2, z=2>, vel=<x=-2, y=5, z=6>"));
            Assert.That(moons[2].ToString(), Is.EqualTo("pos=<x=1, y=-4, z=-1>, vel=<x=0, y=3, z=-6>"));
            Assert.That(moons[3].ToString(), Is.EqualTo("pos=<x=1, y=-4, z=2>, vel=<x=-1, y=-6, z=2>"));

            Step(moons);
            //After 3 steps:
            Assert.That(moons[0].ToString(), Is.EqualTo("pos=<x=5, y=-6, z=-1>, vel=<x=0, y=-3, z=0>"));
            Assert.That(moons[1].ToString(), Is.EqualTo("pos=<x=0, y=0, z=6>, vel=<x=-1, y=2, z=4>"));
            Assert.That(moons[2].ToString(), Is.EqualTo("pos=<x=2, y=1, z=-5>, vel=<x=1, y=5, z=-4>"));
            Assert.That(moons[3].ToString(), Is.EqualTo("pos=<x=1, y=-8, z=2>, vel=<x=0, y=-4, z=0>"));

            Step(moons);
            //After 4 steps:
            Assert.That(moons[0].ToString(), Is.EqualTo("pos=<x=2, y=-8, z=0>, vel=<x=-3, y=-2, z=1>"));
            Assert.That(moons[1].ToString(), Is.EqualTo("pos=<x=2, y=1, z=7>, vel=<x=2, y=1, z=1>"));
            Assert.That(moons[2].ToString(), Is.EqualTo("pos=<x=2, y=3, z=-6>, vel=<x=0, y=2, z=-1>"));
            Assert.That(moons[3].ToString(), Is.EqualTo("pos=<x=2, y=-9, z=1>, vel=<x=1, y=-1, z=-1>"));

            Step(moons);
            //After 5 steps:
            Assert.That(moons[0].ToString(), Is.EqualTo("pos=<x=-1, y=-9, z=2>, vel=<x=-3, y=-1, z=2>"));
            Assert.That(moons[1].ToString(), Is.EqualTo("pos=<x=4, y=1, z=5>, vel=<x=2, y=0, z=-2>"));
            Assert.That(moons[2].ToString(), Is.EqualTo("pos=<x=2, y=2, z=-4>, vel=<x=0, y=-1, z=2>"));
            Assert.That(moons[3].ToString(), Is.EqualTo("pos=<x=3, y=-7, z=-1>, vel=<x=1, y=2, z=-2>"));

            Step(moons);
            //After 6 steps:
            Assert.That(moons[0].ToString(), Is.EqualTo("pos=<x=-1, y=-7, z=3>, vel=<x=0, y=2, z=1>"));
            Assert.That(moons[1].ToString(), Is.EqualTo("pos=<x=3, y=0, z=0>, vel=<x=-1, y=-1, z=-5>"));
            Assert.That(moons[2].ToString(), Is.EqualTo("pos=<x=3, y=-2, z=1>, vel=<x=1, y=-4, z=5>"));
            Assert.That(moons[3].ToString(), Is.EqualTo("pos=<x=3, y=-4, z=-2>, vel=<x=0, y=3, z=-1>"));

            Step(moons);
            //After 7 steps:
            Assert.That(moons[0].ToString(), Is.EqualTo("pos=<x=2, y=-2, z=1>, vel=<x=3, y=5, z=-2>"));
            Assert.That(moons[1].ToString(), Is.EqualTo("pos=<x=1, y=-4, z=-4>, vel=<x=-2, y=-4, z=-4>"));
            Assert.That(moons[2].ToString(), Is.EqualTo("pos=<x=3, y=-7, z=5>, vel=<x=0, y=-5, z=4>"));
            Assert.That(moons[3].ToString(), Is.EqualTo("pos=<x=2, y=0, z=0>, vel=<x=-1, y=4, z=2>"));

            Step(moons);
            //After 8 steps:
            Assert.That(moons[0].ToString(), Is.EqualTo("pos=<x=5, y=2, z=-2>, vel=<x=3, y=4, z=-3>"));
            Assert.That(moons[1].ToString(), Is.EqualTo("pos=<x=2, y=-7, z=-5>, vel=<x=1, y=-3, z=-1>"));
            Assert.That(moons[2].ToString(), Is.EqualTo("pos=<x=0, y=-9, z=6>, vel=<x=-3, y=-2, z=1>"));
            Assert.That(moons[3].ToString(), Is.EqualTo("pos=<x=1, y=1, z=3>, vel=<x=-1, y=1, z=3>"));

            Step(moons);
            //After 9 steps:
            Assert.That(moons[0].ToString(), Is.EqualTo("pos=<x=5, y=3, z=-4>, vel=<x=0, y=1, z=-2>"));
            Assert.That(moons[1].ToString(), Is.EqualTo("pos=<x=2, y=-9, z=-3>, vel=<x=0, y=-2, z=2>"));
            Assert.That(moons[2].ToString(), Is.EqualTo("pos=<x=0, y=-8, z=4>, vel=<x=0, y=1, z=-2>"));
            Assert.That(moons[3].ToString(), Is.EqualTo("pos=<x=1, y=1, z=5>, vel=<x=0, y=0, z=2>"));

            Step(moons);
            //After 10 steps:
            Assert.That(moons[0].ToString(), Is.EqualTo("pos=<x=2, y=1, z=-3>, vel=<x=-3, y=-2, z=1>"));
            Assert.That(moons[1].ToString(), Is.EqualTo("pos=<x=1, y=-8, z=0>, vel=<x=-1, y=1, z=3>"));
            Assert.That(moons[2].ToString(), Is.EqualTo("pos=<x=3, y=-6, z=1>, vel=<x=3, y=2, z=-3>"));
            Assert.That(moons[3].ToString(), Is.EqualTo("pos=<x=2, y=0, z=4>, vel=<x=1, y=-1, z=-1>"));

            Assert.That(GetTotalEnergy(moons), Is.EqualTo(179));
            int counter = 10;
            while (true)
            {
                if (GetTotalEnergy(moons) == 0 && moons[0].ToString() == "pos=<x=-1, y=0, z=2>, vel=<x=0, y=0, z=0>")
                    break;

                Step(moons);
                counter++;
            }
            Assert.That(counter, Is.EqualTo(2772));
            Console.WriteLine(counter);
        }
        [Test]
        public void part1test2()
        {
            var moons = new[]{ Moon.Parse("<x=-8, y=-10, z=0>"),
            Moon.Parse("<x=5, y=5, z=10>"),
            Moon.Parse("<x=2, y=-7, z=3>"),
            Moon.Parse("<x=9, y=-8, z=-3>")};
            //After 0 steps:
            Assert.That(moons[0].ToString(), Is.EqualTo("pos=<x=-8, y=-10, z=0>, vel=<x=0, y=0, z=0>"));
            Assert.That(moons[1].ToString(), Is.EqualTo("pos=<x=5, y=5, z=10>, vel=<x=0, y=0, z=0>"));
            Assert.That(moons[2].ToString(), Is.EqualTo("pos=<x=2, y=-7, z=3>, vel=<x=0, y=0, z=0>"));
            Assert.That(moons[3].ToString(), Is.EqualTo("pos=<x=9, y=-8, z=-3>, vel=<x=0, y=0, z=0>"));

            Repeat(10, () => Step(moons));
            //After 10 steps:
            Assert.That(moons[0].ToString(), Is.EqualTo("pos=<x=-9, y=-10, z=1>, vel=<x=-2, y=-2, z=-1>"));
            Assert.That(moons[1].ToString(), Is.EqualTo("pos=<x=4, y=10, z=9>, vel=<x=-3, y=7, z=-2>"));
            Assert.That(moons[2].ToString(), Is.EqualTo("pos=<x=8, y=-10, z=-3>, vel=<x=5, y=-1, z=-2>"));
            Assert.That(moons[3].ToString(), Is.EqualTo("pos=<x=5, y=-10, z=3>, vel=<x=0, y=-4, z=5>"));

            Repeat(10, () => Step(moons));
            //After 20 steps:
            Assert.That(moons[0].ToString(), Is.EqualTo("pos=<x=-10, y=3, z=-4>, vel=<x=-5, y=2, z=0>"));
            Assert.That(moons[1].ToString(), Is.EqualTo("pos=<x=5, y=-25, z=6>, vel=<x=1, y=1, z=-4>"));
            Assert.That(moons[2].ToString(), Is.EqualTo("pos=<x=13, y=1, z=1>, vel=<x=5, y=-2, z=2>"));
            Assert.That(moons[3].ToString(), Is.EqualTo("pos=<x=0, y=1, z=7>, vel=<x=-1, y=-1, z=2>"));

            Repeat(10, () => Step(moons));
            //After 30 steps:
            Assert.That(moons[0].ToString(), Is.EqualTo("pos=<x=15, y=-6, z=-9>, vel=<x=-5, y=4, z=0>"));
            Assert.That(moons[1].ToString(), Is.EqualTo("pos=<x=-4, y=-11, z=3>, vel=<x=-3, y=-10, z=0>"));
            Assert.That(moons[2].ToString(), Is.EqualTo("pos=<x=0, y=-1, z=11>, vel=<x=7, y=4, z=3>"));
            Assert.That(moons[3].ToString(), Is.EqualTo("pos=<x=-3, y=-2, z=5>, vel=<x=1, y=2, z=-3>"));

            Repeat(10, () => Step(moons));
            //After 40 steps:
            Assert.That(moons[0].ToString(), Is.EqualTo("pos=<x=14, y=-12, z=-4>, vel=<x=11, y=3, z=0>"));
            Assert.That(moons[1].ToString(), Is.EqualTo("pos=<x=-1, y=18, z=8>, vel=<x=-5, y=2, z=3>"));
            Assert.That(moons[2].ToString(), Is.EqualTo("pos=<x=-5, y=-14, z=8>, vel=<x=1, y=-2, z=0>"));
            Assert.That(moons[3].ToString(), Is.EqualTo("pos=<x=0, y=-12, z=-2>, vel=<x=-7, y=-3, z=-3>"));

            Repeat(10, () => Step(moons));
            //After 50 steps:
            Assert.That(moons[0].ToString(), Is.EqualTo("pos=<x=-23, y=4, z=1>, vel=<x=-7, y=-1, z=2>"));
            Assert.That(moons[1].ToString(), Is.EqualTo("pos=<x=20, y=-31, z=13>, vel=<x=5, y=3, z=4>"));
            Assert.That(moons[2].ToString(), Is.EqualTo("pos=<x=-4, y=6, z=1>, vel=<x=-1, y=1, z=-3>"));
            Assert.That(moons[3].ToString(), Is.EqualTo("pos=<x=15, y=1, z=-5>, vel=<x=3, y=-3, z=-3>"));

            Repeat(10, () => Step(moons));
            //After 60 steps:
            Assert.That(moons[0].ToString(), Is.EqualTo("pos=<x=36, y=-10, z=6>, vel=<x=5, y=0, z=3>"));
            Assert.That(moons[1].ToString(), Is.EqualTo("pos=<x=-18, y=10, z=9>, vel=<x=-3, y=-7, z=5>"));
            Assert.That(moons[2].ToString(), Is.EqualTo("pos=<x=8, y=-12, z=-3>, vel=<x=-2, y=1, z=-7>"));
            Assert.That(moons[3].ToString(), Is.EqualTo("pos=<x=-18, y=-8, z=-2>, vel=<x=0, y=6, z=-1>"));

            Repeat(10, () => Step(moons));
            //After 70 steps:
            Assert.That(moons[0].ToString(), Is.EqualTo("pos=<x=-33, y=-6, z=5>, vel=<x=-5, y=-4, z=7>"));
            Assert.That(moons[1].ToString(), Is.EqualTo("pos=<x=13, y=-9, z=2>, vel=<x=-2, y=11, z=3>"));
            Assert.That(moons[2].ToString(), Is.EqualTo("pos=<x=11, y=-8, z=2>, vel=<x=8, y=-6, z=-7>"));
            Assert.That(moons[3].ToString(), Is.EqualTo("pos=<x=17, y=3, z=1>, vel=<x=-1, y=-1, z=-3>"));

            Repeat(10, () => Step(moons));
            //After 80 steps:
            Assert.That(moons[0].ToString(), Is.EqualTo("pos=<x=30, y=-8, z=3>, vel=<x=3, y=3, z=0>"));
            Assert.That(moons[1].ToString(), Is.EqualTo("pos=<x=-2, y=-4, z=0>, vel=<x=4, y=-13, z=2>"));
            Assert.That(moons[2].ToString(), Is.EqualTo("pos=<x=-18, y=-7, z=15>, vel=<x=-8, y=2, z=-2>"));
            Assert.That(moons[3].ToString(), Is.EqualTo("pos=<x=-2, y=-1, z=-8>, vel=<x=1, y=8, z=0>"));

            Repeat(10, () => Step(moons));
            //After 90 steps:
            Assert.That(moons[0].ToString(), Is.EqualTo("pos=<x=-25, y=-1, z=4>, vel=<x=1, y=-3, z=4>"));
            Assert.That(moons[1].ToString(), Is.EqualTo("pos=<x=2, y=-9, z=0>, vel=<x=-3, y=13, z=-1>"));
            Assert.That(moons[2].ToString(), Is.EqualTo("pos=<x=32, y=-8, z=14>, vel=<x=5, y=-4, z=6>"));
            Assert.That(moons[3].ToString(), Is.EqualTo("pos=<x=-1, y=-2, z=-8>, vel=<x=-3, y=-6, z=-9>"));

            Repeat(10, () => Step(moons));
            //After 100 steps:
            Assert.That(moons[0].ToString(), Is.EqualTo("pos=<x=8, y=-12, z=-9>, vel=<x=-7, y=3, z=0>"));
            Assert.That(moons[1].ToString(), Is.EqualTo("pos=<x=13, y=16, z=-3>, vel=<x=3, y=-11, z=-5>"));
            Assert.That(moons[2].ToString(), Is.EqualTo("pos=<x=-29, y=-11, z=-1>, vel=<x=-3, y=7, z=4>"));
            Assert.That(moons[3].ToString(), Is.EqualTo("pos=<x=16, y=-13, z=23>, vel=<x=7, y=1, z=1>"));

            Assert.That(GetTotalEnergy(moons), Is.EqualTo(1940));
        }
        public static void Repeat(int times, Action action)
        {
            for (int i = 0; i < times; i++)
            {
                action();
            }
        }
    }
}
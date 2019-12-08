using NUnit.Framework;
using Shared;
using System;
using System.Linq;

namespace Day8
{
    public class Day8
    {
        public static void Execute()
        {
            var input = InputTransformDay8.ParseLines(InputGetter.GetInputForDay(8).Trim());
            Part1(input);
            Part2(input);
        }

        private static void Part1(ReadOnlySpan<int> input)
        {
            var image = Image.Parse(input, 25, 6);
            var layerWithFewest0 = image.Layers.OrderBy(_ => _.Rows.Sum(_ => _.Count(_ => _ == 0))).First();
            var count1 = layerWithFewest0.Rows.Sum(_ => _.Count(_ => _ == 1));
            var count2 = layerWithFewest0.Rows.Sum(_ => _.Count(_ => _ == 2));
            Console.WriteLine($"part1: {count1 * count2}");
        }

        private static void Part2(ReadOnlySpan<int> input)
        {
            var image = Image.Parse(input, 25, 6).Rasterize();
            Console.WriteLine($"part2");
            foreach (var r in image.Layers[0].Rows)
            {
                Console.WriteLine(new string(r.Select(_ => _ == 0 ? ' ' : '█').ToArray()));
            }
        }

        [Test]
        public void TestPart1()
        {
            var image = Image.Parse(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2 }, 3, 2);

            Assert.That(image.Layers.Count(), Is.EqualTo(2));
            Assert.That(image.Layers[0].Rows.Count(), Is.EqualTo(2));
            Assert.That(image.Layers[0].Rows[0], Is.EquivalentTo(new[] { 1, 2, 3 }));
            Assert.That(image.Layers[0].Rows[1], Is.EquivalentTo(new[] { 4, 5, 6 }));
            Assert.That(image.Layers[1].Rows.Count(), Is.EqualTo(2));
            Assert.That(image.Layers[1].Rows[0], Is.EquivalentTo(new[] { 7, 8, 9 }));
            Assert.That(image.Layers[1].Rows[1], Is.EquivalentTo(new[] { 0, 1, 2 }));
        }
        [Test]
        public void TestPart2()
        {
            var image = Image.Parse(InputTransformDay8.ParseLines("0222112222120000"), 2, 2);
            var plain = image.Rasterize();
            Assert.That(plain.Layers.Count(), Is.EqualTo(1));
            Assert.That(plain.Layers[0].Rows.Count(), Is.EqualTo(2));
            Assert.That(plain.Layers[0].Rows[0], Is.EquivalentTo(new[] { 0, 1 }));
            Assert.That(plain.Layers[0].Rows[1], Is.EquivalentTo(new[] { 1, 0 }));
        }
    }
}
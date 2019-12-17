using NUnit.Framework;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Day16
{
    public class Day16
    {
        public static void Execute()
        {
            var input = InputGetter.GetInputForDay(16).Trim();
            Console.WriteLine(FFTPart1(input));
            Console.WriteLine(FFTPart2(input));
        }

        public static int[] Parse(IEnumerable<char> input)
        {
            return input.Select(_ => (_ - '0')).ToArray();
        }

        private static int[] _out;
        private static int[] _intermediate;
        private static int[] _patternBase;
        public static int[] FFT(int[] input, int[] pattern, int phases)
        {
            _out = input.ToArray();
            _patternBase = pattern;

            for (int phase = 0; phase < phases; phase++)
            {
                _intermediate = new int[_out.Length];

                var chunks = Enumerable.Range(0, _out.Length).GroupBy(_ => _ % 7).Select(_ => _.ToArray()).ToArray();
                var chunkProcessors = new List<Task>();
                foreach (var chunkToProccess in chunks)
                {
                    chunkProcessors.Add(Task.Factory.StartNew(oChunk =>
                    {
                        var chunk = (int[])oChunk;
                        foreach (var index in chunk)
                        {
                            var sum = 0;

                            for (int itemIndex = 0; itemIndex < _out.Length; itemIndex++)
                            {
                                var patternItem = GetPatternIndex(index + 1, itemIndex, _patternBase.Length);
                                if (patternItem == 0 || patternItem == 2) continue;
                                sum += _out[itemIndex] * _patternBase[patternItem];
                            }

                            _intermediate[index] = (Math.Abs(sum) % 10);
                        }
                    }, chunkToProccess, TaskCreationOptions.LongRunning));
                }
                Task.WaitAll(chunkProcessors.ToArray());
                _out = _intermediate;
            }

            return _out;
        }

        public static int[] FFT2(int[] input, int[] pattern, int phases, int interrestedIn)
        {
            for (int phase = 0; phase < phases; phase++)
            {
                for (int i = input.Length - 1; i > 0; i--)
                {
                    input[i - 1] = (input[i - 1] + input[i]) % 10;
                }
            }

            return input;
        }

        private static int[] PreparePattern(int[] inputPattern, int repetitions, int requiredItems)
        {
            var output = new List<int>(requiredItems + 1);
            while (output.Count < requiredItems + 1)
            {
                foreach (var item in inputPattern)
                {
                    for (int j = 0; j < repetitions; j++)
                    {
                        output.Add(item);
                        if (output.Count == requiredItems + 1)
                        {
                            return output.Skip(1).ToArray();
                        }
                    }
                }
            }
            return output.Skip(1).ToArray();
        }

        [Test]
        public void TestPreparePattern()
        {
            int[] start = { 1, 2, 3, 4, 5, 6, 7, 8 };
            Assert.That(PreparePattern(start, 1, 1), Is.EquivalentTo(new[] { 2 }));
            Assert.That(PreparePattern(start, 1, 10), Is.EquivalentTo(new[] { 2, 3, 4, 5, 6, 7, 8, 1, 2, 3 }));
            Assert.That(PreparePattern(start, 2, 10), Is.EquivalentTo(new[] { 1, 2, 2, 3, 3, 4, 4, 5, 5, 6 }));
            Assert.That(PreparePattern(start, 4, 10), Is.EquivalentTo(new[] { 1, 1, 1, 2, 2, 2, 2, 3, 3, 3 }));
        }

        private static int GetPatternIndex(int repetitions, int itemIndex, int itemsInPattern)
        {
            try
            {
                return (((itemIndex + 1) % (repetitions * itemsInPattern)) / repetitions) % itemsInPattern;
            }
            catch (DivideByZeroException)
            {
                Console.WriteLine($"divide by zero (not sure how): repetitions:{repetitions}, itemIndex:{itemIndex}, itemsInPattern:{itemsInPattern}");
                return 0;
            }
        }

        [Test]
        public void TestGetPatternIndex()
        {
            int[] start = { 1, 2, 3, 4, 5, 6, 7, 8 };
            //first round
            Assert.That(GetPatternIndex(1, 0, 8), Is.EqualTo(1));
            Assert.That(GetPatternIndex(1, 1, 8), Is.EqualTo(2));
            Assert.That(GetPatternIndex(1, 2, 8), Is.EqualTo(3));
            Assert.That(GetPatternIndex(1, 3, 8), Is.EqualTo(4));
            Assert.That(GetPatternIndex(1, 4, 8), Is.EqualTo(5));
            Assert.That(GetPatternIndex(1, 5, 8), Is.EqualTo(6));
            Assert.That(GetPatternIndex(1, 6, 8), Is.EqualTo(7));
            Assert.That(GetPatternIndex(1, 7, 8), Is.EqualTo(0));
            Assert.That(GetPatternIndex(1, 8, 8), Is.EqualTo(1));
            Assert.That(GetPatternIndex(1, 9, 8), Is.EqualTo(2));
            Assert.That(GetPatternIndex(1, 10, 8), Is.EqualTo(3));
            Assert.That(GetPatternIndex(1, 11, 8), Is.EqualTo(4));
            //second round
            Assert.That(GetPatternIndex(2, 0, 8), Is.EqualTo(0));
            Assert.That(GetPatternIndex(2, 1, 8), Is.EqualTo(1));
            Assert.That(GetPatternIndex(2, 2, 8), Is.EqualTo(1));
            Assert.That(GetPatternIndex(2, 3, 8), Is.EqualTo(2));
            Assert.That(GetPatternIndex(2, 4, 8), Is.EqualTo(2));
            Assert.That(GetPatternIndex(2, 5, 8), Is.EqualTo(3));
            Assert.That(GetPatternIndex(2, 6, 8), Is.EqualTo(3));
            Assert.That(GetPatternIndex(2, 7, 8), Is.EqualTo(4));
            Assert.That(GetPatternIndex(2, 8, 8), Is.EqualTo(4));
            Assert.That(GetPatternIndex(2, 9, 8), Is.EqualTo(5));
            Assert.That(GetPatternIndex(2, 10, 8), Is.EqualTo(5));
            Assert.That(GetPatternIndex(2, 11, 8), Is.EqualTo(6));
            //forth round
            Assert.That(GetPatternIndex(4, 0, 8), Is.EqualTo(0));
            Assert.That(GetPatternIndex(4, 1, 8), Is.EqualTo(0));
            Assert.That(GetPatternIndex(4, 2, 8), Is.EqualTo(0));
            Assert.That(GetPatternIndex(4, 3, 8), Is.EqualTo(1));
            Assert.That(GetPatternIndex(4, 4, 8), Is.EqualTo(1));
            Assert.That(GetPatternIndex(4, 5, 8), Is.EqualTo(1));
            Assert.That(GetPatternIndex(4, 6, 8), Is.EqualTo(1));
            Assert.That(GetPatternIndex(4, 7, 8), Is.EqualTo(2));
            Assert.That(GetPatternIndex(4, 8, 8), Is.EqualTo(2));
            Assert.That(GetPatternIndex(4, 9, 8), Is.EqualTo(2));
            Assert.That(GetPatternIndex(4, 10, 8), Is.EqualTo(2));
            Assert.That(GetPatternIndex(4, 11, 8), Is.EqualTo(3));
        }

        [Test]
        public void TestFFT1()
        {
            int[] start = { 1, 2, 3, 4, 5, 6, 7, 8 };
            var result = FFT(start, new int[] { 0, 1, 0, -1 }, 1);
            Assert.That(result, Is.EquivalentTo(new[] { 4, 8, 2, 2, 6, 1, 5, 8 }));
            result = FFT(start, new int[] { 0, 1, 0, -1 }, 2);
            Assert.That(result, Is.EquivalentTo(new[] { 3, 4, 0, 4, 0, 4, 3, 8 }));
            result = FFT(start, new int[] { 0, 1, 0, -1 }, 3);
            Assert.That(result, Is.EquivalentTo(new[] { 0, 3, 4, 1, 5, 5, 1, 8 }));
            result = FFT(start, new int[] { 0, 1, 0, -1 }, 4);
            Assert.That(result, Is.EquivalentTo(new[] { 0, 1, 0, 2, 9, 4, 9, 8 }));
        }
        [Test]
        public void TestFFT2()
        {
            Assert.That(FFTPart1("80871224585914546619083218645595"), Is.EqualTo("24176176"));
            Assert.That(FFTPart1("19617804207202209144916044189917"), Is.EqualTo("73745418"));
            Assert.That(FFTPart1("69317163492948606335995924319873"), Is.EqualTo("52432133"));
        }
        [Test]
        public void TestFFTPart2()
        {
            Assert.That(FFTPart2("03036732577212944063491565474664"), Is.EqualTo("84462026"));
            Assert.That(FFTPart2("02935109699940807407585447034323"), Is.EqualTo("78725270"));
            Assert.That(FFTPart2("03081770884921959731165446850517"), Is.EqualTo("53553731"));
        }

        private static string FFTPart1(string v)
        {
            return string.Join("", FFT(Parse(v), new int[] { 0, 1, 0, -1 }, 100).Take(8));
        }
        private static string FFTPart2(string v)
        {
            var signal = Enumerable.Repeat(v, 10000).SelectMany(s => s.Select(_ => (int)(_ - '0'))).ToArray();
            var offset = int.Parse(v[0..7]);
            Console.WriteLine(offset);
            return string.Join("", FFT2(signal, new int[] { 0, 1, 0, -1 }, 100, offset).Skip(offset).Take(8));
        }
    }
}
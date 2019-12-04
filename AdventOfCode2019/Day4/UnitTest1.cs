using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Day4
{
    public class Day4
    {
        public void Execute()
        {
            Part1(235741, 706948);
            Part2(235741, 706948);
        }

        private void Part1(int start, int end)
        {
            Console.WriteLine(Enumerable.Range(start, end - start).Select(_ => _.ToString()).Count(IsValid));
        }

        private void Part2(int start, int end)
        {
            Console.WriteLine(Enumerable.Range(start, end - start).Select(_ => _.ToString()).Count(IsValid2));
        }

        [Test]
        [TestCase("122345", true)]
        [TestCase("111123", true)]
        [TestCase("111111", true)]
        [TestCase("223450", false)]
        [TestCase("123789", false)]
        public void Part1Tests(string input, bool expectedValid)
        {
            Assert.That(IsValid(input), Is.EqualTo(expectedValid));
        }

        private bool IsValid(string input)
        {
            if (input.Length != 6)
                return false;

            //ever increasing
            char[] original = input.ToCharArray();
            char[] ordered = input.OrderBy(_ => _).ToArray();
            char prev = '\0';
            bool doubleSeen = false;
            for (int i = 0; i < 6; i++)
            {
                var a = original[i];
                var b = ordered[i];
                if (a == prev)
                {
                    doubleSeen = true;
                }
                if (a != b)
                    return false;
                prev = a;
            }

            //at least one double
            if (!doubleSeen)
                return false;
            return true;
        }

        [Test]
        [TestCase("112233", true)]
        [TestCase("111122", true)]
        [TestCase("123444", false)]
        public void Part2Tests(string input, bool expectedValid)
        {
            Assert.That(IsValid2(input), Is.EqualTo(expectedValid));
        }

        private bool IsValid2(string input)
        {
            if (input.Length != 6)
                return false;

            //ever increasing
            char[] original = input.ToCharArray();
            char[] ordered = input.OrderBy(_ => _).ToArray();

            Dictionary<char, int> counts = new Dictionary<char, int>();
            for (int i = 0; i < 6; i++)
            {
                var a = original[i];
                var b = ordered[i];
                if (a != b)
                    return false;

                if (!counts.ContainsKey(a))
                {
                    counts[a] = 0;
                }
                counts[a]++;
            }

            //at least one double
            if (!counts.Values.Contains(2))
                return false;
            return true;
        }
    }
}
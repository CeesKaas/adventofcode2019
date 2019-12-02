using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace Day2
{
    public static class Day2
    {
        public static void Execute()
        {
            var input = InputGetter.GetTransformedSplitInputForDay(2, new[] { ',' }, InputTransformDay2.ParseLines).ToArray();
            Part1((int[])input.Clone());
            Part2((int[])input.Clone());
        }

        private static void Part2(int[] input)
        {
            int noun, verb;
            tryUntilValueFound(input, 19690720, out noun, out verb);
            Console.WriteLine($"{noun:00}{verb:00}");
        }

        private static void tryUntilValueFound(int[] source, int value, out int noun, out int verb)
        {
            noun = -1;
            verb = -1;
            var input = new int[source.Length];
            for (noun = 0; noun <= 99; noun++)
                for (verb = 0; verb <= 99; verb++)
                {
                    Array.Copy(source, 0, input, 0, source.Length);
                    input[1] = noun;
                    input[2] = verb;
                    var result = new Computer().Calculate(input);
                    if (result[0] == value) return;
                }
        }

        private static void Part1(int[] input)
        {
            input[1] = 12;
            input[2] = 2;
            var result = new Computer().Calculate(input);
            Console.WriteLine(result[0]);
        }
    }
}

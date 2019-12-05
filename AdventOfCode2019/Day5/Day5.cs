using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace Day5
{
    public static class Day5
    {
        public static void Execute()
        {
            var input = InputGetter.GetTransformedSplitInputForDay(5, new[] { ',' }, InputTransformDay5.ParseLines).ToArray();
            //Part1((int[])input.Clone());
            Part2(input);
        }

        private static void Part1(int[] input)
        {
            Computer computer = new Computer();
            computer.Inputs.Enqueue(1);
            var result = computer.Calculate(input);
            while (computer.Outputs.TryDequeue(out var output))
            {
                Console.WriteLine(output);
            }
        }
        private static void Part2(int[] input)
        {
            Computer computer = new Computer();
            computer.Inputs.Enqueue(5);
            var result = computer.Calculate(input);
            while (computer.Outputs.TryDequeue(out var output))
            {
                Console.WriteLine(output);
            }
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
    }
}

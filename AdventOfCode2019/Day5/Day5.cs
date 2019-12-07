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
    }
}

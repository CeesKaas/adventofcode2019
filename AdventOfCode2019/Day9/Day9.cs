using Shared;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Day9
{
    public static class Day9
    {
        public static void Execute()
        {
            var input = InputGetter.GetTransformedSplitInputForDay(9, new[] { ',' }, InputTransformDay9.ParseItems).ToArray();
            Part1(input.ToArray()).GetAwaiter().GetResult();
            Part2(input.ToArray()).GetAwaiter().GetResult();
        }

        private static async Task Part1(long[] input)
        {
            var computer = new Computer(input);
            computer.Inputs.Add(1);
            await computer.Wait();

            var outputs = computer.Outputs.ToArray();
            if (outputs.Length == 1)
            {
                Console.WriteLine($"Success: {outputs[0]}");
            }
            else
            {
                foreach (var brokenOpcode in outputs)
                Console.WriteLine($"brokenOpcode: {brokenOpcode}");
            }
        }
        private static async Task Part2(long[] input)
        {
            var computer = new Computer(input);
            computer.Inputs.Add(2);
            await computer.Wait();

            var outputs = computer.Outputs.ToArray();
            if (outputs.Length == 1)
            {
                Console.WriteLine($"Success: {outputs[0]}");
            }
        }
    }
}

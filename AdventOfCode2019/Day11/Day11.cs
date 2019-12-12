using Shared;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Day11
{
    public static class Day11
    {
        public static void Execute()
        {
            var input = InputGetter.GetTransformedSplitInputForDay(11, new[] { ',' }, InputTransformDay11.ParseItems).ToArray();
            Part1(input.ToArray()).GetAwaiter().GetResult();
        }

        private static async Task Part1(long[] input)
        {
            var paintBot = new PaintBot();
            var computer = new Computer(input, paintBot, paintBot);
            await computer.Wait();
            Console.WriteLine(paintBot.TouchedPanels());
        }
    }
}

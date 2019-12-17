using Shared;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace Day15
{
    public static class Day15
    {
        public static void Execute()
        {
            Console.CursorVisible = false;
            var input = InputGetter.GetTransformedSplitInputForDay(15, new[] { ',' }, InputTransformDay15.ParseItems).ToArray();
            Part1(ref input);
            /*Console.Clear();
            Part2(ref input);*/

            Console.CursorVisible = true;
        }

        private static void Part1(ref long[] input)
        {
            var computerInput = new BlockingCollectionInputOutput();
            var computerOutput = new BlockingCollectionInputOutput();
            var robot = new RepairRobot(computerInput, computerOutput);
            var computer = new Computer(input, computerInput, computerOutput);
            robot.Start();
            computer.Wait().GetAwaiter().GetResult();
            Console.SetCursorPosition(0, 25);
        }
        private static void Part2(ref long[] input)
        {
        }
    }
}

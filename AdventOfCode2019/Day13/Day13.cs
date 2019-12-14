using Shared;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace Day13
{
    public static class Day13
    {
        public static void Execute()
        {
            var input = InputGetter.GetTransformedSplitInputForDay(13, new[] { ',' }, InputTransformDay13.ParseItems).ToArray();
            Part1(ref input);
            Console.Clear();
            Part2(ref input);
        }

        private static void Part1(ref long[] input)
        {
            var arcade = new Arcade();
            var joystick = new JoyStick();
            var computer = new Computer(input, joystick, arcade);
            computer.Wait().GetAwaiter().GetResult();
            Console.SetCursorPosition(0, 25);
            Console.WriteLine(arcade.BlocksLeft());
            input = computer.GetCurrentState();
        }
        private static void Part2(ref long[] input)
        {
            var arcade = new Arcade();
            var joystick = new AutoJoyStick();
            input[0] = 2;
            var computer = new Computer(input, joystick, arcade);
            computer.Wait().GetAwaiter().GetResult();
            Console.SetCursorPosition(0, 30);
            Console.WriteLine(arcade.BlocksLeft());
        }
    }
}

using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Day1
{
    public static class Day1
    {
        public static void Execute()
        {
            var input = InputGetter.GetTransformedSplitInputForDay(1, new[] { '\n' }, InputTransformDay1.ParseLines);
            Part1(input);
            Part2(input);
        }

        public static void Part1(ICollection<int> input)
        {
            var fuelRequired = input.Select(FuelCalculator.Calculate).Sum();
            Console.WriteLine($"FuelRequired = {fuelRequired} (Part1)");
        }

        public static void Part2(ICollection<int> input)
        {
            var fuelRequired = input.Select(FuelCalculator.CalculateRecursivly).Sum();
            Console.WriteLine($"FuelRequired = {fuelRequired} (Part2)");
        }
    }
}

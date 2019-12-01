using System;

namespace Day1
{
    public class FuelCalculator
    {
        public static int Calculate(int input)
        {
            return (int)Math.Floor(input / 3f) - 2;
        }
        public static int CalculateRecursivly(int input)
        {
            var intermediate = Calculate(input);
            if (intermediate <= 0) return 0;
            return intermediate + CalculateRecursivly(intermediate);
        }
    }
}
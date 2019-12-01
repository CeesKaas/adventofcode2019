using Day1;
using NUnit.Framework;

namespace Day1Tests
{
    public class Tests
    {
        [Test]
        [TestCase(12, 2)]
        [TestCase(14, 2)]
        [TestCase(1969, 654)]
        [TestCase(100756, 33583)]
        public void FuelCalculations(int input, int expectedOutput)
        {
            Assert.That(FuelCalculator.Calculate(input), Is.EqualTo(expectedOutput));
        }
        [Test]
        [TestCase(12, 2)]
        [TestCase(14, 2)]
        [TestCase(1969, 966)]
        [TestCase(100756, 50346)]
        public void FuelCalculationRecursively(int input, int expectedOutput)
        {
            Assert.That(FuelCalculator.CalculateRecursivly(input), Is.EqualTo(expectedOutput));
        }
    }
}
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Day2
{
    public class UnitTest
    {
        [Test]
        [TestCase("1,0,0,0,99", "2,0,0,0,99")]
        [TestCase("2,3,0,3,99", "2,3,0,6,99")]
        [TestCase("2,4,4,5,99,0", "2,4,4,5,99,9801")]
        [TestCase("1,1,1,4,99,5,6,0,99", "30,1,1,4,2,5,6,0,99")]
        [TestCase("1,9,10,3,2,3,11,0,99,30,40,50", "3500,9,10,70,2,3,11,0,99,30,40,50")]
        public void ComputerTest(string input, string outputExpected)
        {
            Computer c = new Computer();
            var parsedInput = input.Split(',').Select((s, index) => (index, int.Parse(s))).ToDictionary(_ => _.Item1, _ => _.Item2);
            var result = c.Calculate(parsedInput);
            var stringifiedResult = string.Join(",", result.Values);
            Assert.That(stringifiedResult, Is.EqualTo(outputExpected));
        }
    }
}

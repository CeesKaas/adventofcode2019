using NUnit.Framework;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day13
{
    public class UnitTest
    {
        [Test]
        public void DiagnosticProgram()
        {
            var program = "1102,34463338,34463338,63,1007,63,34463338,63,1005,63,53,1101,0,3,1000,109,988,209,12,9,1000,209,6,209,3,203,0,1008,1000,1,63,1005,63,65,1008,1000,2,63,1005,63,904,1008,1000,0,63,1005,63,58,4,25,104,0,99,4,0,104,0,99,4,17,104,0,99,0,0,1102,1,432,1027,1101,439,0,1026,1101,0,36,1010,1101,0,34,1018,1102,278,1,1029,1101,0,24,1002,1102,1,20,1016,1102,1,31,1011,1102,319,1,1024,1102,21,1,1012,1102,1,763,1022,1102,1,25,1007,1101,0,287,1028,1102,32,1,1008,1101,0,22,1013,1102,38,1,1001,1101,0,314,1025,1102,35,1,1009,1102,1,23,1015,1102,39,1,1019,1102,27,1,1000,1102,1,37,1003,1102,1,28,1017,1101,0,0,1020,1101,0,29,1004,1102,1,30,1006,1102,1,756,1023,1102,1,33,1005,1101,0,1,1021,1102,26,1,1014,109,13,2108,28,-7,63,1005,63,201,1001,64,1,64,1105,1,203,4,187,1002,64,2,64,109,8,21107,40,41,-3,1005,1018,225,4,209,1001,64,1,64,1105,1,225,1002,64,2,64,109,-3,1206,2,239,4,231,1105,1,243,1001,64,1,64,1002,64,2,64,109,-21,1201,6,0,63,1008,63,35,63,1005,63,267,1001,64,1,64,1105,1,269,4,249,1002,64,2,64,109,35,2106,0,-4,4,275,1001,64,1,64,1105,1,287,1002,64,2,64,109,-11,1205,-1,303,1001,64,1,64,1105,1,305,4,293,1002,64,2,64,109,8,2105,1,-5,4,311,1106,0,323,1001,64,1,64,1002,64,2,64,109,-7,21108,41,38,-6,1005,1016,339,1106,0,345,4,329,1001,64,1,64,1002,64,2,64,109,2,21102,42,1,-8,1008,1016,45,63,1005,63,369,1001,64,1,64,1105,1,371,4,351,1002,64,2,64,109,-14,21101,43,0,1,1008,1011,43,63,1005,63,397,4,377,1001,64,1,64,1106,0,397,1002,64,2,64,109,-8,21101,44,0,8,1008,1010,47,63,1005,63,417,1105,1,423,4,403,1001,64,1,64,1002,64,2,64,109,25,2106,0,0,1001,64,1,64,1105,1,441,4,429,1002,64,2,64,109,-20,2107,37,-6,63,1005,63,463,4,447,1001,64,1,64,1106,0,463,1002,64,2,64,109,8,2108,25,-8,63,1005,63,485,4,469,1001,64,1,64,1106,0,485,1002,64,2,64,109,-1,21107,45,44,-1,1005,1013,505,1001,64,1,64,1106,0,507,4,491,1002,64,2,64,109,-11,1207,-1,25,63,1005,63,529,4,513,1001,64,1,64,1106,0,529,1002,64,2,64,109,23,1206,-5,545,1001,64,1,64,1106,0,547,4,535,1002,64,2,64,109,-31,2102,1,5,63,1008,63,27,63,1005,63,569,4,553,1106,0,573,1001,64,1,64,1002,64,2,64,109,27,21102,46,1,-9,1008,1013,46,63,1005,63,595,4,579,1105,1,599,1001,64,1,64,1002,64,2,64,109,-26,2101,0,6,63,1008,63,24,63,1005,63,625,4,605,1001,64,1,64,1106,0,625,1002,64,2,64,109,5,1208,0,37,63,1005,63,645,1001,64,1,64,1105,1,647,4,631,1002,64,2,64,109,7,2102,1,-3,63,1008,63,31,63,1005,63,671,1001,64,1,64,1105,1,673,4,653,1002,64,2,64,109,2,1202,-5,1,63,1008,63,33,63,1005,63,699,4,679,1001,64,1,64,1105,1,699,1002,64,2,64,109,-4,2101,0,-3,63,1008,63,35,63,1005,63,719,1105,1,725,4,705,1001,64,1,64,1002,64,2,64,109,-5,1207,4,32,63,1005,63,741,1106,0,747,4,731,1001,64,1,64,1002,64,2,64,109,29,2105,1,-7,1001,64,1,64,1106,0,765,4,753,1002,64,2,64,109,-26,2107,36,5,63,1005,63,781,1105,1,787,4,771,1001,64,1,64,1002,64,2,64,109,10,1201,-6,0,63,1008,63,32,63,1005,63,809,4,793,1106,0,813,1001,64,1,64,1002,64,2,64,109,3,21108,47,47,-5,1005,1012,835,4,819,1001,64,1,64,1106,0,835,1002,64,2,64,109,-24,1202,9,1,63,1008,63,25,63,1005,63,859,1001,64,1,64,1106,0,861,4,841,1002,64,2,64,109,19,1205,9,875,4,867,1106,0,879,1001,64,1,64,1002,64,2,64,109,-3,1208,-1,32,63,1005,63,897,4,885,1106,0,901,1001,64,1,64,4,64,99,21102,27,1,1,21101,915,0,0,1105,1,922,21201,1,60043,1,204,1,99,109,3,1207,-2,3,63,1005,63,964,21201,-2,-1,1,21102,1,942,0,1106,0,922,21202,1,1,-1,21201,-2,-3,1,21101,957,0,0,1106,0,922,22201,1,-1,-2,1105,1,968,22102,1,-2,-2,109,-3,2105,1,0".Split(',').Select(long.Parse).ToArray();

            var io = new BlockingCollectionInputOutput();
            Computer c = new Computer(program, io, io);
            io.WriteOutput(1);

            c.Wait().GetAwaiter().GetResult();
            var outputs = io.Collection.ToArray();
            Assert.That(outputs, Is.EquivalentTo(new[] { 3063082071 }));
        }

        [Test]
        [TestCase("1,0,0,0,99", "2,0,0,0,99")]
        [TestCase("2,3,0,3,99", "2,3,0,6,99")]
        [TestCase("2,4,4,5,99,0", "2,4,4,5,99,9801")]
        [TestCase("1,1,1,4,99,5,6,0,99", "30,1,1,4,2,5,6,0,99")]
        [TestCase("1,9,10,3,2,3,11,0,99,30,40,50", "3500,9,10,70,2,3,11,0,99,30,40,50")]
        [TestCase("1002,4,3,4,33", "1002,4,3,4,99")]
        [TestCase("1101,100,-1,4,0", "1101,100,-1,4,99")]
        public async Task ComputerDay2Test(string input, string outputExpected)
        {
            var program = input.Split(',').Select(long.Parse).ToArray();
            var io = new BlockingCollectionInputOutput();
            Computer c = new Computer(program, io, io);
            await c.Wait();
            var stringifiedResult = string.Join(",", c.GetCurrentState().Take(program.Length));
            Assert.That(stringifiedResult, Is.EqualTo(outputExpected));
        }
        [Test]
        [TestCase("3,9,8,9,10,9,4,9,99,-1,8", 8, 1)]
        [TestCase("3,9,8,9,10,9,4,9,99,-1,8", 9, 0)]
        [TestCase("3,9,7,9,10,9,4,9,99,-1,8", 7, 1)]
        [TestCase("3,9,7,9,10,9,4,9,99,-1,8", 9, 0)]
        [TestCase("3,3,1108,-1,8,3,4,3,99", 8, 1)]
        [TestCase("3,3,1108,-1,8,3,4,3,99", 9, 0)]
        [TestCase("3,3,1107,-1,8,3,4,3,99", 7, 1)]
        [TestCase("3,3,1107,-1,8,3,4,3,99", 9, 0)]
        [TestCase("3,12,6,12,15,1,13,14,13,4,13,99,-1,0,1,9", 0, 0)]
        [TestCase("3,12,6,12,15,1,13,14,13,4,13,99,-1,0,1,9", 9, 1)]
        [TestCase("3,3,1105,-1,9,1101,0,0,12,4,12,99,1", 0, 0)]
        [TestCase("3,3,1105,-1,9,1101,0,0,12,4,12,99,1", 9, 1)]
        [TestCase("3,21,1008,21,8,20,1005,20,22,107,8,21,20,1006,20,31,1106,0,36,98,0,0,1002,21,125,20,4,20,1105,1,46,104,999,1105,1,46,1101,1000,1,20,4,20,1105,1,46,98,99", 1, 999)]
        [TestCase("3,21,1008,21,8,20,1005,20,22,107,8,21,20,1006,20,31,1106,0,36,98,0,0,1002,21,125,20,4,20,1105,1,46,104,999,1105,1,46,1101,1000,1,20,4,20,1105,1,46,98,99", 8, 1000)]
        [TestCase("3,21,1008,21,8,20,1005,20,22,107,8,21,20,1006,20,31,1106,0,36,98,0,0,1002,21,125,20,4,20,1105,1,46,104,999,1105,1,46,1101,1000,1,20,4,20,1105,1,46,98,99", 9, 1001)]
        public async Task ComputerDay5Test(string input, int firstInput, int expectedOutput)
        {
            var program = input.Split(',').Select(long.Parse).ToArray();
            var io = new BlockingCollectionInputOutput();
            var c = new Computer(program.ToArray(), io, io);
            io.WriteOutput(firstInput);
            await c.Wait();
            var firstOutput = io.ReadInput();
            Assert.That(firstOutput, Is.EqualTo(expectedOutput));
        }

        [Test]
        [TestCase("3,15,3,16,1002,16,10,16,1,16,15,15,4,15,99,0,0", 43210, 4, 3, 2, 1, 0)]
        [TestCase("3,23,3,24,1002,24,10,24,1002,23,-1,23,101,5,23,23,1,24,23,23,4,23,99,0,0", 54321, 0, 1, 2, 3, 4)]
        [TestCase("3,31,3,32,1002,32,10,32,1001,31,-2,31,1007,31,0,33,1002,33,7,33,1,33,31,31,1,32,31,31,4,31,99,0,0,0", 65210, 1, 0, 4, 3, 2)]
        public async Task ComputerDay7Test(string input, int expectedOutput, int amp1input, int amp2input, int amp3input, int amp4input, int amp5input)
        {
            var program = input.Split(',').Select(long.Parse).ToArray();
            var io = new BlockingCollectionInputOutput();
            var io1 = new BlockingCollectionInputOutput();
            var io2 = new BlockingCollectionInputOutput();
            var io3 = new BlockingCollectionInputOutput();
            var io4 = new BlockingCollectionInputOutput();
            var io5 = new BlockingCollectionInputOutput();
            Computer amp1 = new Computer(program.ToArray(), io, io1);
            Computer amp2 = new Computer(program.ToArray(), io1, io2);
            Computer amp3 = new Computer(program.ToArray(), io2, io3);
            Computer amp4 = new Computer(program.ToArray(), io3, io4);
            Computer amp5 = new Computer(program.ToArray(), io4, io5);


            io.WriteOutput(amp1input);
            io.WriteOutput(0);
            io1.WriteOutput(amp2input);
            io2.WriteOutput(amp3input);
            io3.WriteOutput(amp4input);
            io4.WriteOutput(amp5input);
            await Task.WhenAll(amp1.Wait(), amp2.Wait(), amp3.Wait(), amp4.Wait(), amp5.Wait());
            var firstOutput = io5.ReadInput();

            Assert.That(firstOutput, Is.EqualTo(expectedOutput));
        }
        [Test]
        [TestCase("3,26,1001,26,-4,26,3,27,1002,27,2,27,1,27,26,27,4,27,1001,28,-1,28,1005,28,6,99,0,0,5", 139629729, 9, 8, 7, 6, 5)]
        [TestCase("3,52,1001,52,-5,52,3,53,1,52,56,54,1007,54,5,55,1005,55,26,1001,54,-5,54,1105,1,12,1,53,54,53,1008,54,0,55,1001,55,1,55,2,53,55,53,4,53,1001,56,-1,56,1005,56,6,99,0,0,0,0,10", 18216, 9, 7, 8, 5, 6)]
        public async Task ComputerDay7Part2Test(string input, int expectedOutput, int amp1input, int amp2input, int amp3input, int amp4input, int amp5input)
        {
            var program = input.Split(',').Select(long.Parse).ToArray();
            var io1 = new BlockingCollectionInputOutput();
            var io2 = new BlockingCollectionInputOutput();
            var io3 = new BlockingCollectionInputOutput();
            var io4 = new BlockingCollectionInputOutput();
            var io5 = new BlockingCollectionInputOutput();
            Computer amp1 = new Computer(program.ToArray(), io5, io1);
            Computer amp2 = new Computer(program.ToArray(), io1, io2);
            Computer amp3 = new Computer(program.ToArray(), io2, io3);
            Computer amp4 = new Computer(program.ToArray(), io3, io4);
            Computer amp5 = new Computer(program.ToArray(), io4, io5);


            io5.WriteOutput(amp1input);
            io5.WriteOutput(0);
            io1.WriteOutput(amp2input);
            io2.WriteOutput(amp3input);
            io3.WriteOutput(amp4input);
            io4.WriteOutput(amp5input);
            await Task.WhenAll(amp1.Wait(), amp2.Wait(), amp3.Wait(), amp4.Wait(), amp5.Wait());
            var firstOutput = io5.ReadInput();
            Assert.That(firstOutput, Is.EqualTo(expectedOutput));
        }
        [Test]
        public async Task ComputerDay9Test1()
        {
            var input = "109,1,204,-1,1001,100,1,100,1008,100,16,101,1006,101,0,99";
            var program = input.Split(',').Select(long.Parse).ToArray();
            var io = new BlockingCollectionInputOutput();
            var computer = new Computer(program.ToArray(), io, io);
            await computer.Wait();
            long[] firstOutput = io.Collection.ToArray();
            Assert.That(firstOutput, Is.EquivalentTo(program));
        }
        [Test]
        public async Task ComputerDay9Test2()
        {
            var input = "1102,34915192,34915192,7,4,7,99,0";
            var program = input.Split(',').Select(long.Parse).ToArray();
            var io = new BlockingCollectionInputOutput();
            var computer = new Computer(program.ToArray(), io, io);
            await computer.Wait();
            string firstOutput = io.Collection.Take().ToString();
            Console.WriteLine(firstOutput);
            Assert.That(firstOutput, Has.Length.EqualTo(16));
        }
        [Test]
        public async Task ComputerDay9Test3()
        {
            var input = "104,1125899906842624,99";
            var program = input.Split(',').Select(long.Parse).ToArray();
            var io = new BlockingCollectionInputOutput();
            var computer = new Computer(program.ToArray(), io, io);
            await computer.Wait();
            long firstOutput = io.Collection.Take();
            Assert.That(firstOutput, Is.EqualTo(1125899906842624));
        }
    }
}

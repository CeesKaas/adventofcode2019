using Shared;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Day7
{
    public static class Day7
    {
        public static void Execute()
        {
            var input = InputGetter.GetTransformedSplitInputForDay(7, new[] { ',' }, InputTransformDay7.ParseLines).ToArray();
            Part1(input.ToArray()).GetAwaiter().GetResult();
            Part2(input.ToArray()).GetAwaiter().GetResult();
        }

        private static async Task Part1(int[] input)
        {
            int max = 0;
            for (int i = 0; i <= 4; i++)
            {
                for (int j = 0; j <= 4; j++)
                {
                    if (i == j) continue;
                    for (int k = 0; k <= 4; k++)
                    {
                        if (i == k || j == k) continue;
                        for (int l = 0; l <= 4; l++)
                        {
                            if (i == l || j == l || k == l) continue;
                            for (int m = 0; m <= 4; m++)
                            {
                                if (i == m || j == m || k == m || l == m) continue;
                                var current = await Run1(i, j, k, l, m, input);
                                Console.WriteLine($"i={i}, j={j}, k={k}, l={l}, m={m}, current={current}");
                                max = Math.Max(max, current);
                            }
                        }
                    }
                }
            }
            Console.WriteLine(max);
        }
        private static async Task Part2(int[] input)
        {
            int max = 0;
            for (int i = 5; i <= 9; i++)
            {
                for (int j = 5; j <= 9; j++)
                {
                    if (i == j) continue;
                    for (int k = 5; k <= 9; k++)
                    {
                        if (i == k || j == k) continue;
                        for (int l = 5; l <= 9; l++)
                        {
                            if (i == l || j == l || k == l) continue;
                            for (int m = 5; m <= 9; m++)
                            {
                                if (i == m || j == m || k == m || l == m) continue;
                                var current = await Run2(i, j, k, l, m, input);
                                Console.WriteLine($"i={i}, j={j}, k={k}, l={l}, m={m}, current={current}");
                                max = Math.Max(max, current);
                            }
                        }
                    }
                }
            }
            Console.WriteLine(max);
        }

        public static async Task<int> Run1(int amp1input, int amp2input, int amp3input, int amp4input, int amp5input, int[] program)
        {
            Computer amp1 = new Computer(program.ToArray());
            Computer amp2 = new Computer(program.ToArray());
            Computer amp3 = new Computer(program.ToArray());
            Computer amp4 = new Computer(program.ToArray());
            Computer amp5 = new Computer(program.ToArray());


            amp1.Inputs = new BlockingCollection<int>();
            amp1.Inputs.Add(amp1input);
            amp1.Inputs.Add(0);
            amp2.Inputs = amp1.Outputs;
            amp2.Inputs.Add(amp2input);
            amp3.Inputs = amp2.Outputs;
            amp3.Inputs.Add(amp3input);
            amp4.Inputs = amp3.Outputs;
            amp4.Inputs.Add(amp4input);
            amp5.Inputs = amp4.Outputs;
            amp5.Inputs.Add(amp5input);
            var finalOutput = amp5.Outputs;
            await Task.WhenAll(amp1.Wait(), amp2.Wait(), amp3.Wait(), amp4.Wait(), amp5.Wait());
            var firstOutput = finalOutput.Take();
            return firstOutput;
        }
        public static async Task<int> Run2(int amp1input, int amp2input, int amp3input, int amp4input, int amp5input, int[] program)
        {
            Computer amp1 = new Computer(program.ToArray());
            Computer amp2 = new Computer(program.ToArray());
            Computer amp3 = new Computer(program.ToArray());
            Computer amp4 = new Computer(program.ToArray());
            Computer amp5 = new Computer(program.ToArray());


            amp1.Inputs = amp5.Outputs;
            amp1.Inputs.Add(amp1input);
            amp1.Inputs.Add(0);
            amp2.Inputs = amp1.Outputs;
            amp2.Inputs.Add(amp2input);
            amp3.Inputs = amp2.Outputs;
            amp3.Inputs.Add(amp3input);
            amp4.Inputs = amp3.Outputs;
            amp4.Inputs.Add(amp4input);
            amp5.Inputs = amp4.Outputs;
            amp5.Inputs.Add(amp5input);
            var finalOutput = amp5.Outputs;
            await Task.WhenAll(amp1.Wait(), amp2.Wait(), amp3.Wait(), amp4.Wait(), amp5.Wait());
            var firstOutput = finalOutput.Take();
            return firstOutput;
        }
    }
}

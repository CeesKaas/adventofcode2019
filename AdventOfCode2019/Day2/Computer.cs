using System;
using System.Collections.Generic;
using System.Text;

namespace Day2
{
    class Computer
    {
        public int[] Calculate(int[] input)
        {
            int position = 0;
            while (true)
            {
                switch (input[position])
                {
                    case 1:// addition
                        {
                            var a = input[input[position + 1]];
                            var b = input[input[position + 2]];
                            var destination = input[position + 3];
                            input[destination] = a + b;
                            position += 4;
                        }
                        break;
                    case 2: // multiplication
                        {
                            var a = input[input[position + 1]];
                            var b = input[input[position + 2]];
                            var destination = input[position + 3];
                            input[destination] = a * b;
                            position += 4;
                        }
                        break;
                    case 99:
                        return input;
                    default:
                        throw new NotSupportedException($"operation {input[position]} at {position} was not a valid operation");
                }
            }
        }
    }
}

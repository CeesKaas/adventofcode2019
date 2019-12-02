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
            int parameter1 = 0, parameter2 = 0;
            int destination = 0;
            while (true)
            {
                int opcode = input[position];
                int step;
                switch (opcode)
                {
                    case 1:
                    case 2:
                        {
                            Span<int> parameters = input[(position + 1) .. (position + 4)];
                            parameter1 = input[parameters[0]];
                            parameter2 = input[parameters[1]];
                            destination = parameters[2];
                            step = 4;
                        }
                        break;
                    case 99:
                        return input;
                    default:
                        throw new NotSupportedException($"operation {input[position]} at {position} was not a valid operation");
                }
                switch (opcode)
                {
                    case 1:// addition
                        input[destination] = parameter1 + parameter2;
                        break;
                    case 2: // multiplication
                        input[destination] = parameter1 * parameter2;
                        break;
                }
                position += step;
            }
        }
    }
}

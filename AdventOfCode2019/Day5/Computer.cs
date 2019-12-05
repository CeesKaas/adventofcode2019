using System;
using System.Collections.Generic;
using System.Text;

namespace Day5
{
    class Computer
    {
        public Queue<int> Inputs { get; } = new Queue<int>();
        public Queue<int> Outputs { get; } = new Queue<int>();
        public int[] Calculate(int[] input)
        {
            int instructionPointer = 0;
            while (true)
            {
                int parameter1 = 0, parameter2 = 0, parameter3 = 0;
                int opcode = input[instructionPointer] % 100;
                ParameterMode parameterMode1 = (ParameterMode)((input[instructionPointer] / 100) % 10);
                ParameterMode parameterMode2 = (ParameterMode)((input[instructionPointer] / 1000) % 10);
                ParameterMode parameterMode3 = (ParameterMode)((input[instructionPointer] / 10000) % 10);
                int step;
                switch (opcode)
                {
                    case 1:
                    case 2:
                    case 7:
                    case 8:
                        {
                            parameter1 = GetParameterValue(input, input[instructionPointer + 1], parameterMode1);
                            parameter2 = GetParameterValue(input, input[instructionPointer + 2], parameterMode2);
                            parameter3 = input[instructionPointer + 3];
                            step = 4;
                        }
                        break;
                    case 3:
                        {
                            parameter1 = input[instructionPointer + 1];
                            step = 2;
                        }
                        break;
                    case 4:
                        {
                            parameter1 = GetParameterValue(input, input[instructionPointer + 1], parameterMode1);
                            step = 2;
                        }
                        break;
                    case 5:
                    case 6:
                        {
                            parameter1 = GetParameterValue(input, input[instructionPointer + 1], parameterMode1);
                            parameter2 = GetParameterValue(input, input[instructionPointer + 2], parameterMode2);
                            step = 3;
                        }
                        break;
                    case 99:
                        return input;
                    default:
                        throw new NotSupportedException($"operation {input[instructionPointer]} at {instructionPointer} was not a valid operation");
                }
                switch (opcode)
                {
                    case 1:// Addition
                        input[parameter3] = parameter1 + parameter2;
                        break;
                    case 2: // Multiplication
                        input[parameter3] = parameter1 * parameter2;
                        break;
                    case 3: // Read
                        input[parameter1] = ReadInput();
                        break;
                    case 4: // Write
                        WriteOutput(parameter1);
                        break;
                    case 5: // Jump if true
                        if (parameter1 != 0)
                        {
                            instructionPointer = parameter2;
                            continue;
                        }
                        break;
                    case 6: // Jump if false
                        if (parameter1 == 0)
                        {
                            instructionPointer = parameter2;
                            continue;
                        }
                        break;
                    case 7: // Less then
                        input[parameter3] = parameter1 < parameter2 ? 1 : 0;
                        break;
                    case 8: // Equals
                        input[parameter3] = parameter1 == parameter2 ? 1 : 0;
                        break;
                }
                instructionPointer += step;
            }
        }

        private int GetParameterValue(int[] input, int parameterValueInput, ParameterMode parameterMode)
        {
            switch (parameterMode)
            {
                case ParameterMode.Position:
                    return input[parameterValueInput];
                case ParameterMode.Immediate:
                    return parameterValueInput;
                default:
                    throw new ArgumentOutOfRangeException(nameof(parameterMode));
            }
        }

        private void WriteOutput(int v)
        {
            Outputs.Enqueue(v);
        }

        private int ReadInput()
        {
            return Inputs.Dequeue();
        }
    }
}

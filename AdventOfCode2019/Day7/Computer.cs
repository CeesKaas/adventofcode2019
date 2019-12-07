using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Day7
{
    public class Computer
    {
        private int[] _memoryState;

        private Task _thread;
        public BlockingCollection<int> Inputs { get; set; }
        public BlockingCollection<int> Outputs { get; } = new BlockingCollection<int>();

        public Computer(int[] program)
        {
            _memoryState = program;
        }
        public void Start()
        {
            if (Inputs == null)
            {
                throw new InvalidOperationException("no Inputs defined");
            }
            _thread = Task.Factory.StartNew(() =>
            {
                int instructionPointer = 0;
                while (true)
                {
                    int parameter1 = 0, parameter2 = 0, parameter3 = 0;
                    int opcode = _memoryState[instructionPointer] % 100;
                    ParameterMode parameterMode1 = (ParameterMode)((_memoryState[instructionPointer] / 100) % 10);
                    ParameterMode parameterMode2 = (ParameterMode)((_memoryState[instructionPointer] / 1000) % 10);
                    ParameterMode parameterMode3 = (ParameterMode)((_memoryState[instructionPointer] / 10000) % 10);
                    int step;
                    switch (opcode)
                    {
                        case 1:
                        case 2:
                        case 7:
                        case 8:
                            {
                                parameter1 = GetParameterValue(_memoryState, _memoryState[instructionPointer + 1], parameterMode1);
                                parameter2 = GetParameterValue(_memoryState, _memoryState[instructionPointer + 2], parameterMode2);
                                parameter3 = _memoryState[instructionPointer + 3];
                                step = 4;
                            }
                            break;
                        case 3:
                            {
                                parameter1 = _memoryState[instructionPointer + 1];
                                step = 2;
                            }
                            break;
                        case 4:
                            {
                                parameter1 = GetParameterValue(_memoryState, _memoryState[instructionPointer + 1], parameterMode1);
                                step = 2;
                            }
                            break;
                        case 5:
                        case 6:
                            {
                                parameter1 = GetParameterValue(_memoryState, _memoryState[instructionPointer + 1], parameterMode1);
                                parameter2 = GetParameterValue(_memoryState, _memoryState[instructionPointer + 2], parameterMode2);
                                step = 3;
                            }
                            break;
                        case 99:
                            return;
                        default:
                            throw new NotSupportedException($"operation {_memoryState[instructionPointer]} at {instructionPointer} was not a valid operation");
                    }
                    switch (opcode)
                    {
                        case 1:// Addition
                            _memoryState[parameter3] = parameter1 + parameter2;
                            break;
                        case 2: // Multiplication
                            _memoryState[parameter3] = parameter1 * parameter2;
                            break;
                        case 3: // Read
                            _memoryState[parameter1] = ReadInput();
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
                            _memoryState[parameter3] = parameter1 < parameter2 ? 1 : 0;
                            break;
                        case 8: // Equals
                            _memoryState[parameter3] = parameter1 == parameter2 ? 1 : 0;
                            break;
                    }
                    instructionPointer += step;
                }
            }, TaskCreationOptions.LongRunning);
        }

        public int[] GetFinalState()
        {
            return _memoryState;
        }

        public Task Wait()
        {
            if (_thread == null)
                Start();
            return _thread;
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
            Outputs.Add(v);
        }

        private int ReadInput()
        {
            return Inputs.Take();
        }
    }
}

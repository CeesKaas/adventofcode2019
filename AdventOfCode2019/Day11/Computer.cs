using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day11
{
    public class Computer
    {
        private Dictionary<long, long> _memoryState;

        private Task _thread;
        private long _relativeBase;
        private IOutput _output;
        private IInput _input;

        public Computer(long[] program, IInput input, IOutput output)
        {
            _memoryState = new Dictionary<long, long>(program.Select((value, index) => new KeyValuePair<long, long>(index, value)));
            _input = input;
            _output = output;
        }

        public void Start()
        {
            _thread = Task.Factory.StartNew(() =>
            {
                long instructionPointer = 0;
                while (true)
                {
                    long parameter1 = 0, parameter2 = 0, parameter3 = 0;
                    long opcode = _memoryState[instructionPointer] % 100;
                    ParameterMode parameterMode1 = (ParameterMode)((_memoryState[instructionPointer] / 100) % 10);
                    ParameterMode parameterMode2 = (ParameterMode)((_memoryState[instructionPointer] / 1000) % 10);
                    ParameterMode parameterMode3 = (ParameterMode)((_memoryState[instructionPointer] / 10000) % 10);
                    long step;
                    switch (opcode)
                    {
                        case 1:
                        case 2:
                        case 7:
                        case 8:
                            {
                                parameter1 = GetParameterValue(instructionPointer + 1, parameterMode1);
                                parameter2 = GetParameterValue(instructionPointer + 2, parameterMode2);
                                parameter3 = instructionPointer + 3;
                                step = 4;
                            }
                            break;
                        case 3:
                        case 4:
                        case 9:
                            {
                                parameter1 = instructionPointer + 1;
                                step = 2;
                            }
                            break;
                        case 5:
                        case 6:
                            {
                                parameter1 = GetParameterValue(instructionPointer + 1, parameterMode1);
                                parameter2 = GetParameterValue(instructionPointer + 2, parameterMode2);
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
                            SetParameterValue(parameter1 + parameter2, parameter3, parameterMode3);
                            break;
                        case 2: // Multiplication
                            SetParameterValue(parameter1 * parameter2, parameter3, parameterMode3);
                            break;
                        case 3: // Read
                            SetParameterValue(_input.ReadInput(), parameter1, parameterMode1);
                            break;
                        case 4: // Write
                            _output.WriteOutput(GetParameterValue(parameter1, parameterMode1));
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
                            SetParameterValue(parameter1 < parameter2 ? 1 : 0, parameter3, parameterMode3);
                            break;
                        case 8: // Equals
                            SetParameterValue(parameter1 == parameter2 ? 1 : 0, parameter3, parameterMode3);
                            break;
                        case 9:
                            _relativeBase += GetParameterValue(parameter1, parameterMode1);
                            break;
                    }
                    instructionPointer += step;
                }
            }, TaskCreationOptions.LongRunning);
        }

        public long[] GetCurrentState()
        {
            return _memoryState.Values.ToArray();
        }

        public Task Wait()
        {
            if (_thread == null)
                Start();
            return _thread;
        }

        private long GetParameterValue(long parameterPointer, ParameterMode parameterMode)
        {
            long position;
            switch (parameterMode)
            {
                case ParameterMode.Position:
                    position = GetParameterValue(parameterPointer, ParameterMode.Immediate);
                    break;
                case ParameterMode.Immediate:
                    position = parameterPointer;
                    break;
                case ParameterMode.Relative:
                    position = GetParameterValue(parameterPointer, ParameterMode.Immediate) + _relativeBase;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(parameterMode));
            }
            if (!_memoryState.TryGetValue(position, out var value))
            {
                _memoryState[position] = 0;
                value = 0;
            }
            return value;
        }

        private void SetParameterValue(long value, long parameterPointer, ParameterMode parameterMode)
        {
            long position;
            switch (parameterMode)
            {
                case ParameterMode.Position:
                    position = GetParameterValue(parameterPointer, ParameterMode.Immediate);
                    break;
                case ParameterMode.Relative:
                    position = GetParameterValue(parameterPointer, ParameterMode.Immediate) + _relativeBase;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(parameterMode));
            }
            _memoryState[position] = value;

        }
    }
}

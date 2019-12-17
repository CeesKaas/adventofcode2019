using System;
using System.Collections.Generic;
using System.Text;

namespace IntCodeComputer
{
    public class ConsoleIn : IInput
    {
        public static ConsoleIn Default { get; } = new ConsoleIn();
        public long ReadInput()
        {
            return long.Parse(Console.ReadLine());
        }
    }
    public class ConsoleOut : IOutput
    {
        public static ConsoleOut Default { get; } = new ConsoleOut();

        public void WriteOutput(long value)
        {
            Console.Write((char)value);
        }
    }
}

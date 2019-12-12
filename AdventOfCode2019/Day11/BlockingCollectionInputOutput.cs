using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace Day11
{
    public class BlockingCollectionInputOutput : IInput, IOutput
    {
        public BlockingCollection<long> Collection { get; } = new BlockingCollection<long>();

        public long ReadInput()
        {
            return Collection.Take();
        }

        public void WriteOutput(long value)
        {
            Collection.Add(value);
        }
    }
}

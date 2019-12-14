using System;
using System.Collections.Generic;
using System.Linq;

namespace Day14
{
    public class Reaction
    {
        public (string chemical, int number)[] Input { get; }


        public (string chemical, int number) Output { get; }

        public Reaction((string chemical, int number)[] input, (string chemical, int number) output)
        {
            Input = input;
            Output = output;
        }

        internal void Execute(Dictionary<string, long> store)
        {
            var timesNeeded = (long)Math.Ceiling((store[Output.chemical] * -1f) / Output.number);
            foreach (var (chemical, number) in Input)
            {
                store[chemical] = GetCurrentAmountFromStore(store, chemical) - (number * timesNeeded);
            }
            store[Output.chemical] = GetCurrentAmountFromStore(store, Output.chemical) + (Output.number * timesNeeded);
        }
        private long GetCurrentAmountFromStore(Dictionary<string, long> store, string chemical)
        {
            if (!store.TryGetValue(chemical, out var current))
            {
                store.Add(chemical, 0);
                return 0;
            }
            return current;
        }

        public override string ToString()
        {
            return $"{string.Join(",", Input.Select(_ => $"{_.number} {_.chemical}"))} => {Output.number} {Output.chemical}";
        }
    }
}
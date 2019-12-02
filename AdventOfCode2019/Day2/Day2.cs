using Shared;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Day2
{
    public static class Day2
    {
        public static void Execute()
        {
            var input = InputGetter.GetTransformedSplitInputForDay(2, new[] { ',' }, InputTransformDay2.ParseLines);
            Part1(input);
            Part2(input);
        }

        private static void Part2(ICollection<int> input)
        {
            Dictionary<int, int> source = input.Select((item, index) => (index, item)).ToDictionary(_ => _.Item1, _ => _.Item2);
            int noun, verb;
            tryUntilValueFound(source, 19690720, out noun, out verb);
            Console.WriteLine($"{noun:00}{verb:00}");
        }

        private static void tryUntilValueFound(Dictionary<int, int> source, int value, out int noun, out int verb)
        {
            noun = -1;
            verb = -1;
            for (noun = 0; noun <= 99; noun++)
                for (verb = 0; verb <= 99; verb++)
                {
                    var inputDictionary = source.ToDictionary(_ => _.Key, _ => _.Value);
                    inputDictionary[1] = noun;
                    inputDictionary[2] = verb;
                    var result = new Computer().Calculate(inputDictionary);
                    if (result[0] == value) return;
                }
        }

        private static void Part1(ICollection<int> input)
        {
            Dictionary<int, int> inputDictionary = input.Select((item, index) => (index, item)).ToDictionary(_ => _.Item1, _ => _.Item2);
            inputDictionary[1] = 12;
            inputDictionary[2] = 2;
            var result = new Computer().Calculate(inputDictionary);
            Console.WriteLine(result[0]);
        }
    }
}

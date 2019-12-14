using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Day14
{
    public static class InputTransformDay14
    {
        public static Reaction ParseLine(string input)
        {
            var reaction = new Regex("(?<Input>.*) => (?<Output>.*)", RegexOptions.ExplicitCapture).Match(input);
            var chemical = new Regex("(?<Number>[0-9]+) (?<Chemical>\\S*)", RegexOptions.ExplicitCapture);

            var inputs = reaction.Groups["Input"].Value.Split(",").Select(_ => chemical.Match(_).Groups);
            var output = chemical.Match(reaction.Groups["Output"].Value).Groups;
            return new Reaction(inputs.Select(_ => (_["Chemical"].Value, int.Parse(_["Number"].Value))).ToArray(), (output["Chemical"].Value, int.Parse(output["Number"].Value)));
        }
    }
}

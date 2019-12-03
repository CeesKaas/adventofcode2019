using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Day3
{
    public static class InputTransformDay3
    {
        public static Vector[] ParseLines(string input)
        {
            return input.Split(",").Select(s => s.Trim()).Select(s => new Vector((Direction)Enum.Parse(typeof(Direction), s.Substring(0, 1)), int.Parse(s.Substring(1)))).ToArray();
        }
    }
}

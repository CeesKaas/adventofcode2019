using System;
using System.Collections.Generic;
using System.Text;

namespace Day6
{
    public static class InputTransformDay6
    {
        public static (string, string) ParseLines(string s)
        {
            var split = s.Split(')');
            return (split[0], split[1]);
        }
    }
}

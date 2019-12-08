using System;
using System.Collections.Generic;
using System.Text;

namespace Day8
{
    public static class InputTransformDay8
    {
        public static ReadOnlySpan<int> ParseLines(string s)
        {
            List<int> digits = new List<int>(s.Length);
            foreach (char c in s)
            {
                digits.Add((int)c - (int)'0');
            }
            return new ReadOnlySpan<int>(digits.ToArray());
        }
    }
}

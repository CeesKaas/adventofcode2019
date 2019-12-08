using System;
using System.Collections.Generic;
using System.Linq;

namespace Day8
{
    public class Layer
    {
        public Layer(int width, int height)
        {
            Rows = new List<int[]>(height);
        }

        public List<int[]> Rows { get; }
        public static Layer Parse(ReadOnlySpan<int> inputDigits, int width, int height)
        {
            Layer layer = new Layer(width, height);

            for (int i = 0; i < height; i++)
            {
                layer.Rows.Add(inputDigits.Slice(i * width, width).ToArray());
            }

            return layer;
        }

        internal static Layer Blank(int width, int height)
        {
            Layer layer = new Layer(width, height);

            for (int i = 0; i < height; i++)
            {
                layer.Rows.Add(Enumerable.Repeat(2, width).ToArray());
            }

            return layer;
        }
    }
}
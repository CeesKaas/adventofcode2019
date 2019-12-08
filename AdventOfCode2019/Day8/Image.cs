using System;
using System.Collections.Generic;

namespace Day8
{
    public class Image
    {
        private int Width;
        private int Height;

        public Image(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public List<Layer> Layers { get; } = new List<Layer>();

        internal static Image Parse(ReadOnlySpan<int> inputDigits, int width, int height)
        {
            Image image = new Image(width, height);

            int itemsPerBlock = width * height;

            int current = 0;
            while (current < inputDigits.Length)
            {
                var layer = inputDigits.Slice(current, itemsPerBlock);
                current += itemsPerBlock;
                image.Layers.Add(Layer.Parse(layer, width, height));
            }

            return image;
        }

        internal Image Rasterize()
        {
            Image image = new Image(Width, Height);
            image.Layers.Add(Layer.Blank(Width, Height));
            foreach (var layer in Layers)
            {
                for (int x = 0; x < Width; x++)
                {
                    for (int y = 0; y < Height; y++)
                    {
                        var pixel = layer.Rows[y][x];

                        if (pixel != 2 && image.Layers[0].Rows[y][x] == 2)
                        {
                            image.Layers[0].Rows[y][x] = pixel;
                        }
                    }
                }
            }
            return image;
        }
    }
}
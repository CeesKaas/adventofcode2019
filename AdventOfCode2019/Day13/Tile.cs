using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Day13
{
    public abstract class Tile
    {
        private static int _drawn;

        public Point Point { get; }

        protected Tile(Point point)
        {
            Point = point;
        }

        protected abstract void PrepareDraw();

        public void Draw()
        {
            Console.SetCursorPosition(Point.X, Point.Y);
            PrepareDraw();
            Console.Write('█');
            Thread.Sleep(1);
        }
    }
    public class Empty : Tile
    {
        public Empty(Point point) : base(point)
        {
        }

        protected override void PrepareDraw()
        {
            Console.ForegroundColor = Console.BackgroundColor;
        }
    }
    public class Wall : Tile
    {
        public Wall(Point point) : base(point)
        {
        }

        protected override void PrepareDraw()
        {
            Console.ForegroundColor = ConsoleColor.Red;
        }
    }
    public class Block : Tile
    {
        public Block(Point point) : base(point)
        {
        }

        protected override void PrepareDraw()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
        }
    }
    public class Paddle : Tile
    {
        public Paddle(Point point) : base(point)
        {
            Current = point;
        }

        public static Point Current { get; internal set; }

        protected override void PrepareDraw()
        {
            Console.ForegroundColor = ConsoleColor.Green;
        }
    }
    public class Ball : Tile
    {
        public Ball(Point point) : base(point)
        {
            Current = point;
        }

        public static Point Current { get; internal set; }

        protected override void PrepareDraw()
        {
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Day15
{
    public abstract class Tile
    {
        private void PointToConsole(Point p)
        {
            Console.SetCursorPosition(p.X + Console.WindowWidth / 2, Console.WindowHeight / 2 - p.Y);
        }

        public Point Point { get; }

        protected Tile(Point point)
        {
            Point = point;
        }

        protected abstract void PrepareDraw();

        public void Draw()
        {
            PointToConsole(Point);
            PrepareDraw();
            Console.Write('█');
        }
        public void DrawAsCurrent()
        {
            PointToConsole(Point);
            PrepareDraw();
            Console.Write('Θ');
        }
    }
    public class Empty : Tile
    {
        public Empty(Point point) : base(point)
        {
        }

        protected override void PrepareDraw()
        {
            if (Point.X == 0 && Point.Y == 0)
            {
                Console.ForegroundColor = ConsoleColor.Black;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Gray;
            }
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
    public class OxygenSystem : Tile
    {
        public OxygenSystem(Point point) : base(point)
        {
        }

        protected override void PrepareDraw()
        {
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}

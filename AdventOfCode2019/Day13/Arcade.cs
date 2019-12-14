using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Day13
{
    public class Arcade : IOutput
    {
        private Dictionary<Point, Tile> _panels = new Dictionary<Point, Tile>();
        private long _x;
        private long _y;


        private int _outputCallCount = 0;
        public void WriteOutput(long value)
        {
            switch (_outputCallCount % 3)
            {
                case 0://x
                    _x = value;
                    break;
                case 1://y
                    _y = value;
                    break;
                case 2://tileType
                    if (_x == -1 && _y == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.SetCursorPosition(0, 24);
                        Console.Write(value);
                        break;
                    }
                    Point point = new Point((int)_x, (int)_y);
                    Tile tile;
                    switch (value)
                    {
                        case 0:
                            tile = new Empty(point);
                            break;
                        case 1:
                            tile = new Wall(point);
                            break;
                        case 2:
                            tile = new Block(point);
                            break;
                        case 3:
                            tile = new Paddle(point);
                            break;
                        case 4:
                            tile = new Ball(point);
                            break;
                        default:
                            return;//not known
                    }
                    _panels[point] = tile;
                    tile.Draw();
                    break;
            }
            _outputCallCount++;
        }
        public int BlocksLeft()
        {
            return _panels.Values.OfType<Block>().Count();
        }
    }
}

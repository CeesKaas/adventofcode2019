using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Day11
{
    public class PaintBot : IInput, IOutput
    {
        private Point _currentLocation;
        private int _currentHeading;
        private Dictionary<Point, int> _panels = new Dictionary<Point, int>();

        public PaintBot()
        {
            _panels[new Point(0, 0)] = 1;
        }

        public long ReadInput()
        {
            return GetPanelColor(_currentLocation);
        }

        private int GetPanelColor(Point location)
        {
            if (!_panels.TryGetValue(location, out var currentColor))
            {
                return 0;
            }
            return currentColor;
        }

        private int _outputCallCount = 0;
        public void WriteOutput(long value)
        {
            _outputCallCount++;
            var previousLocation = _currentLocation;
            switch (_outputCallCount % 2)
            {
                case 0://move
                    switch (value)
                    {
                        case 0:
                            _currentHeading = (_currentHeading + 3) % 4;
                            break;
                        case 1:
                            _currentHeading = (_currentHeading + 1) % 4;
                            break;
                    }
                    switch (_currentHeading)
                    {
                        case 0: //▲
                            _currentLocation = new Point(_currentLocation.X, _currentLocation.Y + 1);
                            break;
                        case 1: //►
                            _currentLocation = new Point(_currentLocation.X + 1, _currentLocation.Y);
                            break;
                        case 2: //▼
                            _currentLocation = new Point(_currentLocation.X, _currentLocation.Y - 1);
                            break;
                        case 3: //◄
                            _currentLocation = new Point(_currentLocation.X - 1, _currentLocation.Y);
                            break;
                    }
                    break;
                case 1://paint
                    _panels[_currentLocation] = (int)value;
                    break;
            }
            PointToConsole(previousLocation);
            Console.Write(IntToPanel(GetPanelColor(previousLocation)));
            PointToConsole(_currentLocation);
            Console.Write(IntToHeading(_currentHeading));
            Console.SetCursorPosition(0, 0);
            Thread.Sleep(2);
        }

        private char IntToHeading(int heading)
        {
            switch (heading)
            {
                case 0: return '^';//'▲';
                case 1: return '>';//'►';
                case 2: return 'v';//'▼';
                case 3: return '<';//'◄';
                default: return '0';
            }
        }

        private char IntToPanel(int heading)
        {
            switch (heading)
            {
                case 0: return ' ';
                case 1: return '█';
                default: return '#';
            }
        }

        private void PointToConsole(Point p)
        {
            Console.SetCursorPosition(p.X + Console.WindowWidth / 2, Console.WindowHeight / 2 - p.Y);
        }

        public int TouchedPanels()
        {
            return _panels.Values.Count;
        }
    }
}

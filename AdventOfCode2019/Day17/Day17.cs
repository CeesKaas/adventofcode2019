using IntCodeComputer;
using NUnit.Framework;
using Shared;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Day17
{
    public class Day17
    {
        public static void Execute()
        {
            var input = InputGetter.GetTransformedSplitInputForDay(17, new[] { ',' }, long.Parse).ToArray();
            var stringStream = new MemoryStream();
            var computer = new Computer(input, ConsoleIn.Default, new StreamOutput(new StreamWriter(stringStream)));

            computer.Wait().GetAwaiter().GetResult();

            string secondaryInput = string.Join("", stringStream.GetBuffer().Select(_ => (char)_));
            Console.WriteLine("   0123456789|123456789|123456789|123456789");
            Console.WriteLine(string.Join("\r\n", secondaryInput.Split("\n").Select((line, index) => $"{index,-2}:{line}")));
            bool[][] splitBothWays = secondaryInput.Substring(0, secondaryInput.IndexOf("\n\n")).Split('\n').Select(s => s.Select(c => c != '.').ToArray()).ToArray();
            var crossings = new List<Point>();
            var corners = new List<Point>();
            var ends = new List<Point>();

            int rows = splitBothWays.Length;
            int columns = splitBothWays[0].Length;
            for (int row = 0; row < rows; row++)
            {
                for (int column = 0; column < columns; column++)
                {
                    if (!splitBothWays[row][column]) continue;
                    if (ShouldCheckCrossing(row, column, rows, columns) && splitBothWays[row - 1][column] && splitBothWays[row + 1][column] && splitBothWays[row][column - 1] && splitBothWays[row][column + 1])
                    {
                        crossings.Add(new Point(column, row));
                        continue;
                    }
                    if ((ShouldCheckNW(row, column, rows, columns) && splitBothWays[row - 1][column] && splitBothWays[row][column - 1])
                        || (ShouldCheckSE(row, column, rows, columns) && splitBothWays[row][column + 1] && splitBothWays[row + 1][column])
                        || (ShouldCheckSW(row, column, rows, columns) && splitBothWays[row][column - 1] && splitBothWays[row + 1][column])
                        || (ShouldCheckNE(row, column, rows, columns) && splitBothWays[row][column + 1] && splitBothWays[row - 1][column])
                        )
                    {
                        corners.Add(new Point(column, row));
                    }
                    if (ShouldCheckCrossing(row, column, rows, columns) && new[] { splitBothWays[row - 1][column], splitBothWays[row + 1][column], splitBothWays[row][column - 1], splitBothWays[row][column + 1] }.Count(_ => _) == 1)
                    {
                        ends.Add(new Point(column, row));
                        continue;
                    }
                }
            }

            /*Console.WriteLine(crossings.Select(_ => _.x * _.y).Sum());
            Console.WriteLine("crossings");
            Console.WriteLine(string.Join(Environment.NewLine, crossings));
            Console.WriteLine("corners");
            Console.WriteLine(string.Join(Environment.NewLine, corners));
            Console.WriteLine("ends");
            Console.WriteLine(string.Join(Environment.NewLine, ends));*/

            var lines = new List<Line>();

            Console.WriteLine("lines");
            var point1 = new Point(x: 8, y: 18);
            var lastLine = new Line(new Point(0, 0), new Point(0, 1));

            while (corners.Count() > 0)
            {
                var possibleLines = corners.Where(_ => _.x == point1.x || _.y == point1.y).Select(_ => new Line(point1, _)).Where(_ => _.IsHorizontal != lastLine.IsHorizontal);


                Line item = possibleLines.OrderBy(_ => _.Length).First(_ => splitBothWays[_.NextToA.y][_.NextToA.x]);
                Console.WriteLine(item);
                lines.Add(item);
                point1 = item.b;
                corners.Remove(point1);
                lastLine = item;
            }

            lines.Add(new Line(point1, new Point(22, 14)));

            Console.WriteLine(string.Join(Environment.NewLine, lines));
            input[0] = 2;
            string commands = "A,A,B,C,A,C,B,C,A,B\nL,4,L,10,L,6\nL,6,L,4,R,8,R,8\nL,6,R,8,L,10,L,8,L,8\ny\n";
            var computer2 = new Computer(input, new Stringinput(commands), new StreamOutput(Console.Out));
            Console.Read();
            Console.Clear();

            computer2.Wait().GetAwaiter().GetResult();
        }

        private static int StraightLineDistance((int x, int y) a, (int x, int y) b)
        {
            if (a.x == b.x)
            {
                return Math.Max(a.y, b.y) - Math.Min(a.y, b.y);
            }
            else if (a.y == b.y)
            {
                return Math.Max(a.x, b.x) - Math.Min(a.x, b.x);
            }
            else
            {
                return int.MaxValue;
            }
        }

        private static bool ShouldCheckCrossing(int row, int column, int rows, int columns)
        {
            return row > 0 && column > 0 && row < rows - 1 && column < columns - 1;
        }
        private static bool ShouldCheckNE(int row, int column, int rows, int columns)
        {
            return row > 0 && column < columns - 1;
        }
        private static bool ShouldCheckSE(int row, int column, int rows, int columns)
        {
            return row < rows - 1 && column < columns - 1;
        }
        private static bool ShouldCheckSW(int row, int column, int rows, int columns)
        {
            return column > 0 && row < rows - 1;
        }
        private static bool ShouldCheckNW(int row, int column, int rows, int columns)
        {
            return row > 0 && column > 0;
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
    public class Stringinput : IInput
    {
        private StreamReader _streamReader;

        public Stringinput(string s)
        {
            _streamReader = new StreamReader(new MemoryStream(Encoding.ASCII.GetBytes(s)));
        }

        public long ReadInput()
        {
            return _streamReader.Read();
        }
    }
    public class StreamOutput : IOutput
    {
        private TextWriter _streamWriter;

        public StreamOutput(TextWriter streamWriter)
        {
            _streamWriter = streamWriter;
        }
        bool LastWasNewLine = false;
        public void WriteOutput(long value)
        {
            if (value > 128)
            {
                _streamWriter.Write($"\n\n{value}");
            }
            else
            {
                _streamWriter.Write((char)value);
            }
            _streamWriter.Flush();

            if (LastWasNewLine && value == 10)
            {
                Console.SetCursorPosition(0, 0);
            }
            LastWasNewLine = value == 10;
        }
    }

    internal struct Point
    {
        public int x;
        public int y;

        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public override string ToString()
        {
            return $"[{x,2},{y,2}]";
        }
    }

    internal struct Line
    {
        public Point a;
        public Point b;

        public bool IsHorizontal => a.y == b.y;

        public Line(Point a, Point b)
        {
            this.a = a;
            this.b = b;
        }
        public Point NextToA => IsHorizontal ? new Point(a.x + (a.x < b.x ? 1 : -1), a.y) : new Point(a.x, a.y + (a.y < b.y ? 1 : -1));

        public int Length
        {
            get
            {
                if (a.x == b.x)
                {
                    return Math.Max(a.y, b.y) - Math.Min(a.y, b.y);
                }
                else if (a.y == b.y)
                {
                    return Math.Max(a.x, b.x) - Math.Min(a.x, b.x);
                }
                else
                {
                    return int.MaxValue;
                }
            }
        }
        public override string ToString()
        {
            return $"<{a},{b}>: {Length,2}";
        }
    }
}
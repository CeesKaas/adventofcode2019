using Day12;
using Day13;
using System;
using System.Diagnostics;

namespace AdventOfCode2019
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var s = Stopwatch.StartNew();
            Day13.Day13.Execute();
            s.Stop();
            Console.WriteLine($"Done! (took {s.Elapsed})");

            Console.ReadLine();
        }

    }
}

using Day1;
using Day2;
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
            Day2.Day2.Execute();
            s.Stop();
            Console.WriteLine($"Done! (took {s.Elapsed})");
            Console.ReadLine();
        }

    }
}

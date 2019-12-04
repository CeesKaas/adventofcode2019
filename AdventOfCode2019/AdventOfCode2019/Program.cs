using Day1;
using Day2;
using Day3;
using Day4;
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
            new Day4.Day4().Execute();
            s.Stop();
            Console.WriteLine($"Done! (took {s.Elapsed})");
            
            Console.ReadLine();
        }

    }
}

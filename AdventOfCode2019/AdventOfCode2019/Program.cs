using Day1;
using Day2;
using Day3;
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
            Day3.Day3.Execute();
            s.Stop();
            Console.WriteLine($"Done! (took {s.Elapsed})");
            
            Console.ReadLine();
        }

    }
}

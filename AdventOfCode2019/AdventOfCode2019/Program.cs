﻿using Day9;
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
            Day9.Day9.Execute();
            s.Stop();
            Console.WriteLine($"Done! (took {s.Elapsed})");

            Console.ReadLine();
        }

    }
}

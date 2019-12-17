﻿using Day15;
using Day16;
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
            Day16.Day16.Execute();
            s.Stop();
            Console.WriteLine($"Done! (took {s.Elapsed})");

            Console.ReadLine();
        }

    }
}

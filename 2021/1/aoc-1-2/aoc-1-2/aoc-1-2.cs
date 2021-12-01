using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace aoc_1_2
{
    class Program
    {
        static string workdir = Environment.CurrentDirectory;
        static string inputPath = new DirectoryInfo(workdir).Parent.Parent.Parent.Parent.Parent.ToString();
        static readonly List<string> input = File.ReadAllLines(inputPath + "\\input.txt").ToList();
        //static readonly List<string> input = File.ReadAllLines(inputPath + "\\test.txt").ToList();
        int increases = 0;

        void FindIncreases()
        {
            var watch = new System.Diagnostics.Stopwatch();
            watch.Start();
            for (int i = 0; i < input.Count - 3; i++)
            {
                int d1 = Int32.Parse(input[i]) + Int32.Parse(input[i+1]) + Int32.Parse(input[i+2]);
                int d2 = Int32.Parse(input[i + 1]) + Int32.Parse(input[i+2]) + Int32.Parse(input[i+3]);
                if (d2 > d1) increases++;
            }
            watch.Stop();
            Console.WriteLine($"The result for part one is: {increases}");
            Console.WriteLine($"Time is {watch.ElapsedMilliseconds.ToString()} ms");
        }


        static void Main(string[] args)
        {
            Program p = new Program();
            p.FindIncreases();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace aoc_10_1
{
    class Program
    {
        static string workdir = Environment.CurrentDirectory;
        static string inputPath = new DirectoryInfo(workdir).Parent.Parent.Parent.Parent.Parent.ToString();
        List<string> input = File.ReadAllLines(inputPath + "\\input.txt").ToList();
        //List<string> input = File.ReadAllLines(inputPath + "\\test.txt").ToList();

        void Parse()
        {
            List<int> parsed = new List<int>();
            foreach(var line in input)
            {
                parsed.Add(int.Parse(line));
            }
            parsed.Sort();
            int ones, threes, previous;
            ones = threes = previous = 0;
            foreach (var volt in parsed)
            {
                Console.WriteLine("{0} - {1}", volt, volt - previous);
                if (volt - previous == 1) ones++;
                else if (volt - previous == 3) threes++;
                previous = volt;
            }
            threes++;
            Console.WriteLine("There are {0} ones and {1} threes making the result {2}", ones, threes, ones * threes);
        }


        static void Main(string[] args)
        {
            Program p = new Program();
            p.Parse();
        }
    }
}

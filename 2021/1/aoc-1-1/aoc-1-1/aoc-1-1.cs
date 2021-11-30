using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace aoc_1_1
{
    class Program
    {
        static string workdir = Environment.CurrentDirectory;
        static string inputPath = new DirectoryInfo(workdir).Parent.Parent.Parent.Parent.Parent.ToString();
        static readonly List<string> input = File.ReadAllLines(inputPath + "\\input.txt").ToList();
        //static readonly List<string> input = File.ReadAllLines(inputPath + "\\test.txt").ToList();


        void PrintConsole() {
            System.Console.WriteLine("Hello World");
        
        }


        static void Main(string[] args)
        {
            Program p = new Program();
            p.PrintConsole();
        }
    }
}

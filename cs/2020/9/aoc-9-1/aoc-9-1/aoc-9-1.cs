using System;
using System.Collections.Generic;
using System.Linq;

namespace aoc_9_1
{
    class Program
    {
        static string workdir = Environment.CurrentDirectory;
        static string inputPath = new DirectoryInfo(workdir).Parent.Parent.Parent.Parent.Parent.ToString();
        List<string> input = File.ReadAllLines(inputPath + "\\input.txt").ToList();
        //List<string> input = File.ReadAllLines(inputPath + "\\test.txt").ToList();
        
        void FindNonPramble()
        {
            int sizeOfPramble = 25;
            for(int i = sizeOfPramble; i < input.Count; i++)
            {
                bool foundAPramble = false;
                for(int j = i-sizeOfPramble; j < i; j++)
                {
                    for (int n = j + 1; n < i; n++)
                    {
                        if (j != n)
                        {
                            if (long.Parse(input[j]) + long.Parse(input[n]) == long.Parse(input[i])) foundAPramble = true;
                        }
                    }
                }
                if (!foundAPramble)
                {
                    Console.WriteLine("{0} {1}",i,int.Parse(input[i]));
                }
            }
        }

        static void Main(string[] args)
        {
            for (int i = 0; i < 5; i++)
            {
                var watch = System.Diagnostics.Stopwatch.StartNew();
                Program p = new Program();
                p.FindNonPramble();
                watch.Stop();
                Console.WriteLine(watch.ElapsedMilliseconds);
            }
        }
    }
}

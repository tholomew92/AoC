using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace aoc_15_1
{
    class Program
    {
        static readonly string workdir = Environment.CurrentDirectory;
        static readonly string inputPath = new DirectoryInfo(workdir).Parent.Parent.Parent.Parent.Parent.ToString();
        static readonly List<string> input = File.ReadAllLines(inputPath + "\\input.txt").ToList();
        //static readonly List<string> input = File.ReadAllLines(inputPath + "\\test.txt").ToList();

        void Memory(int rounds)
        {

            var watch = System.Diagnostics.Stopwatch.StartNew();
            Dictionary<int, int> memoryDict = new Dictionary<int, int>();
            var numbers = input[0].Split(",", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
            int currentNumber = -1;
            int nextNumber = numbers[0];
            int count = 0;
            for(int i = 0; i < rounds; i++)
            {
                currentNumber = nextNumber;
                if (memoryDict.ContainsKey(currentNumber))
                {
                    nextNumber = i - memoryDict[currentNumber];
                }
                else if (count + 1 < numbers.Count)
                {
                    count++;
                    nextNumber = numbers[count];
                }
                else 
                {
                    nextNumber = 0;
                }
                if (memoryDict.ContainsKey(currentNumber)) memoryDict[currentNumber] = i;
                else memoryDict.Add(currentNumber, i);
            }
            Console.WriteLine(currentNumber);
        }


        static void Main(string[] args)
        {
            Program p = new Program();
            var watch = System.Diagnostics.Stopwatch.StartNew();
            p.Memory(2020);
            watch.Stop();
            Console.WriteLine(watch.ElapsedMilliseconds.ToString());
            watch.Restart();
            p.Memory(30000000);
            watch.Stop();
            Console.WriteLine(watch.ElapsedMilliseconds.ToString());
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace aoc_9_2
{
    class Program
    {
        //static readonly List<String> input = System.IO.File.ReadLines(@"C:\Users\Sebbe\Desktop\aoc\2020\9\test.txt").ToList();
        static readonly List<String> input = System.IO.File.ReadLines(@"C:\Users\Sebbe\Desktop\aoc\2020\9\input.txt").ToList();

        void BreakPramble()
        {
            int sizeOfPramble = 25;
            int invalidNumber = 69316178;
            int weakness = 0;
            List<int> numbersInRange = new List<int>();
            for (int i = sizeOfPramble; i < input.Count; i++)
            {
                for (int j = i - sizeOfPramble; j < i; j++)
                {
                    int count = int.Parse(input[j]);
                    for (int n = j+1; n < i; n++)
                    {
                        count += int.Parse(input[n]);
                        if (count > invalidNumber) n = i;
                        else if(count == invalidNumber)
                        {
                            for (int x = j; x < n; x++) numbersInRange.Add(int.Parse(input[x]));
                            i = j = n = input.Count;
                        }
                    }
                }
            }
            if (numbersInRange.Any()) weakness = numbersInRange.Min() + numbersInRange.Max();
            Console.WriteLine(weakness);
        }

        static void Main(string[] args)
        {
            for (int i = 0; i < 5; i++)
            {
                var watch = System.Diagnostics.Stopwatch.StartNew();
                Program p = new Program();
                p.BreakPramble();
                watch.Stop();
                Console.WriteLine(watch.ElapsedMilliseconds);
            }
        }
    }
}

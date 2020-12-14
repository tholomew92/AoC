using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace aoc_5_2_15
{
    class Program
    {
        List<string> input = File.ReadAllLines(@"C:\Users\sega9727\source\repos\aoc-5-1-15\aoc-5-1-15\bin\Debug\input.txt").ToList();

        void FindNice()
        {
            int nice = 0;
            foreach (var row in input)
            {
                bool isDoubble = false;
                bool isRepeat = false;
                for (int i = 0; i < row.Length - 2; i++)
                {
                    if (row[i].Equals(row[i + 2])) isDoubble = true;
                }
                for(int j = 0; j < row.Length-2; j++)
                {
                    string s = row.Substring(j, 2);
                    if (row.Substring(j + 2).Contains(s)) isRepeat = true;
                }

                if (isRepeat & isDoubble)
                    nice++;
            }
            Console.WriteLine(nice);
        }


        static void Main(string[] args)
        {
            Program p = new Program();
            p.FindNice();
        }
    }
}

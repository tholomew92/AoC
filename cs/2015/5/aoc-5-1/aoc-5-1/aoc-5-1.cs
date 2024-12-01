using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace aoc_5_1_15
{
    class Program
    {
        List<string> input = File.ReadAllLines(@"C:\Users\sega9727\source\repos\aoc-5-1-15\aoc-5-1-15\bin\Debug\input.txt").ToList();

        void FindNice()
        {
            string vowels = "aeiou";
            List<string> forbidden = new List<string>
            {
                "ab",
                "cd",
                "pq",
                "xy"
            };
            int nice = 0;
            foreach(var row in input)
            {
                bool isNaughty = false;
                bool isVowel = false;
                bool isDoubble = false;
                int vowelCount = 0;
                foreach (var f in forbidden) 
                {
                    if (row.Contains(f)) isNaughty = true;
                }
                vowelCount = row.Sum(c => Convert.ToInt32(vowels.Contains(c)));
                if (vowelCount >= 3) isVowel = true;
                for(int i = 0; i < row.Length-1; i++)
                {
                    if (row[i].Equals(row[i + 1])) isDoubble = true;
                }
                Console.WriteLine("{0} is naughty {1} vowels {2} doubble {3}", row, isNaughty, isVowel, isDoubble);
                if (!isNaughty & isVowel & isDoubble) 
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

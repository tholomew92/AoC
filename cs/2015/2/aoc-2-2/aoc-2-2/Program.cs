using System;
using System.Collections.Generic;
using System.Linq;

namespace aoc_2_2
{
    class Program
    {

        static readonly List<String> input = System.IO.File.ReadLines(@"C:\Users\Sebbe\Desktop\aoc\2015\2\input.txt").ToList();

        void GetSquareFeet()
        {
            int totalRibbon = 0;

            foreach (var row in input)
            {
                List<int> sides = row.Split('x').Select(int.Parse).ToList();
                int l = sides[0];
                int h = sides[1];
                int w = sides[2];
                sides.Sort();
                int smallest = sides[0];
                int secondSmall = sides[1];
                int total = (smallest + smallest + secondSmall + secondSmall) + (l * h * w);
                Console.WriteLine("Total is {0}, smallest side is {1}, second smallest side is {2}, all sides are {1},{2},{3}",total, smallest, secondSmall, sides.Max());
                totalRibbon += total;
            }

            Console.WriteLine(totalRibbon);
        }

        static void Main(string[] args)
        {

            Program p = new Program();
            p.GetSquareFeet();
        }
    }
}

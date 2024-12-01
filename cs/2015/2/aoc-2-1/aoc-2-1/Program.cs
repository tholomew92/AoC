using System;
using System.Collections.Generic;
using System.Linq;

namespace aoc_2_1
{
    class Program
    {

        static readonly List<String> input = System.IO.File.ReadLines(@"C:\Users\Sebbe\Desktop\aoc\2015\2\input.txt").ToList();

        void GetSquareFeet()
        {
            int totalFabric = 0;

            foreach(var row in input)
            {
                List<int> sides = row.Split('x').Select(int.Parse).ToList();
                int l = sides[0] * sides[1];
                int h = sides[1] * sides[2];
                int w = sides[2] * sides[0];
                int smallest = sides.Min();
                sides.Remove(smallest);
                smallest *= sides.Min();
                int total = 2 * l + 2 * h + 2 * w + smallest;
                totalFabric += total;
                Console.WriteLine("{0}  {1} {2} produces {3} with {4}", l, h, w, total, smallest);
            }

            Console.WriteLine(totalFabric);
        }

        static void Main(string[] args)
        {

            Program p = new Program();
            p.GetSquareFeet();
        }
    }
}

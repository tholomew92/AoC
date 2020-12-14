using System;
using System.Collections.Generic;
using System.Linq;

namespace aoc_1_1
{
    class Program
    {
        List<String> input = System.IO.File.ReadLines(@"C:\Users\Sebbe\Desktop\aoc\2015\1\input.txt").ToList();
        void GetFloor()
        {
            int floor = 0;
            string line = input[0];
            for(int i = 0; i < line.Length; i++)
            {
                if (line[i].Equals('(')) floor++;
                else if (line[i].Equals(')')) floor--;
                if (floor == -1) Console.WriteLine(i+1<);
                
            }

            Console.WriteLine(floor.ToString());
        }


        static void Main(string[] args)
        {
            Program p = new Program();
            p.GetFloor();

            
        }
    }
}

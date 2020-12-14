using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace aoc_10_2
{
    class Program
    {
        //List<string> input = File.ReadAllLines(@"C:\Users\Sebbe\Desktop\aoc\2020\10\test.txt").ToList();
        List<string> input = File.ReadAllLines(@"C:\Users\Sebbe\Desktop\aoc\2020\10\input.txt").ToList();

        List<int> parsed = new List<int>();

        void Parse()
        {
            parsed.Add(0); 
            foreach (var line in input)
            {
                parsed.Add(int.Parse(line));
            }
            parsed.Sort();
            int count = FindOutArrays(parsed[0], 0);
            Console.WriteLine("There are {0} distinct arrays", count);
        }

        int FindOutArrays(int volt, int place) 
        {
            int count = 0;
            int p1, p2, p3;
            p1 = place + 1;
            p2 = place + 2;
            p3 = place + 3;
            if (p1 < parsed.Count)
            {
                if (parsed[p1] - volt == 3) count += FindOutArrays(parsed[p1], p1);
                else if (parsed[p1] - volt == 2)
                {
                    count += FindOutArrays(parsed[p1], p1);
                    if (p2 < parsed.Count)
                        if (parsed[p2] - volt == 3) count += FindOutArrays(parsed[p2], p2);
                }
                else if (parsed[p1] - volt == 1)
                {
                    count += FindOutArrays(parsed[p1], p1);
                    if (p2 < parsed.Count)
                    {
                        if (parsed[p2] - volt == 2)
                        {
                            count += FindOutArrays(parsed[p2], p2);
                            if (parsed[p2] - volt == 2 & p3 < parsed.Count)
                            {
                                if (parsed[p3] - volt == 3) count += FindOutArrays(parsed[p3], p3);
                            }
                        }
                        else if (parsed[p2] - volt == 3) count += FindOutArrays(parsed[p3], p3);

                    }
                }
            }
            else count = 1;

            return count;
        }


        static void Main(string[] args)
        {
            Program p = new Program();
            p.Parse();
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace aoc_13_2
{
    class Program
    {
        //List<string> input = File.ReadAllLines(@"C:\Users\Sebbe\Desktop\aoc\2020\13\test.txt").ToList();
        List<string> input = File.ReadAllLines(@"C:\Users\Sebbe\Desktop\aoc\2020\13\input.txt").ToList();

        void GetBusDepartings()
        {
            long time = 0;
            string[] line = input[1].Split(",", StringSplitOptions.RemoveEmptyEntries);
            long step = long.Parse(line[0]);
            for(int i = 1; i < line.Length; i++)
            {
                if (!line[i].Equals("x"))
                {
                    long bus = long.Parse(line[i]);
                    while (true)
                    {
                        time += step;
                        if ((time + i) % bus == 0)
                        {
                            step *= bus;
                            break;
                        }
                    }
                }
            }
            Console.WriteLine(time);
        }

        static void Main(string[] args)
        {
            var watch = new System.Diagnostics.Stopwatch();
            Program p = new Program();
            watch.Start();
            p.GetBusDepartings();
            watch.Stop();
            Console.WriteLine(watch.ElapsedMilliseconds.ToString());
        }
    }
}


using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace aoc_6_1_15
{
    class Program
    {
        List<string> input = File.ReadAllLines(@"C:\Users\sebbe\desktop\aoc\2015\6\input.txt").ToList();
        int[,] grid = new int[1000, 1000];
        void FlickLights()
        {
            foreach(var row in input)
            {
                Console.WriteLine(row);
                string[] line = row.Split(" ",StringSplitOptions.RemoveEmptyEntries);
                if (line[0].Equals("toggle"))
                {
                    int[] start = Array.ConvertAll(line[1].Split(","), int.Parse);
                    int[] end = Array.ConvertAll(line[3].Split(","), int.Parse);
                    for(int x = start[0]; x <= end[0]; x++)
                    {
                        for (int y = start[1]; y <= end[1]; y++)
                        {
                            grid[x, y]+=2;
                        }
                    }
                }
                else if(line[0].Equals("turn"))
                {
                    int[] start = Array.ConvertAll(line[2].Split(","), int.Parse);
                    int[] end = Array.ConvertAll(line[4].Split(","), int.Parse);
                    for (int x = start[0]; x <= end[0]; x++)
                    {
                        for (int y = start[1]; y <= end[1]; y++)
                        {
                            if (line[1].Equals("on")) grid[x, y]++;
                            else if(grid[x, y] > 0) grid[x, y]--;
                        }
                    }
                }
            }
            int lights = 0;
            for(int i = 0; i < 1000; i++)
            {
                for(int j = 0; j < 1000; j++)
                {
                    lights+=grid[i,j];
                }
            }
            Console.WriteLine(lights);
        }

        static void Main(string[] args)
        {
            Program p = new Program();
            p.FlickLights();
        }
    }
}

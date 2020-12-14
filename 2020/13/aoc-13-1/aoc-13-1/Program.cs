using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace aoc_13_1
{
    class Program
    {
        //List<string> input = File.ReadAllLines(@"C:\Users\Sebbe\Desktop\aoc\2020\13\test.txt").ToList();
        List<string> input = File.ReadAllLines(@"C:\Users\Sebbe\Desktop\aoc\2020\13\input.txt").ToList();
        Dictionary<string, List<int>> busDict = new Dictionary<string, List<int>>();

        void GetBusDepartings()
        {
            int time = int.Parse(input[0]);
            int nearestbus = int.MaxValue;
            int answer = 0;
            var line = input[1].Split(",", StringSplitOptions.RemoveEmptyEntries);
            foreach(var bus in line)
            {
                if (int.TryParse(bus, out int b))
                {
                    int mod = time % b;
                    int nearestDepartue = time + b - mod;
                    int waitTime = nearestDepartue - time;
                    if (waitTime < nearestbus) { nearestbus = waitTime; answer = waitTime * b; }
                }
            }
            Console.WriteLine(answer);
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


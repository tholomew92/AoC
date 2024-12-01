using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace aoc_10_2
{
    class Program
    {
        static string workdir = Environment.CurrentDirectory;
        static string inputPath = new DirectoryInfo(workdir).Parent.Parent.Parent.Parent.Parent.ToString();
        List<string> input = File.ReadAllLines(inputPath + "\\input.txt").ToList();
        //List<string> input = File.ReadAllLines(inputPath + "\\test.txt").ToList();

        List<int> parsed = new List<int>();

        void Parse()
        {
            Dictionary<int, Int64> inputdic = new Dictionary<int, Int64>();
            parsed.Add(0);
            foreach (var line in input)
            {
                parsed.Add(int.Parse(line));
            }
            parsed.Sort();
            inputdic.Add(0, 1);
            for (int i = 1; i < parsed.Count; i++)
            {
                Int64 count = 0;
                if (i == 1) {
                    if (parsed[i] - parsed[i - 1] <= 3) count += inputdic[parsed[i - 1]];
                }

               else if (i == 2)
               {
                    if (parsed[i] - parsed[i - 2] <= 3) count += inputdic[parsed[i - 2]];
                    if (parsed[i] - parsed[i - 1] <= 3) count += inputdic[parsed[i - 1]];
               }

                else
                {
                    if (parsed[i] - parsed[i - 3] <= 3) count += inputdic[parsed[i - 3]];
                    if (parsed[i] - parsed[i - 2] <= 3) count += inputdic[parsed[i - 2]];
                    if (parsed[i] - parsed[i - 1] <= 3) count += inputdic[parsed[i - 1]];
                }
                inputdic.Add(parsed[i], count);
                if(i + 1 == parsed.Count) Console.WriteLine("There are {0} distinct arrays", count);

            }
        }

        static void Main(string[] args)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            Program p = new Program();
            p.Parse();
            watch.Stop();
            Console.WriteLine(watch.ElapsedMilliseconds.ToString());
        }
    }
}

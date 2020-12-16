using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace aoc_16_1
{
    class Program
    {
        static string workdir = Environment.CurrentDirectory;
        static string inputPath = new DirectoryInfo(workdir).Parent.Parent.Parent.Parent.Parent.ToString();
        List<string> input = File.ReadAllLines(inputPath + "\\input.txt").ToList();
        //List<string> input = File.ReadAllLines(inputPath + "\\test.txt").ToList();

        Dictionary<string,List<int>> validTickets = new Dictionary<string, List<int>>();

        void PartOne()
        {
            ValidRanges();

            int spacecount = 0;
            int startIndex = 0;
            foreach(var row in input)
            {
                if (row.Equals(""))
                {
                    spacecount++;
                    if(spacecount == 2)
                    {
                        break;
                    }
                }
                startIndex++;
            }
            int total = 0;
            for(int i = startIndex + 2; i < input.Count; i++)
            {
                var values = input[i].Split(",").Select(int.Parse).ToList();
                foreach(var v in values)
                {
                    bool validnum = false;
                    foreach(var ticketrow in validTickets)
                    {
                        int minLow = ticketrow.Value[0];
                        int maxLow = ticketrow.Value[1];
                        int minHigh = ticketrow.Value[2];
                        int maxHigh = ticketrow.Value[3];
                        IEnumerable<int> low = Enumerable.Range(minLow, maxLow-minLow+1);
                        IEnumerable<int> high = Enumerable.Range(minHigh, maxHigh-minHigh+1);
                        if (low.Contains(v) | high.Contains(v))
                        {
                            validnum = true;
                            break;
                        }
                    }
                    if (!validnum)
                    {
                        total += v;
                    }
                }
            }
            Console.WriteLine(total);
        }

        void ValidRanges()
        {
            foreach(var row in input)
            {
                if (row.Equals(""))
                {
                    break;
                }
                else
                {
                    var line = row.Split(":", StringSplitOptions.RemoveEmptyEntries).Select(p => p.Trim()).ToList();
                    string ticketfield = line[0];
                    var values = line[1].Split(" or ", StringSplitOptions.RemoveEmptyEntries);
                    validTickets.Add(ticketfield, new List<int>());
                    foreach(var v in values)
                    {
                        var value = v.Split("-");
                        int v1 = int.Parse(value[0]);
                        int v2 = int.Parse(value[1]);
                        validTickets[ticketfield].Add(v1);
                        validTickets[ticketfield].Add(v2);
                    }
                }
            }
        }

        static void Main(string[] args)
        {
            Program p = new Program();
            var watch = System.Diagnostics.Stopwatch.StartNew();
            p.PartOne();
            watch.Stop();
            Console.WriteLine(watch.ElapsedMilliseconds.ToString());
        }
    }
}

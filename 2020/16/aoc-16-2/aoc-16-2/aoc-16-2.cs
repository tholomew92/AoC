using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace aoc_16_2
{
    class Program
    {
        static string workdir = Environment.CurrentDirectory;
        static string inputPath = new DirectoryInfo(workdir).Parent.Parent.Parent.Parent.Parent.ToString();
        List<string> input = File.ReadAllLines(inputPath + "\\input.txt").ToList();
        //List<string> input = File.ReadAllLines(inputPath + "\\test.txt").ToList();

        readonly Dictionary<string, List<int>> ticketFields = new Dictionary<string, List<int>>();
        Dictionary<string, List<int>> fieldPos = new Dictionary<string, List<int>>();
        List<string> validTickets = new List<string>();

        void PartTwo()
        {
            int startIndex = 0;
            foreach (var row in input)
            {
                if (row.Equals(""))
                {
                    break;
                }
                startIndex++;
            }
            var myTicket = input[startIndex + 2].Split(",").Select(int.Parse).ToList();
            ValidRanges(myTicket.Count);
            int index = 0;
            GetValidTickets(startIndex + 5);
            foreach (var tickets in validTickets) 
            {
                int count = 0;
                var ticket = tickets.Split(",").Select(int.Parse).ToList();
                foreach (var v in ticket)
                {
                    foreach (var field in ticketFields)
                    {

                        string s = field.Key.ToString();
                        if (fieldPos[s].Contains(count) & fieldPos[s].Count > 1)
                        {
                            int minLow = field.Value[0];
                            int maxLow = field.Value[1];
                            int minHigh = field.Value[2];
                            int maxHigh = field.Value[3];
                            IEnumerable<int> low = Enumerable.Range(minLow, maxLow - minLow + 1);
                            IEnumerable<int> high = Enumerable.Range(minHigh, maxHigh - minHigh + 1);
                            if (!low.Contains(v) & !high.Contains(v))
                            {
                                fieldPos[s].Remove(count);
                                if (fieldPos[s].Count == 1)
                                    RemoveAllOtherPos(s, fieldPos[s][0]);
                            }
                        }
                    }
                    count++;
                } 
            }

            Int64 total = 1;
            foreach (var pos in fieldPos)
            {
                if (pos.Key.StartsWith("departure"))
                {
                    total = total * myTicket[pos.Value[0]];
                }
            }
            Console.WriteLine(total);
        }

        void RemoveAllOtherPos(string field, int pos)
        {
            foreach(var position in fieldPos)
            {
                if (!position.Key.Equals(field) & position.Value.Count != 1)
                {
                    if(position.Value.Contains(pos))
                        position.Value.Remove(pos);
                    if (position.Value.Count == 1)
                        RemoveAllOtherPos(position.Key, position.Value[0]);
                }
            }
        }

        void GetValidTickets(int startIndex)
        {
            for (int i = startIndex; i < input.Count; i++)
            {
                var values = input[i].Split(",").Select(int.Parse).ToList();
                bool valid = true;
                foreach (var v in values)
                {
                    bool validnum = false;
                    foreach (var ticketrow in ticketFields)
                    {
                        int minLow = ticketrow.Value[0];
                        int maxLow = ticketrow.Value[1];
                        int minHigh = ticketrow.Value[2];
                        int maxHigh = ticketrow.Value[3];
                        IEnumerable<int> low = Enumerable.Range(minLow, maxLow - minLow + 1);
                        IEnumerable<int> high = Enumerable.Range(minHigh, maxHigh - minHigh + 1);
                        if (low.Contains(v) | high.Contains(v))
                        {
                            validnum = true;
                        }
                    }
                    if (!validnum)
                    {
                        valid = false;
                    }
                }
                if (valid)
                {
                    validTickets.Add(input[i]);
                }
            }
        }

        void ValidRanges(int index)
        {
            foreach (var row in input)
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
                    ticketFields.Add(ticketfield, new List<int>());
                    foreach (var v in values)
                    {
                        var value = v.Split("-");
                        int v1 = int.Parse(value[0]);
                        int v2 = int.Parse(value[1]);
                        ticketFields[ticketfield].Add(v1);
                        ticketFields[ticketfield].Add(v2);
                    }
                    List<int> pos = new List<int>();
                    pos.AddRange(Enumerable.Range(0, index));
                    fieldPos.Add(ticketfield, pos);
                }
            }
        }

        static void Main(string[] args)
        {
            Program p = new Program();
            var watch = System.Diagnostics.Stopwatch.StartNew();
            p.PartTwo();
            watch.Stop();
            Console.WriteLine(watch.ElapsedMilliseconds.ToString());
        }
    }
}

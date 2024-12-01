using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace aoc_7_2
{
    class Program
    {
        static string workdir = Environment.CurrentDirectory;
        static string inputPath = new DirectoryInfo(workdir).Parent.Parent.Parent.Parent.Parent.ToString();
        List<string> input = File.ReadAllLines(inputPath + "\\input.txt").ToList();
		Dictionary<string, List<string>> bagDict = new Dictionary<string, List<string>>();

        void FindBags()
        {
            foreach (var bag in input)
            {
                List<string> parsed = bag.Split("bags contain", StringSplitOptions.RemoveEmptyEntries).ToList();
                List<string> bagList = new List<string>();
                if (!parsed[1].Equals(" no other bags."))
                {
                    string[] contain = parsed[1].Split(",", StringSplitOptions.RemoveEmptyEntries);
                    foreach (string s in contain)
                    {
                        StringBuilder sb = new StringBuilder();
                        string[] temp = s.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                        sb.Append(temp[0] + "," + temp[1] + " " + temp[2]);
                        bagList.Add(sb.ToString());
                    }
                }
                bagDict.Add(parsed[0].Trim(), bagList);
            }

            int count = BagLookUp(bagDict["shiny gold"]);

            Console.WriteLine(count);
        }

        int BagLookUp(List<string> bags)
        {
            if (!bags.Any()) return 0;
            int count = 0;
            foreach (var bag in bags)
            {
                string[] temp = bag.Split(",", StringSplitOptions.RemoveEmptyEntries);
                int multi = int.Parse(temp[0]);
                string b = temp[1];
                int c = BagLookUp(bagDict[b]);
                Console.WriteLine("There is {0} {1}", multi, b);
                if (c != 0) count += multi + multi * c;
                else count += multi;
            }
            return count;
        }

        static void Main(string[] args)
        {
            Program p = new Program();
            p.FindBags();
        }
    }
}

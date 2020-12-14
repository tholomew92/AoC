using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace aoc_7_1
{
    class Program
    {
        static string workdir = Environment.CurrentDirectory;
        static string inputPath = new DirectoryInfo(workdir).Parent.Parent.Parent.Parent.Parent.ToString();
        List<string> input = File.ReadAllLines(inputPath + "\\input.txt").ToList();
		Dictionary<string, List<string>> bagDict = new Dictionary<string, List<string>>();

        void FindBags()
        {
            int count = 0;
            foreach(var bag in input)
            {
                List<string> parsed = bag.Split("bags contain", StringSplitOptions.RemoveEmptyEntries).ToList();
                List<string> bagList = new List<string>();
                if (!parsed[1].Equals(" no other bags."))
                {
                    string[] contain = parsed[1].Split(",", StringSplitOptions.RemoveEmptyEntries);
                    foreach(string s in contain)
                    {
                        StringBuilder sb = new StringBuilder();
                        string[] temp = s.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                        sb.Append(temp[1] + " " + temp[2]);
                        bagList.Add(sb.ToString());
                    }
                }
                bagDict.Add(parsed[0].Trim(), bagList);
            }

            foreach(var bag in bagDict)
            {
                if(BagLookUp(bag.Value)) count++;
            }
            Console.WriteLine(count);
        }

        bool BagLookUp(List<string> bags)
        {
            foreach(var bag in bags)
            {
                if (bag.Equals("shiny gold")) return true;
                else if (bagDict.TryGetValue(bag, out _)) 
                    if(BagLookUp(bagDict[bag])) return true ;
            }
            return false;
        }

        static void Main(string[] args)
        {
            Program p = new Program();
            p.FindBags();
        }
    }
}

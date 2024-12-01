using System;
using System.Collections.Generic;
using System.Linq;

namespace aoc_6_2
{
    class Program
    {
        static string workdir = Environment.CurrentDirectory;
        static string inputPath = new DirectoryInfo(workdir).Parent.Parent.Parent.Parent.Parent.ToString();
        List<string> input = File.ReadAllLines(inputPath + "\\input.txt").ToList();
		
        void FindAnswers()
        {
            input.Add("");
            int latestGroupStart = 0;
            int amountOfAnswers = 0;
            for (int i = 0; i < input.Count; i++)
            {
                var row = input[i];
                if (row.Equals(""))
                {
                    
                    foreach (var c in input[i - 1])
                    {
                        bool charDup = true;
                        for (int j = latestGroupStart; j < i; j++)
                        {
                            Console.Write(" Checking if {0} contains {1} ", input[j], c);
                            if (!input[j].Contains(c))
                            {
                                charDup = false;
                                Console.Write("{0} does not contain {1} ", input[j], c);
                            }
                        }
                        Console.WriteLine();
                        if (charDup) amountOfAnswers++;
                    }
                    Console.WriteLine();
                    latestGroupStart = i + 1;
                }
            }
            Console.WriteLine(amountOfAnswers);
        }

        static void Main(string[] args)
        {
            Program p = new Program();
            p.FindAnswers();
        }
    }
}

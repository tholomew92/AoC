using System;
using System.Collections.Generic;
using System.Linq;

namespace aoc_6_1
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
                    List<char> answerList = new List<char>();
                    answerList.Clear();
                    for (int j = latestGroupStart; j < i; j++)
                    {
                        foreach (var c in input[j])
                        {
                            if (!answerList.Contains(c))
                            {
                                answerList.Add(c);
                                Console.Write(" Added " + c + ", current count is " + answerList.Count + " ");
                            }
                        }
                        Console.WriteLine();
                    }
                    Console.WriteLine();
                    amountOfAnswers += answerList.Count;
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

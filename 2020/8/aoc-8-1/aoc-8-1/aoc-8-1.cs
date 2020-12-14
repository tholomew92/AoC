using System;
using System.Collections.Generic;
using System.Linq;

namespace aoc_8_1
{
    class Program
    {
        static string workdir = Environment.CurrentDirectory;
        static string inputPath = new DirectoryInfo(workdir).Parent.Parent.Parent.Parent.Parent.ToString();
        List<string> input = File.ReadAllLines(inputPath + "\\input.txt").ToList();
		
        void Boot()
        {
            int acc = 0;
            int mod = 0;
            List<int> positions = new List<int>();
            for (int i = 0; i < input.Count; i++)
            {
                var line = input[i].Split(" ");
                string instruction = line[0].Trim();
                int modifier = int.Parse(line[1].Trim());
                switch (instruction)
                {
                    case "acc":
                        Console.WriteLine("Add {0} to acc: {1}", line[1], acc);
                        acc += modifier;
                        break;
                    case "jmp":
                        Console.WriteLine("Jumping from {0} with space {1}", i, line[1]);
                        i += modifier - 1;
                        Console.WriteLine(i);
                        break;
                    case "nop":
                        Console.WriteLine("No instructions");
                        break;
                    default:
                        Console.WriteLine("Error");
                        break;
                }
                if (positions.Contains(i)) i = input.Count;
                positions.Add(i);
            }
            Console.WriteLine(acc);
        }

        static void Main(string[] args)
        {
            Program p = new Program();
            p.Boot();
        }
    }
}

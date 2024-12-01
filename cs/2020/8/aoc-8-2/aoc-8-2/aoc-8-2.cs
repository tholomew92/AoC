using System;
using System.Collections.Generic;
using System.Linq;

namespace aoc_8_2
{
    class Program
    {
        static string workdir = Environment.CurrentDirectory;
        static string inputPath = new DirectoryInfo(workdir).Parent.Parent.Parent.Parent.Parent.ToString();
        List<string> input = File.ReadAllLines(inputPath + "\\input.txt").ToList();
		
        bool Boot()
        {
            int mod = 0;
            foreach (var row in input)
            {
                bool check = true;
                int count = 0;
                int acc = 0;
                Console.WriteLine("Mod is currently {0} and this {1} is affected", mod, input[mod]);
                for (int i = 0; i < input.Count; i++)
                {
                    var line = input[i].Split(" ");
                    string instruction = line[0].Trim();
                    int modifier = int.Parse(line[1].Trim());
                    if (i == mod & instruction.Equals("jmp")) instruction = "nop";
                    else if (i == mod & instruction.Equals("nop")) instruction = "jmp";
                    switch (instruction)
                    {
                        case "acc":
                            acc += modifier;
                            break;
                        case "jmp":
                            i += modifier - 1;
                            break;
                        case "nop":
                            break;
                        default:
                            break;
                    }
                    count++;
                    if (count > 5000)
                    {
                        Console.WriteLine("Enough");
                        i = input.Count;
                        check = false;
                    }
                }
                if (check)
                {
                    Console.WriteLine(acc);
                    return true;
                }
                mod++;
            }
            return false;
        }

        static void Main(string[] args)
        {
            Program p = new Program();
            p.Boot();
        }
    }
}

using System;
using System.Collections.Generic;

namespace aoc_2_2
{
    class Program
    {
        static string workdir = Environment.CurrentDirectory;
        static string inputPath = new DirectoryInfo(workdir).Parent.Parent.Parent.Parent.Parent.ToString();
        List<string> input = File.ReadAllLines(inputPath + "\\input.txt").ToList();

        Boolean isPwValid(int firstPos, int secondPos, char letter, String pw)
        {
            Boolean validPw = false;
            if (pw[firstPos - 1] == letter ^ pw[secondPos - 1] == letter)
                validPw = true;
            
            return validPw;
        }

        int findValidPws()
        {
            int validPws = 0;
            int firstPos, secondPos;
            char letter;
            String pw;

            foreach (String row in input)
            {
                String[] r = row.Split(" ", 3, StringSplitOptions.RemoveEmptyEntries);
                String[] valueFind = r[0].Split("-", 2, StringSplitOptions.RemoveEmptyEntries);
                firstPos = Int32.Parse(valueFind[0]);
                secondPos = Int32.Parse(valueFind[1]);
                letter = r[1][0];
                pw = r[2];
                if (isPwValid(firstPos, secondPos, letter, pw))
                    validPws++;
            }


            return validPws;
        }


        static void Main(string[] args)
        {
            Program p = new Program();
            Console.WriteLine(p.findValidPws());
        }
    }
}

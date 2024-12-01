using System;

namespace aoc_2_1
{
    class Program
    {
        static string workdir = Environment.CurrentDirectory;
        static string inputPath = new DirectoryInfo(workdir).Parent.Parent.Parent.Parent.Parent.ToString();
        List<string> input = File.ReadAllLines(inputPath + "\\input.txt").ToList();

        Boolean isPwValid(int min,int max, char letter, String pw)
        {
            int count = 0;
            Boolean validPw = false;
            foreach (char c in pw)
                if (c == letter)
                    count++;

            if (count <= max && count >= min)
                validPw = true;
            return validPw;
        }

        int findValidPws()
        {
            int validPws = 0;
            int minVal, maxVal;
            char letter;
            String pw;

            foreach(String row in input)
            {
                String[] r = row.Split('-', ' ', StringSplitOptions.RemoveEmptyEntries);
                minVal = Int32.Parse(r[0]);
                maxVal = Int32.Parse(r[1]);
                letter = r[2][0];
                pw = r[3];
                if (isPwValid(minVal, maxVal, letter, pw))
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

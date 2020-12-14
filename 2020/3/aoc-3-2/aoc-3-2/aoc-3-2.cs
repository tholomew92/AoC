using System;
using System.Collections.Generic;

namespace aoc_3_2
{
    class Program
    {
        static string workdir = Environment.CurrentDirectory;
        static string inputPath = new DirectoryInfo(workdir).Parent.Parent.Parent.Parent.Parent.ToString();
        List<string> input = File.ReadAllLines(inputPath + "\\input.txt").ToList();


        int TraverseSlope(int right, int down)
        {
            int amountOfTrees = 0;
            int maxHorizontal = input[0].Length;
            int horizontalSteps = 0;

            for (int i = 0; i < input.Length; i+=down)
            {

                if (input[i][horizontalSteps].Equals('#'))
                    amountOfTrees++;

                for (int j = 0; j < right; j++)
                {
                    horizontalSteps++;
                    if (horizontalSteps == maxHorizontal)
                        horizontalSteps = 0;
                }
            }
            Console.WriteLine(amountOfTrees);
            return amountOfTrees;
        }

        void Start()
        {
            Int64 a1, a2, a3, a4, a5;

            a1 = TraverseSlope(1,1);
            a2 = TraverseSlope(3,1);
            a3 = TraverseSlope(5,1);
            a4 = TraverseSlope(7,1);
            a5 = TraverseSlope(1,2);


            Console.WriteLine(a1 * a2 * a3 * a4 * a5);
        }


        static void Main(string[] args)
        {
            Program p = new Program();
            p.Start();

        }
    }
}

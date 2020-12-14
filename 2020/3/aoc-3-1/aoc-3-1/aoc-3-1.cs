using System;

namespace aoc_3_1
{
    class Program
    {
        static string workdir = Environment.CurrentDirectory;
        static string inputPath = new DirectoryInfo(workdir).Parent.Parent.Parent.Parent.Parent.ToString();
        List<string> input = File.ReadAllLines(inputPath + "\\input.txt").ToList();

        void TraverseSlope()
        {
            int amountOfTrees = 0;
            int maxHorizontal = input[0].Length;
            int horizontalSteps = 0;

            for (int i = 0; i < input.Length; i++)
            {
                Console.WriteLine(horizontalSteps);

                if (input[i][horizontalSteps].Equals('#'))
                    amountOfTrees++;

                for (int j = 0; j < 3; j++)
                {
                    horizontalSteps++;
                    if (horizontalSteps == maxHorizontal)
                        horizontalSteps = 0;
                }
            }
            Console.WriteLine(amountOfTrees);
        }

        static void Main(string[] args)
        {
            Program p = new Program();
            p.TraverseSlope();

        }
    }
}

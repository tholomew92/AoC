using System;

namespace aoc_3_1
{
    class Program
    {
        static readonly String[] input = System.IO.File.ReadAllLines(@"C:\Users\Sebbe\Desktop\aoc\2020\3\input.txt");

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

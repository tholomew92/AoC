using System;
using System.Collections.Generic;

namespace aoc_2_1
{
    class Program
    {
        static readonly System.IO.StreamReader file = new System.IO.StreamReader(@"C:\Users\Sebbe\Desktop\aoc\2019\2\input.txt");
        static readonly int[] inputCopy = Array.ConvertAll(file.ReadLine().Split(','), int.Parse);
        int[] parsedInput = (int[])inputCopy.Clone();

        void FindValues()
        {
            int baseLength = parsedInput.Length;
            Boolean valueFound = false;
            Boolean moreValues = true;
            int noun = 0;
            foreach (int val in parsedInput)
            {
                parsedInput[1] = noun;
                int searchIndex = baseLength / 2;
                int indexShift = searchIndex / 2;
                int max = baseLength / 2;
                int searches = 0;
                moreValues = true;
                while (!valueFound && moreValues)
                {
                    parsedInput[2] = searchIndex;
                    if (max == searches)
                        moreValues = false;
                    long value = ParseOpCodes();
                    if (value == 19690720)
                    {
                        valueFound = true;
                        Console.WriteLine(parsedInput[1] + parsedInput[2]);
                    }
                    else if (value > 19690720)
                    {
                        searchIndex = searchIndex - indexShift;
                        if (indexShift > 1)
                            indexShift /= 2;
                    }
                    else
                    {
                        searchIndex = searchIndex + indexShift;
                        if (indexShift > 1)
                            indexShift /= 2;

                    }
                    searches++;
                }
                if (valueFound)
                    break;
                noun++;
            }
        }

        long ParseOpCodes()
        {
            for(int i = 0; i < parsedInput.Length-1; i += 4)
            {
                switch (parsedInput[i])
                {
                    case 1:
                        Addition(parsedInput[i+1], parsedInput[i+2], parsedInput[i+3]);
                        break;
                    case 2:
                        Multiplication(parsedInput[i + 1], parsedInput[i + 2], parsedInput[i + 3]);
                        break;
                    case 3:
                        Quit();
                        break;
                    default:
                        break;
                }
            }

            return (100 * parsedInput[1]) + parsedInput[2];
        }

        void Addition(int firstValue, int secondValue, int placement)
        {
            parsedInput[placement] = parsedInput[firstValue] + parsedInput[secondValue];
            if (100 * parsedInput[1] + parsedInput[2] == 19690720)
                Console.WriteLine(parsedInput[1] + parsedInput[2]);
        }

        void Multiplication(int firstValue, int secondValue, int placement)
        {
            parsedInput[placement] = parsedInput[firstValue] * parsedInput[secondValue];
            if(100 * parsedInput[1] + parsedInput[2] == 19690720)
                Console.WriteLine(parsedInput[1] + parsedInput[2]);
        }

        void Quit()
        {
            Environment.Exit(1);
        }

        static void Main(string[] args)
        {
            Program p = new Program();
            p.FindValues();
        }
    }
}

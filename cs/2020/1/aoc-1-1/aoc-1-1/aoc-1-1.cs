using System;

namespace aoc_1_1
{
    class Program
    {
        static string workdir = Environment.CurrentDirectory;
        static string inputPath = new DirectoryInfo(workdir).Parent.Parent.Parent.Parent.Parent.ToString();
        List<string> input = File.ReadAllLines(inputPath + "\\input.txt").ToList();

        void findValues(String[] input)
        {
            int startValue = 0;
            Boolean valueNotFound = true;
            do
            {
                for (int i = startValue; i < (input.Length - 2); i++)
                {
                    int v1 = Int32.Parse(input[startValue]);
                    int v2 = Int32.Parse(input[i + 1]);
                    if(v1+v2 < 2020)
                    {
                        for(int j = i + 2; j <(input.Length - 2); j++)
                        {
                            int v3 = Int32.Parse(input[j]);
                            if (v1 + v2 + v3 == 2020)
                            {
                                Console.WriteLine(v1 * v2 * v3);
                                valueNotFound = false;
                                break;
                            }
                        }
                    }
                }
                startValue++;
                Console.WriteLine(startValue);
            } while (valueNotFound);
        }

        static void Main(string[] args)
        {
            Program program = new Program();
            program.findValues(input);
        }
    }
}

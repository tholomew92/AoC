namespace aoc_1_new
{
    class Program
    {
        static string workdir = Environment.CurrentDirectory;
        static string inputPath = new DirectoryInfo(workdir).Parent.Parent.Parent.Parent.Parent.ToString();
        static readonly List<string> input = File.ReadAllLines(inputPath + "\\input.txt").ToList();
        //static readonly List<string> input = File.ReadAllLines(inputPath + "\\test.txt").ToList();

        long PartOne() 
        {
            List<long> fishyInput = input[0].Split(',').Select(x => long.Parse(x)).ToList();
            long[] allFishy = new long[9];
            long[] allFishyTemp = new long[9];

            foreach (var fish in fishyInput)
            {
                allFishy[fish]++;
            }

            for (int i = 0; i < 80; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (j == 0)
                    {
                        allFishyTemp[6] = allFishy[0];
                        allFishyTemp[8] = allFishy[0];
                    }
                    else
                    {
                        allFishyTemp[j - 1] += allFishy[j];
                    }

                    if (j == 8)
                    {
                        allFishy = allFishyTemp;
                        allFishyTemp = new long[9];
                    }
                }
            }

            return allFishy.Sum();
        }

        long PartTwo()
        {
            List<long> fishyInput = input[0].Split(',').Select(x => long.Parse(x)).ToList();
            long[] allFishy = new long[9];
            long[] allFishyTemp = new long[9];

            foreach (var fish in fishyInput) 
            {
                allFishy[fish]++;
            }

            for (int i = 0; i < 256; i++)
            {
                for (int j = 0; j < 9; j++) 
                {
                    if (j == 0) 
                    {
                        allFishyTemp[6] = allFishy[0];
                        allFishyTemp[8] = allFishy[0];
                    }
                    else 
                    {
                        allFishyTemp[j-1] += allFishy[j];
                    }

                    if (j == 8) 
                    {
                        allFishy = allFishyTemp;
                        allFishyTemp = new long[9];
                    }
                }
            }

            return allFishy.Sum();
        }

        void Run()
        {
            long result;
            var watch = new System.Diagnostics.Stopwatch();
            watch.Start();
            result = PartOne();
            watch.Stop();
            Console.WriteLine($"The result for part one is: {result}");
            Console.WriteLine($"Time is {watch.ElapsedMilliseconds.ToString()} ms");
            watch.Reset();
            result = PartTwo();
            watch.Stop();
            Console.WriteLine($"The result for part one is: {result}");
            Console.WriteLine($"Time is {watch.ElapsedMilliseconds.ToString()} ms");
        }

        static void Main(string[] args)
        {
            Program p = new Program();
            p.Run();
        }
    }
}
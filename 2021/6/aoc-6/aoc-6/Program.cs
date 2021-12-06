namespace aoc_1_new
{
    class Program
    {
        static string workdir = Environment.CurrentDirectory;
        static string inputPath = new DirectoryInfo(workdir).Parent.Parent.Parent.Parent.Parent.ToString();
        static readonly List<string> input = File.ReadAllLines(inputPath + "\\input.txt").ToList();
        //static readonly List<string> input = File.ReadAllLines(inputPath + "\\test.txt").ToList();

        int PartOne() 
        {
            List<int> allFishy = (input[0].Split(',', StringSplitOptions.RemoveEmptyEntries).Select(x => Int32.Parse(x)).ToList());
            for(int i = 0; i < 80; i++) 
            {
                List<int> tempFishList = allFishy;
                for(int j = 0; j < allFishy.Count; j++)
                {
                    if (allFishy[j] == 0)
                    {
                        tempFishList[j] = 6;
                        tempFishList.Add(9);
                    }
                    else tempFishList[j]--;
                }
                allFishy = tempFishList;
            }

            return allFishy.Count;
        }

        int PartTwo()
        {
            List<int> allFishy = (input[0].Split(',', StringSplitOptions.RemoveEmptyEntries).Select(x => Int32.Parse(x)).ToList());
            for (int i = 0; i < 256; i++)
            {   
                Console.WriteLine(i);
                List<int> tempFishList = allFishy;
                for (int j = 0; j < allFishy.Count; j++)
                {
                    if (allFishy[j] == 0)
                    {
                        tempFishList[j] = 6;
                        tempFishList.Add(9);
                    }
                    else tempFishList[j]--;
                }
                allFishy = tempFishList;
            }

            return allFishy.Count;
        }

        void Run()
        {
            int result;
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
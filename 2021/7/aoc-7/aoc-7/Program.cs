namespace aoc_7
{
    class Program
    {
        static string workdir = Environment.CurrentDirectory;
        static string inputPath = new DirectoryInfo(workdir).Parent.Parent.Parent.Parent.Parent.ToString();
        static readonly List<string> input = File.ReadAllLines(inputPath + "\\input.txt").ToList();
        //static readonly List<string> input = File.ReadAllLines(inputPath + "\\test.txt").ToList();

        int PartOne() 
        {
            var crabPos = input[0].Split(',').Select(x => Int32.Parse(x)).ToList();
            var minPos = crabPos.Min();
            var maxPos = crabPos.Max();
            int[] changePerPos = new int[maxPos+1];

            for (int i = 0; i <= maxPos; i++) 
            {
                changePerPos[i] = crabPos.Select(x => Math.Abs(x - i)).Sum();
            }
            var minDiff = changePerPos.Min();

            return minDiff;
        }

        int PartTwo()
        {
            var crabPos = input[0].Split(',').Select(x => Int32.Parse(x)).ToList();
            var minPos = crabPos.Min();
            var maxPos = crabPos.Max();
            int[] changePerPos = new int[maxPos + 1];

            for (int i = 0; i <= maxPos; i++)
            {
                changePerPos[i] = crabPos.Select(x => Enumerable.Range(1,Math.Abs(x - i)).Sum()).Sum();
            }
            var minDiff = changePerPos.Min();

            return minDiff;
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
            watch.Start();
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
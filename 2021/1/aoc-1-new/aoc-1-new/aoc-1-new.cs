namespace aoc_1_new
{
    class Program
    {
        static string workdir = Environment.CurrentDirectory;
        static string inputPath = new DirectoryInfo(workdir).Parent.Parent.Parent.Parent.Parent.ToString();
        static readonly List<string> input = File.ReadAllLines(inputPath + "\\input.txt").ToList();
        //static readonly List<string> input = File.ReadAllLines(inputPath + "\\test.txt").ToList();
        int increases;


        void Run()
        {
            var watch = new System.Diagnostics.Stopwatch();
            watch.Start();
            FindIncreases(1);
            watch.Stop();
            Console.WriteLine($"The result for part one is: {increases}");
            Console.WriteLine($"Time is {watch.ElapsedMilliseconds.ToString()} ms");
            watch.Reset(); 
            FindIncreases(3);
            watch.Stop();
            Console.WriteLine($"The result for part one is: {increases}");
            Console.WriteLine($"Time is {watch.ElapsedMilliseconds.ToString()} ms");
        }

        void FindIncreases(int increments)
        {
            increases = 0;
            for (int i = 0; i < input.Count - increments; i++)
            {
                int d1 = Int32.Parse(input[i]);
                int d2 = Int32.Parse(input[i + increments]);
                if (d2 > d1) increases++;
            }

        }


        static void Main(string[] args)
        {
            Program p = new Program();
            p.Run();
        }
    }
}

namespace aoc_6_new
{
    class Program
    {
        static string workdir = Environment.CurrentDirectory;
        static string inputPath = new DirectoryInfo(workdir).Parent.Parent.Parent.Parent.Parent.ToString();
        static readonly List<string> input = File.ReadAllLines(inputPath + "\\input.txt").ToList();
        //static readonly List<string> input = File.ReadAllLines(inputPath + "\\test.txt").ToList();

        void GetFish(int days)
        {
            List<long> fishyInput = input[0].Split(',').Select(x => long.Parse(x)).ToList();
            List<long> allFishy = new List<long>();

            for (int i = 0; i <= 8; i++) allFishy.Add(fishyInput.Count(x => x == i));
            for (int i = 0; i < days; i++)
            {
                allFishy[7] += allFishy[0];
                allFishy.Add(allFishy[0]);
                allFishy.RemoveAt(0);
            }

            Console.WriteLine(allFishy.Sum());
        }
        static void Main(string[] args)
        {
            Program p = new Program();
            p.GetFish(80);
            p.GetFish(256);
        }
    }
}
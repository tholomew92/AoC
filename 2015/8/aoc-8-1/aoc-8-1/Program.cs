using System.Text.RegularExpressions;

namespace aoc_10_new
{
    class Program
    {
        static string workdir = Environment.CurrentDirectory;
        static string inputPath = new DirectoryInfo(workdir).Parent.Parent.Parent.Parent.Parent.ToString();
        static readonly List<string> input = File.ReadAllLines(inputPath + "\\input.txt").ToList();
        //static readonly List<string> input = File.ReadAllLines(inputPath + "\\test.txt").ToList();

        long GetValue(string line)
        {
            Console.WriteLine(Regex.Replace(line.Replace("\\\"", "A").Replace("\\\\", "B"), "\\\\x[a-f0-9]{2}", "C"));
            return Regex.Replace(line.Replace("\\\"", "A").Replace("\\\\", "B"), "\\\\x[a-f0-9]{2}", "C").Length;
        }

        long PartOne()
        {
            long result = 0;

            foreach(var line in input)
            {
                result += (line.Length - GetValue(line.Trim('"')));
            }
            return result;
        }

        long PartTwo()
        {
            return 0;
        }

        void Run()
        {
            long result;
            var watch = new System.Diagnostics.Stopwatch();
            watch.Start();
            result = PartOne();
            watch.Stop();
            Console.WriteLine($"The result for part one is: {result}");
            Console.WriteLine($"Time is {watch.ElapsedMilliseconds} ms");
            watch.Reset();
            watch.Start();
            result = PartTwo();
            watch.Stop();
            Console.WriteLine($"The result for part one is: {result}");
            Console.WriteLine($"Time is {watch.ElapsedMilliseconds} ms");
        }

        static void Main(string[] args)
        {
            Program p = new();
            p.Run();
        }
    }
}
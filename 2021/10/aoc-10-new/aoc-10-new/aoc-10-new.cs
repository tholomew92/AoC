namespace aoc_10_new
{
    class Program
    {
        static string workdir = Environment.CurrentDirectory;
        static string inputPath = new DirectoryInfo(workdir).Parent.Parent.Parent.Parent.Parent.ToString();
        static readonly List<string> input = File.ReadAllLines(inputPath + "\\input.txt").ToList();
        //static readonly List<string> input = File.ReadAllLines(inputPath + "\\test.txt").ToList();

        int PartOne()
        {
            Dictionary<char, char> brackets = new Dictionary<char, char>() { { '(', ')' }, { '[', ']' }, { '{', '}' }, { '<', '>' } };
            Dictionary<char, int> points = new Dictionary<char, int>() { { ')', 3 }, { ']', 57 }, { '}', 1197 }, { '>', 25137 } };
            Stack<char> symbols = new Stack<char>();
            int score = 0;
            foreach (var line in input)
            {
                foreach (char symbol in line)
                {
                    if (brackets.ContainsKey(symbol)) symbols.Push(symbol);
                    else
                    {
                        char topStack = symbols.Pop();
                        if (symbol != brackets[topStack])
                        {
                            if (points.ContainsKey(symbol)) score += points[symbol]; 
                        }
                    }
                }
            }
            return score;
        }

        long PartTwo()
        {
            Dictionary<char, char> brackets = new Dictionary<char, char>() { { '(', ')' }, { '[', ']' }, { '{', '}' }, { '<', '>' } };
            Dictionary<char, int> points = new Dictionary<char, int>() { { '(', 1 }, { '[', 2 }, { '{', 3 }, { '<', 4 } };
            List<long> scores = new List<long>();
            foreach (var line in input)
            {
                Stack<char> symbols = new Stack<char>();
                long score = 0;
                bool corrupted = false;
                foreach (char symbol in line)
                {
                    if (brackets.ContainsKey(symbol)) symbols.Push(symbol);
                    else
                    {
                        char topStack = symbols.Pop();
                        if (symbol != brackets[topStack])
                        {
                            corrupted = true;
                            break;
                        }
                    }
                }
                while (!corrupted && symbols.Count > 0)
                {
                    char symbol = symbols.Pop();
                    if (points.ContainsKey(symbol)) score = score * 5 + points[symbol]; 
                }
                if (score > 0) scores.Add(score);
            }
            scores.Sort();
            return scores[scores.Count / 2];
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
            Program p = new Program();
            p.Run();
        }
    }
}
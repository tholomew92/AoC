namespace aoc_10_new
{
    class Program
    {
        static string workdir = Environment.CurrentDirectory;
        static string inputPath = new DirectoryInfo(workdir).Parent.Parent.Parent.Parent.Parent.ToString();
        static readonly List<string> input = File.ReadAllLines(inputPath + "\\input.txt").ToList();
        //static readonly List<string> input = File.ReadAllLines(inputPath + "\\test.txt").ToList();
        void IllegalLines()
        {
            Dictionary<char, char> brackets = new Dictionary<char, char>() { { '(', ')' }, { '[', ']' }, { '{', '}' }, { '<', '>' } };
            Dictionary<char, int> corruptPoints = new Dictionary<char, int>() { { ')', 3 }, { ']', 57 }, { '}', 1197 }, { '>', 25137 } };
            Dictionary<char, int> incompletePoints = new Dictionary<char, int>() { { '(', 1 }, { '[', 2 }, { '{', 3 }, { '<', 4 } };
            List<long> scores = new List<long>();
            int corruptScore = 0;
            foreach (var line in input)
            {
                Stack<char> symbols = new Stack<char>();
                long incompleteScore = 0;
                bool corrupted = false;
                foreach (char symbol in line)
                {
                    if (brackets.ContainsKey(symbol)) symbols.Push(symbol);
                    else
                    {
                        char topStack = symbols.Pop();
                        if (symbol != brackets[topStack])
                        {
                            if (corruptPoints.ContainsKey(symbol)) corruptScore += corruptPoints[symbol];
                            corrupted = true;
                            break;
                        }
                    }
                }
                while (!corrupted && symbols.Count > 0)
                {
                    char symbol = symbols.Pop();
                    if (incompletePoints.ContainsKey(symbol)) incompleteScore = incompleteScore * 5 + incompletePoints[symbol];
                }
                if (incompleteScore > 0) scores.Add(incompleteScore);
            }
            scores.Sort();
            Console.WriteLine($"The result for part one is: {corruptScore}");
            Console.WriteLine($"The result for part two is: {scores[scores.Count/2]}");
        }


        static void Main(string[] args)
        {
            var watch = new System.Diagnostics.Stopwatch();
            watch.Start();
            Program p = new Program();
            p.IllegalLines();
            watch.Stop();
            Console.WriteLine($"Time is: {watch.ElapsedMilliseconds}");
        }
    }
}
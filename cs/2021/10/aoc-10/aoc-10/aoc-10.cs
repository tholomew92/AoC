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
            Dictionary<char,char> brackets = new Dictionary<char,char>();
            brackets.Add('(', ')');
            brackets.Add('[', ']');
            brackets.Add('{', '}');
            brackets.Add('<', '>');
            Stack<char> symbols = new Stack<char>();
            int score = 0;
            foreach (var line in input) {
                foreach (char symbol in line)
                {
                    if (brackets.ContainsKey(symbol)) symbols.Push(symbol);
                    else 
                    {
                        char topStack = symbols.Pop();
                        if (symbol != brackets[topStack]) 
                        {
                            if (symbol.Equals(')')) score += 3;
                            else if (symbol.Equals(']')) score += 57;
                            else if (symbol.Equals('}')) score += 1197;
                            else if (symbol.Equals('>')) score += 25137;
                        }
                    }
                }
            }
            return score;
        }

        long PartTwo()
        {
            Dictionary<char, char> brackets = new Dictionary<char, char>();
            brackets.Add('(', ')');
            brackets.Add('[', ']');
            brackets.Add('{', '}');
            brackets.Add('<', '>');
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
                bool notEmpty = true;
                while (notEmpty && !corrupted) 
                {
                    char symbol = symbols.Pop();
                    score *= 5;
                    if (symbol.Equals('(')) score += 1;
                    else if (symbol.Equals('[')) score += 2;
                    else if (symbol.Equals('{')) score += 3;
                    else if (symbol.Equals('<')) score += 4;
                    if(symbols.Count == 0) notEmpty = false;
                }
                if(score > 0) scores.Add(score);
            }
            scores.Sort();
            return scores[scores.Count/2];
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
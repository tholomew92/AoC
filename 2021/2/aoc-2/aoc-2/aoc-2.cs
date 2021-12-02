namespace aoc_1_new
{
    class Program
    {
        static string workdir = Environment.CurrentDirectory;
        static string inputPath = new DirectoryInfo(workdir).Parent.Parent.Parent.Parent.Parent.ToString();
        static readonly List<string> input = File.ReadAllLines(inputPath + "\\input.txt").ToList();
        //static readonly List<string> input = File.ReadAllLines(inputPath + "\\test.txt").ToList();


        void Run()
        {
            int result;
            var watch = new System.Diagnostics.Stopwatch();
            watch.Start();
            result = FindPosition();
            watch.Stop();
            Console.WriteLine($"The result for part one is: {result}");
            Console.WriteLine($"Time is {watch.ElapsedMilliseconds.ToString()} ms");
            watch.Reset();
            result = FindPositionPart2();
            watch.Stop();
            Console.WriteLine($"The result for part one is: {result}");
            Console.WriteLine($"Time is {watch.ElapsedMilliseconds.ToString()} ms");
        }

        int FindPosition()
        {
            int horizontal, depth;
            horizontal = depth = 0; 
            foreach(var line in input)
            {
                var instructions = line.Split(' ');
                _ = instructions[0] switch
                {
                    "forward" => horizontal += Int32.Parse(instructions[1]),
                    "up" => depth -= Int32.Parse(instructions[1]),
                    "down" => depth += Int32.Parse(instructions[1]),
                    _ => throw new Exception()
                };
            }
            return horizontal * depth;
        }

        int FindPositionPart2()
        {
            int horizontal, depth, aim;
            horizontal = depth = aim = 0;
            foreach (var line in input)
            {
                var instructions = line.Split(' ');
                switch (instructions[0])
                {
                    case "forward":
                        horizontal += Int32.Parse(instructions[1]);
                        depth += aim * Int32.Parse(instructions[1]);
                        break;
                    case "up":
                        aim -= Int32.Parse(instructions[1]);
                        break;
                    case "down":
                        aim += Int32.Parse(instructions[1]);
                        break;
                    default: throw new Exception();
                }
            }
            return horizontal * depth;
        }


        static void Main(string[] args)
        {
            Program p = new Program();
            p.Run();
        }
    }
}


Skip to content
Pull requests
Issues
Marketplace
Explore
@tholomew92
tholomew92 /
AoC
Public

1
0

    0

Code
Issues
Pull requests
Actions
Projects
Wiki
Security
Insights

    Settings

AoC/2021/5/aoc-5/aoc-5/Program.cs /
@tholomew92
tholomew92 Adding day 4 and 5
Latest commit ed9de57 24 minutes ago
History
1 contributor
173 lines (157 sloc) 5.28 KB
namespace aoc_4
{
    class Program
    {
        static string workdir = Environment.CurrentDirectory;
        static string inputPath = new DirectoryInfo(workdir).Parent.Parent.Parent.Parent.Parent.ToString();
        static readonly List<string> input = File.ReadAllLines(inputPath + "\\input.txt").ToList();
        //static readonly List<string> input = File.ReadAllLines(inputPath + "\\test.txt").ToList();


        int PartOne(int size)
        {
            int[,] matrix = new int[size, size];

            foreach (var line in input)
            {
                var splits = line.Split("->", StringSplitOptions.TrimEntries);
                var point1 = splits[0].Split(',');
                var point2 = splits[1].Split(',');
                int x1, y1, x2, y2;
                x1 = Int32.Parse(point1[0]);
                y1 = Int32.Parse(point1[1]);
                x2 = Int32.Parse(point2[0]);
                y2 = Int32.Parse(point2[1]);

                if (x1 == x2)
                {
                    if (y1 < y2)
                    {
                        for (int i = y1; i <= y2; i++)
                        {
                            matrix[x1, i]++;
                        }
                    }
                    else
                    {
                        for (int i = y2; i <= y1; i++)
                        {
                            matrix[x1, i]++;
                        }
                    }
                }
                else if (y1 == y2)
                {
                    if (x1 < x2)
                    {
                        for (int i = x1; i <= x2; i++)
                        {
                            matrix[i, y1]++;
                        }
                    }
                    else
                    {
                        for (int i = x2; i <= x1; i++)
                        {
                            matrix[i, y1]++;
                        }
                    }
                }


            }
            int count = 0;
            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    if (matrix[y, x] > 1) count++;
                }
            }

            return count;
        }

        int PartTwo(int size)
        {
            int[,] matrix = new int[size, size];

            foreach (var line in input)
            {
                var splits = line.Split("->", StringSplitOptions.TrimEntries);
                var point1 = splits[0].Split(',');
                var point2 = splits[1].Split(',');
                int x1, y1, x2, y2;
                x1 = Int32.Parse(point1[0]);
                y1 = Int32.Parse(point1[1]);
                x2 = Int32.Parse(point2[0]);
                y2 = Int32.Parse(point2[1]);
                int xDiff, yDiff;
                xDiff = x1 - x2;
                yDiff = y1 - y2;

                if (x1 == x2 && y1 != y2)
                {
                    if (y1 < y2)
                    {
                        for (int i = y1; i <= y2; i++)
                        {
                            matrix[x1, i]++;
                        }
                    }
                    else
                    {
                        for (int i = y2; i <= y1; i++)
                        {
                            matrix[x1, i]++;
                        }
                    }
                }
                else if (y1 == y2 && x1 != x2)
                {
                    if (x1 < x2)
                    {
                        for (int i = x1; i <= x2; i++)
                        {
                            matrix[i, y1]++;
                        }
                    }
                    else
                    {
                        for (int i = x2; i <= x1; i++)
                        {
                            matrix[i, y1]++;
                        }
                    }
                }
                else if (xDiff == yDiff || xDiff == -yDiff)
                {
                    for (int xy = 0; xy < Math.Abs(xDiff) + 1; xy++)
                    {
                        var xCoord = x1 + ((xDiff < 0) ? xy : xy * -1);
                        var yCoord = y1 + ((yDiff < 0) ? xy : xy * -1);
                        matrix[xCoord, yCoord]++;
                    }

                }
            }
            int count = 0;
            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    if (matrix[x, y] > 1) count++;
                }
            }

            return count;
        }


        void Run()
        {
            int result;
            var watch = new System.Diagnostics.Stopwatch();
            watch.Start();
            result = PartOne(1000);
            watch.Stop();
            Console.WriteLine($"The result for part one is: {result}");
            Console.WriteLine($"Time is {watch.ElapsedMilliseconds.ToString()} ms");
            watch.Reset();
            result = PartTwo(1000);
            watch.Stop();
            Console.WriteLine($"The result for part two is: {result}");
            Console.WriteLine($"Time is {watch.ElapsedMilliseconds.ToString()} ms");
        }

        static void Main(string[] args)
        {
            Program p = new Program();
            p.Run();
        }

    }
}

    © 2021 GitHub, Inc.

    Terms
    Privacy
    Security
    Status
    Docs
    Contact GitHub
    Pricing
    API
    Training
    Blog
    About

6 results found.
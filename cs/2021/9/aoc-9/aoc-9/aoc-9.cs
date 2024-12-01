namespace aoc_1_new
{
    class Program
    {
        static string workdir = Environment.CurrentDirectory;
        static string inputPath = new DirectoryInfo(workdir).Parent.Parent.Parent.Parent.Parent.ToString();
        //static readonly List<string> input = File.ReadAllLines(inputPath + "\\input.txt").ToList();
        static readonly List<string> input = File.ReadAllLines(inputPath + "\\test.txt").ToList();

        int PartOne()
        {
            int maxRow = input.Count;
            int maxColumn = input[0].Length;
            int[,] matrix = new int[maxRow, maxColumn];
            for (int i = 0; i < maxRow; i++)
            {
                string line = input[i];
                for (int j = 0; j < maxColumn; j++)
                {
                    matrix[i, j] = Int32.Parse(line.Substring(j, 1));
                }
            }
            int count = 0;
            for (int i = 0; i < maxRow; i++) 
            {
                for(int j = 0; j < maxColumn; j++) 
                {
                    bool isSmaller = true;
                    int value = matrix[i, j];
                    int min, max;
                    min = i - 1;
                    if (min < 0) min = 0;
                    max = i + 1;
                    if (max == maxRow) max--;
                    for (int k = min; k <= max; k++) 
                    {
                        if (k == i) continue;
                        if (value >= matrix[k, j]) isSmaller = false;
                    }
                    min = j - 1;
                    if (min < 0) min = 0;
                    max = j + 1;
                    if (max == maxColumn) max--;
                    for (int k = min; k <= max; k++)
                    {
                        if (k == j) continue;
                        if (value >= matrix[i, k]) isSmaller = false;
                    }
                    if (isSmaller) count = count + 1 + value; 
                }
            }

            return count;
        }

        int PartTwo()
        {
            int maxRow = input.Count;
            int maxColumn = input[0].Length;
            int[,] matrix = new int[maxRow, maxColumn];
            for (int i = 0; i < maxRow; i++)
            {
                string line = input[i];
                for (int j = 0; j < maxColumn; j++)
                {
                    matrix[i, j] = Int32.Parse(line.Substring(j, 1));
                }
            }
            List<int> biggestBasins = new List<int>();
            for (int i = 0; i < maxRow; i++)
            {
                for (int j = 0; j < maxColumn; j++)
                {
                    if (matrix[i, j] == 9 || matrix[i, j] == -1) continue;
                    else
                    {
                        matrix[i, j] = -1;
                        int count = 1;
                        if (i != 0)
                        {
                            for (int k = i - 1; k >= 0; k--)
                            {
                                if (matrix[k, j] == 9) break;
                                else
                                {
                                    count++;
                                    matrix[k, j] = -1;
                                }
                            }
                        }
                        if (j != 0)
                        {
                            for (int k = j - 1; k >= 0; k--)
                            {
                                if (matrix[i, k] == 9) break;
                                else
                                {
                                    count++;
                                    matrix[i, k] = -1;
                                }
                            }
                        }
                        if (i != maxRow-1)
                        {
                            for (int k = i + 1; k < maxRow; k++)
                            {
                                if (matrix[k, j] == 9) break;
                                else
                                {
                                    count++;
                                    matrix[k, j] = -1;
                                }
                            }
                        }
                        if (j != maxColumn - 1)
                        {
                            for (int k = j + 1; k < maxColumn; k++)
                            {
                                if (matrix[i, k] == 9) break;
                                else
                                {
                                    count++;
                                    matrix[i, k] = -1;
                                }
                            }
                        }
                        Console.WriteLine(count);
                        if (biggestBasins.Count < 3) biggestBasins.Add(count);
                        else if (count > biggestBasins.Min()) 
                        {
                            int min = biggestBasins.Min();
                            biggestBasins.Remove(min);
                            biggestBasins.Add(count);
                        }
                    }
                }
            }
            int sum = biggestBasins[0] * biggestBasins[1] * biggestBasins[2];
            return sum;
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
namespace aoc_11
{
    class Program
    {
        static string workdir = Environment.CurrentDirectory;
        static string inputPath = new DirectoryInfo(workdir).Parent.Parent.Parent.Parent.Parent.ToString();
        //static readonly List<string> input = File.ReadAllLines(inputPath + "\\input.txt").ToList();
        static readonly List<string> input = File.ReadAllLines(inputPath + "\\test.txt").ToList();

        int PartOne()
        {
            int minRow, minCol, maxRow, maxCol;
            minRow = minCol = 0;
            maxRow = input.Count;
            maxCol = input[0].Length;
            int[,] matrix = new int[maxRow,maxCol];
            for (int days = 0; days < 100; days++) 
            {
                for (int row = 0; row < maxRow; row++) 
                {
                    for(int col = 0; col < maxCol; col++)
                    {
                        int value = matrix[row,col];
                        if (value == 9) continue;
                        matrix[row, col]++;
                        if(value == 9)
                        {
                            int startRow = (row - 1 == minRow) ? row : row - 1;
                            int startCol = (col - 1 == minCol) ? col : col - 1;
                            int endRow = (row + 1 == maxRow) ? row : row + 1;
                            int endCol = (col + 1 > maxCol) ? col : col + 1;
                        }
                        
                    }
                }
            }

            return -1;
        }

        int PartTwo()
        {
            
            return -2;
        }

        void Run()
        {
            int result;
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
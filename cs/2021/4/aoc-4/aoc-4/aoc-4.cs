namespace aoc_4
{
    class Program
    {
        static string workdir = Environment.CurrentDirectory;
        static string inputPath = new DirectoryInfo(workdir).Parent.Parent.Parent.Parent.Parent.ToString();
        static readonly List<string> input = File.ReadAllLines(inputPath + "\\input.txt").ToList();
        //static readonly List<string> input = File.ReadAllLines(inputPath + "\\test.txt").ToList();

        private List<int> numbers = input[0].Split(',').Where(x => !string.IsNullOrWhiteSpace(x)).Select(int.Parse).ToList();

        public List<int[,]> boards = new List<int[,]>();

        void ParseBoards() { 
            int[,] board = new int[5,5];
            int rowNumber = 0;

            foreach (var line in input.Skip(1))
            {
                if (string.IsNullOrWhiteSpace(line)) continue;

                var row = line.Split(' ').Where(x => !string.IsNullOrWhiteSpace(x)).Select(int.Parse).ToArray();

                for (int i = 0; i < row.Length; i++) board[rowNumber, i] = row[i];

                rowNumber++;

                if (rowNumber == 5)
                {
                    boards.Add(board);

                    board = new int[5, 5];
                    rowNumber = 0;

                    continue;
                }

            }
        }

        void Run()
        {
            int result;
            var watch = new System.Diagnostics.Stopwatch();
            watch.Start();
            ParseBoards();
            result = PartOne();
            watch.Stop();
            Console.WriteLine($"The result for part one is: {result}");
            Console.WriteLine($"Time is {watch.ElapsedMilliseconds.ToString()} ms");
            watch.Reset();
            ParseBoards();
            result = PartTwo();
            watch.Stop();
            Console.WriteLine($"The result for part one is: {result}");
            Console.WriteLine($"Time is {watch.ElapsedMilliseconds.ToString()} ms");
        }

        int PartOne() 
        {

            foreach (int number in numbers)
            {
                foreach (var board in boards)
                {
                    for (int row = 0; row < 5; row++)
                    {
                        for (int column = 0; column < 5; column++)
                        {
                            if (board[row, column] == number)
                            {
                                board[row, column] = -1;
                                bool winner = CheckIfWinner(board,row,column,number);
                                if (winner) {

                                    return number * GetWinningSum(board);
                                }
                            }
                        }
                    }
                }

            }
            return 1;
        }

        int PartTwo()
        {
            List<int[,]> winners = new List<int[,]>();
            foreach (int number in numbers)
            {
                foreach (var board in boards)
                {
                    for (int row = 0; row < 5; row++)
                    {
                        for (int column = 0; column < 5; column++)
                        {
                            if (board[row, column] == number)
                            {
                                board[row, column] = -1;
                                bool winner = false;
                                if(!winners.Contains(board)) winner = CheckIfWinner(board, row, column, number);
                                if (winner)
                                {
                                    winners.Add(board);
                                    if (winners.Count == boards.Count) return number * GetWinningSum(board);
                                }
                            }
                        }
                    }
                }

            }

            return 1;
        }

        static bool CheckIfWinner(int[,] board, int row, int column, int number)
        {
            int winCount = 0;
            for (int i = 0; i < 5; i++)
            {
                if (i == column) continue;

                if (board[row, i] == -1) winCount++;
                if (winCount == 4)
                {
                    return true;
                }
            }
            
            winCount = 0;
            for (int i = 0; i < 5; i++) {
                if (i == row) continue;

                if (board[i, column] == -1) winCount++;
                if (winCount == 4)
                {
                    return true;
                }
            }

            return false;
        }

        static int GetWinningSum(int[,] board)
        {
            int sum = 0;
            for (int row = 0; row < 5; row++) 
            {
                for (int column = 0; column < 5; column++) 
                {
                    if(board[row,column] != -1)
                        sum = sum + board[row,column];
                }
            }
            return sum;
        }


        static void Main(string[] args)
        {
            Program p = new Program();
            p.Run();
        }
    }


}


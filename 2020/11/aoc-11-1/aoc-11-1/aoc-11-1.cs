using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace aoc_11_1
{
    class Program
    {
        //static readonly List<string> input = File.ReadAllLines(@"C:\Users\Sebbe\Desktop\aoc\2020\11\test.txt").ToList();
        static readonly List<string> input = File.ReadAllLines(@"C:\Users\Sebbe\Desktop\aoc\2020\11\input.txt").ToList();
        char[,] seats = new char[input.Count, input[0].Length];
        public static readonly char TakenSeatToken = '#';
        public static readonly char AvailableSeatToken = 'L';
        public static readonly char FloorToken = '.';

        void FindSeats()
        {
            bool adjustmen = true;
            int prevCount = 0;
            while (adjustmen)
            {
                char[,] tempSeats = new char[input.Count, input[0].Length];
                for (int i = 0; i < input.Count; i++)
                {
                    string row = input[i];
                    for (int j = 0; j < row.Length; j++)
                    {
                        if (row[j].Equals(AvailableSeatToken))
                        {
                            int check = CountSurroundingSeats(i, j);
                            if (check == 0)
                            {
                                tempSeats[i, j] = TakenSeatToken;
                            }
                            else if (check == 1)
                            {
                                tempSeats[i, j] = AvailableSeatToken;
                            }
                            else
                                tempSeats[i, j] = seats[i, j];
                        }
                        else
                            tempSeats[i, j] = FloorToken;
                    }
                }
                seats = tempSeats;
                int c = 0;
                for (int i = 0; i < input.Count; i++)
                {
                    for (int j = 0; j < input[0].Length; j++)
                    {
                        if (seats[i, j].Equals(TakenSeatToken)) c++;
                    }
                }
                if (c == prevCount) adjustmen = false;
                else prevCount = c;
            }


            Console.WriteLine(prevCount);

        }

        int CountSurroundingSeats(int x, int y)
        {
            int startx, starty, stopx, stopy, count;
            startx = x - 1 < 0 ? 0 : x - 1;
            starty = y - 1 < 0 ? 0 : y - 1;
            stopx = x + 1 >= input.Count ? input.Count - 1 : x + 1;
            stopy = y + 1 >= input[0].Length ? input[0].Length - 1 : y + 1;
            count = 0;
            for (int i = startx; i <= stopx; i++)
            {
                for (int j = starty; j <= stopy; j++)
                {
                    if (i == x & j == y)
                        continue;
                    else
                    {
                        if (seats[i, j].Equals('#'))
                            count++;
                    }
                    
                }
            }

            if (count == 0)
                return 0;
            else if (count >= 4)
                return 1;
            else
                return -1;
        }



        static void Main(string[] args)
        {
            
            var watch = System.Diagnostics.Stopwatch.StartNew();
            Program p = new Program();
            p.FindSeats();
            watch.Stop();
            Console.WriteLine(watch.ElapsedMilliseconds);
        }
    }
}

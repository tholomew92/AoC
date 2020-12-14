using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace aoc_11_2
{
    class Program
    {
        //static readonly List<string> input = File.ReadAllLines(@"C:\Users\Sebbe\Desktop\aoc\2020\11\test.txt").ToList();
        static readonly List<string> input = File.ReadAllLines(@"C:\Users\Sebbe\Desktop\aoc\2020\11\input.txt").ToList();
        public static readonly char TakenSeatToken = '#';
        public static readonly char AvailableSeatToken = 'L';
        public static readonly char FloorToken = '.';
        public static readonly int MaxYLength = input[0].Length;
        char[,] seats = new char[input.Count, MaxYLength];

        void FindSeats()
        {
            bool adjustment = true;
            //int adjustment = 0;
            int prevCount = 0;
            while (adjustment)
            {
                char[,] tempSeats = new char[input.Count, input[0].Length];
                for (int i = 0; i < input.Count; i++)
                {
                    string row = input[i];
                    for (int j = 0; j < MaxYLength; j++)
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
                    for (int j = 0; j < MaxYLength; j++)
                    {
                        //Console.Write(seats[i, j]);
                        if (seats[i, j].Equals(TakenSeatToken)) c++;
                    }
                    //Console.WriteLine();
                }
                //Console.WriteLine(c);
                //adjustment++;
                if (c == prevCount) adjustment = false;
                prevCount = c;
            }
            Console.WriteLine(prevCount);


        }

        int CountSurroundingSeats(int x, int y)
        {
            int count = FindSeatUp(x - 1, y);
            count += FindSeatDown(x + 1, y);
            count += FindSeatLeft(x, y - 1);
            count += FindSeatRight(x, y + 1);
            count += FindSeatDiagUpLeft(x - 1, y - 1);
            count += FindSeatDiagUpRight(x - 1, y + 1);
            count += FindSeatDiagDownLeft(x + 1, y - 1);
            count += FindSeatDiagDownRight(x + 1, y + 1);
            if (count == 0)
                return 0;
            else if (count > 4)
                return 1;
            else
                return -1;
        }

        int FindSeatUp(int x, int y)
        {
            if (x < 0)
                return 0;
            else
            {
                char seat = seats[x, y];
                return seat switch
                {
                    '#' => 1,
                    'L' => 0,
                    '.' => FindSeatUp(x - 1, y),
                    _ => 0
                };
            }
        }

        int FindSeatDown(int x, int y)
        {
            if (x >= input.Count)
                return 0;
            else
            {
                char seat = seats[x, y];
                return seat switch
                {
                    '#' => 1,
                    'L' => 0,
                    '.' => FindSeatDown(x + 1, y),
                    _ => 0
                };
            }
        }

        int FindSeatLeft(int x, int y)
        {
            if (y < 0)
                return 0;
            else
            {
                char seat = seats[x, y];
                return seat switch
                {
                    '#' => 1,
                    'L' => 0,
                    '.' => FindSeatLeft(x, y - 1),
                    _ => 0
                };
            }
        }

        int FindSeatRight(int x, int y)
        {
            if (y >= MaxYLength)
                return 0;
            else
            {
                char seat = seats[x, y];
                return seat switch
                {
                    '#' => 1,
                    'L' => 0,
                    '.' => FindSeatRight(x, y + 1),
                    _ => 0
                };
            }
        }

        int FindSeatDiagUpLeft(int x, int y)
        {
            if (x < 0 | y < 0)
                return 0;
            else
            {
                char seat = seats[x, y];
                return seat switch
                {
                    '#' => 1,
                    'L' => 0,
                    '.' => FindSeatDiagUpLeft(x - 1, y - 1),
                    _ => 0
                };
            }
        }

        int FindSeatDiagUpRight(int x, int y)
        {
            if (x < 0 | y >= MaxYLength)
                return 0;
            else
            {
                char seat = seats[x, y];
                return seat switch
                {
                    '#' => 1,
                    'L' => 0,
                    '.' => FindSeatDiagUpRight(x - 1, y + 1),
                    _ => 0
                };
            }
        }

        int FindSeatDiagDownLeft(int x, int y)
        {
            if (x >= input.Count | y < 0)
                return 0;
            else
            {
                char seat = seats[x, y];
                return seat switch
                {
                    '#' => 1,
                    'L' => 0,
                    '.' => FindSeatDiagDownLeft(x + 1, y - 1),
                    _ => 0
                };
            }
        }

        int FindSeatDiagDownRight(int x, int y)
        {
            if (x >= input.Count | y >= MaxYLength)
                return 0;
            else
            {
                char seat = seats[x, y];
                return seat switch
                {
                    '#' => 1,
                    'L' => 0,
                    '.' => FindSeatDiagDownRight(x + 1, y + 1),
                    _ => 0
                };
            }
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

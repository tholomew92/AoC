using System;
using System.Collections.Generic;
using System.Linq;

namespace aoc_3_1
{
    class Program
    {
        static readonly List<String> input = System.IO.File.ReadLines(@"C:\Users\Sebbe\Desktop\aoc\2015\3\input.txt").ToList();
        void FollowSantasPath()
        {
            var parsed = input[0];
            Console.WriteLine(parsed);
            Console.WriteLine(parsed.Length);
            int[,] path = new int[10000, 10000];
            int x, y, a, b, count, houses;
            count = houses = 0;
            x = y = a = b = 5000;
            path[x, y]++;
            path[x, y]++;
            houses++;
            foreach (var p in parsed)
            {
                if (count % 2 == 0)
                {
                    switch (p)
                    {
                        case '>':
                            x++;
                            break;
                        case '<':
                            x--;
                            break;
                        case '^':
                            y++;
                            break;
                        case 'v':
                            y--;
                            break;
                    }
                    Console.WriteLine("x is {0}, y is {1}", x, y);
                    path[x, y]++;
                    if (path[x, y] == 1) houses++;
                }
                else
                {
                    switch (p)
                    {
                        case '>':
                            a++;
                            break;
                        case '<':
                            a--;
                            break;
                        case '^':
                            b++;
                            break;
                        case 'v':
                            b-- ;
                            break;
                    }
                    Console.WriteLine("a is {0}, b is {1}", a, b);
                    path[a, b]++;
                    if (path[a, b] == 1) houses++;
                }
                count++;
            }
            Console.WriteLine(houses);
        }


        static void Main(string[] args)
        {
            Program p = new Program();
            p.FollowSantasPath();
        }
    }
}

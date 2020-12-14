using System;

namespace data
{
    class Program
    {


        static void Main(string[] args)
        {
            string SampleInput = @"L.LL.LL.LL
#.L#.##.L#
#L#####.LL
L.#.#..#..
##L#.##.##
#.##.#L.##
#.#####.#L
..#.#.....
LLL####LL#
#.L#####.L
#.L####.L#";
            int count = 0;
            foreach(var row in SampleInput)
            {
                if (row.Equals('#')) count++;
            }
            Console.WriteLine(count);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace aoc_5_1
{
    class Program
    {
        static string workdir = Environment.CurrentDirectory;
        static string inputPath = new DirectoryInfo(workdir).Parent.Parent.Parent.Parent.Parent.ToString();
        List<string> input = File.ReadAllLines(inputPath + "\\input.txt").ToList();

        void ParseTickets()
        {
            int highestSeatID = 0;
            int amountOfRows = 128;
            int amountOfColumns = 8;
            foreach(var ticket in input)
            {
                int rowRange = amountOfRows;
                int lowRowRan = 0;
                int upRowRan = 127;
                String row = ticket.Substring(0, ticket.Length - 3);
                foreach(var r in row)
                {
                    rowRange /= 2;
                    if (r.Equals('B'))
                        lowRowRan += rowRange;
                    else if (r.Equals('F'))
                        upRowRan -= rowRange;
                    Console.WriteLine("Range is now " + lowRowRan + " " + upRowRan + " Debug: " + rowRange);
                }

                int colRange = amountOfColumns;
                int lowColRan = 0;
                int upColRan = 7;
                String column = ticket.Substring(ticket.Length - 3);
                foreach (var c in column)
                {
                    colRange /= 2;
                    if (c.Equals('R'))
                        lowColRan += colRange;
                    else if (c.Equals('L'))
                        upColRan -= colRange;
                    Console.WriteLine("Range is now " + lowColRan + " " + upColRan + " Debug: " + colRange);
                }
                if (lowRowRan * 8 + lowColRan > highestSeatID)
                    highestSeatID = lowRowRan * 8 + lowColRan;
            }

            Console.WriteLine(highestSeatID);
        }

        static void Main(string[] args)
        {
            Program p = new Program();
            p.ParseTickets();
        }
    }
}

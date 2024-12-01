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
		
        List<int> seatIDList = new List<int>();
        void ParseTickets()
        {
            int amountOfRows = 128;
            int amountOfColumns = 8;
            foreach (var ticket in input)
            {
                int rowRange = amountOfRows;
                int lowRowRan = 0;
                int upRowRan = 127;
                String row = ticket[0..^3];
                foreach (var r in row)
                {
                    rowRange /= 2;
                    if (r.Equals('B'))
                        lowRowRan += rowRange;
                    else if (r.Equals('F'))
                        upRowRan -= rowRange;
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
                }
                int seatID = lowRowRan * 8 + lowColRan;
                seatIDList.Add(seatID);
            }

            Console.WriteLine(FindMySeat());
        }

        int FindMySeat()
        {
            seatIDList.Sort();
            for(int i = 0; i<seatIDList.Count-1; i++)
            {
                if (seatIDList[i + 1] - seatIDList[i] == 2)
                    return seatIDList[i] + 1;
            }

            return 0;
        }

        static void Main(string[] args)
        {
            Program p = new Program();
            p.ParseTickets();
        }
    }
}

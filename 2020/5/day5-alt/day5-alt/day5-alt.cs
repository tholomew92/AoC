using System;
using System.Collections.Generic;
using System.Linq;

namespace aoc_5_1
{
    class Program
    {
        static readonly List<String> input = System.IO.File.ReadLines(@"C:\Users\Sebbe\Desktop\aoc\2020\5\input.txt").ToList();


        void ParseTickets()
        {
            List<int> seatIDList = new List<int>();
            int highestSeatID = 0;
            foreach (var ticket in input)
            {
                String row = ticket.Substring(0, ticket.Length - 3);
                var rowbyte = 0;
                foreach (var r in row)
                {
                    rowbyte = rowbyte << 1;
                    if (r.Equals('B')) rowbyte = rowbyte | 1;
                }
                String column = ticket.Substring(ticket.Length - 3);
                var colbyte = 0;
                foreach (var c in column)
                {
                    colbyte = colbyte << 1;
                    if (c.Equals('R')) colbyte = colbyte | 1;
                }
                int seatID = rowbyte * 8 + colbyte;
                seatIDList.Add(seatID);
                if (seatID > highestSeatID)
                    highestSeatID = seatID;
            }

            seatIDList.Sort();
            for(int i = 0; i < seatIDList.Count-1; i++)
            {
                if (seatIDList[i + 1] - seatIDList[i] == 2)
                    Console.WriteLine(seatIDList[i] + 1);
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

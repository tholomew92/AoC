using System;
using System.Collections.Generic;

namespace aoc_1_1
{
    class Program
    {
        static readonly String[] input = System.IO.File.ReadAllLines(@"C:\Users\Sebbe\Desktop\aoc\2019\1\input.txt");

        List<int> allFuels = new List<int>();

        Int64 FuelCalculate()
        {
            Int64 amountOfFuel = 0;

            foreach (String value in input)
            {
                int mass = int.Parse(value);
                amountOfFuel += TotalMassForFuel(mass);

            }

            foreach (int value in allFuels)
                Console.WriteLine(value);

            return amountOfFuel;
        }

        int TotalMassForFuel(int mass)
        {
            int total = 0;
            do
            {
                mass = mass / 3 - 2;
                if (mass > 0)
                {
                    total += mass;
                }
            } while (mass > 0);
            allFuels.Add(total);
            return total;
        }


        static void Main(string[] args)
        {
            Program p = new Program();
            Console.WriteLine(p.FuelCalculate());
        }
    }
}

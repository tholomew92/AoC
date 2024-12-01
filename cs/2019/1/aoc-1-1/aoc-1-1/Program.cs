using System;

namespace aoc_1_1
{
    class Program
    {
        static readonly String[] input = System.IO.File.ReadAllLines(@"C:\Users\Sebbe\Desktop\aoc\2019\1\input.txt");

        Int64 FuelCalculate()
        {
            Int64 amountOfFuel = 0;

            foreach(String value in input)
            {
                amountOfFuel = amountOfFuel + ((int.Parse(value)/3)+2);
            }

            return amountOfFuel;
        }

        static void Main(string[] args)
        {
            Program p = new Program();
            Console.WriteLine(p.FuelCalculate());
        }
    }
}

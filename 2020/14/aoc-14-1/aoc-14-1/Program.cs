using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace aoc_14_1
{
    class Program
    {
        List<string> input = File.ReadAllLines(@"C:\Users\sega9727\source\repos\14\input.txt").ToList();

        void ParseBits()
        {
            Dictionary<int, string> memoryList = new Dictionary<int, string>();
            string mask = "";
            foreach (var row in input)
            {
                var rowsplit = row.Split("=");
                if (rowsplit[0].Trim().Equals("mask"))
                {
                    mask = rowsplit[1].Trim();
                }
                else
                {
                    string mem = rowsplit[0][4..^2];
                    int memory = int.Parse(mem);
                    int value = int.Parse(rowsplit[1].Trim());
                    string bitValue = Convert.ToString(value, 2);
                    bitValue = bitValue.PadLeft(36, '0');
                    string newValue = "";
                    for (int i = 0; i < mask.Length; i++)
                    {
                        if (!mask[i].Equals('X'))
                        {
                            Console.WriteLine(i + "," + bitValue);
                            newValue += mask[i].ToString();
                        }
                        else
                        {
                            Console.WriteLine(i + "," + bitValue);
                            newValue += bitValue[i].ToString();
                        }
                    }
                    if (!memoryList.ContainsKey(memory)) memoryList.Add(memory, newValue);
                    else memoryList[memory] = newValue;
                }
            }
            long total = 0;
            foreach (var value in memoryList)
            {
                total += Convert.ToInt64(value.Value, 2);
            }
            Console.WriteLine(total);
            
        }

        static void Main(string[] args)
        {
            Program p = new Program();
            p.ParseBits();
        }
    }
}

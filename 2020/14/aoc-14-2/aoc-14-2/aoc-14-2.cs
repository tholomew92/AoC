using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;

namespace aoc_14_2
{
    class Program
    {
        static string workdir = Environment.CurrentDirectory;
        static string inputPath = new DirectoryInfo(workdir).Parent.Parent.Parent.Parent.Parent.ToString();
        List<string> input = File.ReadAllLines(inputPath + "\\input.txt").ToList();
        //List<string> input = File.ReadAllLines(inputPath + "\\test.txt").ToList();

        void ParseBits()
        {
            Dictionary<Int64, int> memoryList = new Dictionary<Int64, int>();
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
                    string bitValue = Convert.ToString(memory, 2);
                    bitValue = bitValue.PadLeft(36, '0');
                    List<int> xPos = new List<int>();
                    StringBuilder sb = new StringBuilder(bitValue);
                    for (int i = 0; i < mask.Length; i++)
                    {
                        if (mask[i].Equals('X'))
                        {
                            sb[i] = 'X';
                            xPos.Add(i);
                        }
                        else if (mask[i].Equals('1'))
                        {
                            sb[i] = '1';
                        }
                    }
                    bitValue = sb.ToString();

                    List<string> mems = ReturnAdresses(xPos, bitValue);
                    foreach(string s in mems)
                    {
                        Int64 memPos = Convert.ToInt64(s, 2);
                        if (!memoryList.ContainsKey(memPos)) memoryList.Add(memPos, value);
                        else memoryList[memPos] = value;
                    }
                }
            }
            long total = 0;
            foreach (var value in memoryList)
            {
                total += value.Value;
            }
            Console.WriteLine(total);

        }

        List<string> ReturnAdresses(List<int> pos, string bitvalue)
        {
            int position = pos[0];
            pos.Remove(position);
            List<string> adresses = new List<string>();
            if (pos.Any())
            {
                List<string> tempList = ReturnAdresses(pos, bitvalue);
                foreach (string adress in tempList)
                {
                    StringBuilder sb = new StringBuilder(adress);
                    sb[position] = '0';
                    adresses.Add(sb.ToString());
                    sb[position] = '1';
                    adresses.Add(sb.ToString());
                }
            }
            else
            {
                StringBuilder sb = new StringBuilder(bitvalue);
                sb[position] = '0';
                adresses.Add(sb.ToString());
                sb[position] = '1';
                adresses.Add(sb.ToString());
            }

            return adresses;
        }

        static void Main(string[] args)
        {
            Program p = new Program();
            p.ParseBits();

            

        }
    }
}

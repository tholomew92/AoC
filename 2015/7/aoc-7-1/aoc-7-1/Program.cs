using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace aoc_7_1
{
    class Program
    {
        static readonly List<String> input = System.IO.File.ReadLines("C:\\Users\\Sebbe\\Desktop\\aoc\\2015\\7\\input.txt").ToList();
        Dictionary<string, uint> memory = new Dictionary<string, uint>();
        int max = 65079;
        void ParseInput()
        {
            foreach(var row in input)
            {
                string[] line = row.Split("->", StringSplitOptions.RemoveEmptyEntries);
                memory.Add(line[1].Trim(), 0);
                if(!line[0].Trim().Contains(" "))
                {
                    Console.WriteLine(row);
                    uint v1;
                    if (int.TryParse(line[0].Trim(), out int x)) v1 = (uint)x;
                    else if (memory.ContainsKey(line[0])) v1 = memory[line[0]];
                    else v1 = 0;
                    memory[line[1].Trim()] = v1;
                }
                else
                {
                    string[] result = HandleInstruction(line).Split(",",StringSplitOptions.RemoveEmptyEntries);
                    memory[result[0]] = uint.Parse(result[1]);
                }

            }

        }

        /*string SetValue(string row)
        {


        }*/


        string HandleInstruction(string[] line)
        {
            var instr = line[0].Split(" ", StringSplitOptions.RemoveEmptyEntries);
            return instr[instr.Length - 2] switch
            {
                "AND" => AndInstr(instr, line[1]),
                "OR" => OrInstr(instr, line[1]),
                "LSHIFT" => LShInstr(instr, line[1]),
                "RSHIFT" => RShfInstr(instr, line[1]),
                "NOT" => NotInstr(instr, line[1]),
                _ => "",
            };
        }

        string AndInstr(string[] line, string key)
        {
            bool intTest = uint.TryParse(line[0].Trim(), out uint x);
            uint v1;
            if (intTest) v1 = x;
            else if (memory.ContainsKey(line[0])) v1 = memory[line[0]];
            else v1 = 0;
            intTest = uint.TryParse(line[2].Trim(), out uint y);
            uint v2;
            if (intTest) v2 = y;
            else if (memory.ContainsKey(line[2])) v2 = memory[line[2]];
            else v2 = 0;
            var value = v1 & v2;
            string s = key.Trim() + "," + value;
            Console.WriteLine(s);
            return s;
        }

        string OrInstr(string[] line, string key)
        {
            bool intTest = uint.TryParse(line[0].Trim(), out uint x);
            uint v1;
            if (intTest) v1 = x;
            else if (memory.ContainsKey(line[0])) v1 = memory[line[0]];
            else v1 = 0;
            intTest = uint.TryParse(line[2].Trim(), out uint y);
            uint v2;
            if (intTest) v2 = y;
            else if (memory.ContainsKey(line[2])) v2 = memory[line[2]];
            else v2 = 0;
            var value = v1 | v2;
            string s = key.Trim() + "," + value;
            return s;
        }

        string LShInstr(string[] line, string key)
        {
            bool intTest = uint.TryParse(line[0].Trim(), out uint x);
            uint v1;
            if (intTest) v1 = x;
            else if (memory.ContainsKey(line[0])) v1 = memory[line[0]];
            else v1 = 0;
            int v2 = int.Parse(line[2].Trim());
            var value = v1 << v2;
            string s = key.Trim() + "," + value;
            return s;
        }

        string RShfInstr(string[] line, string key)
        {
            bool intTest = uint.TryParse(line[0].Trim(), out uint x);
            uint v1;
            if (intTest) v1 = x;
            else if (memory.ContainsKey(line[0])) v1 = memory[line[0]];
            else v1 = 0;
            int v2 = int.Parse(line[2].Trim());
            var value = v1 >> v2;
            string s = key.Trim() + "," + value;
            return s;
        }
        
        string NotInstr(string[] line, string key)
        {
            bool intTest = uint.TryParse(line[0].Trim(), out uint x);
            uint v1;
            if (intTest) v1 = x;
            else if (memory.ContainsKey(line[0])) v1 = memory[line[0]];
            else v1 = 0;
            var value = (UInt16) ~v1;
            string s = key.Trim() + "," + value;
            return s;
        }

        static void Main(string[] args)
        {
            Program p = new Program();
            p.ParseInput();
        }
    }
}

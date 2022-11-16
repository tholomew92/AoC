using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace aoc_7_1
{
    class Program
    {
        static string workdir = Environment.CurrentDirectory;
        static string inputPath = new DirectoryInfo(workdir).Parent.Parent.Parent.Parent.Parent.ToString();
        static readonly List<string> input = File.ReadAllLines(inputPath + "\\input.txt").ToList();
        //static readonly List<string> input = File.ReadAllLines(inputPath + "\\test.txt").ToList();
        Dictionary<string, string> instructions = new Dictionary<string, string>();
        Dictionary<string, ushort> memory = new Dictionary<string, ushort>();
        //int max = 65079;
        void ParseInput()
        {
            foreach (var row in input)
            {
                string[] line = row.Split("->", StringSplitOptions.RemoveEmptyEntries);

                instructions[line[1].Trim()] = line[0].Trim();
            }
        }

        ushort PartOne()
        {
            HandleInstruction("a");
            return memory["a"];
        }

        ushort PartTwo(ushort partOneResult)
        {
            memory.Clear();
            instructions["b"] = partOneResult.ToString();
            HandleInstruction("a");
            return memory["a"];
        }

        void HandleInstruction(string key)
        {
            string instr = instructions[key];
                memory[key] = instr switch
                {
                    var _ when instr.Contains("AND") => AndInstr(instr),
                    var _ when instr.Contains("OR") => OrInstr(instr),
                    var _ when instr.Contains("LSHIFT") => LShInstr(instr),
                    var _ when instr.Contains("RSHIFT") => RShInstr(instr),
                    var _ when instr.Contains("NOT") => NotInstr(instr),
                    _ => AssInstr(instr),
                };
               ;
        }

        ushort AndInstr(string instr)
        {
            var values = instr.Split(" AND ", StringSplitOptions.RemoveEmptyEntries);
            ushort val1, val2;
            var isNumber = ushort.TryParse(values[0].Trim(), out val1);
            if (!isNumber)
            {
                if (memory.ContainsKey(values[0].Trim())) val1 = memory[values[0]];
                else
                {
                    HandleInstruction(values[0].Trim());
                    val1 = memory[values[0]];
                }
            }
            isNumber = ushort.TryParse(values[1].Trim(), out val2);
            if (!isNumber)
            {
                if (memory.ContainsKey(values[1].Trim())) val2 = memory[values[1]];
                else
                {
                    HandleInstruction(values[1].Trim());
                    val2 = memory[values[1]];
                }
            }
            var value = val1 & val2;
            return (ushort)value;
        }

        ushort OrInstr(string instr)
        {
            var values = instr.Split(" OR ", StringSplitOptions.RemoveEmptyEntries);
            ushort val1, val2;
            var isNumber = ushort.TryParse(values[0].Trim(), out val1);
            if (!isNumber)
            {
                if (memory.ContainsKey(values[0].Trim())) val1 = memory[values[0]];
                else
                {
                    HandleInstruction(values[0].Trim());
                    val1 = memory[values[0]];
                }
            }
            isNumber = ushort.TryParse(values[1].Trim(), out val2);
            if (!isNumber)
            {
                if (memory.ContainsKey(values[1].Trim())) val2 = memory[values[1]];
                else
                {
                    HandleInstruction(values[1].Trim());
                    val2 = memory[values[1]];
                }
            }
            var value = val1 | val2;
            return (ushort)value;
        }
        ushort LShInstr(string instr)
        {
            var values = instr.Split(" LSHIFT ", StringSplitOptions.RemoveEmptyEntries);
            ushort val1, val2;
            var isNumber = ushort.TryParse(values[0].Trim(), out val1);
            if (!isNumber)
            {
                if (memory.ContainsKey(values[0].Trim())) val1 = memory[values[0]];
                else
                {
                    HandleInstruction(values[0].Trim());
                    val1 = memory[values[0]];
                }
            }
            isNumber = ushort.TryParse(values[1].Trim(), out val2);
            if (!isNumber)
            {
                if (memory.ContainsKey(values[1].Trim())) val2 = memory[values[1]];
                else
                {
                    HandleInstruction(values[1].Trim());
                    val2 = memory[values[1]];
                }
            }
            var value = val1 << val2;
            return (ushort)value;
        }

        ushort RShInstr(string instr)
        {
            var values = instr.Split(" RSHIFT ", StringSplitOptions.RemoveEmptyEntries);
            ushort val1, val2;
            var isNumber = ushort.TryParse(values[0].Trim(), out val1);
            if (!isNumber)
            {
                if (memory.ContainsKey(values[0].Trim())) val1 = memory[values[0]];
                else
                {
                    HandleInstruction(values[0].Trim());
                    val1 = memory[values[0]];
                }
            }
            isNumber = ushort.TryParse(values[1].Trim(), out val2);
            if (!isNumber)
            {
                if (memory.ContainsKey(values[1].Trim())) val2 = memory[values[1]];
                else
                {
                    HandleInstruction(values[1].Trim());
                    val2 = memory[values[1]];
                }
            }
            var value = val1 >> val2;
            return (ushort)value;
        }

        ushort NotInstr(string instr)
        {
            var values = instr.Split("NOT ", StringSplitOptions.RemoveEmptyEntries);
            ushort val;
            var isNumber = ushort.TryParse(values[0].Trim(), out val);
            if (!isNumber)
            {
                if (memory.ContainsKey(values[0].Trim())) val = memory[values[0]];
                else
                {
                    HandleInstruction(values[0].Trim());
                    val = memory[values[0]];
                }
            }
            var value = ~val;
            return (ushort)value;
        }

        ushort AssInstr(string instr)
        {
            var isNumber = ushort.TryParse(instr.Trim(), out ushort val);
            if (isNumber) return val;
            else
            {
                if (memory.ContainsKey(instr.Trim())) val = memory[instr.Trim()];
                else
                {
                    HandleInstruction(instr.Trim());
                    val = memory[instr];
                }
            }
            return val;
        }

        void Run()
        {
            ParseInput();
            ushort result;
            var watch = new System.Diagnostics.Stopwatch();
            watch.Start();
            result = PartOne();
            watch.Stop();
            Console.WriteLine($"The result for part one is: {result}");
            Console.WriteLine($"Time is {watch.ElapsedMilliseconds} ms");
            watch.Reset();
            watch.Start();
            result = PartTwo(result);
            watch.Stop();
            Console.WriteLine($"The result for part one is: {result}");
            Console.WriteLine($"Time is {watch.ElapsedMilliseconds} ms");
        }

        static void Main(string[] args)
        {
            Program p = new Program();
            p.Run();
        }
    }
}

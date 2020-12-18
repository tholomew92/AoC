using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace aoc_18_1
{
    class Program
    {
        static string workdir = Environment.CurrentDirectory;
        static string inputPath = new DirectoryInfo(workdir).Parent.Parent.Parent.Parent.Parent.ToString();
        static readonly List<string> input = File.ReadAllLines(inputPath + "\\input.txt").ToList();
        //static readonly List<string> input = File.ReadAllLines(inputPath + "\\test.txt").ToList();


        void MathHomework()
        {
            var watch = new System.Diagnostics.Stopwatch();
            watch.Start();
            char[] allOps = new char[] { '+', '*', '(', ')' };
            char[] mathOps = new char[] { '+', '*' };

            var precedenceOne = new List<(char, int)> { ('+', 1), ('*', 1), ('(', 0) };

            long result = 0;
            
            foreach(var line in input)
            {
                var maths = ParseLine(line, precedenceOne, allOps);
                result += RunCalc(maths, mathOps);
                
            }
            watch.Stop();
            Console.WriteLine($"The result for part one is: {result}");
            Console.WriteLine($"Time is {watch.ElapsedMilliseconds.ToString()} ms");
            watch.Restart();
            var precedenceTwo = new List<(char, int)> { ('+', 2), ('*', 1), ('(', 0) };

            result = 0;

            foreach (var line in input)
            {
                var maths = ParseLine(line, precedenceTwo, allOps);
                result += RunCalc(maths, mathOps);

            }
            watch.Stop();
            Console.WriteLine($"The result for part two is: {result}");
            Console.WriteLine($"Time is {watch.ElapsedMilliseconds.ToString()} ms");
        }

        List<char> ParseLine(string line, List<(char, int)> precendence, char[] allOps)
        {
            var maths = new List<char>();
            var stack = new Stack<char>();

            var formattedLine = line.Replace(" ", "");

            foreach(var element in formattedLine)
            {
                if (allOps.Contains(element))
                {
                    if (stack.Count == 0)
                        stack.Push(element);
                    else
                    {
                        if (element.Equals('('))
                        {
                            stack.Push(element);
                        }
                        else
                        {
                            if (element.Equals(')'))
                            {
                                char lastElement = stack.Pop();
                                while (!lastElement.Equals('('))
                                {
                                    maths.Add(lastElement);
                                    lastElement = stack.Pop();
                                }
                            }
                            else
                            {
                                var topOfStack = stack.Peek();
                                if(precendence.Find(x => x.Item1 == element).Item2 > precendence.Find(x => x.Item1 == topOfStack).Item2)
                                {
                                    stack.Push(element);
                                }
                                else
                                {
                                    var popped = stack.Pop();
                                    maths.Add(popped);
                                    stack.Push(element);
                                }
                            }
                        }
                    }
                }
                else
                {
                    maths.Add(element);
                }
            }

            while(stack.Count > 0)
            {
                maths.Add(stack.Pop());
            }

            return maths;
        }

        long RunCalc(List<char> maths, char[] mathOps)
        {
            Stack<long> stack = new Stack<long>();

            foreach(var element in maths)
            {
                if(mathOps.Contains(element) & stack.Count > 1)
                {
                    long left = stack.Pop();
                    long right = stack.Pop();

                    if (element.Equals('+'))
                    {
                        stack.Push(left + right);
                    }
                    else if (element.Equals('*'))
                    {
                        stack.Push(left * right);
                    }
                }
                else
                {
                    stack.Push(long.Parse(element.ToString()));
                }
            }

            return stack.Pop();
        }


        static void Main(string[] args)
        {
            Program p = new Program();
            p.MathHomework();
        }
    }
}

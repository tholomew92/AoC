using System;
using System.Collections.Generic;

namespace aoc_4_1
{
    class Program
    {
        static readonly String[] input = System.IO.File.ReadAllLines(@"C:\Users\Sebbe\Desktop\aoc\2020\4\input.txt");

        List<String> parsedInput = new List<String>();

        List<String> requiredParameters = new List<String>()
        {
            "byr",
            "iyr",
            "eyr",
            "hgt",
            "hcl",
            "ecl",
            "pid",
        };

        void ParseInput()
        {
            String passport = "";
            String s;
            for(int i = 0; i < input.Length; i++)
            {
                s = input[i];
                if (String.IsNullOrEmpty(s)| i == input.Length)
                {
                    parsedInput.Add(passport);
                    passport = "";
                }
                else 
                {
                    passport = passport + " " + s;
                }
            }
        }

        void checkValidPassport()
        {
            Boolean valid = true;
            int validCount = 0;
            foreach(String pass in parsedInput)
            {
                valid = true;
                foreach (String req in requiredParameters)
                    if (!pass.Contains(req))
                        valid = false;
                if (valid)
                    validCount++;

                Console.WriteLine(pass + " is " + valid.ToString());
            }
            Console.WriteLine(validCount);

        }

        static void Main(string[] args)
        {
            Program p = new Program();
            p.ParseInput();
            p.checkValidPassport();
        }
    }
}

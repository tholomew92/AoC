using System;
using System.Collections.Generic;

namespace aoc_4_1
{
    class Program
    {
        static string workdir = Environment.CurrentDirectory;
        static string inputPath = new DirectoryInfo(workdir).Parent.Parent.Parent.Parent.Parent.ToString();
        List<string> input = File.ReadAllLines(inputPath + "\\input.txt").ToList();

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

        List<String> validEyeColours = new List<String>()
        {
            "amb",
            "blu",
            "brn",
            "gry",
            "grn",
            "hzl",
            "oth",
        };


        void ParseInput()
        {
            String passport = "";
            String s;
            for (int i = 0; i < input.Length; i++)
            {
                s = input[i];
                if (String.IsNullOrEmpty(s) || i == input.Length)
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

        void CheckValidPassport()
        {
            Boolean valid;
            int validCount = 0;

            foreach (String pass in parsedInput)
            {
                valid = true;
                Console.Write(pass + "- ");
                foreach (String req in requiredParameters)
                    if (!pass.Contains(req))
                        valid = false;
                String[] parameters = pass.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                foreach(String p in parameters)
                {
                    String[] data = p.Split(':');
                    if (!ValidateParameters(data[0], data[1]))
                    {
                        valid = false;
                        Console.Write(p + " is false ");
                    }
                    
                }
                Console.WriteLine(valid);

                if (valid)
                    validCount++;
            }
            Console.WriteLine(validCount);
        }

        Boolean ValidateParameters(String param, String data)
        {
            return param switch
            {
                "byr" => Byr(data),
                "iyr" => Iyr(data),
                "eyr" => Eyr(data),
                "hgt" => Hgt(data),
                "hcl" => Hcl(data),
                "ecl" => Ecl(data),
                "pid" => Pid(data),
                "cid" => true,
                _ => false,
            };
        }
        Boolean Byr(String year)
        {
            int.TryParse(year, out int byr);
            return 1920 <= byr && byr <= 2002;
        }

        Boolean Iyr(String year)
        {
            int.TryParse(year, out int iyr);
            return 2010 <= iyr && iyr <= 2020;
        }

        Boolean Eyr(String year)
        {
            int.TryParse(year, out int eyr);
            return 2020 <= eyr && eyr <= 2030;
        }

        Boolean Hgt(String data)
        {
            if (data.Contains("cm"))
            {
                int.TryParse(data[0..^2], out int hgt);
                return 150 <= hgt && hgt <= 193;
            }
            else if (data.Contains("in"))
            {
                int.TryParse(data[0..^2], out int hgt);
                return 59 <= hgt && hgt <= 76;
            }

            return false;
        }

        Boolean Hcl(String data)
        {
            if (data[0] == '#' && data.Length.Equals(7))
            {
                return int.TryParse(data.Substring(1), System.Globalization.NumberStyles.HexNumber, null, out _);
            }
            return false;
        }

        Boolean Ecl(String data)
        {
            if (validEyeColours.Contains(data))
            {
                return true;
            }
            return false;
        }

        Boolean Pid(String data)
        {
            return data.Length == 9 & int.TryParse(data, out _);
        }

        static void Main(string[] args)
        {
            Program p = new Program();
            p.ParseInput();
            p.CheckValidPassport();
        }
    }
}

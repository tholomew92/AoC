using System;
using System.Collections.Generic;

namespace TestingOfInputs
{
    class Program
    {
        static void Main(string[] args)
        {
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
            Console.WriteLine("2002 is " + Byr("2002"));
            Console.WriteLine("2003 is " + Byr("2003"));
            Console.WriteLine("2010 is " + Iyr("2010"));
            Console.WriteLine("2009 is " + Iyr("2009"));
            Console.WriteLine("2020 is " + Eyr("2020"));
            Console.WriteLine("2019 is " + Eyr("2019"));
            Console.WriteLine("60in is " + Hgt("60in"));
            Console.WriteLine("190in is " + Hgt("190in"));
            Console.WriteLine("190cm is " + Hgt("190cm"));
            Console.WriteLine("60cm is " + Hgt("60cm"));
            Console.WriteLine("#123abc is " + Hcl("#123abc"));
            Console.WriteLine("#123abz is " + Hcl("#123abz"));
            Console.WriteLine("123abc is " + Hcl("123abc"));
            Console.WriteLine("brn is " + Ecl("brn"));
            Console.WriteLine("wat is " + Ecl("wat"));
            Console.WriteLine("000000001 is " + Pid("000000001"));
            Console.WriteLine("0123456789 is " + Pid("0123456789"));




            Boolean Byr(String year)
            {
                int.TryParse(year, out int byr);
                return 1920 <= byr && byr <= 2002;
;
            }

            Boolean Iyr(String year)
            {
                int.TryParse(year, out int iyr);
                return 2010 <= iyr && iyr <= 2020;
                ;
            }

            Boolean Eyr(String year)
            {
                int.TryParse(year, out int eyr);
                return 2020 <= eyr && eyr <= 2030;
                ;
            }

            Boolean Hgt(String data)
            {
                if (data.Contains("cm"))
                {
                    int.TryParse(data.Substring(0, data.Length - 2), out int hgt);
                    return 150 <= hgt && hgt <= 193;
                }else if (data.Contains("in"))
                {
                    int.TryParse(data.Substring(0, data.Length - 2), out int hgt);
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

        }
    }
}

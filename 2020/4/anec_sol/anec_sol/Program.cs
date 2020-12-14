using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace anec_sol
{
    class Program
    {
        static void Main(string[] args)
        {


				List<string> rows = GetFileValues(@"C:\Users\Sebbe\Desktop\aoc\2020\4\input.txt");


				Queue<string> ThePassportQueue = new Queue<string>();

				foreach (var entery in rows)
				{
					ThePassportQueue.Enqueue(entery);
				}

				List<string> AvailableFields = new List<string> { "byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid", "cid" };
				List<string> RequiredFields = new List<string> { "byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid" };

				var countValid = GetValidPasswordCount(ThePassportQueue, RequiredFields);

				Console.WriteLine("Valid Part One " + countValid.PartOne);
				Console.WriteLine("Valid Part Two " + countValid.PartTwo);
			


			(int PartOne, int PartTwo) GetValidPasswordCount(Queue<string> Passports, List<string> RequiredFields)
			{
				int ValidPassportsPartOne = 0;
				int ValidPassportsPartTwo = 0;
				int NumberOfFieldsRequired = RequiredFields.Count;

				while (Passports.Count > 0)
				{
					Dictionary<string, string> currentPassport = new Dictionary<string, string>();
					//check each passport
					while (Passports.Count > 0 && Passports.Peek().Length > 0)
					{
						var currentLine = Passports.Dequeue();


						var split = currentLine.Split(' ');

						foreach (var entery in split)
						{
							var components = entery.Split(':');
							currentPassport.Add(components[0], components[1]);
						}
					}
					if (Passports.Count > 0)
					{
						var empty = Passports.Dequeue();
					}

					//here we check if the passport contains the required fields.
					int ValidFieldsPartOne = 0;
					int validFields = 0;
					foreach (var entery in currentPassport)
					{
						if (RequiredFields.Contains(entery.Key))
							ValidFieldsPartOne++;

						if (RequiredFields.Contains(entery.Key))
						{
							if (entery.Key == "byr")
							{
								if (Validatebyr(entery.Value))
									validFields++;
							}
							else if (entery.Key == "iyr")
							{
								if (Validateiyr(entery.Value))
									validFields++;
							}
							else if (entery.Key == "eyr")
							{
								if (Validateeyr(entery.Value))
									validFields++;
							}
							else if (entery.Key == "hgt")
							{
								if (Validatehgt(entery.Value))
									validFields++;
							}
							else if (entery.Key == "hcl")
							{
								if (Validatehcl(entery.Value))
									validFields++;
							}
							else if (entery.Key == "ecl")
							{
								if (Validateecl(entery.Value))
									validFields++;
							}
							else if (entery.Key == "pid")
							{
								if (Validatepid(entery.Value))
									validFields++;
							}

						}
					}

					if (validFields >= NumberOfFieldsRequired)
						ValidPassportsPartTwo++;

					if (ValidFieldsPartOne >= NumberOfFieldsRequired)
						ValidPassportsPartOne++;

				}

				return (ValidPassportsPartOne, ValidPassportsPartTwo);
			}


			static bool Validatebyr(string byr)
			{
				if (int.TryParse(byr, out int birthyear))
				{
					bool res = birthyear >= 1920 && birthyear <= 2002;
					return res;
				}
				return false;
			}
			static bool Validateiyr(string iyr)
			{
				if (int.TryParse(iyr, out int issueyear))
				{
					bool res = issueyear >= 2010 && issueyear <= 2020;
					return res;
				}
				return false;
			}
			static bool Validateeyr(string eyr)
			{
				if (int.TryParse(eyr, out int expireyear))
				{
					bool res = expireyear >= 2020 && expireyear <= 2030;
					return res;
				}
				return false;
			}

			static bool Validatehgt(string hgt)
			{
				if (hgt.Contains("cm") || hgt.Contains("in"))
				{
					if (hgt.Contains("cm"))
					{
						var index = hgt.IndexOf("cm");
						var numbers = hgt.Substring(0, index);
						if (int.TryParse(numbers, out int height))
						{
							bool res = height >= 150 && height <= 193;
							return res;
						}
					}
					else if (hgt.Contains("in"))
					{
						var index = hgt.IndexOf("in");
						var numbers = hgt.Substring(0, index);
						if (int.TryParse(numbers, out int height))
						{
							bool res = height >= 59 && height <= 76;
							return res;
						}
					}

				}


				return false;
			}

			static bool Validatehcl(string hcl)
			{
				if (hcl[0] == '#')
				{


					int length = hcl.Length;

					var result = Regex.Replace(hcl, "[^a-fA-f0-9]+", "", RegexOptions.Compiled);

					if (result.Length == 6)
						return true;
				}
				return false;
			}

			static bool Validateecl(string ecl)
			{
				if (ecl.Length == 3)
				{
					if (ecl == "amb" || ecl == "blu" || ecl == "brn" || ecl == "gry" || ecl == "grn" || ecl == "hzl" || ecl == "oth")
						return true;

				}


				return false;
			}

			static bool Validatepid(string pid)
			{
				if (pid.Length == 9)
				{
					var res = Regex.Replace(pid, "[^0-9]+", "", RegexOptions.Compiled);
					if (res.Length == 9)
						return true;
				}



				return false;
			}

			static List<string> GetFileValues(string path)
			{
				List<string> resultList = new List<string>();
				FileStream fileStream = new FileStream(path, FileMode.Open);
				using (StreamReader reader = new StreamReader(fileStream))
				{
					while (!reader.EndOfStream)
					{
						List<int> rowData = new List<int>();
						string line = reader.ReadLine();
						resultList.Add(line);
					}
				}
				return resultList;

			}
		}
    }
}

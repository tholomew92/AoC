using System;
using System.Collections.Generic;

namespace aoc_19_1
{
    class Program
    {
        Dictionary<int, string> rules = new Dictionary<int, string>();
        Dictionary<int, List<string>> dict = new Dictionary<int, List<string>>();

        void Initialize() {
            List<string> test = new List<string>()
            {
                "0: 1",
                "1: 2",
                "2: \"a\"",
                "3: 0"
            };

            foreach (var t in test)
            {
                var split = t.Split(':');
                rules.Add(int.Parse(split[0]), split[1]);
            }

            foreach (var rule in rules)
            {
                if (!dict.ContainsKey(rule.Key))
                {
                    GetRightRules(rule.Key);
                }
            }
        }

        void GetRightRules(int key)
        {
            string s = "";
            List<string> list = new List<string>();
            if (!dict.ContainsKey(key))
            {
                if (rules[key].Contains('"'))
                {
                    list.Add(rules[key].Remove('"'));
                }
            }
        }

        static void Main(string[] args)
        {
            string s = "\"a\"";

            Console.WriteLine(s.Contains('"'));
        }

    }
}

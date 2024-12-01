using System;
using System.Text;

namespace aoc_4_1
{
    class Program
    {
        
        String FindHash()
        {
            var input = "iwrupvqb";
            for(int i = 0; i < 346389; i++)
            {
                var hashInput = input + i;
                byte[] inputBytes = Encoding.ASCII.GetBytes(hashInput);
                var md5 = System.Security.Cryptography.MD5.Create();
                var hashBytes = md5.ComputeHash(inputBytes);
                StringBuilder sb = new StringBuilder();
                for (int j = 0; j < hashBytes.Length; j++)
                {
                    sb.Append(hashBytes[j].ToString("X2"));
                }
                var hash = sb.ToString();
                if (hash.Substring(0, 6).Equals("000000")) return i.ToString();
            }
            return "Could not find";

        }

        static void Main(string[] args)
        {
            Program p = new Program();
            Console.WriteLine(p.FindHash());
        }
    }
}

namespace aoc_3
{
    class Program
    {
        static string workdir = Environment.CurrentDirectory;
        static string inputPath = new DirectoryInfo(workdir).Parent.Parent.Parent.Parent.Parent.ToString();
        static readonly List<string> input = File.ReadAllLines(inputPath + "\\input.txt").ToList();
        //static readonly List<string> input = File.ReadAllLines(inputPath + "\\test.txt").ToList();


        void Run()
        {
            int result;
            var watch = new System.Diagnostics.Stopwatch();
            watch.Start();
            result = GetPowerConsumption();
            watch.Stop();
            Console.WriteLine($"The result for part one is: {result}");
            Console.WriteLine($"Time is {watch.ElapsedMilliseconds.ToString()} ms");
            watch.Reset();
            result = GetLifeSupport();
            watch.Stop();
            Console.WriteLine($"The result for part one is: {result}");
            Console.WriteLine($"Time is {watch.ElapsedMilliseconds.ToString()} ms");
        }

        int GetPowerConsumption()
        {
            int gamma, epsilon;
            gamma = epsilon = 0;
            for (int i = 0; i < input[0].Length; i++) {
                int gammaCount, epsilonCount;
                gammaCount = epsilonCount = 0;
                foreach (var line in input)
                {
                    if (line[i].Equals('1')) gammaCount++;
                    else epsilonCount++;
                }
                gamma = gamma << 1;
                epsilon = epsilon << 1;
                if (gammaCount < epsilonCount) gamma = gamma | 1;
                else epsilon = epsilon | 1;
            }
            return gamma * epsilon;
        }

        int GetLifeSupport()
        {
            int oxygen, co2;
            List<string> oxygenList, co2List;
            oxygenList = input;
            co2List = input;
            for (int i = 0; i < input[0].Length; i++)
            {
                oxygenList = FindOxygen(i, oxygenList);
                if (co2List.Count > 1) co2List = FindCo2(i, co2List);
            }
            oxygen = Convert.ToInt32(oxygenList[0], 2);
            co2 = Convert.ToInt32(co2List[0], 2);
            return oxygen * co2;
        }

        List<string> FindOxygen(int index, List<string> values)
        {
            int ones, zeros;
            ones = zeros = 0;
            List<string> oneList, zeroList;
            oneList = new List<string>();
            zeroList = new List<string>();
            int c = 0;
            foreach (string value in values)
            {  
                if (value[index].Equals('1'))
                {
                    ones++;
                    oneList.Add(value);
                }
                else
                {
                    zeros++;
                    zeroList.Add(value);
                }
                c++;
            }
            if (ones > zeros || ones == zeros)
            {
                return oneList;
            }
            else
            {
                return zeroList;
            }
                
           
            
        }

        List<string> FindCo2(int index, List<string> values)
        {
            int ones, zeros;
            ones = zeros = 0;
            List<string> oneList, zeroList;
            oneList = new List<string>();
            zeroList = new List<string>();
            foreach (string value in values)
            {
                if (value[index].Equals('1'))
                {
                    ones++;
                    oneList.Add(value);
                }
                else
                {
                    zeros++;
                    zeroList.Add(value);
                }
            }
            if (ones > zeros || ones == zeros) return zeroList;
            else return oneList;
        }

        static void Main(string[] args)
        {
            Program p = new Program();
            p.Run();
        }
    }
}
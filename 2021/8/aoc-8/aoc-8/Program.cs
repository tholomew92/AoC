using System.Text;

namespace aoc_1_new
{
    class Program
    {
        static string workdir = Environment.CurrentDirectory;
        static string inputPath = new DirectoryInfo(workdir).Parent.Parent.Parent.Parent.Parent.ToString();
        static readonly List<string> input = File.ReadAllLines(inputPath + "\\input.txt").ToList();
        //static readonly List<string> input = File.ReadAllLines(inputPath + "\\test.txt").ToList();

        int PartOne()
        {
            int count = 0;
            foreach (var line in input)
            {
                var firstSPlit = line.Split('|');
                var signals = firstSPlit[1].Split(' ');
                foreach (var signal in signals) if (signal.Length == 2 || signal.Length == 3 || signal.Length == 4 || signal.Length == 7) count++;
            }

            return count;
        }

        int PartTwo()
        {
            List<int> count = new List<int>();
            foreach (var line in input)
            {
                var firstSPlit = line.Split('|');
                var codedSignals = firstSPlit[0].Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();
                Dictionary<int, string> signalValue = new Dictionary<int, string>();
                bool notFound = true;
                char[] one = new char[2];
                char[] four = new char[4];
                foreach (var cs in codedSignals)
                {
                    if (cs.Length == 2 && !signalValue.ContainsValue(cs)) { signalValue.Add(1, cs); one = cs.ToCharArray(); }
                    if (cs.Length == 4 && !signalValue.ContainsValue(cs)) { signalValue.Add(4, cs); four = cs.ToCharArray(); }
                    if (cs.Length == 3 && !signalValue.ContainsValue(cs)) signalValue.Add(7, cs);
                    if (cs.Length == 7 && !signalValue.ContainsValue(cs)) signalValue.Add(8, cs);

                }

                foreach (var sv in signalValue) codedSignals.Remove(sv.Value);
                do
                {
                    foreach (var cs in codedSignals)
                    {
                        if (cs.Contains(one[0]) && cs.Contains(one[1]))
                        {
                            if (cs.Length == 5) signalValue.Add(3, cs);
                            else
                            {
                                int check = 0;
                                foreach (var f in four)
                                {
                                    if (cs.Contains(f)) check++;
                                }
                                if (check == 4) signalValue.Add(9, cs);
                                else if (!signalValue.ContainsKey(0)) signalValue.Add(0, cs);
                            }

                        }
                        else
                        {
                            if (!signalValue.ContainsKey(6) && cs.Length == 6) signalValue.Add(6, cs);
                            else if (signalValue.ContainsKey(6))
                            {
                                if ((cs.Contains(one[0]) && signalValue[6].Contains(one[0])) || (cs.Contains(one[1]) && signalValue[6].Contains(one[1]))) signalValue.Add(5, cs);
                                else signalValue.Add(2, cs);
                            }
                        }
                    }
                    foreach (var sv in signalValue) codedSignals.Remove(sv.Value);
                    if (codedSignals.Count == 0) notFound = false;
                } while (notFound);
                var signals = firstSPlit[1].Split(' ', StringSplitOptions.RemoveEmptyEntries);
                StringBuilder sb = new StringBuilder();
                foreach (var signal in signals)
                {
                    int value = -1;
                    var sortedSignal = String.Concat(signal.OrderBy(x => x));
                    foreach (var sv in signalValue)
                    {
                        var sortedSV = String.Concat(sv.Value.OrderBy(x => x));
                        if (sortedSignal.Equals(sortedSV))
                        {
                            value = sv.Key;
                        }
                    }
                    sb.Append(value);
                }
                count.Add(Int32.Parse(sb.ToString()));
            }

            return count.Sum();
        }

        void Run()
        {
            int result;
            var watch = new System.Diagnostics.Stopwatch();
            watch.Start();
            result = PartOne();
            watch.Stop();
            Console.WriteLine($"The result for part one is: {result}");
            Console.WriteLine($"Time is {watch.ElapsedMilliseconds.ToString()} ms");
            watch.Reset();
            watch.Start();
            result = PartTwo();
            watch.Stop();
            Console.WriteLine($"The result for part one is: {result}");
            Console.WriteLine($"Time is {watch.ElapsedMilliseconds.ToString()} ms");
        }

        static void Main(string[] args)
        {
            Program p = new Program();
            p.Run();
        }
    }
}
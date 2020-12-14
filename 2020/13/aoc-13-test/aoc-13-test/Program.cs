using System;
using System.IO;
using System.Linq;

namespace aoc_13_test
{
    static class Day13
    {
        const string Filename = @"C:\Users\Sebbe\Desktop\aoc\2020\13\input.txt";

        static string[] Input = File.ReadAllLines(Filename);

        static int Part01()
        {
            var depart = int.Parse(Input[0]);
            int[] bus = Input[1].Split(",").Where(id => !id.Equals("x")).Select(id => int.Parse(id)).ToArray();
            var busToTake = 0;
            var leastDiff = depart;
            foreach (var id in bus)
            {
                var time = 0;
                while (time < depart)
                {
                    time += id;
                }

                var diff = time - depart;
                if (diff < leastDiff)
                {
                    leastDiff = diff;
                    busToTake = id;
                }
            }
            return leastDiff * busToTake;
        }

        static long Part02()
        {
            string[] bus = Input[1].Split(",");
            var time = 0L;
            var inc = long.Parse(bus[0]);
            for (var i = 1; i < bus.Length; i++)
            {
                if (!bus[i].Equals("x"))
                {
                    var newTime = int.Parse(bus[i]);
                    while (true)
                    {
                        time += inc;
                        if ((time + i) % newTime == 0)
                        {
                            inc *= newTime;
                            Console.WriteLine(inc);
                            break;
                        }
                    }
                }
            }
            return time;
        }

        public static void Main(string[] args)
        {
            Console.WriteLine(Part01());
            Console.WriteLine(Part02());
        }
    }
}

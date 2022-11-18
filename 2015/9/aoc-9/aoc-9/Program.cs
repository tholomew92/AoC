// See https://aka.ms/new-console-template for more information
using System;
using System.Linq;
using System.Runtime.CompilerServices;

class Program
{
    static string workdir = Environment.CurrentDirectory;
    static string inputPath = new DirectoryInfo(workdir).Parent.Parent.Parent.Parent.Parent.ToString();
    List<string> input = File.ReadAllLines(inputPath + "\\input.txt").ToList();
    Dictionary<Tuple<string, string>, int> routes = new Dictionary<Tuple<string, string>, int>();
    List<string> allCities = new List<string>();

    void ParseInput()
    {
        foreach (var line in input)
        {
            var sides = line.Split(new[] { " = " }, StringSplitOptions.None);
            var destinations = sides[0];
            var distance = Int16.Parse(sides[1]);
            var cities = destinations.Split(new[] { " to " }, StringSplitOptions.None);
            var route1 = new Tuple<string, string>(cities[0], cities[1]);
            var route2 = new Tuple<string, string>(cities[1], cities[0]);
            routes[route1] = distance;
            routes[route2] = distance;

            if (!allCities.Contains(cities[0])) 
            {
                allCities.Add(cities[0]);
            }
            
            if (!allCities.Contains(cities[1]))
            {
                allCities.Add(cities[1]);
            }
        }
    }

    List<List<string>> Permutations(List<string> items)
    {
        if(items.Count > 1)
        {
            return items.SelectMany(item => Permutations(items.Where(i => !i.Equals(item)).ToList()), (item, permutation) => new[] { item }.Concat(permutation).ToList()).ToList();
        }

        return new List<List<string>> { items };
    }

    void ProcessPermutations()
    {
        long minTravelDistance = long.MaxValue;
        long maxTravelDistance = 0;

        var allPermutations = Permutations(allCities);
        foreach(var perm in allPermutations)
        {
            long travelDistance = 0;
            for (int i = 0; i < perm.Count - 1; i++)
            {
                travelDistance += routes[new Tuple<string, string>(perm[i], perm[i+1])];
            }
            
            minTravelDistance = Math.Min(travelDistance, minTravelDistance);
            maxTravelDistance = Math.Max(travelDistance, maxTravelDistance);
        }

        Console.WriteLine($"The result for part one is: {minTravelDistance}");
        Console.WriteLine($"The result for part one is: {maxTravelDistance}");
    }

    void Run()
    {
        var watch = new System.Diagnostics.Stopwatch();
        watch.Start();
        ParseInput();
        ProcessPermutations();
        watch.Stop();
        Console.WriteLine($"Time is {watch.ElapsedMilliseconds} ms");
    }
    static void Main(string[] args)
    {
        Program p = new();
        p.Run();
    }
}
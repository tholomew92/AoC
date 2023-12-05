using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

var watch = new System.Diagnostics.Stopwatch();
watch.Start();
var workDir = AppDomain.CurrentDomain.BaseDirectory;
var path = new DirectoryInfo(workDir).Parent.Parent.Parent.ToString();
var inputData = File.ReadAllLines(path + "\\input.txt").ToList();
var testData = File.ReadAllLines(path + "\\test.txt").ToList();

var input = testData;

var partOne = Int64.MaxValue;
var partTwo = Int64.MaxValue;

var parse = watch.Elapsed;

var split = input[0].Split(':', StringSplitOptions.TrimEntries);
var seeds = split[1].Split(" ", StringSplitOptions.TrimEntries);
// PART ONE
var seedList = new List<List<Int64>>();

foreach (var seed in seeds)
{
    var list = new List<Int64>();
    list.Add(Int64.Parse(seed));
    seedList.Add(list);
}

var step = 1;
for (int i = 1; i < input.Count; i++)
{
    var line = input[i];
    if (line.Contains("map"))
    {
        foreach (var seed in seedList)
        {
            if(seed.Count < step) seed.Add(seed.Last());
        }
        step++;
    }
    else if(String.IsNullOrEmpty(line)) continue;
    else
    {
        var parameters = line.Split(' ', StringSplitOptions.TrimEntries);
        var destination = Int64.Parse(parameters[0]);
        var source = Int64.Parse(parameters[1]);
        var range = Int64.Parse(parameters[2])-1;
        foreach (var seed in seedList)
        {
            if (seed.Count == step) continue;
            var s = seed.Last();
            if (source <= s && s <= source + range)
            {
                seed.Add(destination + (s - source));
            }
        }
    }
}
foreach (var seed in seedList)
{
    if (seed.Count < step) seed.Add(seed.Last());
}
foreach (var seed in seedList)
{
    if (seed.Last() < partOne) partOne = seed.Last();
}
seedList.Clear();
var timeOne = watch.Elapsed - parse;

// PART ONE SECOND

var partOneSecond = Int64.MaxValue;
for (int i = 0; i < seeds.Length; i ++)
{
    var seed = Int64.Parse(seeds[i]);
    Int64 value = GetLocation(seed);
    if (value < partTwo) partOneSecond = value;
}


var timeOneSecond = watch.Elapsed - timeOne - parse;


// PART TWO


var ranges = new Dictionary<Int64, Int64>();
for (int i = 0; i < seeds.Length; i += 2)
{
    var start = Int64.Parse(seeds[i]);
    var stop = Int64.Parse(seeds[i+1]);
    ranges.Add(start, stop);
}

for (int i = 0; i < int.MaxValue; i++)
{
    bool done = false;
    foreach (var range in ranges)
    {
        Int64 val = GetSeed(i);
        if (range.Key <= val && val <= range.Value)
        {
            partTwo = val;
            done = true;
            break;
        }
    }
    if (done) break;
}

var timeTwo = watch.Elapsed - timeOneSecond - timeOne - parse;
watch.Stop();
Console.WriteLine($"The time for parsing input is: {FormattedTime(parse)}");
Console.WriteLine($"The result for part one is: {partOne}");
Console.WriteLine($"The time for part one is {FormattedTime(timeOne)}");
Console.WriteLine($"The result for part one is: {partOneSecond}");
Console.WriteLine($"The time for part one is {FormattedTime(timeOneSecond)}");
Console.WriteLine($"The result for part two is: {partTwo}");
Console.WriteLine($"The time for part two is {FormattedTime(timeTwo)}");
Console.WriteLine($"Total time is {FormattedTime(watch.Elapsed)}");

string FormattedTime(TimeSpan ts)
{
    var temp = ts.ToString("fffffff");
    var count = 0;
    foreach (var c in temp)
    {
        if (c.Equals('0')) count++;
        else break;
    }
    var formatted = temp.Substring(count);
    if (formatted.Length > 3)
    {
        formatted = formatted.Insert(formatted.Length - 3, ",");
        formatted += " ms";
    }
    else
    {
        formatted += " µs";
    }

    return formatted;
}


Int64 GetLocation(Int64 seed)
{
    //Console.WriteLine($"Seed is {seed}");
    var check = false;
    Int64 val = seed;
    for (int i = 1; i < input.Count; i++)
    {
        var line = input[i];
        if (line.Contains("map"))
        {
            check = false;
        }
        else if (String.IsNullOrEmpty(line)) continue;
        else
        {
            var parameters = line.Split(' ', StringSplitOptions.TrimEntries);
            var destination = Int64.Parse(parameters[0]);
            var source = Int64.Parse(parameters[1]);
            var range = source + Int64.Parse(parameters[2]) - 1;

            if (check) continue;
            else
            {
                if (source <= val && val <= range)
                {
                    {
                        check = true;
                        val =  destination + (val - source);
                        //Console.WriteLine($"{destination} + ({val} - {source}) = {val}");
                    }
                }
                else if(!check)
                {
                    //Console.WriteLine($"{source} and {range} does not contain {val}");
                }
            }
        }
    }
    //Console.WriteLine();
    return val;
}

Int64 GetSeed(Int64 location)
{
    //Console.WriteLine($"Seed is {seed}");
    Int64 val = location;
    
    for (int i = 0; i < int.MaxValue; i++)
    {
        Int64 temp = -1;
        var check = false;
        for (int j = input.Count-1; i > 0; i--)
        {
            var line = input[j];
            if (line.Contains("map"))
            {
                if (temp != -1)
                {
                    val = temp;
                    temp = -1;
                }
                check = false;
            }
            else if (String.IsNullOrEmpty(line)) continue;
            else
            {
                var parameters = line.Split(' ', StringSplitOptions.TrimEntries);
                var destination = Int64.Parse(parameters[0]);
                var source = Int64.Parse(parameters[1]);
                var range = source + Int64.Parse(parameters[2]) - 1;

                if (source <= location && location <= range)
                {

                }
            }
        }
    }
    //Console.WriteLine();
    return val;
}

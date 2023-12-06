using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

var watch = new System.Diagnostics.Stopwatch();
watch.Start();
var workDir = AppDomain.CurrentDomain.BaseDirectory;
var path = new DirectoryInfo(workDir).Parent.Parent.Parent.ToString();
var inputData = File.ReadAllLines(path + "\\input.txt").ToList();
var testData = File.ReadAllLines(path + "\\test.txt").ToList();

var input = inputData;

var partOne = Int64.MaxValue;
var partTwo = Int64.MaxValue;

var parse = watch.Elapsed;

var split = input[0].Split(':', StringSplitOptions.TrimEntries);
var seeds = split[1].Split(" ", StringSplitOptions.TrimEntries);

var rangeList = new List<RangeList>();
var temp = new RangeList();
for(int i = 2; i < input.Count; i++)
{  
    var line = input[i];
    if (line.Contains("map"))
    {
        rangeList.Add(temp);
        temp = new RangeList();
    }
    else if (string.IsNullOrEmpty(line)) continue;
    else
    {
        var parameters = line.Split(' ', StringSplitOptions.TrimEntries);
        var destination = Int64.Parse(parameters[0]);
        var source = Int64.Parse(parameters[1]);
        var end = source+Int64.Parse(parameters[2]) - 1;
        var r = new Range(source, destination, end);
        temp.AddRange(r);
    }
}
rangeList.Add(temp);
// PART ONE
var seedList = new List<List<Int64>>();
Console.WriteLine("Part One");
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
Console.WriteLine("Part Two");
var ranges = new Dictionary<Int64, Int64>();

for (int i = 0; i < seeds.Length; i += 2)
{
    var start = Int64.Parse(seeds[i]);
    var stop = Int64.Parse(seeds[i+1]);
    ranges.Add(start, stop);
}


foreach (var range in ranges)
{
    Int64 stop = range.Key + range.Value;
    for (Int64 i = range.Key; i < stop; i++)
    {
        Int64 val = GetLocation(i);
        if (val < partTwo)
        {
            partTwo = val;
        }

    }
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
    Int64 val = seed;
    foreach(var range in rangeList)
    {
        val = range.InRange(val);
    }
    //Console.WriteLine();
    return val;
}

class Range
{
    Int64 source;
    Int64 destination;
    Int64 end;

    public Range(Int64 source, Int64 destination, Int64 end)
    {
        this.source = source;
        this.destination = destination;
        this.end = end;
    }

    public Int64 InRange(Int64 value)
    {
        //Console.WriteLine($"Checking if {value} is between {source} and {end}");
        if (source <= value && value <= end)  
        {
            //Console.WriteLine($"Yes it is now, {destination + (value - source)}");
            return destination + (value - source); 
        }
        return value;
    }
}
class RangeList
{
    List<Range> ranges = new List<Range>();

    public RangeList()
    {
    }
    public void AddRange(Range range)
    {
        this.ranges.Add(range);
    }
    public Int64 InRange(Int64 value)
    {
        //Console.WriteLine();
        for (int i = 0; i < ranges.Count; i++)
        {
            var val = ranges[i].InRange(value);
            if (val != value) {
                //Console.WriteLine("Hit");
                return val; 
            }
        }
        return value;
    }
}
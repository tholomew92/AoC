using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

var watch = new System.Diagnostics.Stopwatch();
watch.Start();
var workDir = AppDomain.CurrentDomain.BaseDirectory;
var path = new DirectoryInfo(workDir).Parent.Parent.Parent.ToString();
var inputData = File.ReadAllLines(path + "\\input.txt").ToList();
var testData = File.ReadAllLines(path + "\\test.txt").ToList();

var input = inputData;

Int64 partOne = 0;
Int64 partTwo = 0;

var times = new List<Int64>();
var distances = new List<Int64>();

var timeData = input[0].Split(':', StringSplitOptions.TrimEntries);
var recordData = input[1].Split(':', StringSplitOptions.TrimEntries);


var timePartOne = timeData[1].Split(' ', StringSplitOptions.RemoveEmptyEntries);
var recordPartOne = recordData[1].Split(' ', StringSplitOptions.RemoveEmptyEntries);
foreach (var time in timePartOne)
{
    times.Add(Int64.Parse(time.Trim()));
}
foreach (var dist in recordPartOne)
{
    distances.Add(Int64.Parse(dist.Trim()));
}

var partTwoTime = new List<Int64>() { Int64.Parse(timeData[1].Replace(" ", "")) };
var partTwoRecords = new List<Int64>() { Int64.Parse(recordData[1].Replace(" ", "")) };

var parse = watch.Elapsed;

//PART ONE

partOne = Day6(times, distances);

var timeOne = watch.Elapsed - parse;

//PART TWO

partTwo = Day6(partTwoTime, partTwoRecords);

var timeTwo = watch.Elapsed - timeOne - parse;

watch.Stop();
Console.WriteLine($"The time for parsing input is: {FormattedTime(parse)}");
Console.WriteLine($"The result for part one is: {partOne}");
Console.WriteLine($"The time for part one is {FormattedTime(timeOne)}");
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

Int64 Day6(List<Int64> times, List<Int64> records)
{
    Int64 result = 1;
    for (int i = 0; i < times.Count; i++)
    {
        var record = records[i];
        var time = times[i];
        Int64 first = 0;
        Int64 last = 0;
        for (Int64 j = 0; j < time; j++)
        {
            var speed = j;
            var distance = speed * (time - j);
            if (distance > record)
            {
                first = j;
                break;
            }
        }
        for (Int64 j = time; j > 0; j--)
        {
            var speed = j;
            var distance = speed * (time - j);
            if (distance > record)
            {
                last = j;
                break;
            }
        }
        result *= (last - first + 1);
    }
    return result;
}
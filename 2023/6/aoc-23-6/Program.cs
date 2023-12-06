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

var partOne = 1;
Int64 partTwo = 0;

var times = new List<int>();
var distances = new List<int>();

var timeData = input[0].Split(':', StringSplitOptions.TrimEntries)[1].Split(' ', StringSplitOptions.RemoveEmptyEntries);
foreach (var time in timeData)
{
    times.Add(int.Parse(time.Trim()));
}
var distData = input[1].Split(':', StringSplitOptions.TrimEntries)[1].Split(' ', StringSplitOptions.RemoveEmptyEntries);
foreach(var dist in distData)
{
    distances.Add(int.Parse(dist.Trim()));
}
var parse = watch.Elapsed;

for(int i = 0; i < times.Count; i++)
{
    var record = distances[i];
    var time = times[i];
    var wins = 0;
    for(int j = 0; j < time; j++)
    {
        var speed = j;
        var distance = speed * (time - j);
        if (distance > record) wins++;
    }
    if(wins > 0) partOne *= wins;
}

var timeOne = watch.Elapsed - parse;

var partTwoTime = "";
foreach(var time in timeData)
{
    partTwoTime += time.Trim();
}
var partTwoDist = "";
var newTime = Int64.Parse(partTwoTime);
foreach (var dist in distData)
{
    partTwoDist += dist.Trim();
}
var newDist = Int64.Parse(partTwoDist);
Int64 first = 0;
Int64 last = 0;
for(Int64 i = 0; i < newTime; i++)
{
    Int64 speed = i;
    Int64 distance = speed * (newTime - i);
    if (distance > newDist)
    {
        first = i;
        break;
    }
}
for(Int64 i = newTime; i > first; i--)
{
    Int64 speed = i;
    Int64 distance = speed * (newTime - i);
    if (distance > newDist)
    {
        last = i;
        break;
    }
}
partTwo = last - first;

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

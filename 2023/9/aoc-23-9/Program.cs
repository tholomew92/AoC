﻿using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Text.Json;

var watch = new System.Diagnostics.Stopwatch();
watch.Start();
var workDir = AppDomain.CurrentDomain.BaseDirectory;
var path = new DirectoryInfo(workDir).Parent.Parent.Parent.ToString();
var inputData = File.ReadAllLines(path + "\\input.txt").ToList();
var testData = File.ReadAllLines(path + "\\test.txt").ToList();

var input = inputData;

var partOne = 0;
var partTwo = 0;

var parse = watch.Elapsed;

int c = 0;
foreach (var line in input)
{
    var split = line.Split(" ", StringSplitOptions.TrimEntries).ToList();
    var ints = new List<int>();
    foreach(var s in split) ints.Add(int.Parse(s));
    partOne += PartOne(ints) + ints.Last();
}

var timeOne = watch.Elapsed - parse;

foreach (var line in input)
{
    var split = line.Split(" ", StringSplitOptions.TrimEntries).ToList();
    var ints = new List<int>();
    foreach (var s in split) ints.Add(int.Parse(s));
    partTwo += ints.First() - PartTwo(ints);
}

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

int PartOne(List<int> list)
{
    var newList = new List<int>();
    for(int i = 0; i < list.Count - 1; i++)
    {
        var val = list[i+1] - list[i];
        newList.Add(val);
    }

    if (!newList.All(n => n == 0)) return newList[newList.Count() - 1] + PartOne(newList);
    return 0;
}

int PartTwo(List<int> list)
{
    var newList = new List<int>();
    for (int i = 0; i < list.Count - 1; i++)
    {
        var val = list[i + 1] - list[i];
        newList.Add(val);
    }
    if (!newList.All(n => n == 0)) return newList.FirstOrDefault() - PartTwo(newList);
    return 0;
}
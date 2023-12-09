using System.Runtime.CompilerServices;

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

var instructions = input[0];
var nodes = new Dictionary<string, List<String>>();
var first = "AAA";
for(int i = 2; i < input.Count; i++)
{
    var split = input[i].Split("=",StringSplitOptions.TrimEntries);
    var node = split[1].Replace("(", "");
    node = node.Replace(")", "");
    var temp = node.Split(",", StringSplitOptions.TrimEntries).ToList();
    nodes.Add(split[0], temp);
}


var found = false;
var instr = 0;
while (!found)
{
    if (instr == instructions.Length) instr = 0;
    var step = instructions[instr];
    if (step.Equals('L')) first = nodes[first][0];
    else first = nodes[first][1];
    instr++;
    partOne++;
    if (first.Equals("ZZZ")) found = true;
}

var timeOne = watch.Elapsed - parse;

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
    if (formatted.Length > 4)
    {
        formatted = formatted.Insert(formatted.Length - 4, ",");
        formatted += " ms";
    }
    else
    {
        formatted += " µs";
    }

    return formatted;
}

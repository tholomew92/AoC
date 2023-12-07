using System.Runtime.CompilerServices;

var watch = new System.Diagnostics.Stopwatch();
watch.Start();
var workDir = AppDomain.CurrentDomain.BaseDirectory;
var path = new DirectoryInfo(workDir).Parent.Parent.Parent.ToString();
var inputData = File.ReadAllLines(path + "\\input.txt").ToList();
var testData = File.ReadAllLines(path + "\\test.txt").ToList();

var input = testData;

var partOne = 0;
var partTwo = 0;

var parse = watch.Elapsed;


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

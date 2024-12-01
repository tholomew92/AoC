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

foreach (var line in input)
{
    var split = line.Split(" ", StringSplitOptions.TrimEntries).Select(x => int.Parse(x)).ToList();
    partOne += split.Last() + GetNextValue(split);
    split.Reverse();
    partTwo += split.Last() + GetNextValue(split);
}

var time = watch.Elapsed - parse;

watch.Stop();
Console.WriteLine($"The time for parsing input is: {FormattedTime(parse)}");
Console.WriteLine($"The result for part one is: {partOne}");
Console.WriteLine($"The result for part two is: {partTwo}");
Console.WriteLine($"The time for day nine is {FormattedTime(time)}");
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
    var formatted = temp[count..];
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

int GetNextValue(List<int> list)
{
    var newList = new List<int>();
    for(int i = 0; i < list.Count - 1; i++)
    {
        var val = list[i+1] - list[i];
        newList.Add(val);
    }
    if (!newList.All(n => n == newList.First())) return newList.Last() + GetNextValue(newList);
    return newList.Last();
}

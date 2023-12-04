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

foreach(var line in input)
{
    var split = line.Split(':',StringSplitOptions.TrimEntries);
    var card = split[1].Split('|', StringSplitOptions.TrimEntries);
    int value = 0;
    var winning = card[0].Split(' ', StringSplitOptions.RemoveEmptyEntries);
    var playing = card[1].Split(' ', StringSplitOptions.RemoveEmptyEntries);
    foreach (var play in playing)
    {
        if (winning.Contains(play))
        {
            if (value == 0)
            {
                value = 0b_0001;
            }
            else value = value << 1;
        }
    }
    partOne += value;
}

var timeOne = watch.Elapsed - parse;

var dict = new Dictionary<int, int>();
for(int i = 0; i < input.Count; i++)
{
    if (!dict.ContainsKey(i))
    {
        dict.Add(i, 1);
    }
    var line = input[i]; 
    var split = line.Split(':', StringSplitOptions.TrimEntries);
    var card = split[1].Split('|', StringSplitOptions.TrimEntries);
    int value = 0;
    var winning = card[0].Split(' ', StringSplitOptions.RemoveEmptyEntries);
    var playing = card[1].Split(' ', StringSplitOptions.RemoveEmptyEntries);
    foreach(var play in playing)
    {
        if (winning.Contains(play)) value++;
    }
    //value = playing.Intersect(winning).ToList().Count();
    for(int j = 1; j <= value; j++)
    {
        if (!dict.ContainsKey(i + j)) dict.Add(i + j, 1 + dict[i]);
        else dict[i + j] += dict[i];
    }
}

foreach(var dic in dict)
{
    partTwo+= dic.Value;
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

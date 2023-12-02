using System.Runtime.CompilerServices;

var watch = new System.Diagnostics.Stopwatch();
watch.Start();
var workDir = AppDomain.CurrentDomain.BaseDirectory;
var path = new DirectoryInfo(workDir).Parent.Parent.Parent.ToString();
var inputDat = File.ReadAllLines(path + "\\input.txt").ToList();
var testData = File.ReadAllLines(path + "\\test.txt").ToList();

var input = inputDat;

var partOne = 0;
var partTwo = 0;

var parse = watch.Elapsed;
var maxRed = 12;
var maxGreen = 13;
var maxBlue = 14;

foreach(var line in input)
{
    var minRed = 0;
    var minGreen = 0;
    var minBlue = 0;
    var index  = line.IndexOf(':')+1;
    var data = line.Substring(index);
    var split = data.Split(';', StringSplitOptions.TrimEntries);
    var valid = true;
    foreach(var item in split)
    {
        var cubes = item.Split(',', StringSplitOptions.TrimEntries);
        foreach(var c in cubes)
        {
            var cube = c.Split(' ');
            switch (cube[1])
            {
                case "red":
                    if (int.Parse(cube[0]) > minRed) minRed = int.Parse(cube[0]);
                    if (int.Parse(cube[0]) > maxRed) valid = false;
                    break;
                case "green":
                    if (int.Parse(cube[0]) > minGreen) minGreen = int.Parse(cube[0]);
                    if (int.Parse(cube[0]) > maxGreen) valid = false;
                    break;
                case "blue":
                    if (int.Parse(cube[0]) > minBlue) minBlue = int.Parse(cube[0]);
                    if (int.Parse(cube[0]) > maxBlue) valid = false;
                    break;
                default:
                    break;
            }
        }
    }
    if (valid)
    {
        var gameNr = line.Substring(0, index - 1).Split(' ');
        partOne += int.Parse(gameNr[1]);
    }
    partTwo += (minRed * minGreen * minBlue);
}

var timeOne = watch.Elapsed - parse;


watch.Stop();
Console.WriteLine($"The time for parsing input is: {FormattedTime(parse)}");
Console.WriteLine($"The result for part one is: {partOne}");
Console.WriteLine($"The result for part two is: {partTwo}");
Console.WriteLine($"The time for day two is {FormattedTime(timeOne)}");
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

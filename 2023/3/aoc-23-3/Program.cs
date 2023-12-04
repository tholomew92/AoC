using System.Runtime.CompilerServices;
using static System.Net.Mime.MediaTypeNames;

var watch = new System.Diagnostics.Stopwatch();
watch.Start();
var workDir = AppDomain.CurrentDomain.BaseDirectory;
var path = new DirectoryInfo(workDir).Parent.Parent.Parent.ToString();
var inputDat = File.ReadAllLines(path + "\\input.txt").ToList();
var testData = File.ReadAllLines(path + "\\test.txt").ToList();

var input = testData;

var partOne = 0;
var partTwo = 0;
var ySize = input.Count;
var xSize = input[0].Length;

Console.WriteLine("y: " + ySize + ", x: " + xSize);

var matrix = new char[ySize,xSize];
for (int i = 0; i < ySize; i++)
{
    for ( int j = 0; j < xSize; j++)
    {
        matrix[i,j] = input[i][j];
    }
}

var parse = watch.Elapsed;
for(int i = 0; i < ySize; i++)
{
    var line = input[i];
    for (int j = 0; j < xSize; j++)
    {
        var nextX = 0;
        var c = matrix[i,j];
        string num = "";
        if (int.TryParse(c.ToString(), out int a))
        {
            nextX = AmountOfNums(i, j);
        }
        else continue;
        Console.WriteLine(nextX);
        for(int k = 0; k < xSize - j; k++)
        {
            num += matrix[i, k].ToString();
        }
        Console.WriteLine(num + ": " + j + " - " + nextX);
        if ((j + nextX) < xSize - 1) j += nextX;
        else break;
    //Console.WriteLine(num);
}
    Console.WriteLine();
}

var timeOne = watch.Elapsed - parse;

var timeTwo = watch.Elapsed - timeOne - parse;

int AmountOfNums(int y, int x)
{
    if (x < xSize - 2) {
        if (int.TryParse(matrix[y, x+1].ToString(), out int a))
        {
            return AmountOfNums(y, x+1);
        }
        else
        {
            return x;
        }
    }
    return x;
}


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

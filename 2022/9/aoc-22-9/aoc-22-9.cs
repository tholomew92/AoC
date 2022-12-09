using System.Runtime.CompilerServices;

var watch = new System.Diagnostics.Stopwatch();
watch.Start();
var workDir = AppDomain.CurrentDomain.BaseDirectory;
var path = new DirectoryInfo(workDir).Parent.Parent.Parent.Parent.ToString();
//var input = File.ReadAllLines(path + "\\input.txt").ToList();
var input = File.ReadAllLines(path + "\\test.txt").ToList();

var matrix = new int[1000, 1000];

var partOne = 0;
var partTwo = 0;

var parse = watch.Elapsed;

int hRow, hCol, tRow, tCol;
hRow = hCol = tRow = tCol = 500;

foreach(var line in input)
{
    var instr = line.Split(' ', StringSplitOptions.TrimEntries);
    var steps = int.Parse(instr[1]);
    switch (instr[0])
    {
        case "U":
            Up(steps);
            break;
        case "D":
            Down(steps);
            break;
        case "L":
            Left(steps);
            break;
        case "R":
            Right(steps);
            break;
    }
}


for(int i = 0; i < 1000; i++)
{
    for (int j = 0; j < 1000; j++)
    {
        if (matrix[i,j] > 0)
        {
            Console.WriteLine(i + " " + j);
            partOne++;
        }
    }
}

var timeOne = watch.Elapsed - parse;

matrix[500, 500]++;

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

void Up(int steps)
{
    for (int row = 0; row < steps; row++)
    {
        hRow++;
        if (Math.Abs(hRow - tRow) > 1)
        {
            if (tRow < hRow) tRow++;
            else tRow--;
            if (tCol != hCol) tCol = hCol;
        }
        matrix[tRow, tCol]++;
    }
}

void Down(int steps)
{
    for (int row = 0; row < steps; row++)
    {
        hRow--;
        if (Math.Abs(hRow - tRow) > 1)
        {
            if (tRow < hRow) tRow++;
            else tRow--;
            if (tCol != hCol) tCol = hCol;
        }
        matrix[tRow, tCol]++;
    }
}
void Left(int steps)
{
    for (int row = 0; row < steps; row++)
    {
        hCol--;
        if (Math.Abs(hCol - tCol) > 1)
        {
            if (tCol < hCol) tCol++;
            else tCol--;
            if (tRow != hRow) tRow = hRow;

        }
        matrix[tRow, tCol]++;
    }
}
void Right(int steps)
{
    for (int row = 0; row < steps; row++)
    {
        hCol++;
        if (Math.Abs(hCol - tCol) > 1)
        {
            if (tCol < hCol) tCol++;
            else tCol--;
            if (tRow != hRow) tRow = hRow;
        }
        matrix[tRow, tCol]++;
    }
}
var watch = new System.Diagnostics.Stopwatch();
watch.Start();
var workDir = AppDomain.CurrentDomain.BaseDirectory;
var path = new DirectoryInfo(workDir).Parent.Parent.Parent.Parent.ToString();
var input = File.ReadAllLines(path + "\\input.txt").ToList();
//var input = File.ReadAllLines(path + "\\test.txt").ToList();

var matrix = new int[input.Count,input.Count];

var partOne = input.Count * 2 + (input.Count - 2) * 2;
var partTwo = int.MinValue;

for(int i = 0; i < input.Count; i++)
{
    for(int j = 0; j < input[i].Length; j++)
    {
        matrix[i,j] = int.Parse(input[i][j].ToString());
    }
}

var parse = watch.Elapsed;

for(int y = 1; y < input.Count - 1; y++)
{
    for(int x = 1; x < input.Count - 1; x++)
    {
        var check = Up(y, x, matrix[y, x]);
        if (check)
        {
            partOne++;
            continue;
        }
        check = Down(y, x, matrix[y, x]);
        if (check)
        {
            partOne++;
            continue;
        }
        check = Left(y, x, matrix[y, x]);
        if (check)
        {
            partOne++;
            continue;
        }
        check = Right(y, x, matrix[y, x]);
        if (check) partOne++;
    }
}

var timeOne = watch.Elapsed - parse;

for (int y = 1; y < input.Count - 1; y++)
{
    for (int x = 1; x < input.Count - 1; x++)
    {
        var up = Up2(y, x, matrix[y, x]);
        var down = Down2(y, x, matrix[y, x]);
        var left = Left2(y, x, matrix[y, x]);
        var right = Right2(y, x, matrix[y, x]);
        if (up * down * left * right > partTwo)
        {
            partTwo = up * down * left * right;
        }
    }
}

var timeTwo = watch.Elapsed - timeOne - parse;
watch.Stop();
Console.WriteLine($"The time for parsing input is: {FormattedTime(parse)}");
Console.WriteLine($"The result for part one is: {partOne}");
Console.WriteLine($"The time for part one is {FormattedTime(timeOne)} ms");
Console.WriteLine($"The result for part two is: {partTwo}");
Console.WriteLine($"The time for part two is {FormattedTime(timeTwo)} ms");
Console.WriteLine($"Total time is {FormattedTime(watch.Elapsed)} ms");

String FormattedTime(TimeSpan ts)
{
    var temp = ts.ToString("fffffff");
    var count = 0;
    foreach (var c in temp)
    {
        if (c.Equals('0')) count++;
        else break;
    }
    if (temp.Length - count <= 4) count--;
    var formatted = temp.Substring(count);
    if (formatted.Length - 4 > 0) formatted = formatted.Insert(formatted.Length - 4, ",");
    return formatted;
}

bool Up(int y, int x, int val)
{
    for (int i = 0; i < y; i++)
    {
        if (matrix[i, x] >= val) return false;
    }
    return true;
}

bool Down(int y, int x, int val)
{
    for (int i = y + 1; i < input.Count; i++)
    {
        if (matrix[i, x] >= val) return false;
    }
    return true;
}

bool Left(int y, int x, int val)
{
    for (int i = 0; i < x; i++)
    {
        if (matrix[y, i] >= val) return false;
    }
    return true;
}

bool Right(int y, int x, int val)
{
    for (int i = x + 1; i < input.Count; i++)
    {
        if (matrix[y, i] >= val) return false;
    }
    return true;
}

int Up2(int y, int x, int val)
{
    var retVal = 0;
    for (int i = y -1 ; i >= 0; i--)
    {
        retVal++;
        if (matrix[i, x] >= val) break;
    }
    return retVal;
}

int Down2(int y, int x, int val)
{
    var retVal = 0;
    for (int i = y + 1; i < input.Count; i++)
    {
        retVal++;
        if (matrix[i, x] >= val) break;
    }
    return retVal;
}

int Left2(int y, int x, int val)
{
    var retVal = 0;
    for (int i = x - 1; i >= 0; i--)
    {
        retVal++;
        if (matrix[y, i] >= val) break;
    }
    return retVal;
}

int Right2(int y, int x, int val)
{
    var retVal = 0;
    for (int i = x + 1; i < input.Count; i++)
    {
        retVal++;
        if (matrix[y, i] >= val) break;
    }
    return retVal;
}
using System.Runtime.CompilerServices;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

var watch = new System.Diagnostics.Stopwatch();
watch.Start();
var workDir = AppDomain.CurrentDomain.BaseDirectory;
var path = new DirectoryInfo(workDir).Parent.Parent.Parent.ToString();
var inputData = File.ReadAllLines(path + "\\input.txt").ToList();
var testData = File.ReadAllLines(path + "\\test.txt").ToList();

var input = inputData;

var partOne = 0;
var partTwo = 0;
var ySize = input.Count;
var xSize = input[0].Length;
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
    var rowsum = 0;
    for (int j = 0; j < xSize; j++)
    {
        var nextX = 0;
        var c = matrix[i, j];
        string num = "";
        if (int.TryParse(c.ToString(), out int a))
        {
            nextX = AmountOfNums(i, j);
        }
        else continue;
        for (int k = j; k <= nextX; k++)
        {
            num += matrix[i, k].ToString();
        }
        var check = CheckNeighbours(i, j, nextX);
        if (check) 
        {
            //Console.WriteLine(num);
            partOne += int.Parse(num);
            rowsum += int.Parse(num);
        }
        if ((nextX) >= xSize - 1) break;
        else if (nextX != 0) 
        {
            j = nextX+1; 
        }
    }
    //if (i == 9) break;
}

var timeOne = watch.Elapsed - parse;

for (int i = 0; i < ySize; i++)
{
    var line = input[i];
    var rowsum = 0;
    for (int j = 0; j < xSize; j++)
    {
        var nextX = 0;
        var c = matrix[i, j];
        if (c.Equals('*'))
        {
            partTwo += CheckGear(i, j);
        }
        
    }
}


var timeTwo = watch.Elapsed - timeOne - parse;

int AmountOfNums(int y, int x)
{
    if (x < xSize - 1) {
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

bool CheckNeighbours(int row, int startX, int endX)
{
    int minRow, maxRow, minX, maxX;
    if (row == 0) minRow = row;
    else minRow = row - 1;
    if (row == ySize - 1) maxRow = row;
    else maxRow = row + 1;
    if (startX == 0) minX = startX;
    else minX = startX - 1;
    if (endX == xSize - 1) maxX = endX;
    else maxX = endX + 1;
    for(int i = minRow; i <= maxRow; i++)
    {
        for(int j = minX; j <= maxX; j++)
        {
            if (i == row && startX <= j && j <= endX) continue;
            else if (!matrix[i, j].Equals('.')) return true;
        }
    }
    return false;
}
int CheckGear(int row, int col)
{
    int minRow, maxRow, minCol, maxCol;
    if (row == 0) minRow = col;
    else minRow = row - 1;
    if (row == xSize - 1) maxRow= col;
    else maxRow = row + 1;
    if (col == 0) minCol = col;
    else minCol = col - 1;
    if (col == xSize - 1) maxCol = col;
    else maxCol = col + 1;

    int found = 0;
    int value = 1;
    for (int i = minRow; i <= maxRow; i++)
    {
        for (int j = minCol; j <= maxCol; j++)
        {
            if (found > 2) break;
            if (i == row && j == col) continue;
            (int start, int end) num;
            if (int.TryParse(matrix[i, j].ToString(), out int _))
            {
                found++;
                num = GetNumber(i, j);
                var val = "";
                for (int k = num.start; k < num.end; k++)
                {
                    val = val + matrix[i, k];
                }
                //Console.WriteLine(val);
                j = num.end;
                if (int.TryParse(val, out int v)) value *= v;
            }
        }
    }
    if (found == 2)
    {
        Console.WriteLine(value);
        Console.WriteLine();
        return value;
    }
    Console.WriteLine();
    return 0;
}

(int,int) GetNumber(int row, int startPos)
{
    var num = matrix[row, startPos].ToString();
    int startX, endX;
    startX = endX = 0;
    if (startPos != 0)
    {
        for(int i = startPos-1; i >= 0; i--)
        {
            if (int.TryParse(matrix[row, i].ToString(), out int _)) continue;
            else
            {
                startX = i+1;
                break;
            }
        }
    }
    if (startPos != xSize - 1)
    {
        for(int i = startPos+1; i <= xSize -1; i++)
        {
            if (int.TryParse(matrix[row, i].ToString(), out int _)) continue;
            else
            {
                endX = i;
                break;
            }
        }
    }
    else
    {
        endX = startPos;
    }
    return (startX, endX);
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

using System.Runtime.CompilerServices;

var watch = new System.Diagnostics.Stopwatch();
watch.Start();
var workDir = AppDomain.CurrentDomain.BaseDirectory;
var path = new DirectoryInfo(workDir).Parent.Parent.Parent.ToString();
var inputData = File.ReadAllLines(path + "\\input.txt").ToList();
var testData = File.ReadAllLines(path + "\\test.txt").ToList();
var testData2 = File.ReadAllLines(path + "\\test2.txt").ToList();

var input = inputData;
var partOne = new List<int>();
var resultOne = 0;
var partTwo = new List<int>(); ;
var resultTwo = 0;

var parse = watch.Elapsed;
foreach (var line in input)
{
    var listOne = new List<int>();
    int value;
    foreach (var c in line){
        if (int.TryParse(c.ToString(), out value))
        {
            listOne.Add(value);
            break;
        }
    }
    for(int i = line.Length-1; i>=0; i--)
    {
        var c = line[i];
        if (int.TryParse(c.ToString(), out value))
        {
            listOne.Add(value);
            break;
        }
    }
    var newValue = int.Parse(listOne[0].ToString() + listOne.Last().ToString());
    partOne.Add(newValue);
}
resultOne = partOne.Sum(x => x);
var timeOne = watch.Elapsed - parse;

//input = testData2;
string[] numbers = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };

foreach (var line in input)
{
    var listTwo = new List<int>();
    int value;
    for ( int i = 0; i < line.Length; i++)
    {
        var c = line[i];
        if (int.TryParse(c.ToString(), out value))
        {
            listTwo.Add(value);
        }
        else
        {
            for (int j = 0; j < numbers.Length; j++)
            {
                var num = numbers[j];
                if (line.Length - i >= num.Length && line.Substring(i, num.Length).Equals(num))
                {
                    listTwo.Add(j);
                    break;
                }

            }
        if(listTwo.Count > 0)
        {
            break;
        }
    }
    for (int i = line.Length-1; i >= 0; i--)
    {
        var c = line[i];
        if (int.TryParse(c.ToString(), out value))
        {
            listTwo.Add(value);
        }
        else
        {
            for (int j = 0; j < numbers.Length; j++)
            {
                var num = numbers[j];
                int startInd = i - num.Length + 1;
                if (startInd >= 0 && line.Substring(startInd, num.Length).Equals(num))
                {
                    listTwo.Add(j);
                    break;
                }

            }
        }
        
        if (listTwo.Count > 1)
        {
            break;
        }
    }
    var newValue = int.Parse(listTwo[0].ToString() + listTwo.Last().ToString());
    partTwo.Add(newValue);
}
resultTwo = partTwo.Sum(x => x);
var timeTwo = watch.Elapsed - timeOne - parse;
watch.Stop();
Console.WriteLine($"The time for parsing input is: {FormattedTime(parse)}");
Console.WriteLine($"The result for part one is: {resultOne}");
Console.WriteLine($"The time for part one is {FormattedTime(timeOne)}");
Console.WriteLine($"The result for part two is: {resultTwo}");
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

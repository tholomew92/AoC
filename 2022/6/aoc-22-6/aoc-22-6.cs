using System.Globalization;
using System.Text.RegularExpressions;

var watch = new System.Diagnostics.Stopwatch();
watch.Start();
var workDir = AppDomain.CurrentDomain.BaseDirectory;
var path = new DirectoryInfo(workDir).Parent.Parent.Parent.Parent.ToString();
var input = File.ReadAllText(path + "\\input.txt").ToString();
//var input = File.ReadAllText(path + "\\test.txt").ToString();
var watchOne = new System.Diagnostics.Stopwatch();
var watchTwo = new System.Diagnostics.Stopwatch();
watchOne.Start();
var partOne = GetMessage(4);
watchOne.Stop();
watchTwo.Start();
var partTwo = GetMessage(14);
watchTwo.Stop();
watch.Stop(); 
var oneTS = FormatedTime(watchOne.Elapsed);
var twoTS = FormatedTime(watchTwo.Elapsed);
var watchTS = FormatedTime(watch.Elapsed);
Console.WriteLine($"The result for part one is: {partOne}");
Console.WriteLine($"The time for part one is: {oneTS} ms");
Console.WriteLine($"The result for part two is: {partTwo}");
Console.WriteLine($"The time for part two is: {twoTS} ms");
Console.WriteLine($"Total time is {watchTS} ms");

String FormatedTime(TimeSpan ts)
{
    var temp =  ts.ToString("fffffff");
    var count = 0;
    foreach(var c in temp)
    {
        if (c.Equals('0')) count++;
        else break;
    }
    if (temp.Length - count <= 4) count--;
    var formatted = temp.Substring(count);
    if (formatted.Length - 4 > 0) formatted = formatted.Insert(formatted.Length-4, ":");
    return formatted;
}

int GetMessage(int amountOfDistinct)
{
    for(int i = 0; i < input.Length - amountOfDistinct; i++)
    {
        bool check = true;
        var tempString = input[i].ToString();
        for(int j = 1; j <= amountOfDistinct-1; j++)
        {
            if (tempString.Contains(input[i+j]))
            {
                check = false;
                break;
            }
            
            tempString += input[i + j].ToString();
        }
        if (check) return i + amountOfDistinct;
    }
    return 0;
}

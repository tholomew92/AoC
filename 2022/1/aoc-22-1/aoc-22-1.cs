using System.Runtime.InteropServices;

var watch = new System.Diagnostics.Stopwatch();
watch.Start();
var workDir = AppDomain.CurrentDomain.BaseDirectory;
var path = new DirectoryInfo(workDir).Parent.Parent.Parent.Parent.ToString();
var input = File.ReadAllLines(path + "\\input.txt").ToList();
//var input = File.ReadAllLines(path + "\\test.txt").ToList();

var calories = new List<int>();
var cal = 0;
for(int i = 0; i < input.Count; i++)
{
    var line = input[i];
    if (line.Equals(""))
    {
        calories.Add(cal);
        cal = 0;
        continue;
    }
    cal += int.Parse(line);
    if(i == (input.Count - 1)) calories.Add(cal);
}

var partOne = calories.OrderByDescending(x => x).First();
Console.WriteLine($"The result for part one is: {partOne}");
var partTwo = calories.OrderByDescending(x => x).Take(3).Sum();
watch.Stop();
Console.WriteLine($"The result for part one is: {partTwo}");
Console.WriteLine($"Time is {watch.ElapsedMilliseconds} ms");
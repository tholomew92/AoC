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

var sorted = from x in calories
             orderby x descending
             select x;

var partOne = sorted.First();
var partTwo = sorted.Take(3).Sum();
Console.WriteLine($"The result for part one is: {partOne}");
Console.WriteLine($"The result for part one is: {partTwo}");
watch.Stop();
Console.WriteLine($"Time is {watch.ElapsedMilliseconds} ms");
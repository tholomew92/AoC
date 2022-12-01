var watch = new System.Diagnostics.Stopwatch();
watch.Start();
var workDir = AppDomain.CurrentDomain.BaseDirectory;
var path = new DirectoryInfo(workDir).Parent.Parent.Parent.Parent.ToString();
var input = File.ReadAllText(path + "\\input.txt").ToString();

var calories = input.Split("\r\n\r\n").Select(cal => cal.Split("\n").Select(int.Parse).Sum()).OrderDescending();
var partOne = calories.First();
var partTwo = calories.Take(3).Sum();
Console.WriteLine($"The result for part one is: {partOne}");
Console.WriteLine($"The result for part one is: {partTwo}");
watch.Stop();
Console.WriteLine($"Time is {watch.ElapsedMilliseconds} ms");

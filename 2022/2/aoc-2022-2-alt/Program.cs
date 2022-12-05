var watch = new System.Diagnostics.Stopwatch();
watch.Start();
var workDir = AppDomain.CurrentDomain.BaseDirectory;
var path = new DirectoryInfo(workDir).Parent.Parent.Parent.Parent.ToString();
var input = File.ReadAllLines(path + "\\input.txt").ToString();

var partOne = 0;
var partTwo = 0;
foreach(var line in input)
{

}

watch.Stop();
Console.WriteLine($"The result for part one is: {partOne}");
Console.WriteLine($"The result for part one is: {partTwo}");
Console.WriteLine($"Time is {watch.ElapsedMilliseconds} ms");

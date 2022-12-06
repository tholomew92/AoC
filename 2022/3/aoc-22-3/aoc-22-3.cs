var watch = new System.Diagnostics.Stopwatch();
watch.Start();
var workDir = AppDomain.CurrentDomain.BaseDirectory;
var path = new DirectoryInfo(workDir).Parent.Parent.Parent.Parent.ToString();
var input = File.ReadAllLines(path + "\\input.txt").ToList();
//var input = File.ReadAllLines(path + "\\test.txt").ToList();

var letterList = "-abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
var partOne = 0;
var partTwo = 0;
for (int i = 0; i < input.Count; i++)
{
    var line = input[i];
    var mid = line.Length / 2;
    var first = line.Substring(0, mid);
    var second = line.Substring(mid);

    var common = first.Intersect(second).Single();

    partOne += Array.IndexOf(letterList, common);
    
    if (i%3 == 0)
    {
        var intersect = line.Intersect(input[i + 1].Intersect(input[i + 2])).Single();
        partTwo += Array.IndexOf(letterList, intersect);
    }
}


Console.WriteLine($"The result for part one is: {partOne}");
Console.WriteLine($"The result for part one is: {partTwo}");
watch.Stop();
Console.WriteLine($"Time is {watch.ElapsedMilliseconds} ms");
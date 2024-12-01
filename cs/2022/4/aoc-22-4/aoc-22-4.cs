var watch = new System.Diagnostics.Stopwatch();
watch.Start();
var workDir = AppDomain.CurrentDomain.BaseDirectory;
var path = new DirectoryInfo(workDir).Parent.Parent.Parent.Parent.ToString();
var input = File.ReadAllLines(path + "\\input.txt").ToList();
//input = File.ReadAllLines(path + "\\test.txt").ToList();

var partOne = 0;
var partTwo = 0;

foreach(var line in input)
{
    var assignements = line.Split(',', StringSplitOptions.TrimEntries);
    var first = assignements[0].Split('-', StringSplitOptions.TrimEntries).Select(Int32.Parse).ToList();
    var second = assignements[1].Split('-', StringSplitOptions.TrimEntries).Select(Int32.Parse).ToList();
    if ((first[0] <= second[0]) && (first[1] >= second[1])) partOne++;
    else if ((first[0] >= second[0]) && (first[1] <= second[1])) partOne++;

    if ((second[0] <= first[0] && first[0] <= second[1]) || (second[0] <= first[1] && first[1] <= second[1])) partTwo++;
    else if ((first[0] <= second[0] && second[0] <= first[1]) || (first[0] <= second[1] && second[1] <= first[1])) partTwo++;


}

Console.WriteLine($"The result for part one is: {partOne}");
Console.WriteLine($"The result for part one is: {partTwo}");
watch.Stop();
Console.WriteLine($"Time is {watch.ElapsedMilliseconds} ms");
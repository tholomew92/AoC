var watch = new System.Diagnostics.Stopwatch();
watch.Start();
var workDir = AppDomain.CurrentDomain.BaseDirectory;
var path = new DirectoryInfo(workDir).Parent.Parent.Parent.Parent.Parent.ToString();
var input = File.ReadAllLines(path + "\\input.txt").ToList();
//var input = File.ReadAllLines(path + "\\test.txt").ToList();

var partOne = 0;
var partTwo = 0;
foreach (var line in input)
{
    var me = line[2].ToString();
    var opponent = line[0].ToString();
    var result = "";
    switch (me)
    {
        case "X":
            result = XStrat(opponent);
            break;
        case "Y":
            result = YStrat(opponent);
            break;
        case "Z":
            result  = ZStrat(opponent);
            break;
        default: break;
    }
    var results = result.Split('-');
    partOne += int.Parse(results[0]);
    partTwo += int.Parse(results[1]);
    Console.WriteLine(line + " " + results[1]);
}

string XStrat(string opponent)
{
    switch (opponent)
    {
        case "A": return "4-3";
        case "B": return "1-1";
        case "C": return "7-2";
        default: return "";
    }
}

string YStrat(string opponent)
{
    switch (opponent)
    {
        case "A": return "8-4";
        case "B": return "5-5";
        case "C": return "2-6";
        default: return "";
    }
}

string ZStrat(string opponent)
{
    switch (opponent)
    {
        case "A": return "3-8";
        case "B": return "9-9";
        case "C": return "6-7";
        default: return "";
    }
}

Console.WriteLine($"The result for part one is: {partOne}");
Console.WriteLine($"The result for part two is: {partTwo}");
watch.Stop();
Console.WriteLine($"Time is {watch.ElapsedMilliseconds} ms");
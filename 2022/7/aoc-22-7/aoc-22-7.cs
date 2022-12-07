var watch = new System.Diagnostics.Stopwatch();
watch.Start();
var workDir = AppDomain.CurrentDomain.BaseDirectory;
var path = new DirectoryInfo(workDir).Parent.Parent.Parent.Parent.ToString();
var input = File.ReadAllLines(path + "\\input.txt").ToList();
//var input = File.ReadAllLines(path + "\\test.txt").ToList();

var dirs = new List<Directory>();
FillDirectory();
var parse = watch.Elapsed;
var partOne = PartOne();
var timeOne = watch.Elapsed - parse;
var partTwo = PartTwo();
var timeTwo = watch.Elapsed - parse - timeOne;
watch.Stop();
Console.WriteLine($"The time for parse is: {FormatteddTime(parse)} ms");
Console.WriteLine($"The result for part one is: {partOne}");
Console.WriteLine($"The time for part one is {FormatteddTime(timeOne)} ms");
Console.WriteLine($"The result for part two is: {partTwo}");
Console.WriteLine($"The time for part two is {FormatteddTime(timeTwo)} ms");
Console.WriteLine($"Total time is {FormatteddTime(watch.Elapsed)} ms");

String FormatteddTime(TimeSpan ts)
{
    var temp = ts.ToString("fffffff");
    var count = 0;
    foreach (var c in temp)
    {
        if (c.Equals('0')) count++;
        else break;
    }
    if (temp.Length - count <= 4) count--;
    var formatted = temp.Substring(count);
    if (formatted.Length - 4 > 0) formatted = formatted.Insert(formatted.Length - 4, ",");
    return formatted;
}

void FillDirectory()
{
    var current = new Directory(null, "/");
    dirs.Add(current);

    foreach(var line in input)
    {
        if (line.StartsWith("$ cd"))
        {
            var dir = line.Split(" ")[2];
            current = dir.Equals("..") ? current.Up() : current.Down(dir);
            continue;
        }

        else if (line.StartsWith("$ ls")) continue;

        else if (line.StartsWith("dir"))
        {
            var dir = new Directory(current, line.Split(" ", StringSplitOptions.TrimEntries)[1]);
            dirs.Add(dir);
            current.Children.Add(dir);
            continue;
        }

        else
        {
            var file = line.Split(" ");

            current.Files.Add((file[1], int.Parse(file[0])));
            continue;
        }
        
    }
}

int PartOne()
{
    var partOne = 0;
    foreach (var dir in dirs)
    {
        if (dir.GetSize() <= 100000) partOne += dir.GetSize();
    }
    return partOne;
}

int PartTwo()
{
    var totaltSize = 70000000;
    var unusedSize = totaltSize - dirs[0].GetSize();
    var validDirs = new List<Directory>();
    var needSize = 30000000 - unusedSize;

    foreach (var dir in dirs)
    {
        if (dir.GetSize() > needSize)
        { 
            validDirs.Add(dir);
        }
    }

    validDirs = validDirs.OrderBy(s => s.GetSize()).ToList();

    return validDirs[0].GetSize();
}

class Directory
{
    public string Name { get; }
    public Directory Parent { get; }
    public List<Directory> Children { get; } = new();
    public List<(string name, int size)> Files { get; } = new();

    public Directory(Directory parent, string name)
    {
        Parent = parent;
        Name = name;
    }

    public Directory Up() => Parent;

    public Directory Down(string name) => name.Equals("/") ? this : Children.First(d => d.Name.Equals(name));

    public int GetSize() => Children.Any() ? Children.Select(c => c.GetSize()).Sum() + Files.Select(f => f.size).Sum() : Files.Select(f => f.size).Sum();
}
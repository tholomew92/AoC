using System.Collections;

var watch = new System.Diagnostics.Stopwatch();
watch.Start();
var workDir = AppDomain.CurrentDomain.BaseDirectory;
var path = new DirectoryInfo(workDir).Parent.Parent.Parent.Parent.ToString();
var input = File.ReadAllLines(path + "\\input.txt").ToList();
//var input = File.ReadAllLines(path + "\\test.txt").ToList();

var stacks = new Dictionary<int, Stack>();
var split = 0;

var partOne = PartOne();
var partTwo = PartTwo();

watch.Stop();
Console.WriteLine($"The result for part one is: {partOne}");
Console.WriteLine($"The result for part two is: {partTwo}");
Console.WriteLine($"Time is {watch.ElapsedMilliseconds} ms");

Dictionary<int, Stack> ParseInput()
{
    for (int i = 0; i < input.Count; i++)
    {
        var line = input[i];
        if (line[1].Equals('1'))
        {
            split = i + 2;
            break;
        }
        for (int j = 1; j < line.Length; j += 4)
        {
            if (!line[j].Equals(' '))
            {
                int stack = (j + 3) / 4;
                if (!stacks.ContainsKey(stack)) stacks.Add(stack, new Stack());
                stacks[stack].Push(line[j]);
            }
        }
    }
    for (int i = 1; i <= stacks.Count; i++)
    {
        var tempStack = new Stack();
        foreach (var item in stacks[i])
        {
            tempStack.Push(item);
        }
        stacks[i] = tempStack;
    }

    return stacks;
}

string PartOne()
{
    stacks = ParseInput();
    for (int i = split; i < input.Count; i++)
    {
        var instructions = input[i].Split(" ", StringSplitOptions.TrimEntries);
        var steps = int.Parse(instructions[1]);
        var from = int.Parse(instructions[3]);
        var to = int.Parse(instructions[5]);
        var tempStack = new Stack();
        for (int j = 0; j < steps; j++)
        {
            var val = stacks[from].Pop();
            stacks[to].Push(val);
        }

    }
    var result = "";
    for (int i = 1; i <= stacks.Count; i++)
    {
        if (stacks[i].Count != 0) result += stacks[i].Peek();
    }
    return result;
}

string PartTwo()
{
    stacks.Clear();
    stacks = ParseInput();
    for (int i = split; i < input.Count; i++)
    {
        var instructions = input[i].Split(" ", StringSplitOptions.TrimEntries);
        var steps = int.Parse(instructions[1]);
        var from = int.Parse(instructions[3]);
        var to = int.Parse(instructions[5]);
        var tempStack = new Stack();
        for (int j = 0; j < steps; j++)
        {
            var val = stacks[from].Pop();
            tempStack.Push(val);
        }

        for (int j = 0; j < steps; j++)
        {
            stacks[to].Push(tempStack.Pop());
        }

    }

    var result = "";
    for (int i = 1; i <= stacks.Count; i++)
    {
        if (stacks[i].Count != 0) result += stacks[i].Peek();
    }
    return result;
}
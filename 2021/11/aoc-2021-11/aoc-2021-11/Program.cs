// See https://aka.ms/new-console-template for more information

using System.Reflection.Metadata;
using System.Runtime.CompilerServices;

var watch = new System.Diagnostics.Stopwatch();
watch.Start();
string workdir = Environment.CurrentDirectory;
string inputPath = new DirectoryInfo(workdir).Parent.Parent.Parent.Parent.Parent.ToString();
List<string> input = File.ReadAllLines(inputPath + "\\input.txt").ToList();
//List<string> input = File.ReadAllLines(inputPath + "\\test.txt").ToList();
int[,] octopuses = new int[10,10];
int[,] flashed = new int[10, 10];
for (int y = 0; y < 10; y++)
{
    for(int x = 0; x < 10; x++)
    {
        octopuses[y, x] = int.Parse(input[y][x].ToString());
    }
}
var steps = 100;
var flashes = 0;
for (int s = 0; s < steps; s++)
{
    flashed = new int[10, 10];
    for (int y = 0; y < 10; y++)
    {
        for (int x = 0; x < 10; x++)
        {
            if (flashed[y, x] != 1)
            {
                if (octopuses[y, x] < 9) octopuses[y, x]++;
                else
                {
                    octopuses[y, x] = 0;
                    flashed[y, x] = 1;
                    flashes++;
                    Flash(y, x);
                }
            }
        }
    }
}

for (int y = 0; y < 10; y++)
{
    for (int x = 0; x < 10; x++)
    {
        octopuses[y, x] = int.Parse(input[y][x].ToString());
    }
}

steps = 0;
while (true)
{
    steps++;
    flashed = new int[10, 10];
    for (int y = 0; y < 10; y++)
    {
        for (int x = 0; x < 10; x++)
        {
            if (flashed[y, x] != 1)
            {
                if (octopuses[y, x] < 9) octopuses[y, x]++;
                else
                {
                    octopuses[y, x] = 0;
                    flashed[y, x] = 1;
                    flashes++;
                    Flash(y, x);
                }
            }
        }
    }
    var nulCheck = true;
    for (int y = 0; y < 10; y++)
    {
        for (int x = 0; x < 10; x++)
        {
            if (octopuses[y,x] != 0) nulCheck = false;
        }
    }
    if (nulCheck) break;
}

watch.Stop();
Console.WriteLine(flashes);
Console.WriteLine(steps);
Console.WriteLine($"Time is {watch.ElapsedMilliseconds} ms");

void Flash(int y, int x)
{
    int yMin, yMax, xMin, xMax;
    if (y == 0) yMin = y;
    else yMin = y - 1;
    if (y == 9) yMax = y;
    else yMax = y + 1;
    if(x > 0) xMin = x - 1;
    else xMin = x;
    if(x < 9) xMax = x + 1;
    else xMax = x;

    for (int i = yMin; i <= yMax; i++)
    {
        for (int j = xMin ; j <= xMax; j++)
        {
            if (flashed[i, j] != 1)
            {
                if (octopuses[i, j] < 9) octopuses[i, j]++;
                else
                {
                    octopuses[i, j] = 0;
                    flashed[i, j] = 1;
                    flashes++;
                    Flash(i, j);
                }
            }
        } 
    }
}
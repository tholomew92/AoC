// See https://aka.ms/new-console-template for more information
using System.Text;

var input = "1321131112";
long result = 0;
var watch = new System.Diagnostics.Stopwatch();
watch.Start();
result = LookAndSay(input, 40);
watch.Stop();
Console.WriteLine($"The result for part one is: {result}");
Console.WriteLine($"Time is {watch.ElapsedMilliseconds} ms");
watch.Reset();
watch.Start();
result = LookAndSay(input, 50);
watch.Stop();
Console.WriteLine($"The result for part one is: {result}");
Console.WriteLine($"Time is {watch.ElapsedMilliseconds} ms");
long LookAndSay(string input, int loop)
{
    for (int j = 0; j < loop; j++)
    {
        StringBuilder sb = new StringBuilder();
        int count = 0;
        for (int i = 0; i < input.Length; i++)
        {
            if (i < input.Length - 1)
            {
                if (input[i].Equals(input[i + 1]))
                {
                    count++;
                }
                else
                {
                    sb.Append(count + 1);
                    sb.Append(input[i]);

                    count = 0;
                }
            }
            else
            {
                sb.Append(count + 1);
                sb.Append(input[i]);
            }

        }
        input = sb.ToString();
    }
    return input.Length;
}

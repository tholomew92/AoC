// See https://aka.ms/new-console-template for more information

using System.Diagnostics;

var sw = new Stopwatch();
sw.Start();
var input = "cqjxjnds";
var forbidden = new char[] { 'i', 'l', 'o' };
string HandleText(string text)
{
    var lastChar = text[text.Length - 1];
    var texty = text[..^1];
    if (lastChar.Equals('z'))
    {
        lastChar = 'a';
        if(texty.Length > 1) texty = HandleText(texty);
    }
    else lastChar = (char)(lastChar + 1);

    return texty + lastChar;
}

bool CheckIfValid(string text)
{
    if (text.IndexOfAny(forbidden) != -1) return false;

    var tripletsCheck = false;
    for(int i = 0; i < text.Length - 2; i++)
    {
        if ((text[i].Equals((char)(text[i + 1] - 1))) && (text[i].Equals((char)(text[i + 2] - 2))))
        {
            tripletsCheck = true;
            break;
        }
    }

    if (!tripletsCheck) return false;

    int pair = 0;
    for(int i = 0; i < text.Length -1; i++)
    {
        if (text[i].Equals(text[i + 1]))
        {
            pair++;
            i++;
        }
        if (pair == 2) break;
    }
    if(pair < 2) return false;

    return true;
}

var check = false;
do
{
    input = HandleText(input);
    check = CheckIfValid(input);
}while( !check );

Console.WriteLine($"The result for part one is: {input}");

do
{
    input = HandleText(input);
    check = CheckIfValid(input);
} while (!check);
sw.Stop();
Console.WriteLine($"The result for part two is: {input}");
Console.WriteLine($"Time is {sw.ElapsedMilliseconds} ms");
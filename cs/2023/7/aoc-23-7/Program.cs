using System.Runtime.CompilerServices;

var watch = new System.Diagnostics.Stopwatch();
watch.Start();
var workDir = AppDomain.CurrentDomain.BaseDirectory;
var path = new DirectoryInfo(workDir).Parent.Parent.Parent.ToString();
var inputData = File.ReadAllLines(path + "\\input.txt").ToList();
var testData = File.ReadAllLines(path + "\\test.txt").ToList();

var input = inputData;

var partOne = 0;
var partTwo = 0;

var handList = new List<Hand>();
var dict1 = new Dictionary<Hand, int>();
var dict2 = new Dictionary<Hand, int>();

foreach (var line in input)
{
    var split = line.Split(' ');
    var hand = new Hand();
    hand.cards = split[0];
    hand.bet = int.Parse(split[1]);
    handList.Add(hand);
    dict1.Add(hand, 1);
    dict2.Add(hand, 1);
}

var parse = watch.Elapsed;

var order = "23456789TJQKA";

for (int i = 0; i < handList.Count-1; i++)
{
    var hand = handList[i];
    for(int j = i+1; j < handList.Count; j++)
    {
        var hand2 = handList[j];
        if (i == j) continue;
        var check = CompareHand(hand, hand2, 1);
        if (check == 1) dict1[hand]++;
        else dict1[hand2]++;
    }
}
foreach(var h in dict1)
{
    partOne += h.Key.bet * h.Value; 
}
var timeOne = watch.Elapsed - parse;

order = "J23456789TQKA";
for (int i = 0; i < handList.Count - 1; i++)
{
    var hand = handList[i];
    for (int j = i + 1; j < handList.Count; j++)
    {
        var hand2 = handList[j];
        if (i == j) continue;
        var check = CompareHand(hand, hand2, 2);
        if (check == 1) dict2[hand]++;
        else dict2[hand2]++;
    }
}

foreach (var h in dict2)
{
    partTwo += h.Key.bet * h.Value;
}

var timeTwo = watch.Elapsed - timeOne - parse;
watch.Stop();
Console.WriteLine($"The time for parsing input is: {FormattedTime(parse)}");
Console.WriteLine($"The result for part one is: {partOne}");
Console.WriteLine($"The time for part one is {FormattedTime(timeOne)}");
Console.WriteLine($"The result for part two is: {partTwo}");
Console.WriteLine($"The time for part two is {FormattedTime(timeTwo)}");
Console.WriteLine($"Total time is {FormattedTime(watch.Elapsed)}");

string FormattedTime(TimeSpan ts)
{
    var temp = ts.ToString("fffffff");
    var count = 0;
    foreach (var c in temp)
    {
        if (c.Equals('0')) count++;
        else break;
    }
    var formatted = temp.Substring(count);
    if (formatted.Length > 3)
    {
        formatted = formatted.Insert(formatted.Length - 3, ",");
        formatted += " ms";
    }
    else
    {
        formatted += " µs";
    }

    if(formatted.Length > 10) formatted = formatted.Insert(formatted.Length - 10, " ");

    return formatted;
}

int CompareHand(Hand h1, Hand h2, int version)
{
    var jc1 = h1.cards.Count(c => c == 'J');
    var jc2 = h2.cards.Count(c => c == 'J');

    var js1 = h1.cards;
    var js2 = h2.cards;

    if(version == 2)
    {
        js1 = h1.cards.Replace("J", "");
        js2 = h2.cards.Replace("J", "");

        js1 = String.Concat(js1.GroupBy(c => c).OrderByDescending(group => group.Count()).SelectMany(group => group));
        js2 = String.Concat(js2.GroupBy(c => c).OrderByDescending(group => group.Count()).SelectMany(group => group));

        if (String.IsNullOrEmpty(js1)) js1 = "JJJJJ";
        if (js1.Length < 5)
        {
            var newC = "";
            if (js1.Distinct().Count() == js1.Length)
            {
                var index = -1;
                foreach (var c in js1)
                {
                    if (order.IndexOf(c) > index) index = order.IndexOf(c);
                }
                newC = order[index].ToString();
                
            }
            else
            {
                newC = js1[0].ToString();
            }
            for (int i = js1.Length; i < 5; i++)
            {
                js1 += newC;
            }
        }
        
        if (String.IsNullOrEmpty(js2)) js2 = "JJJJJ";
        if (js2.Length < 5)
        {
            var newC = "";
            if (js2.Distinct().Count() == js2.Length)
            {
                var index = -1;
                foreach (var c in js2)
                {
                    if (order.IndexOf(c) > index) index = order.IndexOf(c);
                }
                newC = order[index].ToString();

            }
            else
            {
                newC = js2[0].ToString();
            }
            for (int i = js2.Length; i < 5; i++)
            {
                js2 += newC;
            }
        }
    }
    var hd1 = js1.Distinct().Count();
    var hd2 = js2.Distinct().Count();

    if (hd1 < hd2) return 1;
    else if(hd1 > hd2) return 0;

    var hs1 = String.Concat(js1.GroupBy(c => c).OrderByDescending(group => group.Count()).SelectMany(group => group));
    var hs2 = String.Concat(js2.GroupBy(c => c).OrderByDescending(group => group.Count()).SelectMany(group => group));

    var count1 = hs1.LastIndexOf(hs1[0]);
    var count2 = hs2.LastIndexOf(hs2[0]);

    if (count1 > count2) return 1;
    else if (count1 < count2)  return 0;

    for (int i = 0; i < 5; i++)
    {
        var c1 = order.IndexOf(h1.cards[i]);
        var c2 = order.IndexOf(h2.cards[i]);

        if (c1 > c2) return 1;
        else if (c1 < c2) return 0;
    }
    return 0;
}

class Hand
{
    public string cards { get; set; }
    public int bet { get; set; }

}


// See https://aka.ms/new-console-template for more information

using Newtonsoft.Json;
using System.Reflection.Metadata;

using (StreamReader r = new StreamReader("json.json"))
{
    string json = r.ReadToEnd();
    dynamic array = JsonConvert.DeserializeObject(json);
    foreach (var item in array)
    {
        Console.WriteLine(item);
        Console.WriteLine();
        break;
    }
}

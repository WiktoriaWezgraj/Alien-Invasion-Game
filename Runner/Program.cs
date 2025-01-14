using Simulator.Maps;
using System;
using System.Diagnostics;
using System.Runtime.Intrinsics;
using System.Text;
using System.Text.Json;

namespace Simulator;

public class Program
{
    static void Main(string[] args)
    {
        //domyslnie serializowane sa tylko publiczne property-dlatego przy point json jest pusty
        Point p1 = new(2, 4);
        string json = JsonSerializer.Serialize(p1);
        Console.WriteLine(json); // {}

        Point p2 = JsonSerializer.Deserialize<Point>(json);
        Console.WriteLine(p2);
    }
}


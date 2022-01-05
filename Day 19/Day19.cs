using System;

public class AdventOfCodeDay19
{

    public static void run()
    {
        string[] lines = System.IO.File.ReadAllLines("./Day 19/Problem1Input.txt");
        int beacons = Problem1(lines);
        Console.WriteLine("Day 19 - Problem 1: There are  " + beacons + " beacons");
    }

    private static int Problem1(string[] lines)
    {
        List<List<Beacon>> scanners = ParseInput(lines);
        while (scanners.Count > 1)
        {
            for (int i = 1; i < scanners.Count - 1; i++)
            {
                (bool success, List<Beacon>? newScanner) = TryCombineTwoScanners(scanners[0], scanners[i]);
                if (success && newScanner != null)
                {
                    scanners.Remove(scanners[i]);
                    scanners.Remove(scanners[0]);
                    scanners.Add(newScanner);
                    break;
                }
            }
        }
        return scanners[0].Count;
    }

    public static (bool, List<Beacon>?) TryCombineTwoScanners(List<Beacon> scanner1, List<Beacon> scanner2)
    {

        return (false, null);
    }

    public static List<List<Beacon>> ParseInput(string[] lines)
    {
        List<List<Beacon>> scanners = new List<List<Beacon>>();
        List<Beacon> scanner = new List<Beacon>();
        for (int i = 1; i < lines.Length - 1; i++)
        {
            if (lines[i].Contains("scanner"))
            {
                scanners.Add(scanner);
                scanner = new List<Beacon>();
            }
            else
            {
                string[] coords = lines[i].Split(',');
                scanner.Add(new Beacon(Convert.ToInt32(coords[0]), Convert.ToInt32(coords[1]), Convert.ToInt32(coords[2])));
            }
        }
        return scanners;
    }
}

public class Beacon
{
    int x;
    int y;
    int z;

    public Beacon(int X, int Y, int Z)
    {
        x = X;
        y = Y;
        z = Z;
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as Beacon);
    }

    public bool Equals(Beacon? other)
    {
        return other != null && other.x == x && other.y == y;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(x, y, z);
    }
}
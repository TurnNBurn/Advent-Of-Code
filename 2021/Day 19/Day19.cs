using System;
using System.IO;
using System.Threading.Tasks;

public class AdventOfCodeDay19
{

    public static void run()
    {
        string[] lines = System.IO.File.ReadAllLines("./2021/Day 19/Problem1Input.txt");
        int beacons = Problem1(lines);
        Console.WriteLine("Day 19 - Problem 1: There are " + beacons + " beacons");
    }

    private static int Problem1(string[] lines)
    {
        List<Scanner> scanners = ParseInput(lines);
        List<Scanner> normalizedScanners = new List<Scanner>();
        normalizedScanners.Add(scanners[0]);
        scanners.RemoveAt(0);
        while (scanners.Count > 0)
        {
            Console.WriteLine("Scanners remaining: " + scanners.Count);
            for (int i = 0; i < normalizedScanners.Count; i++)
            {
                for (int j = 0; j < scanners.Count; j++)
                {
                    if (CompareTwoScanners(normalizedScanners[i], scanners[j], scanners.Count))
                    {
                        normalizedScanners.Add(scanners[j]);
                        scanners.Remove(scanners[j]);
                        break;
                    }
                }
            }
        }
        foreach (Scanner scanner in normalizedScanners)
        {
            Console.WriteLine("Scanner at " + scanner.x + "," + scanner.y + "," + scanner.z);
        }
        return CountBeacons(normalizedScanners);
    }

    public static int CountBeacons(List<Scanner> scanners)
    {
        List<Beacon> deduplicatedScanner = new List<Beacon>();
        foreach (Scanner scanner in scanners)
        {
            foreach (Beacon beacon in scanner.beacons)
            {
                if (!deduplicatedScanner.Contains(beacon))
                {
                    deduplicatedScanner.Add(beacon);
                }
            }
        }
        return deduplicatedScanner.Count;
    }

    public static void PrintOneScanner(Scanner scanner)
    {
        StreamWriter writer = new StreamWriter("./Day 19/Problem1Output.txt");
        writer.WriteLine("Beacons");
        foreach (Beacon beacon in scanner.beacons)
        {
            string text = beacon.x + "," + beacon.y + "," + beacon.z;
            writer.WriteLine(text);
        }
        writer.WriteLine("Pairs");
        foreach (Pair pair in scanner.pairs)
        {
            string text = "Beacon " + pair.beacon1.x + "," + pair.beacon1.y + "," + pair.beacon1.z + " and beacon " + pair.beacon2.x + "," + pair.beacon2.y + "," + pair.beacon2.z + " at distance " + pair.distance.x + "," + pair.distance.y + "," + pair.distance.z;
            writer.WriteLine(text);
        }
    }

    public static List<Pair> PopulateBeaconDistances(List<Beacon> scanner)
    {
        List<Pair> newScanner = new List<Pair>();
        for (int i = 0; i < scanner.Count; i++)
        {
            for (int j = i + 1; j < scanner.Count; j++)
            {
                newScanner.Add(new Pair(scanner[i], scanner[j], ComputeDistance(scanner[i], scanner[j])));
            }
        }
        return newScanner;
    }

    public static Distance ComputeDistance(Beacon beacon1, Beacon beacon2)
    {
        return new Distance(
            Math.Abs(Math.Max(beacon1.x, beacon2.x) - Math.Min(beacon1.x, beacon2.x)),
            Math.Abs(Math.Max(beacon1.y, beacon2.y) - Math.Min(beacon1.y, beacon2.y)),
            Math.Abs(Math.Max(beacon1.z, beacon2.z) - Math.Min(beacon1.z, beacon2.z)));
    }

    public static bool CompareTwoScanners(Scanner scanner1, Scanner scanner2, int scannerCount)
    {
        for (int i = 0; i < 6; i++)
        {
            (bool match, List<(Pair, Pair)>? matchingSet) result = CompareScanners(scanner1, scanner2, i);
            if (result.match && result.matchingSet != null)
            {
                if (NormalizeScanner(scanner1, scanner2, i, result.matchingSet))
                {
                    return true;
                }
            }
        }
        return false;
    }

    public static bool NormalizeScanner(Scanner scanner1, Scanner scanner2, int rotate, List<(Pair, Pair)> matchingSet)
    {
        scanner2.Rotate(rotate);
        (bool foundMatch, Beacon s1Beacon, Beacon s2Beacon) matchingPair = IdentifyMatchingBeacons(matchingSet);
        if (matchingPair.foundMatch)
        {
            for (int i = 0; i < matchingSet.Count; i++)
            {
                if (matchingSet[i].Item1.Contains(matchingPair.s1Beacon) && matchingSet[i].Item2.Contains(matchingPair.s2Beacon))
                {
                    if (matchingSet[i].Item1.beacon1.Equals(matchingPair.s1Beacon))
                    {
                        if (matchingSet[i].Item2.beacon1.Equals(matchingPair.s2Beacon))
                        {
                            FlipAndTranslate(scanner2, matchingSet[i].Item1.beacon1, matchingSet[i].Item1.beacon2, matchingSet[i].Item2.beacon1, matchingSet[i].Item2.beacon2);
                            return true;
                        }
                        else
                        {
                            FlipAndTranslate(scanner2, matchingSet[i].Item1.beacon1, matchingSet[i].Item1.beacon2, matchingSet[i].Item2.beacon2, matchingSet[i].Item2.beacon1);
                            return true;
                        }
                    }
                    else
                    {
                        if (matchingSet[i].Item2.beacon1.Equals(matchingPair.s2Beacon))
                        {
                            FlipAndTranslate(scanner2, matchingSet[i].Item1.beacon2, matchingSet[i].Item1.beacon1, matchingSet[i].Item2.beacon1, matchingSet[i].Item2.beacon2);
                            return true;
                        }
                        else
                        {
                            FlipAndTranslate(scanner2, matchingSet[i].Item1.beacon2, matchingSet[i].Item1.beacon1, matchingSet[i].Item2.beacon2, matchingSet[i].Item2.beacon1);
                            return true;
                        }
                    }
                }
            }
        }
        Console.WriteLine("Failed out");
        return false;
    }

    public static void FlipAndTranslate(Scanner scanner, Beacon s1Beacon1, Beacon s1Beacon2, Beacon s2Beacon1, Beacon s2Beacon2)
    {
        if (s1Beacon1.x - s1Beacon2.x != s2Beacon1.x - s2Beacon2.x)
        {
            s2Beacon1.x = s2Beacon1.x * -1;
            scanner.FlipX();
        }
        if (s1Beacon1.y - s1Beacon2.y != s2Beacon1.y - s2Beacon2.y)
        {
            s2Beacon1.y = s2Beacon1.y * -1;
            scanner.FlipY();
        }
        if (s1Beacon1.z - s1Beacon2.z != s2Beacon1.z - s2Beacon2.z)
        {
            s2Beacon1.z = s2Beacon1.z * -1;
            scanner.FlipZ();
        }

        scanner.x = s1Beacon1.x - s2Beacon1.x;
        scanner.y = s1Beacon1.y - s2Beacon1.y;
        scanner.z = s1Beacon1.z - s2Beacon1.z;
        scanner.Translate();
    }

    //This function searches for an instance within matchingSet where there exist two entries such that
    //there is a beacon s1Beacon which exists in Item1 of both entries, and there is a beacon s2Beacon which
    //exists in Item2 of both entries.
    //If two such entries exist, then we assume s1Beacon represents the same beacon as s2Beacon, and we return a Tuple indicating "true" that 
    //we found such beacons, as well as the beacons themselves
    //Not the most elegant structure but here we are.
    public static (bool, Beacon, Beacon) IdentifyMatchingBeacons(List<(Pair, Pair)> matchingSet)
    {
        Beacon s1Beacon = matchingSet[0].Item1.beacon1;
        Beacon s2Beacon1 = matchingSet[0].Item2.beacon1;
        Beacon s2Beacon2 = matchingSet[0].Item2.beacon2;
        //It's possible that the first entry in matchingSet contains beacons that we don't see again in later entries.
        //So we potentially need to compare every entry with every other entry (though we will likely find a matching entry sooner and short circuit)
        for (int j = 0; j < matchingSet.Count; j++)
        {
            s1Beacon = matchingSet[j].Item1.beacon1;
            s2Beacon1 = matchingSet[j].Item2.beacon1;
            s2Beacon2 = matchingSet[j].Item2.beacon2;
            for (int i = 1; i < matchingSet.Count; i++)
            {
                if (matchingSet[i].Item1.Contains(s1Beacon))
                {
                    if (matchingSet[i].Item2.Contains(s2Beacon1))
                    {
                        return (true, s1Beacon, s2Beacon1);
                    }
                    if (matchingSet[i].Item2.Contains(s2Beacon2))
                    {
                        return (true, s1Beacon, s2Beacon2);
                    }
                }
            }
        }
        return (false, s1Beacon, s2Beacon1);
    }

    public static (bool, List<(Pair, Pair)>?) CompareScanners(Scanner scanner1, Scanner scanner2, int rotate)
    {
        int matchCount = 0;
        List<(Pair, Pair)> matchingSet = new List<(Pair, Pair)>();
        for (int i = 0; i < scanner1.pairs.Count; i++)
        {
            for (int j = 0; j < scanner2.pairs.Count; j++)
            {
                if (scanner1.pairs[i].distance.Equals(RotateDistance(scanner2.pairs[j].distance, rotate)))
                {
                    matchCount++;
                    matchingSet.Add((scanner1.pairs[i], RotatePair(scanner2.pairs[j], rotate)));
                }
                if (matchCount >= 66)
                {
                    return (true, matchingSet);
                }
            }
        }
        return (false, null);
    }

    public static Distance RotateDistance(Distance distance, int rotate)
    {
        switch (rotate)
        {
            case 1:
                return new Distance(distance.x, distance.z, distance.y);
            case 2:
                return new Distance(distance.y, distance.x, distance.z);
            case 3:
                return new Distance(distance.y, distance.z, distance.x);
            case 4:
                return new Distance(distance.z, distance.x, distance.y);
            case 5:
                return new Distance(distance.z, distance.y, distance.x);
            default:
                return new Distance(distance.x, distance.y, distance.z);
        }
    }

    public static Beacon RotateBeacon(Beacon beacon, int rotate)
    {
        switch (rotate)
        {
            case 1:
                return new Beacon(beacon.x, beacon.z, beacon.y);
            case 2:
                return new Beacon(beacon.y, beacon.x, beacon.z);
            case 3:
                return new Beacon(beacon.y, beacon.z, beacon.x);
            case 4:
                return new Beacon(beacon.z, beacon.x, beacon.y);
            case 5:
                return new Beacon(beacon.z, beacon.y, beacon.x);
            default:
                return new Beacon(beacon.x, beacon.y, beacon.z);
        }
    }

    public static Pair RotatePair(Pair pair, int rotate)
    {
        return new Pair(RotateBeacon(pair.beacon1, rotate), RotateBeacon(pair.beacon2, rotate), RotateDistance(pair.distance, rotate));
    }

    public static List<Scanner> ParseInput(string[] lines)
    {
        List<Scanner> scanners = new List<Scanner>();
        List<Beacon> scanner = new List<Beacon>();
        for (int i = 1; i < lines.Length; i++)
        {
            if (lines[i].Contains("scanner"))
            {
                scanners.Add(new Scanner(scanner, PopulateBeaconDistances(scanner)));
                scanner = new List<Beacon>();
            }
            else if (lines[i].Contains(','))
            {
                string[] coords = lines[i].Split(',');
                scanner.Add(new Beacon(Convert.ToInt32(coords[0]), Convert.ToInt32(coords[1]), Convert.ToInt32(coords[2])));
            }
        }
        scanners.Add(new Scanner(scanner, PopulateBeaconDistances(scanner)));
        return scanners;
    }
}

public class Vector
{
    public int x;
    public int y;
    public int z;

    public override bool Equals(object? obj)
    {
        return Equals(obj as Vector);
    }

    public bool Equals(Vector? other)
    {
        return other != null && other.x == x && other.y == y && other.z == z;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(x, y, z);
    }

    public void FlipX()
    {
        x = x * -1;
    }

    public void FlipY()
    {
        y = y * -1;
    }

    public void FlipZ()
    {
        z = z * -1;
    }

    public void Rotate(int rotate)
    {
        int oldX = x;
        int oldY = y;
        int oldZ = z;
        switch (rotate)
        {
            case 1:
                y = oldZ;
                z = oldY;
                break;
            case 2:
                x = oldY;
                y = oldX;
                break;
            case 3:
                x = oldY;
                y = oldZ;
                z = oldX;
                break;
            case 4:
                x = oldZ;
                z = oldX;
                break;
            case 5:
                x = oldZ;
                y = oldX;
                z = oldY;
                break;
            default:
                break;
        }
    }
}

public class Beacon : Vector
{
    public Beacon(int X, int Y, int Z)
    {
        x = X;
        y = Y;
        z = Z;
    }
}

public class Distance : Vector
{
    public Distance(int X, int Y, int Z)
    {
        x = X;
        y = Y;
        z = Z;
    }
}

public class Pair
{
    public Beacon beacon1;
    public Beacon beacon2;
    public Distance distance;

    public Pair(Beacon Beacon1, Beacon Beacon2, Distance dist)
    {
        beacon1 = Beacon1;
        beacon2 = Beacon2;
        distance = dist;
    }

    public bool Contains(Beacon beacon)
    {
        if (beacon1.Equals(beacon))
        {
            return true;
        }
        if (beacon2.Equals(beacon))
        {
            return true;
        }
        return false;
    }
}

public class Scanner
{
    public int x;
    public int y;
    public int z;
    public List<Beacon> beacons;
    public List<Pair> pairs;
    public Scanner(List<Beacon> Beacons, List<Pair> Pairs)
    {
        beacons = Beacons;
        pairs = Pairs;
        x = 0;
        y = 0;
        z = 0;
    }

    public void Rotate(int rotate)
    {
        foreach (Beacon beacon in beacons)
        {
            beacon.Rotate(rotate);
        }
        foreach (Pair pair in pairs)
        {
            //Don't need to rotate the beacons in each pair because pairs references
            //the same objects as the beacons property
            pair.distance.Rotate(rotate);
        }
    }

    public void Translate()
    {
        foreach (Beacon beacon in beacons)
        {
            beacon.x = beacon.x + x;
            beacon.y = beacon.y + y;
            beacon.z = beacon.z + z;
        }
    }

    public void FlipX()
    {
        foreach (Beacon beacon in beacons)
        {
            beacon.FlipX();
        }
    }

    public void FlipY()
    {
        foreach (Beacon beacon in beacons)
        {
            beacon.FlipY();
        }
    }

    public void FlipZ()
    {
        foreach (Beacon beacon in beacons)
        {
            beacon.FlipZ();
        }
    }
}
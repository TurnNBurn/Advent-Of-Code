using System;
using System.IO;
using System.Threading.Tasks;

public class AdventOfCodeDay19
{

    public static void run()
    {
        string[] lines = System.IO.File.ReadAllLines("./Day 19/Problem1Input.txt");
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
            // Math.Abs(Math.Max(beacon1.x, beacon2.x) - Math.Min(beacon1.x, beacon2.x)),
            // Math.Abs(Math.Max(beacon1.y, beacon2.y) - Math.Min(beacon1.y, beacon2.y)),
            // Math.Abs(Math.Max(beacon1.z, beacon2.z) - Math.Min(beacon1.z, beacon2.z)));
            Math.Abs(beacon1.x - beacon2.x),
            Math.Abs(beacon1.y - beacon2.y),
            Math.Abs(beacon1.z - beacon2.z));
    }

    public static bool CompareTwoScanners(Scanner scanner1, Scanner scanner2, int scannerCount)
    {
        for (int i = 0; i < 6; i++)
        {
            (bool match, List<(Pair, Pair)>? matchingSet) result = CompareScanners(scanner1, scanner2, i);
            if (result.match && result.matchingSet != null)
            {
                if (NormalizeScanner2(scanner1, scanner2, i, result.matchingSet))
                {
                    return true;
                }
            }
        }
        return false;
    }

    public static void NormalizeScanner(Scanner scanner, int rotate, List<(Pair, Pair)> matchingSet)
    {
        Console.WriteLine("Matching pair (A,B) (" + matchingSet[0].Item1.beacon1.x + "," + matchingSet[0].Item1.beacon1.y + "," + matchingSet[0].Item1.beacon1.z + "),(" + matchingSet[0].Item1.beacon2.x + "," + matchingSet[0].Item1.beacon2.y + "," + matchingSet[0].Item1.beacon2.z + ")");
        Console.WriteLine("With pair (C,D) (" + matchingSet[0].Item2.beacon1.x + "," + matchingSet[0].Item2.beacon1.y + "," + matchingSet[0].Item2.beacon1.z + "),(" + matchingSet[0].Item2.beacon2.x + "," + matchingSet[0].Item2.beacon2.y + "," + matchingSet[0].Item2.beacon2.z + ")");

        scanner.Rotate(rotate);

        if (IsXFlipped(matchingSet))
        {
            scanner.FlipX();
            foreach ((Pair, Pair) set in matchingSet)
            {
                set.Item2.FlipX();
            }
        }
        if (IsYFlipped(matchingSet))
        {
            scanner.FlipY();
            foreach ((Pair, Pair) set in matchingSet)
            {
                set.Item2.FlipY();
            }
        }
        if (IsZFlipped(matchingSet))
        {
            scanner.FlipZ();
            foreach ((Pair, Pair) set in matchingSet)
            {
                set.Item2.FlipZ();
            }
        }

        scanner.x = XOffset(matchingSet);
        scanner.y = YOffset(matchingSet);
        scanner.z = ZOffset(matchingSet);

        scanner.Translate();

        Console.WriteLine("Resulting pair (C,D) (" + matchingSet[0].Item2.beacon1.x + "," + matchingSet[0].Item2.beacon1.y + "," + matchingSet[0].Item2.beacon1.z + "),(" + matchingSet[0].Item2.beacon2.x + "," + matchingSet[0].Item2.beacon2.y + "," + matchingSet[0].Item2.beacon2.z + ")");
    }

    public static bool NormalizeScanner2(Scanner scanner1, Scanner scanner2, int rotate, List<(Pair, Pair)> matchingSet)
    {
        scanner2.Rotate(rotate);
        (bool foundMatch, Beacon s1Beacon, Beacon s2Beacon) matchingPair = IdentifyMatchingBeacons(matchingSet);
        if (!matchingPair.foundMatch)
        {
            return false;
        }
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
        // for (int i = -1; i < 2; i += 2)
        // {
        //     for (int j = -1; j < 2; j += 2)
        //     {
        //         for (int k = -1; k < 2; k += 2)
        //         {
        //             Scanner testScanner = new Scanner(scanner2);
        //             if (matchingSet[0].Item1.beacon1.x - matchingSet[0].Item2.beacon1.x == matchingSet[1].Item1.beacon1.x - matchingSet[1].Item2.beacon1.x)
        //             {
        //                 if (TranslateAndCompare(scanner1, testScanner, matchingSet[0].Item1.beacon1, matchingSet[0].Item2.beacon1, i, j, k))
        //                 {
        //                     UpdateScanner(scanner2, testScanner, i == -1, j == -1, k == -1);
        //                     return true;
        //                 }
        //             }
        //             else
        //             {
        //                 if (TranslateAndCompare(scanner1, testScanner, matchingSet[0].Item1.beacon1, matchingSet[0].Item2.beacon2, i, j, k))
        //                 {
        //                     UpdateScanner(scanner2, testScanner, i == -1, j == -1, k == -1);
        //                     return true;
        //                 }
        //             }
        //         }
        //     }
        // }
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

    public static (bool, Beacon, Beacon) IdentifyMatchingBeacons(List<(Pair, Pair)> matchingSet)
    {
        Beacon s1Beacon = matchingSet[0].Item1.beacon1;
        Beacon s2Beacon1 = matchingSet[0].Item2.beacon1;
        Beacon s2Beacon2 = matchingSet[0].Item2.beacon2;
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

    public static void UpdateScanner(Scanner scanner, Scanner testScanner, bool flipX, bool flipY, bool flipZ)
    {
        scanner.x = testScanner.x;
        scanner.y = testScanner.y;
        scanner.z = testScanner.z;
        if (flipX)
        {
            scanner.FlipX();
        }
        if (flipY)
        {
            scanner.FlipY();
        }
        if (flipZ)
        {
            scanner.FlipZ();
        }
        scanner.Translate();
        Console.WriteLine("Test Scanner at " + scanner.x + "," + scanner.y + "," + scanner.z);
    }

    public static bool TranslateAndCompare(Scanner scanner, Scanner testScanner, Beacon normalizedBeacon, Beacon translatedBeacon, int flipX, int flipY, int flipZ)
    {
        testScanner.x = normalizedBeacon.x - (translatedBeacon.x * flipX);
        testScanner.y = normalizedBeacon.y - (translatedBeacon.y * flipY);
        testScanner.z = normalizedBeacon.z - (translatedBeacon.z * flipZ);
        if (flipX == -1)
        {
            testScanner.FlipX();
        }
        if (flipY == -1)
        {
            testScanner.FlipY();
        }
        if (flipZ == -1)
        {
            testScanner.FlipZ();
        }
        testScanner.Translate();

        int matchCount = 0;
        foreach (Beacon beacon in testScanner.beacons)
        {
            if (scanner.beacons.Contains(beacon))
            {
                matchCount++;
            }
        }
        return matchCount >= 12;
    }

    public static int XOffset(List<(Pair pair1, Pair pair2)> matchingSet)
    {
        Console.WriteLine("XOFfset calculated using: " + (matchingSet[0].pair1.beacon1.x - matchingSet[0].pair2.beacon1.x) + " and " + (matchingSet[1].pair1.beacon1.x - matchingSet[1].pair2.beacon1.x));
        Console.WriteLine("Our options at this point are: " + (matchingSet[0].pair1.beacon1.x - matchingSet[0].pair2.beacon1.x) + " or " + (matchingSet[0].pair1.beacon1.x - matchingSet[0].pair2.beacon2.x));
        if (matchingSet[0].pair1.beacon1.x - matchingSet[0].pair2.beacon1.x == matchingSet[1].pair1.beacon1.x - matchingSet[1].pair2.beacon1.x)
        {
            return matchingSet[0].pair1.beacon1.x - matchingSet[0].pair2.beacon1.x;
        }
        else
        {
            return matchingSet[0].pair1.beacon1.x - matchingSet[0].pair2.beacon2.x;
        }
    }

    public static int YOffset(List<(Pair pair1, Pair pair2)> matchingSet)
    {
        Console.WriteLine("YOFfset calculated using: " + (matchingSet[0].pair1.beacon1.y - matchingSet[0].pair2.beacon1.y) + " and " + (matchingSet[1].pair1.beacon1.y - matchingSet[1].pair2.beacon1.y));
        Console.WriteLine("Our options at this point are: " + (matchingSet[0].pair1.beacon1.y - matchingSet[0].pair2.beacon1.y) + " or " + (matchingSet[0].pair1.beacon1.y - matchingSet[0].pair2.beacon2.y));
        if (matchingSet[0].pair1.beacon1.y - matchingSet[0].pair2.beacon1.y == matchingSet[1].pair1.beacon1.y - matchingSet[1].pair2.beacon1.y)
        {
            return matchingSet[0].pair1.beacon1.y - matchingSet[0].pair2.beacon1.y;
        }
        else
        {
            return matchingSet[0].pair1.beacon1.y - matchingSet[0].pair2.beacon2.y;
        }
    }

    public static int ZOffset(List<(Pair pair1, Pair pair2)> matchingSet)
    {
        Console.WriteLine("ZOFfset calculated using: " + (matchingSet[0].pair1.beacon1.z - matchingSet[0].pair2.beacon1.z) + " and " + (matchingSet[1].pair1.beacon1.z - matchingSet[1].pair2.beacon1.z));
        Console.WriteLine("Our options at this point are: " + (matchingSet[0].pair1.beacon1.z - matchingSet[0].pair2.beacon1.z) + " or " + (matchingSet[0].pair1.beacon1.z - matchingSet[0].pair2.beacon2.z));
        if (matchingSet[0].pair1.beacon1.z - matchingSet[0].pair2.beacon1.z == matchingSet[1].pair1.beacon1.z - matchingSet[1].pair2.beacon1.z)
        {
            return matchingSet[0].pair1.beacon1.z - matchingSet[0].pair2.beacon1.z;
        }
        else
        {
            return matchingSet[0].pair1.beacon1.z - matchingSet[0].pair2.beacon2.z;
        }
    }

    public static bool IsXFlipped(List<(Pair pair1, Pair pair2)> matchingSet)
    {
        if (matchingSet[0].pair1.beacon1.x - matchingSet[0].pair2.beacon1.x == matchingSet[1].pair1.beacon1.x - matchingSet[1].pair2.beacon1.x)
        {
            return matchingSet[0].pair1.beacon1.x - matchingSet[0].pair1.beacon2.x == -1 * (matchingSet[0].pair2.beacon1.x - matchingSet[0].pair2.beacon2.x);
        }
        else
        {
            return matchingSet[0].pair1.beacon1.x - matchingSet[0].pair1.beacon2.x == -1 * (matchingSet[0].pair2.beacon2.x - matchingSet[0].pair2.beacon1.x);
        }
    }

    public static bool IsYFlipped(List<(Pair pair1, Pair pair2)> matchingSet)
    {
        if (matchingSet[0].pair1.beacon1.y - matchingSet[0].pair2.beacon1.y == matchingSet[1].pair1.beacon1.y - matchingSet[1].pair2.beacon1.y)
        {
            return matchingSet[0].pair1.beacon1.y - matchingSet[0].pair1.beacon2.y == -1 * (matchingSet[0].pair2.beacon1.y - matchingSet[0].pair2.beacon2.y);
        }
        else
        {
            return matchingSet[0].pair1.beacon1.y - matchingSet[0].pair1.beacon2.y == -1 * (matchingSet[0].pair2.beacon2.y - matchingSet[0].pair2.beacon1.y);
        }
    }

    public static bool IsZFlipped(List<(Pair pair1, Pair pair2)> matchingSet)
    {
        if (matchingSet[0].pair1.beacon1.z - matchingSet[0].pair2.beacon1.z == matchingSet[1].pair1.beacon1.z - matchingSet[1].pair2.beacon1.z)
        {
            return matchingSet[0].pair1.beacon1.z - matchingSet[0].pair1.beacon2.z == -1 * (matchingSet[0].pair2.beacon1.z - matchingSet[0].pair2.beacon2.z);
        }
        else
        {
            return matchingSet[0].pair1.beacon1.z - matchingSet[0].pair1.beacon2.z == -1 * (matchingSet[0].pair2.beacon2.z - matchingSet[0].pair2.beacon1.z);
        }
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

    public void FlipX()
    {
        beacon1.FlipX();
        beacon2.FlipX();
    }

    public void FlipY()
    {
        beacon1.FlipY();
        beacon2.FlipY();
    }

    public void FlipZ()
    {
        beacon1.FlipZ();
        beacon2.FlipZ();
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

    public Scanner(Scanner scanner)
    {
        beacons = new List<Beacon>();
        pairs = new List<Pair>();
        foreach (Beacon beacon in scanner.beacons)
        {
            beacons.Add(new Beacon(beacon.x, beacon.y, beacon.z));
        }
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
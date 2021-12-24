using System;

public class AdventOfCodeDay22
{

    public static void run()
    {
        string[] lines = System.IO.File.ReadAllLines("./Day 22/Problem1Input.txt");
        int cubesOn = Problem1(lines);
        int cubesOnEntireZone = Problem2(lines);
        Console.WriteLine("Day 22 - Problem 1: There are " + cubesOn + " cubes on");
        Console.WriteLine("Day 22 - Problem 2: There are " + cubesOn + " cubes on");
    }

    private static int Problem1(string[] lines)
    {
        Dictionary<Coordinate, int> cubesOn = new Dictionary<Coordinate, int>();
        foreach (string line in lines)
        {
            string[] input = line.Split(' ');
            Coordinate low = GetLowerRange(input[1]);
            Coordinate high = GetUpperRange(input[1]);

            if (input[0].Equals("on"))
            {
                TurnCubesOn(low, high, cubesOn);
            }
            else
            {
                TurnCubesOff(low, high, cubesOn);
            }
        }
        return cubesOn.Count;
    }

    private static int Problem2(string[] lines)
    {
        Dictionary<Coordinate, int> cubesOn = new Dictionary<Coordinate, int>();
        foreach (string line in lines)
        {
            string[] input = line.Split(' ');
            Coordinate low = GetLowerRange(input[1]);
            Coordinate high = GetUpperRange(input[1]);


        }
        return cubesOn.Count;
    }

    private static void TurnCubesOn(Coordinate low, Coordinate high, Dictionary<Coordinate, int> cubesOn)
    {

        if (low.x > 50 || high.x < -50)
        {
            return;
        }
        if (low.y > 50 || high.x < -50)
        {
            return;
        }
        if (low.z > 50 || high.z < -50)
        {
            return;
        }
        low.x = low.x > -50 ? low.x : -50;
        low.y = low.y > -50 ? low.y : -50;
        low.z = low.z > -50 ? low.z : -50;
        high.x = high.x < 50 ? high.x : 50;
        high.y = high.y < 50 ? high.y : 50;
        high.z = high.z < 50 ? high.z : 50;

        for (int i = low.x; i <= high.x; i++)
        {
            for (int j = low.y; j <= high.y; j++)
            {
                for (int k = low.z; k <= high.z; k++)
                {
                    Coordinate candidate = new Coordinate(i, j, k);
                    if (!cubesOn.ContainsKey(candidate))
                    {
                        cubesOn.Add(candidate, 1);
                        //Console.WriteLine("Turn on cube " + candidate.x + ", " + candidate.y + ", " + candidate.z);
                    }
                }
            }
        }
    }

    private static void TurnCubesOff(Coordinate low, Coordinate high, Dictionary<Coordinate, int> cubesOn)
    {

        if (low.x > 50 || high.x < -50)
        {
            return;
        }
        if (low.y > 50 || high.x < -50)
        {
            return;
        }
        if (low.z > 50 || high.z < -50)
        {
            return;
        }
        low.x = low.x > -50 ? low.x : -50;
        low.y = low.y > -50 ? low.y : -50;
        low.z = low.z > -50 ? low.z : -50;
        high.x = high.x < 50 ? high.x : 50;
        high.y = high.y < 50 ? high.y : 50;
        high.z = high.z < 50 ? high.z : 50;

        for (int i = low.x; i <= high.x; i++)
        {
            for (int j = low.y; j <= high.y; j++)
            {
                for (int k = low.z; k <= high.z; k++)
                {
                    Coordinate candidate = new Coordinate(i, j, k);
                    if (cubesOn.ContainsKey(candidate))
                    {
                        cubesOn.Remove(candidate);
                        //Console.WriteLine("Turn off cube " + candidate.x + ", " + candidate.y + ", " + candidate.z);
                    }
                }
            }
        }
    }

    private static Coordinate GetLowerRange(string line)
    {
        int x = Convert.ToInt32(line.Split(',')[0].Split('.')[0].Split('=')[1]);
        int y = Convert.ToInt32(line.Split(',')[1].Split('.')[0].Split('=')[1]);
        int z = Convert.ToInt32(line.Split(',')[2].Split('.')[0].Split('=')[1]);
        return new Coordinate(x, y, z);
    }

    private static Coordinate GetUpperRange(string line)
    {
        int x = Convert.ToInt32(line.Split(',')[0].Substring(line.Split(',')[0].IndexOf('.') + 2));
        int y = Convert.ToInt32(line.Split(',')[1].Substring(line.Split(',')[1].IndexOf('.') + 2));
        int z = Convert.ToInt32(line.Split(',')[2].Substring(line.Split(',')[2].IndexOf('.') + 2));
        return new Coordinate(x, y, z);
    }
}

public class Coordinate
{
    public int x;
    public int y;
    public int z;
    public Coordinate(int X, int Y, int Z)
    {
        x = X;
        y = Y;
        z = Z;
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as Coordinate);
    }

    public bool Equals(Coordinate? other)
    {
        return other != null && other.x == x && other.y == y && other.z == z;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(x, y, z);
    }
}
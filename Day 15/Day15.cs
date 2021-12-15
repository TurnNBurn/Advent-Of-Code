using System;

public class AdventOfCodeDay15
{

    public static void run()
    {
        string[] lines = System.IO.File.ReadAllLines("./Day 15/Problem1Input.txt");
        int totalRisk = Problem1(lines);
        int riskOfFullCave = Problem2(lines);
        Console.WriteLine("Day 15 - Problem 1: The lowest total risk out of the cave is " + totalRisk);
        Console.WriteLine("Day 15 - Problem 2: The total risk of traversing the entire cave is " + riskOfFullCave);
    }

    private static int Problem1(string[] lines)
    {
        int[,] caveMap = BuildCaveMap(lines);
        int[,] distanceMap = new int[caveMap.GetLength(0), caveMap.GetLength(1)];
        if (caveMap[0, 1] <= caveMap[1, 0])
        {
            MoveOne(caveMap, distanceMap, 0, new Coordinates(0, 1));
            MoveOne(caveMap, distanceMap, 0, new Coordinates(1, 0));
        }
        else
        {
            MoveOne(caveMap, distanceMap, 0, new Coordinates(1, 0));
            MoveOne(caveMap, distanceMap, 0, new Coordinates(0, 1));
        }
        return distanceMap[distanceMap.GetLength(0) - 1, distanceMap.GetLength(1) - 1];
    }

    private static int Problem2(string[] lines)
    {
        int[,] caveMap = BuildCaveMap(lines);
        int[,] distanceMap = new int[caveMap.GetLength(0), caveMap.GetLength(1)];
        if (caveMap[0, 1] <= caveMap[1, 0])
        {
            MoveOne(caveMap, distanceMap, 0, new Coordinates(0, 1));
            MoveOne(caveMap, distanceMap, 0, new Coordinates(1, 0));
        }
        else
        {
            MoveOne(caveMap, distanceMap, 0, new Coordinates(1, 0));
            MoveOne(caveMap, distanceMap, 0, new Coordinates(0, 1));
        }
        return distanceMap[distanceMap.GetLength(0) - 1, distanceMap.GetLength(1) - 1];
    }

    private static void MoveOne(int[,] caveMap, int[,] distanceMap, int distance, Coordinates spot)
    {
        int currentRisk = caveMap[spot.x, spot.y];
        //Console.WriteLine(spot.x + " " + spot.y + " distance " + distance);
        distance = distance + currentRisk;
        if (distanceMap[spot.x, spot.y] != 0)
        {
            if (distanceMap[spot.x, spot.y] < distance)
            {
                return;
            }
        }
        //We found the shortest distance here so far, record that
        distanceMap[spot.x, spot.y] = distance;

        //Check if we're at the destination
        int caveLength = caveMap.GetLength(0) - 1;
        int caveWidth = caveMap.GetLength(1) - 1;
        if (spot.x == caveLength && spot.y == caveWidth)
        {
            return;
        }

        bool canGoRight = spot.x < caveLength;
        bool canGoDown = spot.y < caveWidth;
        //Search neighbors
        if (canGoDown)
        {
            Coordinates downNeighbor = new Coordinates(spot.x, spot.y + 1);
            if (canGoRight)
            {
                Coordinates rightNeighbor = new Coordinates(spot.x + 1, spot.y);
                if (caveMap[spot.x + 1, spot.y] <= caveMap[spot.x, spot.y + 1])
                {
                    MoveOne(caveMap, distanceMap, distance, rightNeighbor);
                    MoveOne(caveMap, distanceMap, distance, downNeighbor);
                }
                else
                {
                    MoveOne(caveMap, distanceMap, distance, downNeighbor);
                    MoveOne(caveMap, distanceMap, distance, rightNeighbor);
                }
            }
            else
            {
                MoveOne(caveMap, distanceMap, distance, downNeighbor);
            }
        }
        else if (canGoRight)
        {
            MoveOne(caveMap, distanceMap, distance, new Coordinates(spot.x + 1, spot.y));
        }
    }

    private static int[,] BuildCaveMap(string[] lines)
    {
        int[,] caveMap = new int[lines[0].Length, lines.Length];
        for (int i = 0; i < lines.Length; i++)
        {
            char[] line = lines[i].ToCharArray();
            for (int j = 0; j < line.Length; j++)
            {
                caveMap[i, j] = Convert.ToInt32(line[j].ToString());
            }
        }
        return caveMap;
    }
}

public class Coordinates
{
    public Coordinates(int X, int Y)
    {
        x = X;
        y = Y;
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as Coordinates);
    }

    public bool Equals(Coordinates? other)
    {
        return other != null && other.x == x && other.y == y;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(x, y);
    }
    public int x;
    public int y;
}
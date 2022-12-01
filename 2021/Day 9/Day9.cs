using System;

public class AdventOfCodeDay9
{

    public static void run()
    {
        string[] lines = System.IO.File.ReadAllLines("./2021/Day 9/Problem1Input.txt");
        int sumOfLowSpots = Problem1(lines);
        int areaOfLargestBasins = Problem2(lines);
        Console.WriteLine("Day 9 - Problem 1: The sum of the low spots is " + sumOfLowSpots);
        Console.WriteLine("Day 9 - Problem 2: The area of the largest basins is: " + areaOfLargestBasins);
    }

    private static int Problem1(string[] lines)
    {
        int[,] map = BuildMap(lines);
        List<Coordinates> lowPoints = FindLowPoints(map);
        return SumLowPoints(map, lowPoints);
    }

    private static int Problem2(string[] lines)
    {
        int[,] map = BuildMap(lines);
        List<Coordinates> lowPoints = FindLowPoints(map);
        List<int> basinSizes = GetBasinSizes(map, lowPoints);
        return BasinSizeProduct(basinSizes);
    }

    private static int BasinSizeProduct(List<int> basinSizes)
    {
        int ret = 1;
        foreach (int basin in basinSizes)
        {
            ret *= basin;
        }
        return ret;
    }

    private static List<int> GetBasinSizes(int[,] map, List<Coordinates> lowPoints)
    {
        List<int> basinSizes = new List<int>();
        List<Coordinates> markedLocations = new List<Coordinates>();
        foreach (Coordinates lowPoint in lowPoints)
        {
            basinSizes = TryAddBasinSize(basinSizes, GetOneBasinSize(map, ref markedLocations, lowPoint.x, lowPoint.y));
        }
        return basinSizes;
    }

    private static List<int> TryAddBasinSize(List<int> basinSizes, int newSize)
    {
        if (basinSizes.Count < 3)
        {
            basinSizes.Add(newSize);
            return basinSizes;
        }
        int min = basinSizes.Min();
        if (min < newSize)
        {
            basinSizes.Remove(min);
            basinSizes.Add(newSize);
        }
        return basinSizes;
    }

    private static int GetOneBasinSize(int[,] map, ref List<Coordinates> markedLocations, int i, int j)
    {
        if (markedLocations.Contains(new Coordinates(i, j)))
        {
            return 0;
        }
        markedLocations.Add(new Coordinates(i, j));
        int basinSize = 1;
        if (i > 0 && map[i - 1, j] != 9)
        {
            basinSize += GetOneBasinSize(map, ref markedLocations, i - 1, j);
        }
        if (i < (map.GetLength(0) - 1) && map[i + 1, j] != 9)
        {
            basinSize += GetOneBasinSize(map, ref markedLocations, i + 1, j);
        }
        if (j > 0 && map[i, j - 1] != 9)
        {
            basinSize += GetOneBasinSize(map, ref markedLocations, i, j - 1);
        }
        if (j < (map.GetLength(1) - 1) && map[i, j + 1] != 9)
        {
            basinSize += GetOneBasinSize(map, ref markedLocations, i, j + 1);
        }
        return basinSize;
    }

    private static List<Coordinates> FindLowPoints(int[,] map)
    {
        List<Coordinates> lowPoints = new List<Coordinates>();
        for (int i = 0; i < map.GetLength(0); i++)
        {
            for (int j = 0; j < map.GetLength(1); j++)
            {
                if (IsLowPoint(map, i, j))
                {
                    lowPoints.Add(new Coordinates(i, j));
                }
            }
        }
        return lowPoints;
    }

    private static int SumLowPoints(int[,] map, List<Coordinates> lowPoints)
    {
        int lowPointsSum = 0;
        foreach (Coordinates lowPoint in lowPoints)
        {
            lowPointsSum += (map[lowPoint.x, lowPoint.y] + 1);
        }
        return lowPointsSum;
    }

    private static bool IsLowPoint(int[,] map, int i, int j)
    {
        return CheckRightSide(map, i, j)
        && CheckLeftSide(map, i, j)
        && CheckAbove(map, i, j)
        && CheckBelow(map, i, j);
    }

    private static bool CheckRightSide(int[,] map, int i, int j)
    {
        if (j >= (map.GetLength(1) - 1))
        {
            return true;
        }
        return map[i, j] < map[i, j + 1];
    }

    private static bool CheckLeftSide(int[,] map, int i, int j)
    {
        if (j == 0)
        {
            return true;
        }
        return map[i, j] < map[i, j - 1];
    }

    private static bool CheckAbove(int[,] map, int i, int j)
    {
        if (i == 0)
        {
            return true;
        }
        return map[i, j] < map[i - 1, j];
    }

    private static bool CheckBelow(int[,] map, int i, int j)
    {
        if (i >= (map.GetLength(0) - 1))
        {
            return true;
        }
        return map[i, j] < map[i + 1, j];
    }

    private static int[,] BuildMap(string[] lines)
    {
        int[,] map = new int[lines.Length, lines[0].Length];
        for (int i = 0; i < lines.Length; i++)
        {
            for (int j = 0; j < lines[i].Length; j++)
            {
                map[i, j] = Convert.ToInt32(lines[i][j].ToString());
            }
        }
        return map;
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
}
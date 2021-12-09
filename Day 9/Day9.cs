using System;

public class AdventOfCodeDay9
{

    public static void run()
    {
        string[] lines = System.IO.File.ReadAllLines("./Day 9/Problem1Input.txt");
        int sumOfLowSpots = Problem1(lines);
        Console.WriteLine("Day 9 - Problem 1: The sum of the low spots is " + sumOfLowSpots);
    }

    private static int Problem1(string[] lines)
    {
        int[,] map = BuildMap(lines);
        return FindLowPoints(map);
    }

    private static int FindLowPoints(int[,] map)
    {
        int lowPointSum = 0;
        for (int i = 0; i < map.GetLength(0); i++)
        {
            for (int j = 0; j < map.GetLength(1); j++)
            {
                if (IsLowPoint(map, i, j))
                {
                    lowPointSum += (map[i, j] + 1);
                }
            }
        }
        return lowPointSum;
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

    private static void PrintSurroundingVals(int[,] map, int i, int j)
    {
        //Check right side
        if (j < (map.GetLength(1) - 1))
        {
            Console.Write(" Right: " + map[i, j + 1]);
        }
        //Check left side
        if (j > 0)
        {
            Console.Write(" Left: " + map[i, j - 1]);
        }
        //Check above
        if (i > 0)
        {
            Console.Write(" Up: " + map[i - 1, j]);
        }
        //Check below
        if (i < (map.GetLength(0) - 1))
        {
            Console.Write(" Down: " + map[i + 1, j]);
        }
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
}
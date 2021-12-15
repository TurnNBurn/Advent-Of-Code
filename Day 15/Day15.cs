using System;

public class AdventOfCodeDay15
{

    public static void run()
    {
        string[] lines = System.IO.File.ReadAllLines("./Day 15/Problem1Input.txt");
        int totalRisk = Problem1(lines);
        Console.WriteLine("Day 15 - Problem 1: The lowest total risk out of the cave is " + totalRisk);
    }

    private static int Problem1(string[] lines)
    {
        int[,] caveMap = BuildCaveMap(lines);
        return Math.Min(MoveOne(caveMap, 0, 1), MoveOne(caveMap, 1, 0));
    }

    private static int MoveOne(int[,] caveMap, int i, int j)
    {
        int currentRisk = caveMap[i, j];
        int caveLength = caveMap.GetLength(0) - 1;
        int caveWidth = caveMap.GetLength(1) - 1;
        if (i == caveLength && j == caveWidth)
        {
            //Console.WriteLine(currentRisk);
            return currentRisk;
        }
        int nextRisk = 0;
        if (i < caveLength)
        {
            nextRisk = MoveOne(caveMap, i + 1, j);
        }
        if (j < caveWidth)
        {
            int rightRisk = MoveOne(caveMap, i, j + 1);
            nextRisk = nextRisk == 0 ? rightRisk : Math.Min(nextRisk, rightRisk);
        }
        return currentRisk + nextRisk;
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
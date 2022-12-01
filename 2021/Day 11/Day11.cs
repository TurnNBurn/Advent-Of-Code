using System;

public class AdventOfCodeDay11
{

    public static void run()
    {
        string[] lines = System.IO.File.ReadAllLines("./Day 11/Problem1Input.txt");
        int totalFlashes = Problem1(lines);
        int firstSynchronizedStep = Problem2(lines);
        Console.WriteLine("Day 11 - Problem 1: The total number of flashes after 100 steps is " + totalFlashes);
        Console.WriteLine("Day 11 - Problem 2: The first step where all 100 octopus are synchronized is " + firstSynchronizedStep);
    }

    private static int Problem1(string[] lines)
    {
        int[,] octoMap = BuildOctopusMap(lines);
        return CountFlashes(octoMap);
    }

    private static int Problem2(string[] lines)
    {
        int[,] octoMap = BuildOctopusMap(lines);
        return FindFirstSynchronizedStep(octoMap);
    }

    private static int FindFirstSynchronizedStep(int[,] octoMap)
    {
        int step = 0;
        int numFlashes = 0;
        while (numFlashes < 100)
        {
            step++;
            numFlashes = PerformOneStep(ref octoMap);
        }
        return step;
    }

    private static int[,] BuildOctopusMap(string[] lines)
    {
        int[,] octoMap = new int[lines[0].Length, lines.Length];
        for (int i = 0; i < lines.Length; i++)
        {
            for (int j = 0; j < lines[i].Length; j++)
            {
                octoMap[i, j] = Convert.ToInt32(lines[i][j].ToString());
            }
        }
        return octoMap;
    }


    private static int CountFlashes(int[,] octoMap)
    {
        int flashes = 0;
        for (int i = 0; i < 100; i++)
        {
            flashes += PerformOneStep(ref octoMap);
        }
        return flashes;
    }

    private static int PerformOneStep(ref int[,] octoMap)
    {
        int numFlashes = 0;
        Charge(ref octoMap);
        numFlashes = Flash(ref octoMap);
        return numFlashes;
    }

    private static void Charge(ref int[,] octoMap)
    {
        for (int i = 0; i < octoMap.GetLength(0); i++)
        {
            for (int j = 0; j < octoMap.GetLength(1); j++)
            {
                octoMap[i, j]++;
            }
        }
    }
    private static int Flash(ref int[,] octoMap)
    {
        int numFlashes = 0;
        for (int i = 0; i < octoMap.GetLength(0); i++)
        {
            for (int j = 0; j < octoMap.GetLength(1); j++)
            {
                if (octoMap[i, j] > 9)
                {
                    numFlashes += FlashOctopus(ref octoMap, i, j);
                }
            }
        }
        return numFlashes;
    }

    private static int FlashOctopus(ref int[,] octoMap, int i, int j)
    {
        int flashCount = 1;
        octoMap[i, j] = 0;
        flashCount += IncrementAdjacent(ref octoMap, i, j);
        return flashCount;
    }

    private static int IncrementAdjacent(ref int[,] octoMap, int i, int j)
    {
        int numAdditionalFlashes = 0;
        for (int x = -1; x < 2; x++)
        {
            for (int y = -1; y < 2; y++)
            {
                if ((i + x) > -1 && (i + x) < octoMap.GetLength(0) && (j + y) > -1 && (j + y) < octoMap.GetLength(1))
                {
                    //This will technically hit the original octopus again - but our 0 check handles that
                    if (octoMap[i + x, j + y] > 0)
                    {
                        octoMap[i + x, j + y]++;
                        if (octoMap[i + x, j + y] > 9)
                        {
                            numAdditionalFlashes += FlashOctopus(ref octoMap, i + x, j + y);
                        }
                    }
                }
            }
        }
        return numAdditionalFlashes;
    }
}
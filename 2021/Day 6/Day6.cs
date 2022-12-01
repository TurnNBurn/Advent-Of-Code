using System;

public class AdventOfCodeDay6
{

    public static void run()
    {
        string[] lines = System.IO.File.ReadAllLines("./2021/Day 6/Problem1Input.txt");
        int numberOfFish = Problem1(lines);
        long numberOfFish2 = Problem2(lines);
        Console.WriteLine("Day 6 - Problem 1: After 80 days there are " + numberOfFish + " fish.");
        Console.WriteLine("Day 6 - Problem 2: After 256 days there are " + numberOfFish2 + " fish.");
    }

    private static int Problem1(string[] lines)
    {
        string[] initialFish = lines[0].Split(',');
        List<int> fish = new List<int>();
        foreach (string lanternFish in initialFish)
        {
            fish.Add(Convert.ToInt32(lanternFish));
        }
        for (int days = 0; days < 80; days++)
        {
            int fishCount = fish.Count;
            for (int i = 0; i < fishCount; i++)
            {
                if (fish[i] == 0)
                {
                    fish[i] = 6;
                    fish.Add(8);
                }
                else
                {
                    fish[i] = fish[i] - 1;
                }
            }
        }
        return fish.Count;
    }

    private static long Problem2(string[] lines)
    {
        long[] fishCounters = new long[9];
        string[] initialFish = lines[0].Split(',');
        foreach (string fish in initialFish)
        {
            int fishCounter = Convert.ToInt32(fish);
            fishCounters[fishCounter] = fishCounters[fishCounter] + 1;
        }
        for (int i = 0; i < 256; i++)
        {
            long fishAtZero = fishCounters[0];
            for (int j = 0; j < 8; j++)
            {
                fishCounters[j] = fishCounters[j + 1];
            }
            fishCounters[8] = fishAtZero;
            fishCounters[6] = fishCounters[6] + fishAtZero;
        }
        long totalFish = 0;
        for (int i = 0; i < 9; i++)
        {
            totalFish += fishCounters[i];
        }
        return totalFish;
    }
}
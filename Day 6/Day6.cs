using System;

public class AdventOfCodeDay6
{

    public static void run()
    {
        string[] lines = System.IO.File.ReadAllLines("./Day 6/Problem1Input.txt");
        int numberOfFish = Problem1(lines);
        Console.WriteLine("Day 6 - Problem 1: After 8 days there are " + numberOfFish + " fish.");
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
}
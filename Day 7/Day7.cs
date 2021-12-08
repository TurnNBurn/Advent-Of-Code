using System;

public class AdventOfCodeDay7
{

    public static void run()
    {
        string[] lines = System.IO.File.ReadAllLines("./Day 7/Problem1Input.txt");
        int lowestGasCost = Problem1(lines);
        int lowestGasCostCorrected = Problem2(lines);
        Console.WriteLine("Day 7 - Problem 1: The lowest gas cost is " + lowestGasCost);
        Console.WriteLine("Day 7 - Problem 2: The corrected lowest gas cost is " + lowestGasCostCorrected);
    }

    private static int Problem1(string[] lines)
    {
        Dictionary<int, int> subLocations = ParseInput(lines[0].Split(','));
        return FindAlignmentPoint(subLocations, GetHighestSubLocation(subLocations));
    }

    private static int Problem2(string[] lines)
    {
        Dictionary<int, int> subLocations = ParseInput(lines[0].Split(','));
        return FindAlignmentPointCorrected(subLocations, GetHighestSubLocation(subLocations));
    }

    private static Dictionary<int, int> ParseInput(string[] nums)
    {
        Dictionary<int, int> subLocations = new Dictionary<int, int>();
        foreach (string numString in nums)
        {
            int num = Convert.ToInt32(numString);
            if (subLocations.ContainsKey(num))
            {
                subLocations[num] = subLocations[num] + 1;
            }
            else
            {
                subLocations.Add(num, 1);
            }
        }
        return subLocations;
    }

    private static int GetHighestSubLocation(Dictionary<int, int> subLocations)
    {
        int highestSubLocation = 0;
        foreach (KeyValuePair<int, int> sub in subLocations)
        {
            if (sub.Key > highestSubLocation)
            {
                highestSubLocation = sub.Key;
            }
        }
        return highestSubLocation;
    }

    private static int FindAlignmentPoint(Dictionary<int, int> subLocations, int highestSubLocation)
    {
        int lowestGasCost = int.MaxValue;

        for (int i = 0; i < highestSubLocation; i++)
        {
            int gasCost = 0;
            foreach (KeyValuePair<int, int> sub in subLocations)
            {
                gasCost += sub.Value * (Math.Abs(i - sub.Key));
            }
            if (gasCost < lowestGasCost)
            {
                lowestGasCost = gasCost;
            }
        }
        return lowestGasCost;
    }

    private static int FindAlignmentPointCorrected(Dictionary<int, int> subLocations, int highestSubLocation)
    {
        int lowestGasCost = int.MaxValue;

        for (int i = 0; i < highestSubLocation; i++)
        {
            int gasCost = 0;
            foreach (KeyValuePair<int, int> sub in subLocations)
            {
                int gasCostPerSub = 0;
                for (int j = 0; j <= Math.Abs(i - sub.Key); j++)
                {
                    gasCostPerSub += j;
                }
                gasCost += (gasCostPerSub * sub.Value);
            }
            if (gasCost < lowestGasCost)
            {
                lowestGasCost = gasCost;
            }
        }
        return lowestGasCost;
    }
}
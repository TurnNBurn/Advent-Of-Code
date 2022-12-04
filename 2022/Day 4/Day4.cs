using System;

public class AdventOfCode2022Day4
{

    public static void run()
    {
        string[] lines = System.IO.File.ReadAllLines("./2022/Day 4/Problem1Input.txt");

        int totalPairsFullyContained = Problem1(lines);
        int totalPairsOverlapping = Problem2(lines);

        Console.WriteLine("Day 4 - Problem 1: There are " + totalPairsFullyContained + " elves with fully contained ranges.");
        Console.WriteLine("Day 4 - Problem 2: There are " + totalPairsOverlapping + " pairs with overlapping segments.");
    }

    private static int Problem1(string[] lines)
    {
        int totalPairsFullyContained = 0;
        foreach (string line in lines)
        {
            string[] pair = SplitPairs(line);
            int[] firstRange = ParseOneRange(pair[0]);
            int[] secondRange = ParseOneRange(pair[1]);
            if (firstRange[0] <= secondRange[0] && firstRange[1] >= secondRange[1])
            {
                totalPairsFullyContained++;
            }
            else if (secondRange[0] <= firstRange[0] && secondRange[1] >= firstRange[1])
            {
                totalPairsFullyContained++;
            }
        }
        return totalPairsFullyContained;
    }

    private static string[] SplitPairs(string line)
    {
        return line.Split(',');
    }

    private static int[] ParseOneRange(string oneRange)
    {
        string[] splitInput = oneRange.Split('-');
        int[] range = { Convert.ToInt32(splitInput[0]), Convert.ToInt32(splitInput[1]) };
        return range;
    }
    private static int Problem2(string[] lines)
    {
        int totalPairsOverlapping = 0;
        foreach (string line in lines)
        {
            string[] pair = SplitPairs(line);
            int[] firstRange = ParseOneRange(pair[0]);
            int[] secondRange = ParseOneRange(pair[1]);
            if (firstRange[0] <= secondRange[0] && firstRange[1] >= secondRange[1])
            {
                totalPairsOverlapping++;
            }
            else if (secondRange[0] <= firstRange[0] && secondRange[1] >= firstRange[1])
            {
                totalPairsOverlapping++;
            }
            else if (firstRange[1] >= secondRange[0] && firstRange[1] <= secondRange[1])
            {
                totalPairsOverlapping++;
            }
            else if (firstRange[0] >= secondRange[0] && firstRange[0] <= secondRange[1])
            {
                totalPairsOverlapping++;
            }
        }
        return totalPairsOverlapping;
    }
}
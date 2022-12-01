using System;

public class AdventOfCodeDay1
{

    public static void run()
    {
        string[] lines = System.IO.File.ReadAllLines("./Day 1/Problem1Input.txt");
        int numIncreases = Problem1(lines);
        int numThreeMeasureIncreases = Problem2(lines);

        Console.WriteLine("Day 1 - Problem 1: There are " + numIncreases + " increases.");
        Console.WriteLine("Day 1 - Problem 2: There are " + numThreeMeasureIncreases + " three measurement increases.");
    }

    private static int Problem1(string[] lines)
    {
        int previousLine = Convert.ToInt32(lines[0]);
        int totalIncreases = 0;
        for (int i = 1; i < lines.Length; i++)
        {
            int nextLine = Convert.ToInt32(lines[i]);
            if (nextLine > previousLine)
            {
                totalIncreases++;
            }
            previousLine = nextLine;
        }
        return totalIncreases;
    }

    private static int Problem2(string[] lines)
    {
        int totalIncreases = 0;
        int previousThree = Convert.ToInt32(lines[0]) + Convert.ToInt32(lines[1]) + Convert.ToInt32(lines[2]);
        for (int i = 1; i < lines.Length - 2; i++)
        {
            int nextThree = Convert.ToInt32(lines[i]) + Convert.ToInt32(lines[i + 1]) + Convert.ToInt32(lines[i + 2]);
            if (nextThree > previousThree)
            {
                totalIncreases++;
            }
            previousThree = nextThree;
        }
        return totalIncreases;
    }
}
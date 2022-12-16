using System;
using System.Text;

public class AdventOfCode2022Day16
{
    const int yToCheckProb1 = 2000000;
    const int maxRange = 4000000;
    public static void run()
    {
        string[] lines = System.IO.File.ReadAllLines("./2022/Day 16/Problem1Input.txt");

        int searchedPositions = Problem1(lines);
        //long distressFrequency = Problem2(lines);

        Console.WriteLine("Day 16 - Problem 1: In the row y = " + yToCheckProb1 + " " + searchedPositions + " cannot contain the distress signal.");
        //Console.WriteLine("Day 16 - Problem 2: The distress beacon's frequency is " + distressFrequency + ".");
    }

    private static int Problem1(string[] lines)
    {
        return -1;
    }

    private static int Problem2(string[] lines)
    {
        return -1;
    }
}
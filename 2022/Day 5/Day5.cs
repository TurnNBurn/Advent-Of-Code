using System;

public class AdventOfCode2022Day5
{

    public static void run()
    {
        string[] lines = System.IO.File.ReadAllLines("./2022/Day 5/Problem1Input.txt");

        int totalPairsFullyContained = Problem1(lines);
        //int totalPairsOverlapping = Problem2(lines);

        Console.WriteLine("Day 5 - Problem 1: There are " + totalPairsFullyContained + " elves with fully contained ranges.");
        //Console.WriteLine("Day 5 - Problem 2: There are " + totalPairsOverlapping + " pairs with overlapping segments.");
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
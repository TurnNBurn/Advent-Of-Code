using System;

public class AdventOfCode2022Day4
{

    public static void run()
    {
        string[] lines = System.IO.File.ReadAllLines("./2022/Day 4/Problem1Input.txt");

        int totalPriority = Problem1(lines);
        //int totalBadgePriority = Problem2(lines);

        Console.WriteLine("Day 4 - Problem 1: The total priority of all the items split across compartments is " + totalPriority + ".");
        //Console.WriteLine("Day 4 - Problem 2: The total priority of the badges is " + totalBadgePriority + ".");
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
using System;

public class AdventOfCode2022Day6
{

    public static void run()
    {
        string[] lines = System.IO.File.ReadAllLines("./2022/Day 5/Problem1Input.txt");

        int topCrates = Problem1(lines);
        //string topCratesCrateMover3001 = Problem2(lines);

        Console.WriteLine("Day 5 - Problem 1: The top crates are " + topCrates + ".");
        //Console.WriteLine("Day 5 - Problem 2: The top crates are " + topCratesCrateMover3001 + ".");
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
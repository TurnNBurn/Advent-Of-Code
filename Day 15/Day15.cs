using System;

public class AdventOfCodeDay15
{

    public static void run()
    {
        string[] lines = System.IO.File.ReadAllLines("./Day 15/Problem1Input.txt");
        long totalRisk = Problem1(lines);
        Console.WriteLine("Day 15 - Problem 1: The lowest total risk out of the cave is " + totalRisk);
    }

    private static long Problem1(string[] lines)
    {
        return 0;
    }
}
using System;

public class AdventOfCodeDay10
{

    public static void run()
    {
        string[] lines = System.IO.File.ReadAllLines("./Day 10/Problem1Input.txt");
        int totalCorruptionScore = Problem1(lines);
        Console.WriteLine("Day 10 - Problem 1: The total corruption score is " + totalCorruptionScore);
    }

    private static int Problem1(string[] lines)
    {
        return 0;
    }
}
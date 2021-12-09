using System;

public class AdventOfCodeDay9
{

    public static void run()
    {
        string[] lines = System.IO.File.ReadAllLines("./Day 9/Problem1Input.txt");
        int sumOfLowSpots = Problem1(lines);
        Console.WriteLine("Day 9 - Problem 1: The sum of the low spots is " + sumOfLowSpots);
    }

    private static int Problem1(string[] lines)
    {
        return 0;
    }
}
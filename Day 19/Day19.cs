using System;

public class AdventOfCodeDay19
{

    public static void run()
    {
        string[] lines = System.IO.File.ReadAllLines("./Day 19/Problem1Input.txt");
        int energy = Problem1(lines);
        Console.WriteLine("Day 19 - Problem 1: The least amount of energy to organize the amphipods is " + energy);
    }

    private static int Problem1(string[] lines)
    {
        return 0;
    }
}
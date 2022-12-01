using System;

public class AdventOfCodeDay23
{

    public static void run()
    {
        string[] lines = System.IO.File.ReadAllLines("./2021/Day 23/Problem1Input.txt");
        int energy = Problem1(lines);
        Console.WriteLine("Day 23 - Problem 1: The least amount of energy to organize the amphipods is " + energy);
    }

    private static int Problem1(string[] lines)
    {
        return 0;
    }
}
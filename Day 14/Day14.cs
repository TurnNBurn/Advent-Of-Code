using System;

public class AdventOfCodeDay14
{

    public static void run()
    {
        string[] lines = System.IO.File.ReadAllLines("./Day 14/Problem1Input.txt");
        int mostCommonMinusLeastCommon = Problem1(lines);
        Console.WriteLine("Day 14 - Problem 1: The most common element minus the least common element is " + mostCommonMinusLeastCommon);
    }

    private static int Problem1(string[] lines)
    {
        return 0;
    }
}
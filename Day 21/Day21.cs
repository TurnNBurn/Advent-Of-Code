using System;

public class AdventOfCodeDay21
{

    public static void run()
    {
        string[] lines = System.IO.File.ReadAllLines("./Day 21/Problem1Input.txt");
        int product = Problem1(lines);
        Console.WriteLine("Day 21 - Problem 1: The product of the losers score and number of dice rolls is " + product);
    }

    private static int Problem1(string[] lines)
    {
        return 0;
    }
}
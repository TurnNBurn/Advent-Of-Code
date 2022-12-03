using System;

public class AdventOfCode2022Day3
{

    public static void run()
    {
        string[] lines = System.IO.File.ReadAllLines("./2022/Day 3/Problem1Input.txt");

        int totalScore = Problem1(lines);
        //int totalOptimizedScore = Problem2(lines);

        Console.WriteLine("Day 2 - Problem 1: My total score according to plan is " + totalScore + ".");
        //Console.WriteLine("Day 2 - Problem 2: My total score with optimized play is " + totalOptimizedScore + ".");
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
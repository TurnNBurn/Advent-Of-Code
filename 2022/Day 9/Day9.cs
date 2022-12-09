using System;
using System.Text;

public class AdventOfCode2022Day9
{

    public static void run()
    {
        string[] lines = System.IO.File.ReadAllLines("./2022/Day 9/Problem1Input.txt");

        int visibleCount = Problem1(lines);
        //int maxScenic = Problem2(lines);

        Console.WriteLine("Day 9 - Problem 1: There are  " + visibleCount + " visible trees.");
        //Console.WriteLine("Day 9 - Problem 2: The most scenic tree has a score of " + maxScenic + ".");
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
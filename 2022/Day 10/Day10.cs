using System;
using System.Text;

public class AdventOfCode2022Day10
{

    public static void run()
    {
        string[] lines = System.IO.File.ReadAllLines("./2022/Day 10/Problem1Input.txt");

        int pointsVisited = Problem1(lines);
        //int ropeEndVisited = Problem2(lines);

        Console.WriteLine("Day 10 - Problem 1: The tail visits " + pointsVisited + " points.");
        //Console.WriteLine("Day 10 - Problem 2: The rope end visits " + ropeEndVisited + " points.");
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
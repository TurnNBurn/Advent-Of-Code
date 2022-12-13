using System;
using System.Text;

public class AdventOfCode2022Day13
{

    public static void run()
    {
        string[] lines = System.IO.File.ReadAllLines("./2022/Day 13/Problem1Input.txt");

        int steps = Problem1(lines);
        //int shortestDistance = Problem2(lines);

        Console.WriteLine("Day 13 - Problem 1: The number of steps to reach the endpoint is " + steps + ".");
        //Console.WriteLine("Day 12 - Problem 2: The distance from the best starting point is " + shortestDistance + ".");
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
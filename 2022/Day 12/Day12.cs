using System;
using System.Text;

public class AdventOfCode2022Day12
{

    public static void run()
    {
        string[] lines = System.IO.File.ReadAllLines("./2022/Day 12/Problem1Input.txt");

        int monkeyLevel = Problem1(lines);
        //long longMonkeyLevel = Problem2(lines);

        Console.WriteLine("Day 12 - Problem 1: The monkey level is " + monkeyLevel + ".");
        //Console.WriteLine("Day 12 - Problem 2: The monkey level after 10000 rounds is " + longMonkeyLevel + " points.");
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
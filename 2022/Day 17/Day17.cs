using System;
using System.Text;

public class AdventOfCode2022Day17
{
    public static void run()
    {
        string[] lines = System.IO.File.ReadAllLines("./2022/Day 17/Problem1Input.txt");

        int totalPressure = Problem1(lines);
        //long distressFrequency = Problem2(lines);

        Console.WriteLine("Day 17 - Problem 1: The most pressure that can be released is " + totalPressure + ".");
        //Console.WriteLine("Day 17 - Problem 2: The distress beacon's frequency is " + distressFrequency + ".");
    }

    private static int Problem1(string[] lines)
    {
        return -1;
    }

    private static int Problem2(string[] lines){
        return -1;
    }
}
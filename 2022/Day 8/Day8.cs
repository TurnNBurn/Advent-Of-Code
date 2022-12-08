using System;
using System.Text;

public class AdventOfCode2022Day8
{

    public static void run()
    {
        string[] lines = System.IO.File.ReadAllLines("./2022/Day 8/Problem1Input.txt");

        int totalSize = Problem1(lines);
        //int directoryToDelete = Problem2(lines);

        Console.WriteLine("Day 8 - Problem 1: The total size of directories under 100000 is " + totalSize + ".");
        //Console.WriteLine("Day 8 - Problem 2: The size of the directory to delete is " + directoryToDelete + ".");
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
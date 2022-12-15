using System;
using System.Text;

public class AdventOfCode2022Day15
{

    public static void run()
    {
        string[] lines = System.IO.File.ReadAllLines("./2022/Day 15/Problem1Input.txt");

        int grainsOfSand = Problem1(lines);
        //int totalSand = Problem2(lines);

        Console.WriteLine("Day 15 - Problem 1: There are " + grainsOfSand + " grains of sand before the cave is full.");
        //Console.WriteLine("Day 15 - Problem 2: There are " + totalSand + " grains of sand before the cave is full.");
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
using System;
using System.Text;

public class AdventOfCode2022Day7
{

    public static void run()
    {
        string[] lines = System.IO.File.ReadAllLines("./2022/Day 7/Problem1Input.txt");

        int firstPacketMarker = Problem1(lines);
        //int firstMessageMarker = Problem2(lines);

        Console.WriteLine("Day 7 - Problem 1: The first packet marker appears after " + firstPacketMarker + " characters.");
        //Console.WriteLine("Day 7 - Problem 2: The first message marker appears after " + firstMessageMarker + " characters.");
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
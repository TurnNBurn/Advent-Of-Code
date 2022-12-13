using System;
using System.Text;

public class AdventOfCode2022Day14
{

    public static void run()
    {
        string[] lines = System.IO.File.ReadAllLines("./2022/Day 14/Problem1Input.txt");

        int inOrderSum = Problem1(lines);
        //nt decoder = Problem2(lines);

        Console.WriteLine("Day 14 - Problem 1: The sum of packets in order is " + inOrderSum + ".");
        //Console.WriteLine("Day 14 - Problem 2: The decoder key is " + decoder + ".");
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
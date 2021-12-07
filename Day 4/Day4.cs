using System;

public class AdventOfCodeDay4
{

    public static void run()
    {
        string[] lines = System.IO.File.ReadAllLines("./Day 3/Problem1Input.txt");
        int bingoScore = Problem1(lines);
        //int lifeSupport = Problem2(lines);

        Console.WriteLine("Day 4 - Problem 1: The bingo score of the first board to win is " + bingoScore);
    }

    private static int Problem1(string[] lines)
    {
        int bingoScore = 0;
        return bingoScore;
    }
}
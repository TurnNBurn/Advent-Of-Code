using System;

public class AdventOfCodeDay21
{

    public static void run()
    {
        string[] lines = System.IO.File.ReadAllLines("./Day 21/Problem1Input.txt");
        int product = Problem1(lines);
        Console.WriteLine("Day 21 - Problem 1: The product of the losers score and number of dice rolls is " + product);
    }

    private static int Problem1(string[] lines)
    {
        int[] positions = GetStartingPositions(lines);
        int[] scores = { 0, 0 };
        int rolls = 0;
        while (scores[0] < 1000 && scores[1] < 1000)
        {
            rolls += 3;
            if (rolls % 2 == 1)
            {
                positions[0] = positions[0] + GetLastThreeRolls(rolls);
                positions[0] = ReducePosition(positions[0]);
                scores[0] += positions[0];
            }
            else
            {
                positions[1] = positions[1] + GetLastThreeRolls(rolls);
                positions[1] = ReducePosition(positions[1]);
                scores[1] += positions[1];
            }
        }
        return Math.Min(scores[0], scores[1]) * rolls;
    }

    private static int GetLastThreeRolls(int rolls)
    {
        if (rolls % 100 == 0)
        {
            return 297;
        }
        else if (rolls % 100 == 1)
        {
            return 200;
        }
        else if (rolls % 100 == 2)
        {
            return 103;
        }
        return (3 * (rolls % 100)) - 3;
    }

    private static int ReducePosition(int position)
    {
        if (position <= 10)
        {
            return position;
        }
        if (position % 10 == 0)
        {
            return 10;
        }
        return position % 10;
    }

    private static int[] GetStartingPositions(string[] lines)
    {
        int[] startingPositions = new int[2];
        startingPositions[0] = Convert.ToInt32(lines[0].Split(':')[1]);
        startingPositions[1] = Convert.ToInt32(lines[1].Split(':')[1]);
        return startingPositions;
    }
}
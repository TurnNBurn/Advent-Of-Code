using System;

public class AdventOfCode2022Day3
{

    public static void run()
    {
        string[] lines = System.IO.File.ReadAllLines("./2022/Day 3/Problem1Input.txt");

        int totalPriority = Problem1(lines);
        //int totalOptimizedScore = Problem2(lines);

        Console.WriteLine("Day 3 - Problem 1: The total priority of all the items split across compartments is " + totalPriority + ".");
        //Console.WriteLine("Day 2 - Problem 2: My total score with optimized play is " + totalOptimizedScore + ".");
    }

    private static int Problem1(string[] lines)
    {
        int sum = 0;
        foreach (string line in lines)
        {
            sum += FindRucksackPriority(line);
        }
        return sum;
    }

    private static int FindRucksackPriority(string rucksack)
    {
        int priority = 0;
        HashSet<char> firstCompartmentItems = new HashSet<char>();
        for (int i = 0; i < rucksack.Length; i++)
        {
            if (i < rucksack.Length / 2)
            {
                firstCompartmentItems.Add(rucksack[i]);
            }
            else
            {
                if (firstCompartmentItems.Contains(rucksack[i]))
                {
                    return GetPriorityForItem(rucksack[i]);
                }
            }
        }
        return priority;
    }

    private static int GetPriorityForItem(char c)
    {
        return c < 97 ? c - 38 : c - 96;
    }

    private static int Problem2(string[] lines)
    {
        return -1;
    }
}
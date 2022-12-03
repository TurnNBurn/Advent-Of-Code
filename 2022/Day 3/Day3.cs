using System;

public class AdventOfCode2022Day3
{

    public static void run()
    {
        string[] lines = System.IO.File.ReadAllLines("./2022/Day 3/Problem1Input.txt");

        int totalPriority = Problem1(lines);
        int totalBadgePriority = Problem2(lines);

        Console.WriteLine("Day 3 - Problem 1: The total priority of all the items split across compartments is " + totalPriority + ".");
        Console.WriteLine("Day 3 - Problem 2: The total priority of the badges is " + totalBadgePriority + ".");
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
        int sum = 0;
        for (int i = 0; i < lines.Length - 2; i += 3)
        {
            sum += FindBadgePriority(lines[i], lines[i + 1], lines[i + 2]);
        }
        return sum;
    }

    private static int FindBadgePriority(string rucksackA, string rucksackB, string rucksackC)
    {
        HashSet<char> firstRucksackItems = new HashSet<char>();
        foreach (char item in rucksackA)
        {
            firstRucksackItems.Add(item);
        }
        HashSet<char> sharedRucksackItems = new HashSet<char>();
        foreach (char item in rucksackB)
        {
            if (firstRucksackItems.Contains(item))
            {
                sharedRucksackItems.Add(item);
            }
        }
        if (sharedRucksackItems.Count == 1)
        {
            foreach (char item in sharedRucksackItems)
            {
                return GetPriorityForItem(item);
            }
        }
        foreach (char item in rucksackC)
        {
            if (sharedRucksackItems.Contains(item))
            {
                return GetPriorityForItem(item);
            }
        }
        return -1;
    }
}
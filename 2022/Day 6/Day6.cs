using System;
using System.Text;

public class AdventOfCode2022Day6
{

    public static void run()
    {
        string[] lines = System.IO.File.ReadAllLines("./2022/Day 6/Problem1Input.txt");

        int firstPacketMarker = Problem1(lines);
        int firstMessageMarker = Problem2(lines);

        Console.WriteLine("Day 6 - Problem 1: The first packet marker appears after " + firstPacketMarker + " characters.");
        Console.WriteLine("Day 6 - Problem 2: The first message marker appears after " + firstMessageMarker + " characters.");
    }

    private static int Problem1(string[] lines)
    {
        int i = 0;
        Dictionary<char, int> buffer = new Dictionary<char, int>();
        while (i < lines[0].Length)
        {
            DictIncrementOrAdd(buffer, lines[0][i]);
            if (i > 3)
            {
                DictRemoveEntry(buffer, lines[0][i - 4]);
            }
            if (buffer.Count == 4)
            {
                return i + 1;
            }
            i++;
        }
        //Add one to account for zero index
        return -1;
    }

    private static void DictRemoveEntry(Dictionary<char, int> dict, char key)
    {
        if (dict.ContainsKey(key))
        {
            if (dict[key] == 1)
            {
                dict.Remove(key);
            }
            else
            {
                dict[key]--;
            }
        }
    }

    private static void DictIncrementOrAdd(Dictionary<char, int> dict, char key)
    {
        if (dict.ContainsKey(key))
        {
            dict[key]++;
        }
        else
        {
            dict.Add(key, 1);
        }
    }

    private static int Problem2(string[] lines)
    {
        int i = 0;
        Dictionary<char, int> buffer = new Dictionary<char, int>();
        while (i < lines[0].Length)
        {
            DictIncrementOrAdd(buffer, lines[0][i]);
            if (i > 13)
            {
                DictRemoveEntry(buffer, lines[0][i - 14]);
            }
            if (buffer.Count == 14)
            {
                return i + 1;
            }
            i++;
        }
        //Add one to account for zero index
        return -1;
    }
}
using System;
using System.Text;

public class AdventOfCodeDay14
{

    public static void run()
    {
        string[] lines = System.IO.File.ReadAllLines("./Day 14/Problem1Input.txt");
        int tenInsertions = Problem1(lines);
        //int fortyInsertions = Problem2(lines);
        Console.WriteLine("Day 14 - Problem 1: The most common element minus the least common element after 10 insertions is " + tenInsertions);
        //Console.WriteLine("Day 14 - Problem 2: The most common element minus the least common element after forty insertions is " + fortyInsertions);
    }

    private static int Problem1(string[] lines)
    {
        Dictionary<string, char> insertions = ParseInsertionRules(lines);
        Dictionary<char, int> elementCount = PerformTenInsertions(lines[0], insertions);
        return DiffMaxMin(elementCount);
    }

    private static int Problem2(string[] lines)
    {
        Dictionary<string, char> insertions = ParseInsertionRules(lines);
        Dictionary<char, int> elementCount = PerformFortyInsertions(lines[0], insertions);
        return DiffMaxMin(elementCount);
    }

    private static Dictionary<string, char> ParseInsertionRules(string[] lines)
    {
        Dictionary<string, char> insertions = new Dictionary<string, char>();
        for (int i = 1; i < lines.Length; i++)
        {
            if (lines[i].Contains("->"))
            {
                string[] line = lines[i].Split(" -> ");
                insertions.Add(line[0], line[1].ToCharArray()[0]);
            }
        }
        return insertions;
    }

    private static Dictionary<char, int> PerformTenInsertions(string polymer, Dictionary<string, char> insertions)
    {
        Dictionary<char, int> elementCount = SetupElementCount(polymer);
        for (int i = 0; i < polymer.Length - 1; i++)
        {
            TraverseOnePair(new string(polymer[i].ToString() + polymer[i + 1].ToString()), insertions, elementCount, 0, 10);
        }
        return elementCount;
    }

    private static Dictionary<char, int> PerformFortyInsertions(string polymer, Dictionary<string, char> insertions)
    {
        Dictionary<char, int> elementCount = SetupElementCount(polymer);
        for (int i = 0; i < polymer.Length - 1; i++)
        {
            TraverseOnePair(new string(polymer[i].ToString() + polymer[i + 1].ToString()), insertions, elementCount, 0, 40);
        }
        return elementCount;
    }

    private static void TraverseOnePair(string pair, Dictionary<string, char> insertions, Dictionary<char, int> elementCount, int numIterations, int limit)
    {
        if (numIterations == limit)
        {
            return;
        }
        if (insertions.ContainsKey(pair))
        {
            char newElement = insertions[pair];
            AddElementCount(elementCount, insertions[pair]);
            TraverseOnePair(new string(pair[0] + newElement.ToString()), insertions, elementCount, numIterations + 1, limit);
            TraverseOnePair(new string(newElement.ToString() + pair[1]), insertions, elementCount, numIterations + 1, limit);
        }
    }

    private static Dictionary<char, int> SetupElementCount(string polymer)
    {
        Dictionary<char, int> elementCount = new Dictionary<char, int>();
        for (int i = 0; i < polymer.Length; i++)
        {
            AddElementCount(elementCount, polymer[i]);
        }
        return elementCount;
    }

    private static void AddElementCount(Dictionary<char, int> elementCount, char element)
    {
        if (elementCount.ContainsKey(element))
        {
            elementCount[element] = elementCount[element] + 1;
        }
        else
        {
            elementCount.Add(element, 1);
        }
    }

    private static int DiffMaxMin(Dictionary<char, int> elementCount)
    {
        int min = 0;
        int max = 0;
        foreach (KeyValuePair<char, int> element in elementCount)
        {
            if (min == 0 || element.Value < min)
            {
                min = element.Value;
            }
            if (element.Value > max)
            {
                max = element.Value;
            }
        }
        return max - min;
    }
}
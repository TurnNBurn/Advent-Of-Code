using System;
using System.Text;

public class AdventOfCodeDay14
{

    public static void run()
    {
        string[] lines = System.IO.File.ReadAllLines("./2021/Day 14/Problem1Input.txt");
        long tenInsertions = Problem1(lines);
        long fortyInsertions = Problem2(lines);
        Console.WriteLine("Day 14 - Problem 1: The most common element minus the least common element after 10 insertions is " + tenInsertions);
        Console.WriteLine("Day 14 - Problem 2: The most common element minus the least common element after forty insertions is " + fortyInsertions);
    }

    private static long Problem1(string[] lines)
    {
        Dictionary<string, char> insertions = ParseInsertionRules(lines);
        Dictionary<string, long> pairCount = PerformInsertions(lines[0], insertions, 10);
        Dictionary<char, long> elementCount = ConvertPairCountToElementCount(pairCount);
        //Every element will always be part of two pairs - except the first and last element
        //Add one to each of their counts to compensate for dividing by two earlier
        AddElementLong(elementCount, lines[0][0], 1);
        AddElementLong(elementCount, lines[0][lines[0].Length - 1], 1);
        return DiffMaxMin(elementCount);
    }

    private static long Problem2(string[] lines)
    {
        Dictionary<string, char> insertions = ParseInsertionRules(lines);
        Dictionary<string, long> pairCount = PerformInsertions(lines[0], insertions, 40);
        Dictionary<char, long> elementCount = ConvertPairCountToElementCount(pairCount);
        //Every element will always be part of two pairs - except the first and last element
        //Add one to each of their counts to compensate for dividing by two earlier
        AddElementLong(elementCount, lines[0][0], 1);
        AddElementLong(elementCount, lines[0][lines[0].Length - 1], 1);
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

    private static Dictionary<string, long> PerformInsertions(string polymer, Dictionary<string, char> insertions, int count)
    {
        Dictionary<string, long> pairCount = SetupPairCount(polymer);
        for (int i = 0; i < count; i++)
        {
            pairCount = ComputeNewPairCount(pairCount, insertions);
        }
        return pairCount;
    }

    private static Dictionary<string, long> ComputeNewPairCount(Dictionary<string, long> pairCount, Dictionary<string, char> insertions)
    {
        Dictionary<string, long> newPairCount = new Dictionary<string, long>();
        foreach (KeyValuePair<string, long> pair in pairCount)
        {
            if (insertions.ContainsKey(pair.Key))
            {
                char newElement = insertions[pair.Key];
                AddPairCount(newPairCount, new string(pair.Key[0] + newElement.ToString()), pair.Value);
                AddPairCount(newPairCount, new string(newElement.ToString() + pair.Key[1]), pair.Value);
            }
        }
        return newPairCount;
    }

    private static Dictionary<char, long> ConvertPairCountToElementCount(Dictionary<string, long> pairCount)
    {
        Dictionary<char, long> elementCount = new Dictionary<char, long>();
        foreach (KeyValuePair<string, long> pair in pairCount)
        {
            //Divide each by two since each char is part of two pairs
            //Will have to add 1 back to the beginning and ending char later
            AddElementLong(elementCount, pair.Key[0], pair.Value / 2);
            AddElementLong(elementCount, pair.Key[1], pair.Value / 2);
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
            AddElementCount(elementCount, insertions[pair], 1);
            TraverseOnePair(new string(pair[0] + newElement.ToString()), insertions, elementCount, numIterations + 1, limit);
            TraverseOnePair(new string(newElement.ToString() + pair[1]), insertions, elementCount, numIterations + 1, limit);
        }
    }

    private static Dictionary<string, long> SetupPairCount(string polymer)
    {
        Dictionary<string, long> pairCount = new Dictionary<string, long>();
        for (int i = 0; i < polymer.Length - 1; i++)
        {
            AddPairCount(pairCount, new string(polymer[i].ToString() + polymer[i + 1].ToString()), 1);
        }
        return pairCount;
    }

    private static void AddElementCount(Dictionary<char, int> elementCount, char element, int count)
    {
        if (elementCount.ContainsKey(element))
        {
            elementCount[element] = elementCount[element] + count;
        }
        else
        {
            elementCount.Add(element, count);
        }
    }

    private static void AddElementLong(Dictionary<char, long> elementCount, char element, long count)
    {
        if (elementCount.ContainsKey(element))
        {
            elementCount[element] = elementCount[element] + count;
        }
        else
        {
            elementCount.Add(element, count);
        }
    }

    //Todo - If I could figure out the syntax to make this generic, this could be combined with AddElementCount
    private static void AddPairCount(Dictionary<string, long> pairCount, string pair, long count)
    {
        if (pairCount.ContainsKey(pair))
        {
            pairCount[pair] = pairCount[pair] + count;
        }
        else
        {
            pairCount.Add(pair, count);
        }
    }

    private static long DiffMaxMin(Dictionary<char, long> elementCount)
    {
        long min = 0;
        long max = 0;
        foreach (KeyValuePair<char, long> element in elementCount)
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
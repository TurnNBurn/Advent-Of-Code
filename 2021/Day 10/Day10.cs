using System;

public class AdventOfCodeDay10
{

    public static void run()
    {
        string[] lines = System.IO.File.ReadAllLines("./2021/Day 10/Problem1Input.txt");
        int totalCorruptionScore = Problem1(lines);
        long middleAutoCompleteScore = Problem2(lines);
        Console.WriteLine("Day 10 - Problem 1: The total corruption score is " + totalCorruptionScore);
        Console.WriteLine("Day 10 - Problem 2: The middle autocomplete score is " + middleAutoCompleteScore);
    }

    private static int Problem1(string[] lines)
    {
        int corruptedSum = 0;
        foreach (string line in lines)
        {
            if (CheckForCorruption(line, out char corruptedChar, out Stack<char> openChunk))
            {
                corruptedSum += CorruptedCharValues[corruptedChar];
            }
        }
        return corruptedSum;
    }

    private static long Problem2(string[] lines)
    {
        List<long> autoCompleteScores = new List<long>();
        foreach (string line in lines)
        {
            if (!CheckForCorruption(line, out char corruptedChar, out Stack<char> openChunk))
            {
                long lineScore = FindAutoCompleteScore(openChunk);
                if (lineScore > 0)
                {
                    autoCompleteScores.Add(lineScore);
                }
            }
        }
        autoCompleteScores.Sort();
        return autoCompleteScores[GetMiddleIndex(autoCompleteScores.Count - 1)];
    }

    private static int GetMiddleIndex(int count)
    {
        return (int)((count / 2.0) + 0.5);
    }

    private static bool CheckForCorruption(string line, out char corruptedChar, out Stack<char> openChunk)
    {
        corruptedChar = ' '; //Hacky and I don't like it
        openChunk = new Stack<char>();
        for (int i = 0; i < line.Length; i++)
        {
            switch (line[i])
            {
                case '(':
                    openChunk.Push('(');
                    break;
                case '[':
                    openChunk.Push('[');
                    break;
                case '{':
                    openChunk.Push('{');
                    break;
                case '<':
                    openChunk.Push('<');
                    break;
                case ')':
                case ']':
                case '}':
                case '>':
                    if (openChunk.Peek().Equals(CloseBracketPairs[line[i]]))
                    {
                        openChunk.Pop();
                    }
                    else
                    {
                        corruptedChar = line[i];
                        return true;
                    }
                    break;
                default:
                    break;
            }
        }
        return false;
    }

    private static long FindAutoCompleteScore(Stack<char> openChunk)
    {
        long autoCompleteSum = 0;
        while (openChunk.Count > 0)
        {
            autoCompleteSum *= 5;
            autoCompleteSum += AutoCompleteCharVals[openChunk.Pop()];
        }
        return autoCompleteSum;
    }

    private static Dictionary<char, char> CloseBracketPairs = new Dictionary<char, char>{
        {')', '('},
        {']', '['},
        {'}', '{'},
        {'>', '<'}
    };

    private static Dictionary<char, int> CorruptedCharValues = new Dictionary<char, int>{
        {')', 3},
        {']', 57},
        {'}', 1197},
        {'>', 25137}
    };

    private static Dictionary<char, int> AutoCompleteCharVals = new Dictionary<char, int>{
        {'(', 1},
        {'[', 2},
        {'{', 3},
        {'<', 4}
    };
}
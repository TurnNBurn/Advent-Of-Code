using System;

public class AdventOfCodeDay10
{

    public static void run()
    {
        string[] lines = System.IO.File.ReadAllLines("./Day 10/Problem1Input.txt");
        int totalCorruptionScore = Problem1(lines);
        Console.WriteLine("Day 10 - Problem 1: The total corruption score is " + totalCorruptionScore);
    }

    private static int Problem1(string[] lines)
    {
        int corruptedSum = 0;
        foreach (string line in lines)
        {
            if (CheckForCorruption(line, out char corruptedChar))
            {
                corruptedSum += CorruptedCharValues[corruptedChar];
            }
        }
        return corruptedSum;
    }

    private static bool CheckForCorruption(string line, out char corruptedChar)
    {
        corruptedChar = ' '; //Hacky and I don't like it
        Stack<char> chunk = new Stack<char>();
        for (int i = 0; i < line.Length; i++)
        {
            switch (line[i])
            {
                case '(':
                    chunk.Push('(');
                    break;
                case '[':
                    chunk.Push('[');
                    break;
                case '{':
                    chunk.Push('{');
                    break;
                case '<':
                    chunk.Push('<');
                    break;
                case ')':
                case ']':
                case '}':
                case '>':
                    if (chunk.Peek().Equals(CloseBracketPairs[line[i]]))
                    {
                        chunk.Pop();
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
}
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
        List<char> corruptedChars = new List<char>();
        foreach (string line in lines)
        {
            corruptedChars = CheckForCorruption(line, corruptedChars);
        }
        return SumCorruptedChars(corruptedChars);
    }

    private static List<char> CheckForCorruption(string line, List<char> corruptedChars)
    {
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
                        corruptedChars.Add(line[i]);
                        return corruptedChars;
                    }
                    break;
                default:
                    break;
            }
        }
        return corruptedChars;
    }

    private static int SumCorruptedChars(List<char> corruptedChars)
    {
        int sum = 0;
        foreach (char corrupted in corruptedChars)
        {
            sum += CorruptedCharValues[corrupted];
        }
        return sum;
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
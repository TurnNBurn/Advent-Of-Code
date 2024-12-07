using System.Diagnostics;

public class AdventOfCode2024Day5
{
    private const string InputFilePath = "./2024/Day 5/Problem1Input.txt";
    public static void Run()
    {
        var lines = File.ReadAllLines(InputFilePath);
        var rules = ParseRules(lines, out var splitIndex);
        var timer = new Stopwatch();
        timer.Start();
        Console.WriteLine($"Day 5 - The sum of the middle pages of correct updates is {Problem1(lines, rules, splitIndex)}");
        var elapsed = timer.Elapsed;
        Console.WriteLine($"Day 5 Part 1 took {elapsed}");
        timer.Reset();
        timer.Start();
        Console.WriteLine($"Day 5 - The sum of the middle pages of the incorrect updates is {Problem2(lines, rules, splitIndex)}");
        elapsed = timer.Elapsed;
        timer.Stop();
        Console.WriteLine($"Day 5 Part 2 took {elapsed}");
    }

    private static int Problem1(string[] lines, Dictionary<int, List<int>> rules, int splitIndex)
    {
        int middleSum = 0;
        for (int i = splitIndex + 1; i < lines.Length; i++)
        {
            var pages = lines[i].Split(',').Select(int.Parse).ToArray();
            if (IsInOrder(pages, rules))
                middleSum += pages[pages.Length / 2];
        }
        return middleSum;
    }

    private static bool IsInOrder(int[] pages, Dictionary<int, List<int>> rules)
    {
        for (int i = pages.Length - 1; i > 0; i--)
        {
            if (!rules.TryGetValue(pages[i], out var currentRules))
                continue;
            for (int j = i - 1; j > -1; j--)
                if (currentRules.Contains(pages[j]))
                    return false;
        }
        return true;
    }

    private static Dictionary<int, List<int>> ParseRules(string[] lines, out int splitIndex)
    {
        Dictionary<int, List<int>> rules = new();
        splitIndex = 0;
        for (int i = 0; i < lines.Length; i++)
        {
            if (lines[i].Trim().Equals(string.Empty))
            {
                splitIndex = i;
                return rules;
            }
            string[] pieces = lines[i].Split('|');
            int key = int.Parse(pieces[0].Trim());
            int val = int.Parse(pieces[1].Trim());
            if (rules.ContainsKey(key))
                rules[key].Add(val);
            else
                rules.Add(key, new List<int> { val });
        }
        return rules;
    }

    private static int Problem2(string[] lines, Dictionary<int, List<int>> rules, int splitIndex)
    {
        int middleSum = 0;
        for (int i = splitIndex + 1; i < lines.Length; i++)
        {
            var pages = lines[i].Split(',').Select(int.Parse).ToArray();
            if (!IsInOrder(pages, rules))
                middleSum += SortedMiddlePageNum(pages, rules);
        }
        return middleSum;
    }

    private static int SortedMiddlePageNum(int[] pages, Dictionary<int, List<int>> rules)
    {
        return SortPages(pages, rules)[pages.Length / 2];
    }

    private static int[] SortPages(int[] pages, Dictionary<int, List<int>> rules)
    {
        for (int i = pages.Length - 1; i > 0; i--)
        {
            if (!rules.TryGetValue(pages[i], out var currentRules))
                continue;
            for (int j = i - 1; j > -1; j--)
                if (currentRules.Contains(pages[j]))
                {
                    (pages[i], pages[j]) = (pages[j], pages[i]);
                    return SortPages(pages, rules);
                }
        }
        return pages;
    }
}
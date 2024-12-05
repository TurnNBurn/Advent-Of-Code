public class AdventOfCode2024Day5
{
    private const string InputFilePath = "./2024/Day 5/Problem1Input.txt";
    public static void Run()
    {
        var lines = System.IO.File.ReadAllLines(InputFilePath);
        Console.WriteLine($"Day 5 - The sum of the middle pages of correct updates is {Problem1(lines)}");
        Console.WriteLine($"Day 5 - There are {Problem2(lines)} x-mas's in the word search");
    }

    private static int Problem1(string[] lines)
    {
        var rules = ParseRules(lines, out var splitIndex);
        int middleSum = 0;
        for (int i = splitIndex + 1; i < lines.Length; i++)
            if (IsInOrder(lines[i], rules))
                middleSum += MiddlePageNum(lines[i]);
        return middleSum;
    }

    private static bool IsInOrder(string line, Dictionary<int, List<int>> rules)
    {
        int[] pages = line.Split(',').Select(int.Parse).ToArray();
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

    private static int MiddlePageNum(string line)
    {
        string[] pages = line.Split(',');
        return int.Parse(pages[pages.Length / 2]);
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

    private static int Problem2(string[] lines)
    {
        return 0;
    }
}
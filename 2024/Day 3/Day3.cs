using System.Diagnostics;
using System.Text.RegularExpressions;

public class AdventOfCode2024Day3
{
    private const string InputFilePath = "./2024/Day 3/Problem1Input.txt";
    public static void Run()
    {
        var lines = File.ReadAllLines(InputFilePath);
        var timer = new Stopwatch();
        timer.Start();
        Console.WriteLine($"Day 3 - The sum of the multiplication is: {Problem1(lines)}");
        var elapsed = timer.Elapsed;
        Console.WriteLine($"Day 3 Part 1 took {elapsed}");
        timer.Reset();
        timer.Start();
        Console.WriteLine($"Day 3 - The sum of the enabled mutliplication is {Problem2(lines)}");
        elapsed = timer.Elapsed;
        timer.Stop();
        Console.WriteLine($"Day 3 Part 2 took {elapsed}");
    }

    private static int Problem1(string[] lines)
    {
        return lines.Sum(ProcessOneLine);
    }

    private static int ProcessOneLine(string line)
    {
        const string pattern = @"mul\(([0-9,]{1,7})\)";
        var matches = Regex.Matches(line, pattern);

        return matches.Sum(match =>
        {
            var variables = match.Groups[1].Value.Split(",");
            return variables.Length >= 2
            ? ExtractVariable(variables[0]) * ExtractVariable(variables[1])
            : 0;
        });
    }

    private static int ExtractVariable(string input)
    {
        return int.Parse(new string(input.Where(char.IsDigit).ToArray()));
    }

    private static int Problem2(string[] lines)
    {
        const string doPattern = @"do\(\)";
        const string dontPattern = @"don't\(\)";

        int sum = 0;
        bool isEnabled = true;

        foreach (string line in lines)
        {
            var doMatches = Regex.Matches(line, doPattern);
            var dontMatches = Regex.Matches(line, dontPattern);

            int startIndex = isEnabled ? 0 : doMatches[0].Index;
            int dontIndex = 0;
            while (dontMatches[dontIndex].Index < startIndex)
                dontIndex++;
            int doIndex = 0;

            while (dontIndex < dontMatches.Count && doIndex < doMatches.Count)
            {
                sum += ProcessOneLine(line[startIndex..dontMatches[dontIndex].Index]);
                while (doIndex < doMatches.Count && doMatches[doIndex].Index < dontMatches[dontIndex].Index)
                    doIndex++;
                if (doIndex >= doMatches.Count)
                {
                    isEnabled = false;
                    continue;
                }
                while (dontIndex < dontMatches.Count && doMatches[doIndex].Index > dontMatches[dontIndex].Index)
                    dontIndex++;
                startIndex = doMatches[doIndex].Index;
            }

            if (doIndex < doMatches.Count)
            {
                sum += ProcessOneLine(line[startIndex..]);
                isEnabled = true;
            }
        }
        return sum;
    }
}
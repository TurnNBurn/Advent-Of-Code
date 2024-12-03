using System.Text;
using System.Text.RegularExpressions;

public class AdventOfCode2024Day3
{
    public static void Run()
    {
        string[] lines = System.IO.File.ReadAllLines("./2024/Day 3/Problem1Input.txt");
        int sum = Problem1(lines);
        Console.WriteLine("Day 3 - The sum of the multiplication is: " + sum);
        int enabledSum = Problem2(lines);
        Console.WriteLine("Day 3 - The sum of the enabled mutliplication is " + enabledSum);
    }

    private static int Problem1(string[] lines)
    {
        var sum = 0;
        foreach (string line in lines)
            sum += ProcessOneLine(line);
        return sum;
    }

    private static int ProcessOneLine(string line)
    {
        string pattern = @"mul\((.{1,7})\)";
        MatchCollection matches = Regex.Matches(line, pattern);

        int sum = 0;
        foreach (Match match in matches)
        {
            string[] variables = match.Groups[1].Value.Split(",");
            if (variables.Length < 2)
                continue;
            sum += ExtractVariable(variables[0]) * ExtractVariable(variables[1]);
        }

        return sum;
    }

    private static int ExtractVariable(string input)
    {
        StringBuilder output = new StringBuilder();
        for (int i = 0; i < input.Length; i++)
            if (char.IsNumber(input[i]))
                output.Append(input[i]);

        return int.Parse(output.ToString());
    }

    private static int Problem2(string[] lines)
    {
        int sum = 0;
        foreach (string line in lines)
        {
            sum += ProcessLineForDos(line);
        }
        return sum;
    }

    private static int ProcessLineForDos(string line)
    {
        int sum = 0;
        string doPattern = @"(?<!don')\bdo\b";
        string dontPattern = @"\bdon't\b";

        MatchCollection doMatches = Regex.Matches(line, doPattern);
        MatchCollection dontMatches = Regex.Matches(line, dontPattern);

        int dontIndex = 0;
        if (dontMatches[0].Index < doMatches[0].Index)
        {
            dontIndex = 1;
            sum += ProcessOneLine(line.Substring(0, dontMatches[0].Index));
        }

        for (int i = 0; i < doMatches.Count; i++)
        {
            if (i >= dontMatches.Count)
                return sum + ProcessOneLine(line.Substring(doMatches[i].Index));
            int length = dontMatches[i + dontIndex].Index - doMatches[i].Index;
            sum += ProcessOneLine(line.Substring(doMatches[i].Index, length));
        }

        return sum;
    }
}
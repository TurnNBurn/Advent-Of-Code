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
        string pattern = @"mul\(([0-9,]{1,7})\)";
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
        string doPattern = @"do\(\)";
        string dontPattern = @"don't\(\)";
        bool isEnabled = true;
        foreach (string line in lines)
        {
            MatchCollection doMatches = Regex.Matches(line, doPattern);
            MatchCollection dontMatches = Regex.Matches(line, dontPattern);

            Console.WriteLine($"There are {dontMatches.Count} dont's and {doMatches.Count} do's");

            int startIndex = isEnabled ? 0 : doMatches[0].Index;
            int dontIndex = 0;
            while (dontMatches[dontIndex].Index < startIndex)
                dontIndex++;
            int doIndex = 0;

            while (dontIndex < dontMatches.Count && doIndex < doMatches.Count)
            {
                Console.WriteLine($"{startIndex} : {dontMatches[dontIndex].Index}");
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
                Console.WriteLine($"Test: {startIndex}");
            }
        }
        return sum;
    }
}
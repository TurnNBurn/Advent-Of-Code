using System.Text;
using System.Text.RegularExpressions;

public class AdventOfCode2024Day3
{
    public static void Run()
    {
        string[] lines = System.IO.File.ReadAllLines("./2024/Day 3/Problem1Input.txt");
        int sum = Problem1(lines);
        Console.WriteLine("Day 3 - The sum of the multiplication is: " + sum);
        int similarityScore = Problem2(lines);
        Console.WriteLine("Day 3 - Problem 2: there are " + similarityScore + " safe reports with the dampener");
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
        return 0;
    }
}
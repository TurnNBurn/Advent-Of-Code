using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

public class AdventOfCode2023Day1
{

    public static void run()
    {
        string[] lines = System.IO.File.ReadAllLines("./2023/Day 1/Problem1Input.txt");
        int calibrationSum = Problem1(lines);
        Console.WriteLine("Day 1 - Problem 1: the sum of the calibration values is " + calibrationSum);
        int realCalibrationSum = Problem2(lines);
        Console.WriteLine("Day 1 - Problem 2: the correct sum of the calibration values is " + realCalibrationSum);
    }

    private static int Problem1(string[] lines)
    {
        int sum = 0;
        foreach (string line in lines)
        {
            string filteredLine = new string(line.Where(char.IsDigit).ToArray());
            string calibrationText = filteredLine.Length > 1 ? $"{filteredLine[0]}{filteredLine[^1]}" : new string(filteredLine[0], 2);
            sum += Convert.ToInt32(calibrationText);
        }
        return sum;
    }

    private static int Problem2(string[] lines)
    {
        int sum = 0;
        string pattern = string.Join("|", Substrings().Select(Regex.Escape));
        string[] reversedSubstring = Substrings().Select(s => new string(s.Reverse().ToArray())).ToArray();
        string reversedPattern = string.Join("|", reversedSubstring.Select(Regex.Escape));

        foreach (string line in lines)
        {
            Match firstMatch = Regex.Match(line, pattern);
            Match lastMatch = Regex.Match(new string(line.Reverse().ToArray()), reversedPattern);
            string lastDigit = new string(lastMatch.Value.Reverse().ToArray());
            sum += Convert.ToInt32($"{ReplaceWordWithNum(firstMatch.Value)}{ReplaceWordWithNum(lastDigit)}");
        }
        return sum;
    }

    private static string[] Substrings()
    {
        return new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
    }

    private static string ReplaceWordWithNum(string word)
    {
        word = word.Replace("one", "1");
        word = word.Replace("two", "2");
        word = word.Replace("three", "3");
        word = word.Replace("four", "4");
        word = word.Replace("five", "5");
        word = word.Replace("six", "6");
        word = word.Replace("seven", "7");
        word = word.Replace("eight", "8");
        word = word.Replace("nine", "9");

        return word;
    }
}
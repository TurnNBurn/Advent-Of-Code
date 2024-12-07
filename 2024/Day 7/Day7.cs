using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

public class AdventOfCode2024Day7
{
    private const string InputFilePath = "./2024/Day 7/Problem1Input.txt";
    public static void Run()
    {
        var lines = System.IO.File.ReadAllLines(InputFilePath);
        Console.WriteLine($"Day 7 - The sum of valid equations is {Problem1(lines)}");
        Console.WriteLine($"Day 7 - The sum of valid equations with concat is {Problem2(lines)}");
    }

    private static long Problem1(string[] lines)
    {
        long validSum = 0;
        foreach (string line in lines)
        {
            var pieces = line.Split(": ");
            if (IsValidEquation(long.Parse(pieces[0]), pieces[1].Split(' ').Select(i => int.Parse(i)).ToArray(), false))
                validSum += long.Parse(pieces[0]);
        }
        return validSum;
    }

    private static bool IsValidEquation(long result, int[] components, bool tryConcat)
    {
        if (components.Length == 1)
            return result == components[0];

        var lastEntry = components[^1];
        if (tryConcat && IsAppended(result, lastEntry))
            if (IsValidEquation(RemoveDigits(result, lastEntry), components.Take(components.Length - 1).ToArray(), tryConcat))
                return true;
        if (result % lastEntry == 0)
            if (IsValidEquation(result / lastEntry, components.Take(components.Length - 1).ToArray(), tryConcat))
                return true;
        return IsValidEquation(result - lastEntry, components.Take(components.Length - 1).ToArray(), tryConcat);
    }

    private static bool IsAppended(long x, int y)
    {
        int yDigits = GetNumDigits(y);
        long lastDigitsOfX = x % TenToPower(yDigits); // Last yDigits of x
        return lastDigitsOfX == y;
    }

    private static long RemoveDigits(long x, int y)
    {
        int yDigits = GetNumDigits(y);
        return x / TenToPower(yDigits);
    }

    private static int GetNumDigits(int num)
    {
        return (int)Math.Log10(num) + 1;
    }

    private static int TenToPower(int power)
    {
        var ten = 10;
        for (int i = 1; i < power; i++)
            ten *= 10;
        return ten;
    }

    private static long Problem2(string[] lines)
    {
        long validSum = 0;
        foreach (string line in lines)
        {
            var pieces = line.Split(": ");
            var result = long.Parse(pieces[0]);
            var components = pieces[1].Split(' ').Select(i => int.Parse(i)).ToArray();
            if (IsValidEquation(result, components, true))
                validSum += result;
        }
        return validSum;
    }
}
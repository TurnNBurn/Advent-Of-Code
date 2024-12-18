using System.Diagnostics;

public class AdventOfCode2024Day2
{

    public static void Run()
    {
        string[] lines = File.ReadAllLines("./2024/Day 2/Problem1Input.txt");
        var timer = new Stopwatch();
        timer.Start();
        Console.WriteLine($"Day 2 - Problem 1: there are {Problem1(lines)} safe reports");
        var elapsed = timer.Elapsed;
        Console.WriteLine($"Day 2 Part 1 took {elapsed}");
        timer.Reset();
        timer.Start();
        Console.WriteLine("Day 2 - Problem 2: there are {Problem2(lines)} safe reports with the dampener");
        elapsed = timer.Elapsed;
        timer.Stop();
        Console.WriteLine($"Day 2 Part 2 took {elapsed}");
    }

    private static int Problem1(string[] lines)
    {
        return lines.Count(line => ProcessOneLine(Array.ConvertAll(line.Split(" "), int.Parse)));
    }

    private static bool ProcessOneLine(int[] input)
    {
        bool? isIncreasing = null;
        for (int i = 0; i < input.Length - 1; i++)
        {
            int diff = input[i + 1] - input[i];
            if (Math.Abs(diff) > 3)
                return false;
            if (diff == 0)
                return false;

            if (isIncreasing == null)
                isIncreasing = diff > 0;
            else if ((isIncreasing == true && diff < 0) || (isIncreasing == false && diff > 0))
                return false;
        }
        return true;
    }

    private static int Problem2(string[] lines)
    {
        return lines.Count(line => ProcessOneLineWithDampener(Array.ConvertAll(line.Split(" "), int.Parse)));
    }

    private static bool ProcessOneLineWithDampener(int[] levels)
    {
        //The below algorithm will not catch if the first element is off, so check that first
        if (ProcessOneLine(levels.TakeLast(levels.Length - 1).ToArray()))
            return true;
        var isIncreasing = levels[1] - levels[0] > 0;
        for (int i = 0; i < levels.Length - 1; i++)
        {
            if (!CheckTwoLevels(levels[i], levels[i + 1], isIncreasing))
            {
                if (ProcessOneLine(levels.Where((value, index) => index != i).ToArray()))
                    return true;
                return ProcessOneLine(levels.Where((value, index) => index != (i + 1)).ToArray());
            }
        }
        return true;
    }

    private static bool CheckTwoLevels(int left, int right, bool isIncreasing)
    {
        var diff = right - left;
        if (Math.Abs(diff) > 3)
            return false;
        if (diff == 0)
            return false;
        return isIncreasing ? diff > 0 : diff < 0;
    }
}
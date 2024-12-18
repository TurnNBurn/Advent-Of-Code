using System.Diagnostics;

public class AdventOfCode2024Day1
{

    public static void Run()
    {
        string[] lines = File.ReadAllLines("./2024/Day 1/Problem1Input.txt");
        var timer = new Stopwatch();
        timer.Start();
        Console.WriteLine($"Day 1 - Problem 1: the total distances in the two lists is {Problem1(lines)}");
        var elapsed = timer.Elapsed;
        Console.WriteLine($"Day 1 Part 1 took {elapsed}");
        timer.Restart();
        timer.Start();
        Console.WriteLine($"Day 1 - Problem 2: the lists similarity score is {Problem2(lines)}");
        elapsed = timer.Elapsed;
        timer.Stop();
        Console.WriteLine($"Day 1 Part 2 took {elapsed}");
    }

    private static int Problem1(string[] lines)
    {
        var leftSide = new int[lines.Length];
        var rightSide = new int[lines.Length];
        for (int i = 0; i < lines.Length; i++)
        {
            var line = lines[i].Split("   ");
            leftSide[i] = int.Parse(line[0]);
            rightSide[i] = int.Parse(line[1]);
        }
        Array.Sort(leftSide);
        Array.Sort(rightSide);
        int ret = 0;
        for (int i = 0; i < lines.Length; i++)
        {
            ret += Math.Abs(leftSide[i] - rightSide[i]);
        }
        return ret;
    }

    private static int Problem2(string[] lines)
    {
        var leftSide = new int[lines.Length];
        var rightSide = new Dictionary<int, int>();
        for (int i = 0; i < lines.Length; i++)
        {
            var line = lines[i].Split("   ");
            leftSide[i] = int.Parse(line[0]);
            var rightValue = int.Parse(line[1]);
            if (rightSide.TryGetValue(rightValue, out var currentVal))
                rightSide[rightValue] = currentVal + 1;
            else
                rightSide.Add(rightValue, 1);
        }

        int ret = 0;
        foreach (var val in leftSide)
            if (rightSide.TryGetValue(val, out var numTimes))
                ret += val * numTimes;

        return ret;
    }
}


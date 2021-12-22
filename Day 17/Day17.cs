using System;

public class AdventOfCodeDay17
{

    public static void run()
    {
        string[] lines = System.IO.File.ReadAllLines("./Day 17/Problem1Input.txt");
        int versionSum = Problem1(lines);
        Console.WriteLine("Day 17 - Problem 1: The highest y point acheived is " + versionSum);
    }

    private static int Problem1(string[] lines)
    {
        List<Coordinates> targetRange = ParseInput(lines);
        return FindMaxY(targetRange);
    }

    private static int FindMaxY(List<Coordinates> targetRange)
    {
        int yMax = 0;
        for (int x = 0; x < targetRange[1].x; x++)
        {
            if (MaxX(x) >= targetRange[0].x)
            {
                for (int y = 0; y < 1000; y++)
                {
                    if (VelocityInRange(x, y, targetRange))
                    {
                        yMax = Math.Max(yMax, (y * (y + 1)) / 2);
                    }
                }
            }
        }
        return yMax;
    }

    private static bool VelocityInRange(int x, int y, List<Coordinates> targetRange)
    {
        bool passedRange = false;
        int step = 0;
        int ySum = 0;
        int xSum = 0;
        while (!passedRange)
        {
            if (step <= x)
            {
                xSum += x - step;
            }
            ySum += y - step;
            step++;
            if (CoordinateInRange(new Coordinates(xSum, ySum), targetRange))
            {
                return true;
            }
            if (xSum > targetRange[1].x || ySum < targetRange[1].y)
            {
                passedRange = true;
            }
        }
        return false;
    }

    private static int MaxX(int initialX)
    {
        return (initialX * (initialX + 1)) / 2;
    }

    private static List<Coordinates> ParseInput(string[] lines)
    {
        List<Coordinates> targetRange = new List<Coordinates>();
        string[] input = lines[0].Split(',');
        string[] xRange = input[0].Split('=');
        string[] yRange = input[1].Split('=');
        int firstPeriod = xRange[1].IndexOf('.');
        int xMin = Convert.ToInt32(xRange[1].Substring(0, firstPeriod));
        int xMax = Convert.ToInt32(xRange[1].Substring(firstPeriod + 2));
        firstPeriod = yRange[1].IndexOf('.');
        int yMin = Convert.ToInt32(yRange[1].Substring(0, firstPeriod));
        int yMax = Convert.ToInt32(yRange[1].Substring(firstPeriod + 2));
        targetRange.Add(new Coordinates(xMin, yMin));
        targetRange.Add(new Coordinates(xMax, yMax));
        return targetRange;
    }

    private static bool CoordinateInRange(Coordinates coordinate, List<Coordinates> targetRange)
    {
        if (coordinate.x < targetRange[0].x)
        {
            return false;
        }
        if (coordinate.x > targetRange[1].x)
        {
            return false;
        }
        if (coordinate.y < targetRange[0].y)
        {
            return false;
        }
        if (coordinate.y > targetRange[1].y)
        {
            return false;
        }
        return true;
    }
}


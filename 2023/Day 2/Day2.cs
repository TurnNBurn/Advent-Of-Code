public class AdventOfCode2023Day2
{

    public static void run()
    {
        string[] lines = System.IO.File.ReadAllLines("./2023/Day 2/Problem1Input.txt");
        int gameSum = Problem1(lines);
        Console.WriteLine("Day 2 - Problem 1: the sum of the possible games is " + gameSum);
        int minPower = Problem2(lines);
        Console.WriteLine("Day 2 - Problem 2: the minimum power of the games is " + minPower);
    }

    private static int Problem1(string[] lines)
    {
        int sum = 0;
        foreach (string line in lines)
        {
            string[] pieces = line.Split(':');
            if (ProcessOneGame(pieces[1]))
                sum += Convert.ToInt16(pieces[0].Split(" ")[1]);
        }
        return sum;
    }

    private static bool ProcessOneGame(string game)
    {
        string[] rounds = game.Split(';');
        foreach (string round in rounds)
        {
            if (!ProcessOneRound(round))
                return false;
        }
        return true;
    }

    private static bool ProcessOneRound(string round)
    {
        string[] colors = round.Split(',');
        foreach (string color in colors)
        {
            if (!ProcessOneColor(color))
                return false;
        }
        return true;
    }

    private static bool ProcessOneColor(string color)
    {
        color = color.Trim();
        int num = Convert.ToInt32(color.Split(" ")[0]);
        if (color.Contains("red"))
            return num < 13;
        else if (color.Contains("blue"))
            return num < 15;
        else if (color.Contains("green"))
            return num < 14;
        else
            return false;
    }

    private static int Problem2(string[] lines)
    {
        int sum = 0;
        foreach (string line in lines)
        {
            string[] pieces = line.Split(':');
            sum += CalculateOneGame(pieces[1]);
        }
        return sum;
    }

    private static int CalculateOneGame(string game)
    {
        int minGreen = int.MinValue;
        int mindRed = int.MinValue;
        int minBlue = int.MinValue;
        string[] rounds = game.Split(';');
        foreach (string round in rounds)
        {
            string[] colors = round.Split(',');
            foreach (string color in colors)
            {
                int num = Convert.ToInt32(color.Trim().Split(" ")[0]);
                if (color.Contains("green"))
                    minGreen = Math.Max(minGreen, num);
                else if (color.Contains("blue"))
                    minBlue = Math.Max(minBlue, num);
                else if (color.Contains("red"))
                    mindRed = Math.Max(mindRed, num);
            }
        }

        return minGreen * mindRed * minBlue;
    }
}
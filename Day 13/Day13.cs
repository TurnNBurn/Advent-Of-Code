using System;

public class AdventOfCodeDay13
{

    public static void run()
    {
        string[] lines = System.IO.File.ReadAllLines("./Day 13/Problem1Input.txt");
        int totalDots = Problem1(lines);
        Console.WriteLine("Day 13 - Problem 1: There are " + totalDots + " after folding the paper once.");
    }

    private static int Problem1(string[] lines)
    {
        List<Coordinates> dots = ParseCoordinates(lines);
        List<string> foldInstructions = ParseFoldInstructions(lines, dots.Count);
        dots = FoldPaper(dots, foldInstructions);
        return dots.Count;
    }

    private static List<Coordinates> ParseCoordinates(string[] lines)
    {
        List<Coordinates> dots = new List<Coordinates>();
        foreach (string line in lines)
        {
            if (line.Contains(','))
            {
                Coordinates newCoord = new Coordinates(Convert.ToInt32(line.Split(',')[0]), Convert.ToInt32(line.Split(',')[1]));
                dots.Add(newCoord);
            }
        }
        return dots;
    }

    private static List<string> ParseFoldInstructions(string[] lines, int dotCount)
    {
        List<string> foldInstructions = new List<string>();
        for (int i = dotCount; i < lines.Length; i++)
        {
            if (lines[i].Contains('='))
            {
                foldInstructions.Add(lines[i].Split(' ')[2]);
            }
        }
        return foldInstructions;
    }

    private static List<Coordinates> FoldPaper(List<Coordinates> dots, List<string> foldInstructions)
    {
        for (int i = 0; i < 1; i++)
        {
            dots = DoOneFold(dots, foldInstructions[i]);
        }
        return dots;
    }

    private static List<Coordinates> DoOneFold(List<Coordinates> dots, string instruction)
    {
        string[] instructionString = instruction.Split('=');
        if (instructionString[0].Equals("x"))
        {
            return FoldVertical(dots, Convert.ToInt32(instructionString[1]));
        }
        else
        {
            return FoldHorizontal(dots, Convert.ToInt32(instructionString[1]));
        }
    }

    private static List<Coordinates> FoldHorizontal(List<Coordinates> dots, int foldLine)
    {
        List<Coordinates> newDots = new List<Coordinates>();
        foreach (Coordinates dot in dots)
        {
            if (dot.y < foldLine)
            {
                if (!newDots.Contains(dot))
                {
                    newDots.Add(dot);
                }
            }
            else if (dot.y > foldLine)
            {
                Coordinates newDot = new Coordinates(dot.x, foldLine - (dot.y - foldLine));
                if (!newDots.Contains(newDot))
                {
                    newDots.Add(newDot);
                }
            }
        }
        return newDots;
    }

    private static List<Coordinates> FoldVertical(List<Coordinates> dots, int foldLine)
    {
        List<Coordinates> newDots = new List<Coordinates>();
        foreach (Coordinates dot in dots)
        {
            if (dot.x < foldLine)
            {
                if (!newDots.Contains(dot))
                {
                    newDots.Add(dot);
                }
            }
            else if (dot.x > foldLine)
            {
                Coordinates newDot = new Coordinates(foldLine - (dot.x - foldLine), dot.y);
                if (!newDots.Contains(newDot))
                {
                    newDots.Add(newDot);
                }
            }
        }
        return newDots;
    }

    public class Coordinates
    {
        public Coordinates(int X, int Y)
        {
            x = X;
            y = Y;
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as Coordinates);
        }

        public bool Equals(Coordinates? other)
        {
            return other != null && other.x == x && other.y == y;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(x, y);
        }
        public int x;
        public int y;
    }
}
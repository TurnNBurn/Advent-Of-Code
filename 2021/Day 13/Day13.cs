using System;
using System.Text;

public class AdventOfCodeDay13
{

    public static void run()
    {
        string[] lines = System.IO.File.ReadAllLines("./2021/Day 13/Problem1Input.txt");
        int totalDots = Problem1(lines);
        Problem2(lines);
        Console.WriteLine("Day 13 - Problem 1: There are " + totalDots + " after folding the paper once.");
        //Console.WriteLine("Day 13 - Problem 2: After folding the paper the code to enter is " + finalCode);
    }

    private static int Problem1(string[] lines)
    {
        List<Coordinates> dots = ParseCoordinates(lines);
        List<string> foldInstructions = ParseFoldInstructions(lines, dots.Count);
        dots = DoOneFold(dots, foldInstructions[0]);
        return dots.Count;
    }

    private static void PrintDots(List<Coordinates> dots)
    {
        Dictionary<int, List<int>> dotMap = new Dictionary<int, List<int>>();
        int maxX = 0;
        int maxY = 0;
        foreach (Coordinates dot in dots)
        {
            maxX = dot.x > maxX ? dot.x : maxX;
            maxY = dot.y > maxY ? dot.y : maxY;
            if (dotMap.ContainsKey(dot.y))
            {
                dotMap[dot.y].Add(dot.x);
            }
            else
            {
                dotMap.Add(dot.y, new List<int>());
                dotMap[dot.y].Add(dot.x);
            }
        }

        string[] graphicMap = new string[maxY + 1];
        for (int i = 0; i <= maxY; i++)
        {
            StringBuilder mapLine = new StringBuilder("");
            if (dotMap.ContainsKey(i))
            {

                for (int j = 0; j <= maxX; j++)
                {
                    if (dotMap[i].Contains(j))
                    {
                        mapLine.Append('#');
                    }
                    else
                    {
                        mapLine.Append('.');
                    }
                }
            }
            else
            {
                mapLine.Append('.', 100);
            }
            graphicMap[i] = mapLine.ToString();
        }
        File.WriteAllLinesAsync("./2021/Day 13/Problem2Output.txt", graphicMap);
    }

    private static void Problem2(string[] lines)
    {
        List<Coordinates> dots = ParseCoordinates(lines);
        List<string> foldInstructions = ParseFoldInstructions(lines, dots.Count);
        dots = FoldPaper(dots, foldInstructions);
        PrintDots(dots);
        ;
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
        for (int i = 0; i < foldInstructions.Count; i++)
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
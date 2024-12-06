using System.Runtime.CompilerServices;
using System.Text;

public class AdventOfCode2024Day6
{
    private const string InputFilePath = "./2024/Day 6/Problem1Input.txt";
    public static void Run()
    {
        var lines = System.IO.File.ReadAllLines(InputFilePath);
        Console.WriteLine($"Day 6 - The guard traverses {Problem1(lines)} squares");
        lines = System.IO.File.ReadAllLines(InputFilePath);
        Console.WriteLine($"Day 6 - There are {Problem2(lines)} places adding an obstacle would trap the guard");
    }

    private static int Problem1(string[] lines)
    {
        (int x, int y) startPosition = FindStartPosition(lines);
        MarkAsVisited(lines, startPosition);
        return TravelUp(lines, startPosition, 1);
    }

    private static int TravelUp(string[] lines, (int x, int y) startPosition, int numVisited)
    {
        while (startPosition.y > 0)
        {
            char nextChar = lines[startPosition.y - 1][startPosition.x];
            if (nextChar.Equals('#'))
                return TravelRight(lines, startPosition, numVisited);
            startPosition.y--;
            if (!nextChar.Equals('X'))
            {
                numVisited++;
                MarkAsVisited(lines, startPosition);
            }
        }
        return numVisited;
    }

    private static int TravelRight(string[] lines, (int x, int y) startPosition, int numVisited)
    {
        while (startPosition.x < lines[startPosition.y].Length - 1)
        {
            char nextChar = lines[startPosition.y][startPosition.x + 1];
            if (nextChar.Equals('#'))
                return TravelDown(lines, startPosition, numVisited);
            startPosition.x++;
            if (!nextChar.Equals('X'))
            {
                numVisited++;
                MarkAsVisited(lines, startPosition);
            }
        }
        return numVisited;
    }

    private static int TravelDown(string[] lines, (int x, int y) startPosition, int numVisited)
    {
        while (startPosition.y < lines.Length - 1)
        {
            char nextChar = lines[startPosition.y + 1][startPosition.x];
            if (nextChar.Equals('#'))
                return TravelLeft(lines, startPosition, numVisited);
            startPosition.y++;
            if (!nextChar.Equals('X'))
            {
                numVisited++;
                MarkAsVisited(lines, startPosition);
            }
        }
        return numVisited;
    }

    private static int TravelLeft(string[] lines, (int x, int y) startPosition, int numVisited)
    {
        while (startPosition.x > 0)
        {
            char nextChar = lines[startPosition.y][startPosition.x - 1];
            if (nextChar.Equals('#'))
                return TravelUp(lines, startPosition, numVisited);
            startPosition.x--;
            if (!nextChar.Equals('X'))
            {
                numVisited++;
                MarkAsVisited(lines, startPosition);
            }
        }
        return numVisited;
    }

    private static void MarkAsVisited(string[] lines, (int x, int y) startPosition)
    {
        var stringBuilder = new StringBuilder(lines[startPosition.y]);
        stringBuilder[startPosition.x] = 'X';
        lines[startPosition.y] = stringBuilder.ToString();
    }

    private static (int, int) FindStartPosition(string[] lines)
    {
        for (int i = 0; i < lines.Length; i++)
        {
            int charIndex = lines[i].IndexOf('^');
            if (charIndex > -1)
                return (charIndex, i);
        }

        throw new InvalidOperationException("Input did not contain \'^\'");
    }

    private static int Problem2(string[] lines)
    {
        int numCycles = 0;
        var startPosition = FindStartPosition(lines);
        for (int i = 0; i < lines.Length; i++)
            for (int j = 0; j < lines[i].Length; j++)
            {
                if (lines[i][j].Equals('#') || (i == startPosition.Item2 && j == startPosition.Item1))
                    continue;
                AddNewObstacle(lines, (j, i));
                if (TravelUpIsCycle(lines, startPosition, new()))
                    numCycles++;
                RemoveObstacle(lines, (j, i));
            }
        return numCycles;
    }

    private static void AddNewObstacle(string[] lines, (int x, int y) position)
    {
        var stringBuilder = new StringBuilder(lines[position.y]);
        stringBuilder[position.x] = '#';
        lines[position.y] = stringBuilder.ToString();
    }

    private static void RemoveObstacle(string[] lines, (int x, int y) position)
    {
        var stringBuilder = new StringBuilder(lines[position.y]);
        stringBuilder[position.x] = '.';
        lines[position.y] = stringBuilder.ToString();
    }

    private static bool TravelUpIsCycle(string[] lines, (int x, int y) startPosition, HashSet<(int, int, string)> visited)
    {
        while (startPosition.y > 0)
        {
            char nextChar = lines[startPosition.y - 1][startPosition.x];
            if (nextChar.Equals('#'))
                return TravelRightIsCycle(lines, startPosition, visited);
            startPosition.y--;
            if (!visited.Add((startPosition.x, startPosition.y, "up")))
                return true;
        }
        return false;
    }

    private static bool TravelRightIsCycle(string[] lines, (int x, int y) startPosition, HashSet<(int, int, string)> visited)
    {
        while (startPosition.x < lines[startPosition.y].Length - 1)
        {
            char nextChar = lines[startPosition.y][startPosition.x + 1];
            if (nextChar.Equals('#'))
                return TravelDownIsCycle(lines, startPosition, visited);
            startPosition.x++;
            if (!visited.Add((startPosition.x, startPosition.y, "right")))
                return true;
        }
        return false;
    }

    private static bool TravelDownIsCycle(string[] lines, (int x, int y) startPosition, HashSet<(int, int, string)> visited)
    {
        while (startPosition.y < lines.Length - 1)
        {
            char nextChar = lines[startPosition.y + 1][startPosition.x];
            if (nextChar.Equals('#'))
                return TravelLeftIsCycle(lines, startPosition, visited);
            startPosition.y++;
            if (!visited.Add((startPosition.x, startPosition.y, "down")))
                return true;
        }
        return false;
    }

    private static bool TravelLeftIsCycle(string[] lines, (int x, int y) startPosition, HashSet<(int, int, string)> visited)
    {
        while (startPosition.x > 0)
        {
            char nextChar = lines[startPosition.y][startPosition.x - 1];
            if (nextChar.Equals('#'))
                return TravelUpIsCycle(lines, startPosition, visited);
            startPosition.x--;
            if (!visited.Add((startPosition.x, startPosition.y, "left")))
                return true;
        }
        return false;
    }

}
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;

public class AdventOfCode2024Day6
{
    private const string InputFilePath = "./2024/Day 6/Problem1Input.txt";
    public static void Run()
    {
        var lines = File.ReadAllLines(InputFilePath);
        var visited = new HashSet<(int, int, string)>();
        var timer = new Stopwatch();
        timer.Start();
        var startPosition = FindStartPosition(lines);
        Console.WriteLine($"Day 6 - The guard traverses {Problem1(lines, startPosition, visited)} squares");
        var elapsed = timer.Elapsed;
        Console.WriteLine($"Day 6 Part 1 took {elapsed}");
        timer.Reset();
        timer.Start();
        Console.WriteLine($"Day 6 - There are {Problem2(lines, startPosition, visited)} places adding an obstacle would trap the guard");
        elapsed = timer.Elapsed;
        timer.Stop();
        Console.WriteLine($"Day 6 Part 2 took {elapsed}");
    }

    private static int Problem1(string[] lines, (int x, int y) startPosition, HashSet<(int, int, string)> visited)
    {
        visited.Add((startPosition.x, startPosition.y, "up"));
        TravelUpIsCycle(lines, startPosition, visited, true, false);
        return visited.Select(entry => (entry.Item1, entry.Item2)).Distinct().Count();
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

    private static int Problem2(string[] lines, (int x, int y) startPosition, HashSet<(int, int, string)> visited)
    {
        int numCycles = 0;
        var obstacleOptions = visited.Select(entry => (entry.Item1, entry.Item2)).Distinct();
        foreach ((int x, int y) position in obstacleOptions)
        {
            if (position.y == startPosition.y && position.x == startPosition.x)
                continue;
            AddNewObstacle(lines, (position.x, position.y));
            if (TravelUpIsCycle(lines, startPosition, new(), false, false))
                numCycles++;
            RemoveObstacle(lines, (position.x, position.y));
        }
        return numCycles;
    }

    private static void AddNewObstacle(string[] lines, (int x, int y) position)
    {
        var stringBuilder = new StringBuilder(lines[position.y]);
        stringBuilder[position.x] = 'O';
        lines[position.y] = stringBuilder.ToString();
    }

    private static void RemoveObstacle(string[] lines, (int x, int y) position)
    {
        var stringBuilder = new StringBuilder(lines[position.y]);
        stringBuilder[position.x] = '.';
        lines[position.y] = stringBuilder.ToString();
    }

    private static bool TravelUpIsCycle(string[] lines, (int x, int y) startPosition, HashSet<(int, int, string)> visited, bool recordVisited, bool lookForCycle)
    {
        while (startPosition.y > 0)
        {
            char nextChar = lines[startPosition.y - 1][startPosition.x];
            if (nextChar.Equals('O'))
                return TravelRightIsCycle(lines, startPosition, visited, recordVisited, true);
            else if (nextChar.Equals('#'))
            {
                if (lookForCycle)
                    if (!visited.Add((startPosition.x, startPosition.y, "up")))
                        return true;
                return TravelRightIsCycle(lines, startPosition, visited, recordVisited, lookForCycle);
            }
            startPosition.y--;
            if (recordVisited)
                visited.Add((startPosition.x, startPosition.y, "up"));
        }
        return false;
    }

    private static bool TravelRightIsCycle(string[] lines, (int x, int y) startPosition, HashSet<(int, int, string)> visited, bool recordVisited, bool lookForCycle)
    {
        while (startPosition.x < lines[startPosition.y].Length - 1)
        {
            char nextChar = lines[startPosition.y][startPosition.x + 1];
            if (nextChar.Equals('O'))
                return TravelDownIsCycle(lines, startPosition, visited, recordVisited, true);
            else if (nextChar.Equals('#'))
            {
                if (lookForCycle)
                    if (!visited.Add((startPosition.x, startPosition.y, "right")))
                        return true;
                return TravelDownIsCycle(lines, startPosition, visited, recordVisited, lookForCycle);
            }
            startPosition.x++;
            if (recordVisited)
                visited.Add((startPosition.x, startPosition.y, "right"));
        }
        return false;
    }

    private static bool TravelDownIsCycle(string[] lines, (int x, int y) startPosition, HashSet<(int, int, string)> visited, bool recordVisited, bool lookForCycle)
    {
        while (startPosition.y < lines.Length - 1)
        {
            char nextChar = lines[startPosition.y + 1][startPosition.x];
            if (nextChar.Equals('O'))
                return TravelLeftIsCycle(lines, startPosition, visited, recordVisited, true);
            else if (nextChar.Equals('#'))
                return TravelLeftIsCycle(lines, startPosition, visited, recordVisited, lookForCycle);
            startPosition.y++;
            if (recordVisited)
                visited.Add((startPosition.x, startPosition.y, "down"));
        }
        return false;
    }

    private static bool TravelLeftIsCycle(string[] lines, (int x, int y) startPosition, HashSet<(int, int, string)> visited, bool recordVisited, bool lookForCycle)
    {
        while (startPosition.x > 0)
        {
            char nextChar = lines[startPosition.y][startPosition.x - 1];
            if (nextChar.Equals('O'))
                return TravelUpIsCycle(lines, startPosition, visited, recordVisited, true);
            if (nextChar.Equals('#'))
            {
                return TravelUpIsCycle(lines, startPosition, visited, recordVisited, lookForCycle);
            }
            startPosition.x--;
            if (recordVisited)
                visited.Add((startPosition.x, startPosition.y, "left"));
        }
        return false;
    }

}
using System.Runtime.CompilerServices;
using System.Text;

public class AdventOfCode2024Day4
{
    private const string InputFilePath = "./2024/Day 4/Problem1Input.txt";
    public static void Run()
    {
        var lines = System.IO.File.ReadAllLines(InputFilePath);
        Console.WriteLine($"Day 4 - There are {Problem1(lines)} xmas's in the word search");
        lines = System.IO.File.ReadAllLines(InputFilePath);
        Console.WriteLine($"Day 4 - There are {Problem2(lines)} x-mas's in the word search");
    }

    private static int Problem1(string[] lines)
    {
        var numMatches = 0;
        for (int i = 0; i < lines.Length; i++)
            for (int j = 0; j < lines[i].Length; j++)
                numMatches += CheckLetter(lines, j, i);
        return numMatches;
    }

    private static int CheckLetter(string[] lines, int x, int y)
    {
        int numMatches = 0;
        for (int i = -1; i < 2; i++)
        {
            for (int j = -1; j < 2; j++)
            {
                if (i == 0 && j == 0)
                    continue;
                if (TryFindWord(lines, x, y, i, j))
                    numMatches++;
            }
        }
        StringBuilder builder = new(lines[y]);
        builder[x] = '.';
        lines[y] = builder.ToString();
        return numMatches;
    }

    private static bool TryFindWord(string[] lines, int x, int y, int i, int j)
    {
        if (!IsStringCompleteForward(lines, lines[y][x], x + i, y + j, i, j))
            return false;
        return IsStringCompleteBackward(lines, lines[y][x], x + (i * -1), y + (j * -1), (i * -1), (j * -1));
    }

    private static bool IsStringCompleteForward(string[] lines, char currentChar, int x, int y, int i, int j)
    {
        if (currentChar.Equals('S'))
            return true;
        if (IsOutOfRange(lines, x, y))
            return false;
        if (!lines[y][x].Equals(GetNextLetter(currentChar)))
            return false;
        if (HowManyForward(lines[y][x]) == 0)
            return true;
        return IsStringCompleteForward(lines, lines[y][x], x + i, y + j, i, j);
    }

    private static bool IsStringCompleteBackward(string[] lines, char currentChar, int x, int y, int i, int j)
    {
        if (currentChar.Equals('X'))
            return true;
        if (IsOutOfRange(lines, x, y))
            return false;
        if (!lines[y][x].Equals(GetPreviousLetter(currentChar)))
            return false;
        if (HowManyBack(lines[y][x]) == 0)
            return true;
        return IsStringCompleteBackward(lines, lines[y][x], x + i, y + j, i, j);
    }

    private static bool IsOutOfRange(string[] lines, int x, int y)
    {
        if (y < 0 || y >= lines.Length)
            return true;
        if (x < 0 || x >= lines[y].Length)
            return true;
        return false;
    }

    private static char GetNextLetter(char c)
    {
        return c switch
        {
            'X' => 'M',
            'M' => 'A',
            'A' => 'S',
            _ => ' '
        };
    }

    private static char GetPreviousLetter(char c)
    {
        return c switch
        {
            'S' => 'A',
            'A' => 'M',
            'M' => 'X',
            _ => ' '
        };
    }

    private static int HowManyForward(char c)
    {
        return c switch
        {
            'X' => 3,
            'M' => 2,
            'A' => 1,
            _ => 0
        };
    }

    private static int HowManyBack(char c)
    {
        return c switch
        {
            'S' => 3,
            'A' => 2,
            'M' => 1,
            _ => 0
        };
    }

    private static int Problem2(string[] lines)
    {
        int numMatches = 0;
        for (int i = 1; i < lines.Length - 1; i++)
            for (int j = 1; j < lines[i].Length - 1; j++)
                if (IsXmas(lines, j, i))
                    numMatches++;
        return numMatches;
    }

    private static bool IsXmas(string[] lines, int x, int y)
    {
        if (!lines[y][x].Equals('A'))
            return false;
        return CheckDiagonal(lines, x, y);
    }

    private static bool CheckDiagonal(string[] lines, int x, int y)
    {
        int mCount = 0;
        int sCount = 0;
        if (lines[y - 1][x - 1].Equals('M'))
        {
            mCount++;
            if (!lines[y + 1][x + 1].Equals('S'))
                return false;
        }
        else if (lines[y - 1][x - 1].Equals('S'))
        {
            sCount++;
            if (!lines[y + 1][x + 1].Equals('M'))
                return false;
        }
        if (lines[y - 1][x + 1].Equals('M'))
            mCount++;
        else if (lines[y - 1][x + 1].Equals('S'))
            sCount++;
        if (lines[y + 1][x - 1].Equals('M'))
            mCount++;
        else if (lines[y + 1][x - 1].Equals('S'))
            sCount++;
        if (lines[y + 1][x + 1].Equals('M'))
            mCount++;
        else if (lines[y + 1][x + 1].Equals('S'))
            sCount++;
        return mCount == 2 && sCount == 2;
    }

}
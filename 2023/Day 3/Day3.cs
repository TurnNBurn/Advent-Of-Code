using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

public class AdventOfCode2023Day3
{

    public static void run()
    {
        string[] lines = System.IO.File.ReadAllLines("./2023/Day 3/Problem1Input.txt");
        int engineSum = Problem1(lines);
        Console.WriteLine("Day 3 - Problem 1: the sum of the engine parts is " + engineSum);
        int gearRatio = Problem2(lines);
        Console.WriteLine("Day 3 - Problem 2: the sum of the gear ratios is " + gearRatio);
    }

    private static int Problem1(string[] lines)
    {
        int sum = 0;
        char[][] grid = new char[lines.Length][];
        for (int i = 0; i < lines.Length; i++)
        {
            grid[i] = lines[i].ToCharArray();
        }
        for (int i = 0; i < lines.Length; i++)
        {
            for (int j = 0; j < lines[i].Length; j++)
            {
                if (grid[i][j].Equals('.'))
                    continue;
                if (char.IsSymbol(grid[i][j]) || char.IsPunctuation(grid[i][j]))
                {
                    sum += AdjacentNumSum(grid, i, j);
                }
            }
        }
        return sum;
    }

    private static int AdjacentNumSum(char[][] lines, int x, int y)
    {
        int sum = 0;
        for (int i = -1; i < 2; i++)
        {
            for (int j = -1; j < 2; j++)
            {
                if (i + x >= 0 && i + x < lines.Length && j + y >= 0 && j + y < lines[i + x].Length)
                    if (char.IsNumber(lines[i + x][j + y]))
                        sum += GetNum(lines, i + x, j + y);
            }
        }
        return sum;
    }

    private static int GetNum(char[][] lines, int x, int y)
    {
        if (y == 0 || !char.IsNumber(lines[x][y - 1]))
        {
            StringBuilder num = new StringBuilder();
            int z = y;
            while (z < lines[x].Length && char.IsNumber(lines[x][z]))
            {
                num.Append(lines[x][z]);
                lines[x][z] = '.';
                z++;
            }
            return Convert.ToInt32(num.ToString());
        }
        else
        {
            return GetNum(lines, x, y - 1);
        }
    }

    private static int Problem2(string[] lines)
    {
        int sum = 0;
        char[][] grid = new char[lines.Length][];
        for (int i = 0; i < lines.Length; i++)
        {
            grid[i] = lines[i].ToCharArray();
        }
        for (int i = 0; i < lines.Length; i++)
        {
            for (int j = 0; j < lines[i].Length; j++)
            {

                if (grid[i][j].Equals('*'))
                {
                    sum += IsGear(grid, i, j);
                }
            }
        }
        return sum;
    }

    private static int IsGear(char[][] grid, int x, int y)
    {
        int numCount = 0;
        int firstGear = 0;
        int result = 0;
        for (int i = -1; i < 2; i++)
        {
            for (int j = -1; j < 2; j++)
            {
                if (i + x >= 0 && i + x < grid.Length && j + y >= 0 && j + y < grid[i + x].Length)
                {
                    if (char.IsNumber(grid[i + x][j + y]))
                    {
                        if (numCount > 1)
                            return 0;
                        if (firstGear != 0)
                        {
                            result = firstGear * GetNum(grid, i + x, j + y);
                        }
                        else
                            firstGear = GetNum(grid, i + x, j + y);
                    }
                }
            }
        }
        return result;
    }
}
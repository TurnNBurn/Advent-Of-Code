using System;
using System.Text;

public class AdventOfCode2022Day8
{

    public static void run()
    {
        string[] lines = System.IO.File.ReadAllLines("./2022/Day 8/Problem1Input.txt");

        int visibleCount = Problem1(lines);
        int maxScenic = Problem2(lines);

        Console.WriteLine("Day 8 - Problem 1: There are  " + visibleCount + " visible trees.");
        Console.WriteLine("Day 8 - Problem 2: The most scenic tree has a score of " + maxScenic + ".");
    }

    private static int Problem1(string[] lines)
    {
        int[][] grid = ParseGrid(lines);
        int[] rowHighest = new int[grid.Length - 1];
        int visibleCount = 0;
        for (int i = 0; i < grid.Length - 1; i++)
        {
            rowHighest[i] = grid[0][i];
        }
        for (int i = 1; i < grid.Length - 1; i++)
        {
            int columnHighest = grid[i][0];
            for (int j = 1; j < grid[0].Length - 1; j++)
            {
                bool isVisible = false;
                if (columnHighest < grid[i][j])
                {
                    columnHighest = grid[i][j];
                    isVisible = true;
                }
                if (rowHighest[j] < grid[i][j])
                {
                    rowHighest[j] = grid[i][j];
                    isVisible = true;
                }
                if (!isVisible)
                {
                    bool foundHigherToRight = false;
                    for (int row = i + 1; row < grid.Length; row++)
                    {
                        if (grid[row][j] >= grid[i][j])
                        {
                            foundHigherToRight = true;
                            break;
                        }
                    }
                    isVisible = !foundHigherToRight;
                }
                if (!isVisible)
                {
                    bool foundHigherBelow = false;
                    for (int column = j + 1; column < grid[0].Length; column++)
                    {
                        if (grid[i][column] >= grid[i][j])
                        {
                            foundHigherBelow = true;
                            break;
                        }
                    }
                    isVisible = !foundHigherBelow;
                }
                if (isVisible)
                {
                    visibleCount++;
                }
            }
        }
        Console.WriteLine(grid.Length + " visible trees in the middle");
        return visibleCount + (4 * (grid.Length - 1));
    }

    private static int Problem2(string[] lines)
    {
        int[][] grid = ParseGrid(lines);
        int height = grid.Length;
        int width = grid[0].Length;
        int[][] scenicGrid = new int[height][];
        int[][] upHeight = new int[height][];
        int[][] downHeight = new int[height][];
        for (int i = 0; i < scenicGrid.Length; i++)
        {
            scenicGrid[i] = new int[width];
            upHeight[i] = new int[width];
            downHeight[i] = new int[width];
        }
        for (int i = 1; i < height - 1; i++)
        {
            int[] leftHeight = new int[width];
            for (int j = 1; j < width - 1; j++)
            {
                if (grid[i][j] > grid[i][j - 1] && j > 1)
                {
                    int leftIndex = leftHeight[j - 1];
                    while (grid[i][j] > grid[i][leftIndex] && leftIndex > 0)
                    {
                        leftIndex = leftHeight[leftIndex];
                    }
                    scenicGrid[i][j] = j - leftIndex;
                    leftHeight[j] = leftIndex;
                }
                else
                {
                    scenicGrid[i][j] = 1;
                    leftHeight[j] = j - 1;
                }
                if (grid[i][j] > grid[i - 1][j] && i > 1)
                {
                    int upIndex = upHeight[i - 1][j];
                    while (grid[i][j] > grid[upIndex][j] && upIndex > 0)
                    {
                        upIndex = upHeight[upIndex][j];
                    }
                    upHeight[i][j] = upIndex;
                    scenicGrid[i][j] *= i - upIndex;
                }
                else
                {
                    upHeight[i][j] = i - 1;
                }
            }
        }
        int maxScenic = 0;
        for (int i = height - 2; i > 0; i--)
        {
            int[] rightScenic = new int[width];
            for (int j = grid.Length - 2; j > 0; j--)
            {
                if (grid[i][j] > grid[i][j + 1] && j < width - 2)
                {
                    int rightIndex = rightScenic[j + 1];
                    while (grid[i][j] > grid[i][rightIndex] && rightIndex < (width - 1))
                    {
                        rightIndex = rightScenic[rightIndex];
                    }
                    rightScenic[j] = rightIndex;
                    scenicGrid[i][j] *= rightIndex - j;
                }
                else
                {
                    rightScenic[j] = j + 1;
                }
                if (grid[i][j] > grid[i + 1][j] && i < height - 2)
                {
                    int downIndex = downHeight[i + 1][j];
                    while (grid[i][j] > grid[downIndex][j] && downIndex < height - 1)
                    {
                        downIndex = downHeight[downIndex][j];
                    }
                    downHeight[i][j] = downIndex;
                    scenicGrid[i][j] *= downIndex - i;
                }
                else
                {
                    downHeight[i][j] = i + 1;
                }
                maxScenic = Math.Max(scenicGrid[i][j], maxScenic);
            }
        }
        //PrintGrid(scenicGrid);
        return maxScenic;
    }



    private static int[][] ParseGrid(string[] lines)
    {
        int[][] grid = new int[lines.Length][];
        for (int i = 0; i < lines.Length; i++)
        {
            grid[i] = new int[lines[i].Length];
            for (int j = 0; j < lines[i].Length; j++)
            {
                grid[i][j] = lines[i][j] - '0';
            }
        }
        return grid;
    }

    private static void PrintGrid(int[][] grid)
    {
        for (int i = 0; i < grid.Length; i++)
        {
            for (int j = 0; j < grid[0].Length; j++)
            {
                Console.Write(grid[i][j] + " ");
            }
            Console.Write('\n');
        }
    }
}
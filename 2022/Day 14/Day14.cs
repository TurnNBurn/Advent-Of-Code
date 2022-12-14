using System;
using System.Text;

public class AdventOfCode2022Day14
{

    public static void run()
    {
        string[] lines = System.IO.File.ReadAllLines("./2022/Day 14/Problem1Input.txt");

        int grainsOfSand = Problem1(lines);
        int totalSand = Problem2(lines);

        Console.WriteLine("Day 14 - Problem 1: There are " + grainsOfSand + " grains of sand before the cave is full.");
        Console.WriteLine("Day 14 - Problem 2: There are " + totalSand + " grains of sand before the cave is full.");
    }

    private static int Problem1(string[] lines)
    {
        Cave cave = BuildCave(lines);
        //PrintCave(cave);
        int sandCount = 0;
        while (true)
        {
            Sand sand = new Sand(500, 0);
            while (TryMoveSand(cave, sand)) { }
            if (IsSandAtBottom(cave, sand))
            {
                return sandCount;
            }
            else
            {
                cave.grid[sand.x][sand.y] = 1;
            }
            sandCount++;
        }
        return -1;
    }

    private static void PrintCave(Cave cave)
    {
        string[] output = new string[cave.grid.Length];
        for (int i = 0; i < cave.grid.Length; i++)
        {
            StringBuilder temp = new StringBuilder();
            for (int j = 0; j < cave.grid[i].Length; j++)
            {
                temp.Append(cave.grid[i][j]);
            }
            output[i] = temp.ToString();
        }
        File.WriteAllLines("./2022/Day 14/Problem2Output.txt", output, Encoding.UTF8);
    }

    public static bool TryMoveSand(Cave cave, Sand sand, bool useCaveFloor = false)
    {
        if (sand.x == 0)
        {
            return false;
        }
        if (useCaveFloor && sand.y == cave.floor)
        {
            return false;
        }
        if (sand.x == cave.grid.Length - 1)
        {
            return false;
        }
        if (sand.y == cave.grid[0].Length - 1)
        {
            return false;
        }
        if (cave.grid[sand.x][sand.y + 1] == 0)
        {
            sand.y++;
            return true;
        }
        else if (cave.grid[sand.x - 1][sand.y + 1] == 0)
        {
            sand.x--;
            sand.y++;
            return true;
        }
        else if (cave.grid[sand.x + 1][sand.y + 1] == 0)
        {
            sand.x++;
            sand.y++;
            return true;
        }
        return false;
    }

    public static bool IsSandAtBottom(Cave cave, Sand sand, bool useCaveFloor = false)
    {
        if (!useCaveFloor && sand.x == 0)
        {
            return true;
        }
        if (!useCaveFloor && sand.x == cave.grid.Length - 1)
        {
            return true;
        }
        if (useCaveFloor)
        {
            return sand.x == 500 && sand.y == 0;
        }
        return sand.y == cave.grid[0].Length - 1;
    }

    private static Cave BuildCave(string[] lines)
    {
        Cave cave = new Cave();
        int caveMax = 0;
        foreach (string line in lines)
        {
            string[] path = line.Split("->");
            for (int i = 0; i < path.Length - 1; i++)
            {
                int beginX = Convert.ToInt32(path[i].Split(',')[0]);
                int beginY = Convert.ToInt32(path[i].Split(',')[1]);
                int endX = Convert.ToInt32(path[i + 1].Split(',')[0]);
                int endY = Convert.ToInt32(path[i + 1].Split(',')[1]);
                caveMax = Math.Max(caveMax, beginY);
                caveMax = Math.Max(caveMax, endY);
                if (beginX == endX)
                {
                    int start = Math.Min(beginY, endY);
                    int end = Math.Max(beginY, endY);
                    for (int j = start; j <= end; j++)
                    {
                        cave.grid[beginX][j] = 1;
                    }
                }
                else
                {
                    int start = Math.Min(beginX, endX);
                    int end = Math.Max(beginX, endX);
                    for (int j = start; j <= end; j++)
                    {
                        cave.grid[j][beginY] = 1;
                    }
                }
            }
        }
        cave.floor = caveMax + 1;
        return cave;
    }

    private static int Problem2(string[] lines)
    {
        Cave cave = BuildCave(lines);
        //PrintCave(cave);
        int sandCount = 0;
        while (true)
        {
            Sand sand = new Sand(500, 0);
            while (TryMoveSand(cave, sand, true)) { }
            if (IsSandAtBottom(cave, sand, true))
            {
                return sandCount + 1;
            }
            else
            {
                cave.grid[sand.x][sand.y] = 2;
            }
            sandCount++;
        }
        return -1;
    }

    public class Sand
    {
        public int x;
        public int y;
        public Sand(int X, int Y)
        {
            this.x = X;
            this.y = Y;
        }
    }

    public class Cave
    {
        public int[][] grid;
        public int floor;
        public Cave()
        {
            grid = new int[1000][];
            for (int i = 0; i < 1000; i++)
            {
                grid[i] = new int[500];
            }
        }
    }
}
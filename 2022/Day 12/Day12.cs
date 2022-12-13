using System;
using System.Text;

public class AdventOfCode2022Day12
{

    public static void run()
    {
        string[] lines = System.IO.File.ReadAllLines("./2022/Day 12/Problem1Input.txt");

        int steps = Problem1(lines);
        int shortestDistance = Problem2(lines);

        Console.WriteLine("Day 12 - Problem 1: The number of steps to reach the endpoint is " + steps + ".");
        Console.WriteLine("Day 12 - Problem 2: The distance from the best starting point is " + shortestDistance + ".");
    }

    private static int Problem1(string[] lines)
    {
        int[][] terrain = new int[lines.Length][];
        Coordinate startingPoint = new Coordinate(0, 0);
        Coordinate endPoint = new Coordinate(0, 0);
        for (int i = 0; i < lines.Length; i++)
        {
            terrain[i] = new int[lines[i].Length];
            for (int j = 0; j < lines[i].Length; j++)
            {
                if (lines[i][j].Equals('S'))
                {
                    startingPoint = new Coordinate(i, j);
                    terrain[i][j] = 0;
                }
                else if (lines[i][j].Equals('E'))
                {
                    terrain[i][j] = 26;
                    endPoint = new Coordinate(i, j);
                }
                else
                {
                    terrain[i][j] = lines[i][j] - 'a';
                }
            }
        }
        //PrintTerrain(terrain);
        int[][] visited = new int[lines.Length][];
        for (int i = 0; i < lines.Length; i++)
        {
            visited[i] = new int[lines[i].Length];
        }
        Traverse(terrain, visited, startingPoint, 0);
        return visited[endPoint.x][endPoint.y];
    }

    private static void Traverse(int[][] terrain, int[][] visited, Coordinate position, int steps)
    {

        if (visited[position.x][position.y] != 0 && visited[position.x][position.y] <= steps)
        {
            return;
        }
        visited[position.x][position.y] = steps;
        int currentHeight = terrain[position.x][position.y];
        if (CanMoveRight(terrain, visited, position, steps))
        {
            Traverse(terrain, visited, new Coordinate(position.x + 1, position.y), steps + 1);
        }
        if (CanMoveDown(terrain, visited, position, steps))
        {
            Traverse(terrain, visited, new Coordinate(position.x, position.y + 1), steps + 1);
        }
        if (CanMoveUp(terrain, visited, position, steps))
        {
            Traverse(terrain, visited, new Coordinate(position.x, position.y - 1), steps + 1);
        }
        if (CanMoveLeft(terrain, visited, position, steps))
        {
            Traverse(terrain, visited, new Coordinate(position.x - 1, position.y), steps + 1);
        }
    }

    private static bool CanMoveLeft(int[][] terrain, int[][] visited, Coordinate position, int steps)
    {
        if (position.x == 0)
        {
            return false;
        }
        if (visited[position.x - 1][position.y] != 0 && visited[position.x - 1][position.y] < steps)
        {
            return false;
        }
        if (terrain[position.x - 1][position.y] > 1 + terrain[position.x][position.y])
        {
            return false;
        }
        return true;
    }

    private static bool CanMoveRight(int[][] terrain, int[][] visited, Coordinate position, int steps)
    {
        if (position.x == terrain.Length - 1)
        {
            return false;
        }
        if (visited[position.x + 1][position.y] != 0 && visited[position.x + 1][position.y] < steps)
        {
            return false;
        }
        if (terrain[position.x + 1][position.y] > 1 + terrain[position.x][position.y])
        {
            return false;
        }
        return true;
    }

    private static bool CanMoveDown(int[][] terrain, int[][] visited, Coordinate position, int steps)
    {
        if (position.y == terrain[0].Length - 1)
        {
            return false;
        }
        if (visited[position.x][position.y + 1] != 0 && visited[position.x][position.y + 1] < steps)
        {
            return false;
        }
        if (terrain[position.x][position.y + 1] > 1 + terrain[position.x][position.y])
        {
            return false;
        }
        return true;
    }

    private static bool CanMoveUp(int[][] terrain, int[][] visited, Coordinate position, int steps)
    {
        if (position.y == 0)
        {
            return false;
        }
        if (visited[position.x][position.y - 1] != 0 && visited[position.x][position.y - 1] < steps)
        {
            return false;
        }
        if (terrain[position.x][position.y - 1] > 1 + terrain[position.x][position.y])
        {
            return false;
        }
        return true;
    }

    private static void PrintTerrain(int[][] terrain)
    {
        for (int i = 0; i < terrain.Length; i++)
        {
            for (int j = 0; j < terrain[i].Length; j++)
            {
                Console.Write(terrain[i][j] + " ");
            }
            Console.Write('\n');
        }
    }

    private static int Problem2(string[] lines)
    {
        int[][] terrain = new int[lines.Length][];
        //Coordinate startingPoint = new Coordinate(0, 0);
        List<Coordinate> startingPoints = new List<Coordinate>();
        Coordinate endPoint = new Coordinate(0, 0);
        for (int i = 0; i < lines.Length; i++)
        {
            terrain[i] = new int[lines[i].Length];
            for (int j = 0; j < lines[i].Length; j++)
            {
                if (lines[i][j].Equals('S'))
                {
                    //startingPoint = new Coordinate(i, j);
                    terrain[i][j] = 0;
                }
                else if (lines[i][j].Equals('E'))
                {
                    terrain[i][j] = 26;
                    endPoint = new Coordinate(i, j);
                }
                else if (lines[i][j].Equals('a'))
                {
                    terrain[i][j] = 0;
                    startingPoints.Add(new Coordinate(i, j));
                }
                else
                {
                    terrain[i][j] = lines[i][j] - 'a';
                }
            }
        }
        int shortestDistance = int.MaxValue;
        foreach (Coordinate coord in startingPoints)
        {
            int steps = BestStartingPoint(terrain, coord, endPoint);
            if (steps != 0)
            {
                shortestDistance = Math.Min(steps, shortestDistance);
            }
        }
        return shortestDistance;
    }

    private static int BestStartingPoint(int[][] terrain, Coordinate startingPoint, Coordinate endPoint)
    {
        int[][] visited = new int[terrain.Length][];
        for (int i = 0; i < terrain.Length; i++)
        {
            visited[i] = new int[terrain[i].Length];
        }
        Traverse(terrain, visited, startingPoint, 0);
        return visited[endPoint.x][endPoint.y];
    }
    public class Coordinate
    {
        public int x;
        public int y;
        public Coordinate(int X, int Y)
        {
            this.x = X;
            this.y = Y;
        }
    }
}
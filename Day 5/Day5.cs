using System;

public class AdventOfCodeDay5
{

    public static void run()
    {
        string[] lines = System.IO.File.ReadAllLines("./Day 5/Problem1Input.txt");
        int coordsWithMoreThanOneHit = Problem1(lines);
        int coordsIncludingDiagonal = Problem2(lines);
        Console.WriteLine("Day 5 - Problem 1: There are " + coordsWithMoreThanOneHit + " coordinates hit by more than one line.");
        Console.WriteLine("Day 5 - Problem 2: There are " + coordsIncludingDiagonal + " coordinates hit by more than one line including diagonals.");
    }

    private static int Problem1(string[] lines)
    {
        Dictionary<Coordinates, int> map = new Dictionary<Coordinates, int>();
        foreach (string line in lines)
        {
            string[] input = line.Split(new string[] { " -> " }, StringSplitOptions.RemoveEmptyEntries);
            Coordinates startCoordinates = ParseCoordinate(input[0]);
            Coordinates endCoordinates = ParseCoordinate(input[1]);
            if (startCoordinates.x == endCoordinates.x)
            {
                if (startCoordinates.y < endCoordinates.y)
                {

                    MarkVerticalLine(startCoordinates, endCoordinates, map);
                }
                else
                {
                    MarkVerticalLine(endCoordinates, startCoordinates, map);
                }
            }
            if (startCoordinates.y == endCoordinates.y)
            {
                if (startCoordinates.x < endCoordinates.x)
                {
                    MarkHorizontalLine(startCoordinates, endCoordinates, map);
                }
                else
                {
                    MarkHorizontalLine(endCoordinates, startCoordinates, map);
                }
            }
        }
        int coordsWithMoreThanOneHit = 0;
        foreach (KeyValuePair<Coordinates, int> val in map)
        {
            if (val.Value > 1)
            {
                coordsWithMoreThanOneHit++;
            }
        }
        return coordsWithMoreThanOneHit;
    }

    private static int Problem2(string[] lines)
    {
        Dictionary<Coordinates, int> map = new Dictionary<Coordinates, int>();
        foreach (string line in lines)
        {
            string[] input = line.Split(new string[] { " -> " }, StringSplitOptions.RemoveEmptyEntries);
            Coordinates startCoordinates = ParseCoordinate(input[0]);
            Coordinates endCoordinates = ParseCoordinate(input[1]);
            if (startCoordinates.x == endCoordinates.x)
            {
                if (startCoordinates.y < endCoordinates.y)
                {

                    MarkVerticalLine(startCoordinates, endCoordinates, map);
                }
                else
                {
                    MarkVerticalLine(endCoordinates, startCoordinates, map);
                }
            }
            else if (startCoordinates.y == endCoordinates.y)
            {
                if (startCoordinates.x < endCoordinates.x)
                {
                    MarkHorizontalLine(startCoordinates, endCoordinates, map);
                }
                else
                {
                    MarkHorizontalLine(endCoordinates, startCoordinates, map);
                }
            }
            else
            {
                MarkDiagonalLine(startCoordinates, endCoordinates, map);
            }
        }
        int coordsWithMoreThanOneHit = 0;
        foreach (KeyValuePair<Coordinates, int> val in map)
        {
            if (val.Value > 1)
            {
                coordsWithMoreThanOneHit++;
            }
        }
        return coordsWithMoreThanOneHit;
    }

    private static Coordinates ParseCoordinate(string input)
    {
        string[] coord = input.Split(',');
        return new Coordinates(Convert.ToInt32(coord[0]), Convert.ToInt32(coord[1]));
    }

    private static void MarkVerticalLine(Coordinates startCoordinates, Coordinates endCoordinates, Dictionary<Coordinates, int> map)
    {
        for (int i = 0; i <= Math.Abs(startCoordinates.y - endCoordinates.y); i++)
        {
            UpdateMap(new Coordinates(startCoordinates.x, startCoordinates.y + i), map);
        }
    }

    private static void MarkHorizontalLine(Coordinates startCoordinates, Coordinates endCoordinates, Dictionary<Coordinates, int> map)
    {
        for (int i = 0; i <= Math.Abs(startCoordinates.x - endCoordinates.x); i++)
        {
            UpdateMap(new Coordinates(startCoordinates.x + i, startCoordinates.y), map);
        }
    }

    private static void MarkDiagonalLine(Coordinates startCoordinates, Coordinates endCoordinates, Dictionary<Coordinates, int> map)
    {
        int xAdjustment = startCoordinates.x > endCoordinates.x ? -1 : 1;
        int yAdjustment = startCoordinates.y > endCoordinates.y ? -1 : 1;
        for (int i = 0; i <= Math.Abs(startCoordinates.x - endCoordinates.x); i++)
        {
            UpdateMap(new Coordinates(startCoordinates.x + (xAdjustment * i), startCoordinates.y + (yAdjustment * i)), map);
        }
    }

    private static void UpdateMap(Coordinates coord, Dictionary<Coordinates, int> map)
    {
        if (map.ContainsKey(coord))
        {
            map[coord] = map[coord] + 1;
        }
        else
        {
            map.Add(coord, 1);
        }
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
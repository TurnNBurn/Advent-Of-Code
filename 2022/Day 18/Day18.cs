using System;
using System.Text;

public class AdventOfCode2022Day18
{
    public static void run()
    {
        string[] lines = System.IO.File.ReadAllLines("./2022/Day 18/Problem1Input.txt");

        int surfaceArea = Problem1(lines);
        //long distressFrequency = Problem2(lines);

        Console.WriteLine("Day 18 - Problem 1: The surface area is " + surfaceArea + ".");
        //Console.WriteLine("Day 18 - Problem 2: The distress beacon's frequency is " + distressFrequency + ".");
    }

    private static int Problem1(string[] lines)
    {
        HashSet<Square> squares = new HashSet<Square>();
        foreach (string line in lines)
        {
            string[] coords = line.Split(',');
            int x = Convert.ToInt32(coords[0]);
            int y = Convert.ToInt32(coords[1]);
            int z = Convert.ToInt32(coords[2]);
            TryAddSquare(new Square(x, x + 1, y, y + 1, z, z), squares);
            TryAddSquare(new Square(x, x + 1, y, y + 1, z + 1, z + 1), squares);
            TryAddSquare(new Square(x, x + 1, y, y, z, z + 1), squares);
            TryAddSquare(new Square(x, x + 1, y + 1, y + 1, z, z + 1), squares);
            TryAddSquare(new Square(x, x, y, y + 1, z, z + 1), squares);
            TryAddSquare(new Square(x + 1, x + 1, y, y + 1, z, z + 1), squares);
        }
        return squares.Count;
    }

    public static void TryAddSquare(Square square, HashSet<Square> squares)
    {
        if (squares.Contains(square))
        {
            Console.WriteLine("Removing square " + square.ToString());
            squares.Remove(square);
        }
        else
        {
            Console.WriteLine("Adding square " + square.ToString());
            squares.Add(square);
        }
    }

    private static int Problem2(string[] lines)
    {
        return -1;
    }

    public class Square
    {
        int lowX;
        int highX;
        int lowY;
        int highY;
        int lowZ;
        int highZ;
        public Square(int x1, int x2, int y1, int y2, int z1, int z2)
        {
            lowX = x1;
            highX = x2;
            lowY = y1;
            highY = y2;
            lowZ = z1;
            highZ = z2;
        }

        public override bool Equals(object? obj)
        {
            return obj != null && Equals(obj as Square);
        }

        public bool Equals(Square other)
        {
            return other != null && this.lowX == other.lowX &&
            other.highX == this.highX && other.lowY == this.highY &&
            other.lowZ == this.lowZ && other.highZ == this.highZ;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.lowX, this.highX, this.lowY, this.highY, this.lowZ, this.highZ);
        }

        public override string ToString()
        {
            return "X from " + this.lowX + " to " + this.highX + " Y from " + this.lowY + " to " + this.highY + " Z from " + this.lowZ + " to " + this.highZ;
        }
    }
}
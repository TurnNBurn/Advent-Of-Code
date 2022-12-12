using System;
using System.Text;

public class AdventOfCode2022Day9
{

    public static void run()
    {
        string[] lines = System.IO.File.ReadAllLines("./2022/Day 9/Problem1Input.txt");

        int pointsVisited = Problem1(lines);
        int ropeEndVisited = Problem2(lines);

        Console.WriteLine("Day 9 - Problem 1: The tail visits " + pointsVisited + " points.");
        Console.WriteLine("Day 9 - Problem 2: The rope end visits " + ropeEndVisited + " points.");
    }

    private static int Problem1(string[] lines)
    {
        Dictionary<Point, int> traversedMap = new Dictionary<Point, int>();
        Point head = new Point(0, 0);
        Point tail = new Point(0, 0);
        traversedMap.Add(new Point(0, 0), 1);
        foreach (string line in lines)
        {
            char direction = line[0];
            int movement = Convert.ToInt32(line.Split(' ')[1]);
            for (int i = 0; i < movement; i++)
            {
                if (direction.Equals('U'))
                {
                    head.y++;
                    MoveUp(head, tail, traversedMap);
                }
                else if (direction.Equals('D'))
                {
                    head.y--;
                    MoveDown(head, tail, traversedMap);
                }
                else if (direction.Equals('L'))
                {
                    head.x--;
                    MoveLeft(head, tail, traversedMap);
                }
                else if (direction.Equals('R'))
                {
                    head.x++;
                    MoveRight(head, tail, traversedMap);
                }
            }
        }
        return traversedMap.Count;
    }

    private static void MoveUp(Point head, Point tail, Dictionary<Point, int> traverseMap, bool record = true)
    {
        if (head.y > (tail.y + 1))
        {
            if (head.x != tail.x)
            {
                tail.move(head.x, tail.y + 1);
            }
            else
            {
                tail.move(tail.x, tail.y + 1);
            }
            if (record)
            {
                IncrementPoint(new Point(tail.x, tail.y), traverseMap);
            }
        }

    }

    private static void MoveDown(Point head, Point tail, Dictionary<Point, int> traverseMap, bool record = true)
    {
        if (head.y < (tail.y - 1))
        {
            if (head.x != tail.x)
            {
                tail.move(head.x, tail.y - 1);
            }
            else
            {
                tail.move(tail.x, tail.y - 1);
            }
            if (record)
            {
                IncrementPoint(new Point(tail.x, tail.y), traverseMap);
            }
        }
    }

    private static void MoveLeft(Point head, Point tail, Dictionary<Point, int> traverseMap, bool record = true)
    {
        if (head.x < (tail.x - 1))
        {
            if (head.y != tail.y)
            {
                tail.move(tail.x - 1, head.y);
            }
            else
            {
                tail.move(tail.x - 1, tail.y);

            }
            if (record)
            {
                IncrementPoint(new Point(tail.x, tail.y), traverseMap);
            }
        }
    }
    private static void MoveRight(Point head, Point tail, Dictionary<Point, int> traverseMap, bool record = true)
    {
        if (head.x > (tail.x + 1))
        {
            if (head.y != tail.y)
            {
                tail.move(tail.x + 1, head.y);
                tail.y = head.y;
            }
            else
            {
                tail.move(tail.x + 1, tail.y);
            }
            if (record)
            {
                IncrementPoint(new Point(tail.x, tail.y), traverseMap);
            }
        }
    }

    private static void GetCloser(Point head, Point tail, Dictionary<Point, int> traverseMap, bool record)
    {
        int newX = tail.x;
        int newY = tail.y;
        if (head.x > tail.x)
        {
            newX = tail.x + 1;
        }
        else if (head.x < tail.x)
        {
            newX = tail.x - 1;
        }
        if (head.y > tail.y)
        {
            newY = tail.y + 1;
        }
        else if (head.y < tail.y)
        {
            newY = tail.y - 1;
        }
        tail.move(newX, newY);
        if (record)
        {
            IncrementPoint(new Point(tail.x, tail.y), traverseMap);
        }
    }

    private static void IncrementPoint(Point point, Dictionary<Point, int> traverseMap)
    {
        if (traverseMap.ContainsKey(point))
        {
            traverseMap[point]++;
        }
        else
        {
            traverseMap.Add(point, 1);
        }
    }

    private static bool IsAdjacentTo(Point head, Point tail)
    {
        if (head.x > tail.x + 1 || head.x < tail.x - 1)
        {
            return false;
        }
        if (head.y > tail.y + 1 || head.y < tail.y - 1)
        {
            return false;
        }
        return true;
    }

    private static int Problem2(string[] lines)
    {
        Dictionary<Point, int> traversedMap = new Dictionary<Point, int>();
        Point[] rope = new Point[10];
        for (int i = 0; i < 10; i++)
        {
            rope[i] = new Point(0, 0);
        }
        traversedMap.Add(new Point(0, 0), 1);
        foreach (string line in lines)
        {
            char direction = line[0];
            int movement = Convert.ToInt32(line.Split(' ')[1]);
            for (int j = 0; j < movement; j++)
            {
                if (direction.Equals('U'))
                {
                    rope[0].y++;
                }
                if (direction.Equals('D'))
                {
                    rope[0].y--;
                }
                if (direction.Equals('L'))
                {
                    rope[0].x--;
                }
                if (direction.Equals('R'))
                {
                    rope[0].x++;
                }
                for (int i = 0; i < 9; i++)
                {
                    if (!IsAdjacentTo(rope[i], rope[i + 1]))
                    {
                        GetCloser(rope[i], rope[i + 1], traversedMap, i == 8);
                    }
                }
            }
        }
        return traversedMap.Count;
    }

    private static void PrintTail(Dictionary<Point, int> traversePoints)
    {
        for (int i = 10; i > -8; i--)
        {
            for (int j = -13; j < 14; j++)
            {
                if (traversePoints.ContainsKey(new Point(j, i)))
                {
                    Console.Write('#');
                }
                else
                {
                    Console.Write('.');
                }
            }
            Console.Write('\n');
        }
    }
}

public class Point
{
    public int x;
    public int y;

    public Point(int X, int Y)
    {
        this.x = X;
        this.y = Y;
    }

    public void move(int newX, int newY)
    {
        x = newX;
        y = newY;
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as Point);
    }

    public bool Equals(Point? other)
    {
        return other != null && other.x == this.x && other.y == this.y;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(this.x, this.y);
    }
}
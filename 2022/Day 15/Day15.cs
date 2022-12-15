using System;
using System.Text;

public class AdventOfCode2022Day15
{
    const int yToCheckProb1 = 2000000;
    const int maxRange = 4000000;
    public static void run()
    {
        string[] lines = System.IO.File.ReadAllLines("./2022/Day 15/Problem1Input.txt");

        int searchedPositions = Problem1(lines);
        //long distressFrequency = Problem2(lines);

        Console.WriteLine("Day 15 - Problem 1: In the row y = " + yToCheckProb1 + " " + searchedPositions + " cannot contain the distress signal.");
        //Console.WriteLine("Day 15 - Problem 2: The distress beacon's frequency is " + distressFrequency + ".");
    }

    private static int Problem1(string[] lines)
    {
        List<Sensor> sensors = BuildMap(lines);
        HashSet<int> visited = new HashSet<int>();
        foreach (Sensor sensor in sensors)
        {
            int distanceFrom2000000 = Math.Abs(sensor.y - yToCheckProb1);
            int wiggleRoom = sensor.manhattanDistance - distanceFrom2000000;
            if (wiggleRoom <= 0)
            {
                continue;
            }
            TryAdd(visited, sensor.x);
            for (int i = 1; i <= wiggleRoom; i++)
            {
                TryAdd(visited, sensor.x - i);
                TryAdd(visited, sensor.x + i);
            }
        }
        return visited.Count - 1;
    }

    public static void TryAdd(HashSet<int> visited, int point)
    {
        if (!visited.Contains(point))
        {
            visited.Add(point);
        }
    }

    private static List<Sensor> BuildMap(string[] lines)
    {
        List<Sensor> sensors = new List<Sensor>();
        foreach (string line in lines)
        {
            int sensorX = Convert.ToInt32(line.Split(',')[0].Split('=')[1]);
            int sensorY = Convert.ToInt32(line.Split(',')[1].Split(':')[0].Split('=')[1]);
            int beaconX = Convert.ToInt32(line.Split("beacon")[1].Split(',')[0].Split('=')[1]);
            int beaconY = Convert.ToInt32(line.Split("beacon")[1].Split(',')[1].Split('=')[1]);
            sensors.Add(new Sensor(sensorX, sensorY, new Coordinate(beaconX, beaconY)));
        }
        return sensors;
    }

    private static long Problem2(string[] lines)
    {
        List<Sensor> sensors = BuildMap(lines);
        Coordinate distressBeacon = FindDistressBeacon(sensors);
        if (distressBeacon.x < 0 || distressBeacon.y < 0)
        {
            return -1;
        }
        return ((long)distressBeacon.x * 4000000) + distressBeacon.y;
    }

    public static Coordinate FindDistressBeacon(List<Sensor> sensors)
    {
        foreach (Sensor sensor in sensors)
        {
            List<Coordinate> borderPoints = GetBorderPoints(sensor);
            foreach (Coordinate coord in borderPoints)
            {
                bool isInSensorRange = false;
                foreach (Sensor otherSensor in sensors)
                {
                    if (otherSensor.IsPointInRange(coord))
                    {
                        isInSensorRange = true;
                        break;
                    }
                }
                if (!isInSensorRange)
                {
                    return coord;
                }
            }
        }
        return new Coordinate(-1, -1);
    }

    public static List<Coordinate> GetBorderPoints(Sensor sensor)
    {
        List<Coordinate> borderPoints = new List<Coordinate>();
        int x = sensor.x;
        int y = sensor.y + sensor.manhattanDistance;
        for (int i = 0; i < sensor.manhattanDistance; i++)
        {
            if (WithinBounds(sensor.x + i, sensor.y + sensor.manhattanDistance - i + 1))
            {
                borderPoints.Add(new Coordinate(sensor.x + i, sensor.y + sensor.manhattanDistance - i + 1));
            }
            if (WithinBounds(sensor.x + sensor.manhattanDistance - i + 1, sensor.y - i))
            {
                borderPoints.Add(new Coordinate(sensor.x + sensor.manhattanDistance - i + 1, sensor.y - i));
            }
            if (WithinBounds(sensor.x - i, sensor.y + sensor.manhattanDistance - i + 1))
            {
                borderPoints.Add(new Coordinate(sensor.x - i, sensor.y + sensor.manhattanDistance - i + 1));
            }
            if (WithinBounds(sensor.x - sensor.manhattanDistance - i + 1, sensor.y + i))
            {
                borderPoints.Add(new Coordinate(sensor.x - sensor.manhattanDistance - i + 1, sensor.y + i));
            }
        }
        return borderPoints;
    }

    public static bool WithinBounds(int x, int y)
    {
        if (x < 0 || x > maxRange)
        {
            return false;
        }
        if (y < 0 || y > maxRange)
        {
            return false;
        }
        return true;
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

    public class Sensor : Coordinate
    {
        public int manhattanDistance;

        public Sensor(int X, int Y, Coordinate beacon) : base(X, Y)
        {
            this.manhattanDistance = GetManhattanDistance(beacon);
        }

        public int GetManhattanDistance(Coordinate coord)
        {
            return Math.Abs(this.x - coord.x) + Math.Abs(this.y - coord.y);
        }

        public bool IsPointInRange(Coordinate coord)
        {
            return GetManhattanDistance(coord) <= manhattanDistance;
        }
    }
}
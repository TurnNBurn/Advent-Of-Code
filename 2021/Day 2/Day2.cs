using System;

public class AdventOfCodeDay2
{

    public static void run()
    {
        string[] lines = System.IO.File.ReadAllLines("./2021/Day 2/Problem1Input.txt");
        int depthTimesHoriz = Problem1(lines);
        int depthTimesHorizCorrected = Problem2(lines);

        Console.WriteLine("Day 2 - Problem 1: The final depth times horizontal distance is " + depthTimesHoriz);
        Console.WriteLine("Day 2 - Problem 2: The final depth times horizontal distance taking into account aim is " + depthTimesHorizCorrected);
    }

    private static int Problem1(string[] lines)
    {
        int depth = 0;
        int horiz = 0;
        foreach (string line in lines)
        {
            string[] words = line.Split(' ');
            int distance = Convert.ToInt32(words[1]);
            string direction = words[0].ToUpper();
            if (direction.Equals("UP"))
            {
                depth -= distance;
            }
            if (direction.Equals("DOWN"))
            {
                depth += distance;
            }
            if (direction.Equals("FORWARD"))
            {
                horiz += distance;
            }
        }

        return depth * horiz;
    }

    static int Problem2(string[] lines)
    {
        int depth = 0;
        int horiz = 0;
        int aim = 0;
        foreach (string line in lines)
        {
            string[] words = line.Split(' ');
            int distance = Convert.ToInt32(words[1]);
            string direction = words[0].ToUpper();
            if (direction.Equals("UP"))
            {
                aim -= distance;
            }
            if (direction.Equals("DOWN"))
            {
                aim += distance;
            }
            if (direction.Equals("FORWARD"))
            {
                horiz += distance;
                depth += distance * aim;
            }
        }

        return depth * horiz;
    }

}
using System;

class AdventOfCodeDay2
{

    static void Main()
    {
        string[] lines = System.IO.File.ReadAllLines("./Day 2/Problem1Input.txt");
        int depthTimesHoriz = Problem1(lines);

        Console.WriteLine("The final depth times horizontal distance is " + depthTimesHoriz);
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

}
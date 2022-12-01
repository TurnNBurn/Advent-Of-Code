using System;

public class AdventOfCodeDay12
{

    public static void run()
    {
        string[] lines = System.IO.File.ReadAllLines("./Day 12/Problem1Input.txt");
        int totalPaths = Problem1(lines);
        int totalPathsWithRevisiting = Problem2(lines);
        Console.WriteLine("Day 12 - Problem 1: There are " + totalPaths + " distinct paths to the exit.");
        Console.WriteLine("Day 12 - Problem 2: There are " + totalPathsWithRevisiting + " distinct paths that include revisiting a small room");
    }

    private static int Problem1(string[] lines)
    {
        Dictionary<string, List<string>> caveMap = BuildMap(lines);
        return FindPaths(caveMap, false);
    }

    private static int Problem2(string[] lines)
    {
        Dictionary<string, List<string>> caveMap = BuildMap(lines);
        return FindPaths(caveMap, true);
    }

    private static int FindPaths(Dictionary<string, List<string>> caveMap, bool canRevisit)
    {
        int numPaths = 0;
        List<string> startPaths = caveMap["start"];
        foreach (string nextRoom in startPaths)
        {
            numPaths += BuildPaths(caveMap, "start," + nextRoom, canRevisit);
        }
        return numPaths;
    }

    private static int BuildPaths(Dictionary<string, List<string>> caveMap, string path, bool canRevisit)
    {
        int numPaths = 0;
        string lastRoom = path.Split(',').Last();
        if (!caveMap.ContainsKey(lastRoom))
        {
            return 0;
        }
        List<string> roomPaths = caveMap[lastRoom];
        foreach (string nextRoom in roomPaths)
        {
            if (nextRoom.Equals("end"))
            {
                numPaths++;
            }
            else if (nextRoom.Any(char.IsLower))
            {
                if (path.Contains(nextRoom))
                {
                    if (canRevisit && !nextRoom.Equals("start"))
                    {
                        //numPaths += BuildPaths(caveMap, path, true);
                        numPaths += BuildPaths(caveMap, path + "," + nextRoom, false);
                    }
                }
                else
                {
                    numPaths += BuildPaths(caveMap, path + "," + nextRoom, canRevisit);
                }
            }
            else
            {
                numPaths += BuildPaths(caveMap, path + "," + nextRoom, canRevisit);
            }
        }
        return numPaths;
    }

    private static Dictionary<string, List<string>> BuildMap(string[] lines)
    {
        Dictionary<string, List<string>> caveMap = new Dictionary<string, List<string>>();
        foreach (string line in lines)
        {
            BuildOneRoom(caveMap, line);
        }
        return caveMap;
    }

    private static Dictionary<string, List<string>> BuildOneRoom(Dictionary<string, List<string>> caveMap, string line)
    {
        string[] rooms = line.Split('-');
        if (caveMap.ContainsKey(rooms[0]))
        {
            caveMap[rooms[0]].Add(rooms[1]);
        }
        else
        {
            List<string> val = new List<string>();
            val.Add(rooms[1]);
            caveMap.Add(rooms[0], val);
        }
        if (caveMap.ContainsKey(rooms[1]))
        {
            caveMap[rooms[1]].Add(rooms[0]);
        }
        else
        {
            List<string> val = new List<string>();
            val.Add(rooms[0]);
            caveMap.Add(rooms[1], val);
        }
        return caveMap;
    }
}
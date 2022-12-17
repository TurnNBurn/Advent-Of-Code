using System;
using System.Text;

public class AdventOfCode2022Day16
{
    public static void run()
    {
        string[] lines = System.IO.File.ReadAllLines("./2022/Day 16/Problem1Input.txt");

        int totalPressure = Problem1(lines);
        //long distressFrequency = Problem2(lines);

        Console.WriteLine("Day 16 - Problem 1: The most pressure that can be released is " + totalPressure + ".");
        //Console.WriteLine("Day 16 - Problem 2: The distress beacon's frequency is " + distressFrequency + ".");
    }

    private static int Problem1(string[] lines)
    {
        Dictionary<string, Valve> valveMap = new Dictionary<string, Valve>();
        Valve firstValve = ParseInput(lines, valveMap);
        //PrintValves(firstValve, null);
        List<Valve> valvesWithFlow = FindAllValvesWithFlow(valveMap);
        CalculateValveDistances(valvesWithFlow, firstValve);
        return TraverseValves(firstValve, 30, new HashSet<string>());
    }

    public static void CalculateValveDistances(List<Valve> valvesWithFlow, Valve firstValve)
    {
        for (int i = 0; i < valvesWithFlow.Count; i++)
        {
            for (int j = i + 1; j < valvesWithFlow.Count; j++)
            {
                if (!valvesWithFlow[i].distanceToFlow.ContainsKey(valvesWithFlow[j]))
                {
                    int distance = BuildDistanceMap(valvesWithFlow[i], valvesWithFlow[j], null);
                    Console.WriteLine("The distance from " + valvesWithFlow[i].ID + " to " + valvesWithFlow[j].ID + " is " + distance);
                    valvesWithFlow[i].distanceToFlow.Add(valvesWithFlow[j], distance);
                    valvesWithFlow[j].distanceToFlow.Add(valvesWithFlow[i], distance);
                }
            }
            firstValve.distanceToFlow.Add(valvesWithFlow[i], BuildDistanceMap(firstValve, valvesWithFlow[i], null));
        }
    }

    public static List<Valve> FindAllValvesWithFlow(Dictionary<string, Valve> valveMap)
    {
        List<Valve> valvesWithFlow = new List<Valve>();
        foreach (KeyValuePair<string, Valve> kvp in valveMap)
        {
            if (kvp.Value.flow != 0)
            {
                valvesWithFlow.Add(kvp.Value);
            }
        }
        return valvesWithFlow;
    }

    private static int BuildDistanceMap(Valve start, Valve end, HashSet<string>? visited)
    {
        if (visited == null)
        {
            visited = new HashSet<string>();
        }
        if (start.distanceToFlow.ContainsKey(end))
        {
            return start.distanceToFlow[end];
        }
        else
        {
            int distance = 10000;
            foreach (KeyValuePair<string, Valve> kvp in start.connections)
            {
                if (kvp.Value.ID.Equals(end.ID))
                {
                    return 1;
                }
                else if (!visited.Contains(kvp.Value.ID))
                {
                    visited.Add(kvp.Value.ID);
                    distance = Math.Min(distance, 1 + BuildDistanceMap(kvp.Value, end, visited));
                    visited.Remove(kvp.Value.ID);
                }
            }
            return distance;
        }
    }

    private static int TraverseValves(Valve curValve, int time, HashSet<string> open)
    {
        int nextPressure = 0;
        int thisPressure = 0;
        if (time == 0)
        {
            return 0;
        }
        if (curValve.flow != 0)
        {
            thisPressure = curValve.flow * time;
        }
        foreach (KeyValuePair<Valve, int> kvp in curValve.distanceToFlow)
        {
            if (!open.Contains(kvp.Key.ID))
            {
                if (kvp.Value < time - 1)
                {
                    open.Add(kvp.Key.ID);
                    nextPressure = Math.Max(nextPressure, TraverseValves(kvp.Key, time - kvp.Value - 1, open));
                    open.Remove(kvp.Key.ID);
                }
            }
        }
        return thisPressure + nextPressure;
    }

    private static int Problem2(string[] lines)
    {
        return -1;
    }

    private static void PrintValves(Valve valve, HashSet<string>? visited)
    {
        if (visited == null)
        {
            visited = new HashSet<string>();
        }
        if (visited.Contains(valve.ID))
        {
            return;
        }
        else
        {
            visited.Add(valve.ID);
        }
        Console.WriteLine("Valve " + valve.ID + " with flow rate " + valve.flow + " has connections:");
        foreach (KeyValuePair<string, Valve> oneValve in valve.connections)
        {
            Console.WriteLine("Valve " + oneValve.Key);
        }
        foreach (KeyValuePair<string, Valve> nextValve in valve.connections)
        {
            PrintValves(nextValve.Value, visited);
        }

    }

    public static Valve ParseInput(string[] lines, Dictionary<string, Valve> valves)
    {
        Dictionary<int, Valve> flowRates = new Dictionary<int, Valve>();
        foreach (string line in lines)
        {
            string ID = line.Substring(6, 2);
            int flow = Convert.ToInt32(line.Split(';')[0].Split('=')[1]);
            valves.Add(ID, new Valve(ID, flow));
        }

        foreach (string line in lines)
        {
            Dictionary<string, Valve> valveMap = new Dictionary<string, Valve>();
            string ID = line.Substring(6, 2);
            if (line.IndexOf("valves") != -1)
            {
                string[] listOfValves = line.Split("valves")[1].Split(',');
                foreach (string oneValve in listOfValves)
                {
                    valveMap.Add(oneValve.Trim(), valves[oneValve.Trim()]);
                }
            }
            else
            {
                string tunnel = line.Split("valve")[1].Trim();
                valveMap.Add(tunnel, valves[tunnel]);
            }
            valves[ID].connections = valveMap;
        }
        return valves["AA"];
    }

    public class Valve
    {
        public int flow;
        public string ID;
        public Dictionary<string, Valve> connections;
        public Dictionary<Valve, int> distanceToFlow;
        public Valve(string id, int rate)
        {
            this.ID = id;
            flow = rate;
            connections = new Dictionary<string, Valve>();
            distanceToFlow = new Dictionary<Valve, int>();
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as Valve);
        }

        public bool Equals(Valve? other)
        {
            return other != null && ID.Equals(other.ID);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.ID);
        }
    }
}
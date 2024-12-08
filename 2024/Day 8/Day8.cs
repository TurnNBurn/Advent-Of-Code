using System.Diagnostics;
using System.Text;

public class AdventOfCode2024Day8
{
    private const string InputFilePath = "./2024/Day 8/Problem1Input.txt";
    public static void Run()
    {
        var lines = File.ReadAllLines(InputFilePath);
        var timer = new Stopwatch();
        timer.Start();
        var antennas = FindAntennas(lines);
        Console.WriteLine($"Day 8 - The number of antinodes on the map is {Problem1(antennas, lines.Length, lines[0].Length)}");
        var elapsed = timer.Elapsed;
        Console.WriteLine($"Day 8 Part 1 took {elapsed}");
        timer.Reset();
        timer.Start();
        Console.WriteLine($"Day 8 - The number of antinodes including frequencies is {Problem2(antennas, lines.Length, lines[0].Length)}");
        elapsed = timer.Elapsed;
        timer.Stop();
        Console.WriteLine($"Day 8 Part 2 took {elapsed}");
    }

    private static int Problem1(Dictionary<char, List<(int, int)>> antennas, int xBound, int yBound)
    {
        var antinodes = new HashSet<(int, int)>();

        foreach (var antenna in antennas)
            if (antenna.Value.Count > 0)
                AddAntinodes(antinodes, antenna.Value, xBound, yBound, false);

        return antinodes.Count;
    }

    private static Dictionary<char, List<(int, int)>> FindAntennas(string[] lines)
    {
        var antennas = new Dictionary<char, List<(int, int)>>();
        for (int i = 0; i < lines.Length; i++)
            for (int j = 0; j < lines[i].Length; j++)
                if (!lines[i][j].Equals('.'))
                {
                    if (antennas.ContainsKey(lines[i][j]))
                        antennas[lines[i][j]].Add((j, i));
                    else
                        antennas.Add(lines[i][j], new List<(int, int)> { (j, i) });
                }
        return antennas;
    }

    private static void AddAntinodes(HashSet<(int, int)> antinodes, List<(int, int)> antennas, int xBound, int yBound, bool looping)
    {
        for (int i = 0; i < antennas.Count - 1; i++)
            for (int j = i + 1; j < antennas.Count; j++)
            {
                if (looping)
                    TryAddAntinodesLooping(antennas[i], antennas[j], antinodes, xBound, yBound);
                else
                    TryAddAntinodes(antennas[i], antennas[j], antinodes, xBound, yBound, 1);
            }
    }

    private static void TryAddAntinodesLooping((int x, int y) antenna1, (int x, int y) antenna2, HashSet<(int, int)> antinodes, int xBound, int yBound)
    {
        int multiple = 1;
        while (TryAddAntinodes(antenna1, antenna2, antinodes, xBound, yBound, multiple))
            multiple++;
    }

    private static bool TryAddAntinodes((int x, int y) antenna1, (int x, int y) antenna2, HashSet<(int, int)> antinodes, int xBound, int yBound, int multiple)
    {
        var xDistance = Math.Abs(antenna1.x - antenna2.x) * multiple;
        var yDistance = Math.Abs(antenna1.y - antenna2.y) * multiple;
        (int x, int y) antiNode1 = (
            antenna1.x < antenna2.x ? antenna1.x - xDistance : antenna1.x + xDistance,
            antenna1.y < antenna2.y ? antenna1.y - yDistance : antenna1.y + yDistance);
        (int x, int y) antiNode2 = (
            antenna2.x < antenna1.x ? antenna2.x - xDistance : antenna2.x + xDistance,
            antenna2.y < antenna1.y ? antenna2.y - yDistance : antenna2.y + yDistance);
        bool foundOne = false;
        if (WithinBounds(antiNode1, xBound, yBound))
        {
            foundOne = true;
            antinodes.Add(antiNode1);
        }
        if (WithinBounds(antiNode2, xBound, yBound))
        {
            foundOne = true;
            antinodes.Add(antiNode2);
        }
        return foundOne;
    }

    private static bool WithinBounds((int x, int y) antiNode, int xBound, int yBound)
    {
        if (antiNode.x < 0 || antiNode.x >= xBound)
            return false;
        if (antiNode.y < 0 || antiNode.y >= yBound)
            return false;
        return true;
    }

    private static int Problem2(Dictionary<char, List<(int, int)>> antennas, int xBound, int yBound)
    {
        var antinodes = new HashSet<(int, int)>(antennas.SelectMany(antenna => antenna.Value));
        foreach (var antenna in antennas)
        {
            if (antenna.Value.Count > 1)
                AddAntinodes(antinodes, antenna.Value, xBound, yBound, true);
        }

        return antinodes.Count;
    }
}
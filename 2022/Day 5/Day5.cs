using System;
using System.Text;

public class AdventOfCode2022Day5
{

    public static void run()
    {
        string[] lines = System.IO.File.ReadAllLines("./2022/Day 5/Problem1Input.txt");

        string topCrates = Problem1(lines);
        string topCratesCrateMover3001 = Problem2(lines);

        Console.WriteLine("Day 5 - Problem 1: The top crates are " + topCrates + ".");
        Console.WriteLine("Day 5 - Problem 2: The top crates are " + topCratesCrateMover3001 + ".");
    }

    private static string Problem1(string[] lines)
    {
        List<Stack<char>> crates = ParseCrates(lines);
        for (int i = 10; i < lines.Length; i++)
        {
            ParseOneInstruction(crates, lines[i]);
        }
        StringBuilder ret = new StringBuilder();
        foreach (Stack<char> crate in crates)
        {
            ret.Append(crate.Peek());
        }
        return ret.ToString();
    }

    private static List<Stack<char>> ParseCrates(string[] lines)
    {
        List<Stack<char>> crates = new List<Stack<char>>();
        List<Stack<char>> temp = new List<Stack<char>>();
        for (int i = 0; i < 9; i++)
        {
            Stack<char> crate = new Stack<char>();
            Stack<char> tempCrate = new Stack<char>();
            crates.Add(crate);
            temp.Add(tempCrate);
        }
        foreach (string line in lines)
        {
            if (line.Equals(String.Empty))
            {
                break;
            }
            for (int i = 0; i < 9; i++)
            {
                if (!line[(i * 4) + 1].Equals(' '))
                {
                    temp[i].Push(line[(i * 4) + 1]);
                }
            }
        }
        for (int i = 0; i < temp.Count; i++)
        {
            //Inefficient but quicker than writing a better loop end
            temp[i].Pop();
            while (temp[i].Count > 0)
            {
                crates[i].Push(temp[i].Pop());
            }
        }
        return crates;
    }

    private static void ParseOneInstruction(List<Stack<char>> crates, string line)
    {
        string[] instruction = line.Split(' ');
        int numCrates = Convert.ToInt32(instruction[1]);
        int fromCrate = Convert.ToInt32(instruction[3]) - 1;
        int toCrate = Convert.ToInt32(instruction[5]) - 1;
        for (int i = 0; i < numCrates; i++)
        {
            crates[toCrate].Push(crates[fromCrate].Pop());
        }
    }

    private static void ParseOneInstruction3001(List<Stack<char>> crates, string line)
    {
        string[] instruction = line.Split(' ');
        int numCrates = Convert.ToInt32(instruction[1]);
        int fromCrate = Convert.ToInt32(instruction[3]) - 1;
        int toCrate = Convert.ToInt32(instruction[5]) - 1;
        Stack<char> temp = new Stack<char>();
        for (int i = 0; i < numCrates; i++)
        {
            temp.Push(crates[fromCrate].Pop());
        }
        for (int i = 0; i < numCrates; i++)
        {
            crates[toCrate].Push(temp.Pop());
        }
    }

    private static string Problem2(string[] lines)
    {
        List<Stack<char>> crates = ParseCrates(lines);
        for (int i = 10; i < lines.Length; i++)
        {
            ParseOneInstruction3001(crates, lines[i]);
        }
        StringBuilder ret = new StringBuilder();
        foreach (Stack<char> crate in crates)
        {
            ret.Append(crate.Peek());
        }
        return ret.ToString();
    }
}
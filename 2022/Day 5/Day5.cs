using System;
using System.Text;

public class AdventOfCode2022Day5
{

    public static void run()
    {
        string[] lines = System.IO.File.ReadAllLines("./2022/Day 5/Problem1Input.txt");

        string topCrates = Problem1(lines);
        //int totalPairsOverlapping = Problem2(lines);

        Console.WriteLine("Day 5 - Problem 1: The top crates are " + topCrates + ".");
        //Console.WriteLine("Day 5 - Problem 2: There are " + totalPairsOverlapping + " pairs with overlapping segments.");
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
        //Console.WriteLine("Moving " + numCrates + " crates from stack " + fromCrate + " to stack " + toCrate);
        for (int i = 0; i < numCrates; i++)
        {
            char crateToMove = crates[fromCrate].Pop();
            crates[toCrate].Push(crateToMove);
        }
    }

    private static int Problem2(string[] lines)
    {
        return -1;
    }
}
using System;

public class AdventOfCodeDay16
{

    public static void run()
    {
        string[] lines = System.IO.File.ReadAllLines("./Day 16/Problem1Input.txt");
        int totalRisk = Problem1(lines);
        Console.WriteLine("Day 16 - Problem 1: The lowest total risk out of the cave is " + totalRisk);
    }

    private static int Problem1(string[] lines)
    {
        return 0;
    }

    private static Dictionary<char, string> HexToBinary = new Dictionary<char, string>{
        {'0',"0000"},
        {'1',"0001"},
        {'2',"0010"},
        {'3',"0011"},
        {'4',"0100"},
        {'5',"0101"},
        {'6',"0110"},
        {'7',"0111"},
        {'8',"1000"},
        {'9',"1001"},
        {'A',"1010"},
        {'B',"1011"},
        {'C',"1100"},
        {'D',"1101"},
        {'E',"1110"},
        {'F',"1111"}
    };
}

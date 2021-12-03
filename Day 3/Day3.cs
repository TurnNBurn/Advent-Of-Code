using System;

class AdventOfCodeDay3
{

    static void Main()
    {
        string[] lines = System.IO.File.ReadAllLines("./Day 3/Problem1Input.txt");
        int powerConsumption = Problem1(lines);

        Console.WriteLine("The power consumption is " + powerConsumption);
    }

    static private int Problem1(string[] lines)
    {
        //Assume that each line will have a consistent length
        int[] binaryCount = new int[lines[0].Length];
        foreach (string line in lines)
        {
            for (int i = 0; i < line.Length; i++)
            {
                if (int.Parse(line[i].ToString()) == 1)
                {
                    binaryCount[i]++;
                }
            }
        }
        int gamma = 0;
        int epsilon = 0;
        for (int i = 0; i < binaryCount.Length; i++)
        {
            int binary = lines.Length / 2 > binaryCount[i] ? 0 : 1;
            int binaryInverse = lines.Length / 2 > binaryCount[i] ? 1 : 0;
            gamma += binary * (int)Math.Pow(2, binaryCount.Length - i);
            epsilon += binaryInverse * (int)Math.Pow(2, binaryCount.Length - i);
        }
        return gamma * epsilon;
    }
}
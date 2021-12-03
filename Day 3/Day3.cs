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
            //Note the assumption here that there will never be an equal
            //amount of 0's and 1's for a given bit.
            int binary = binaryCount[i] * 2 > lines.Length ? 1 : 0;
            int binaryInverse = binaryCount[i] * 2 > lines.Length ? 0 : 1;
            gamma += ConvertToDecimal(binary, binaryCount.Length, i);
            epsilon += ConvertToDecimal(binaryInverse, binaryCount.Length, i);
        }
        return gamma * epsilon;
    }
    private static int ConvertToDecimal(int bit, int binaryLength, int position)
    {
        return bit * (int)Math.Pow(2, binaryLength - position - 1);
    }
}
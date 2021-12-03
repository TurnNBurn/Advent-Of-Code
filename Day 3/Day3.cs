using System;

class AdventOfCodeDay3
{

    static void Main()
    {
        string[] lines = System.IO.File.ReadAllLines("./Day 3/Problem1Input.txt");
        int powerConsumption = Problem1(lines);
        int lifeSupport = Problem2(lines);

        Console.WriteLine("The power consumption is " + powerConsumption);
        Console.WriteLine("The life support rating is " + lifeSupport);
    }

    static private int Problem1(string[] lines)
    {
        //Assume that each line will have a consistent length
        int[] mostCommonBitArray = BuildMostCommonBitArray(lines);
        int gamma = 0;
        int epsilon = 0;
        for (int i = 0; i < mostCommonBitArray.Length; i++)
        {
            //Note the assumption here that there will never be an equal
            //amount of 0's and 1's for a given bit.
            int binary = GetMostCommonBit(mostCommonBitArray, i, lines.Length);
            int binaryInverse = GetLeastCommonBit(mostCommonBitArray, i, lines.Length);
            gamma += ConvertToDecimal(binary, mostCommonBitArray.Length, i);
            epsilon += ConvertToDecimal(binaryInverse, mostCommonBitArray.Length, i);
        }
        return gamma * epsilon;
    }
    private static int ConvertToDecimal(int bit, int binaryLength, int position)
    {
        return bit * (int)Math.Pow(2, binaryLength - position - 1);
    }

    private static int GetMostCommonBit(int[] mostCommonBitArray, int position, int numBytes)
    {
        return mostCommonBitArray[position] * 2 >= numBytes ? 1 : 0;
    }

    private static int GetLeastCommonBit(int[] mostCommonBitArray, int position, int numBytes)
    {
        return mostCommonBitArray[position] * 2 >= numBytes ? 0 : 1;
    }

    private static int[] BuildMostCommonBitArray(string[] lines)
    {
        //Assume that each line will have a consistent length
        int[] mostCommonBitArray = new int[lines[0].Length];
        foreach (string line in lines)
        {
            for (int i = 0; i < line.Length; i++)
            {
                if (int.Parse(line[i].ToString()) == 1)
                {
                    mostCommonBitArray[i]++;
                }
            }
        }
        return mostCommonBitArray;
    }

    private static int Problem2(string[] lines)
    {
        int oxygen = 0;
        int c02scrub = 0;
        int[] mostCommonBitArray = BuildMostCommonBitArray(lines);

        return oxygen * c02scrub;
    }


}
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
        int[] mostCommonBitArray = BuildMostCommonBitArray(lines.ToList<string>());
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

    private static int[] BuildMostCommonBitArray(List<string> lines)
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
        List<string> oxyList = FilterListByBit(lines.ToList<string>(), 0, listType.oxy);
        List<string> c02List = FilterListByBit(lines.ToList<string>(), 0, listType.c02);
        return MultiplyOxyAndC02(oxyList[0], c02List[0]);
    }

    enum listType
    {
        oxy,
        c02
    }

    private static List<string> FilterListByBit(List<string> list, int filterBit, listType type)
    {
        int[] mostCommonBitArray = BuildMostCommonBitArray(list);
        int filterVal = type == listType.oxy ? GetMostCommonBit(mostCommonBitArray, filterBit, list.Count) : GetLeastCommonBit(mostCommonBitArray, filterBit, list.Count);
        List<string> validEntries = new List<string>();
        foreach (string listEntry in list)
        {
            if (int.Parse(listEntry[filterBit].ToString()) == filterVal)
            {
                validEntries.Add(listEntry);
            }
        }
        if (validEntries.Count == 1)
        {
            return validEntries;
        }
        else
        {
            return FilterListByBit(validEntries, filterBit + 1, type);
        }
    }

    private static int MultiplyOxyAndC02(string oxy, string c02)
    {
        int oxyInt = 0;
        int c02Int = 0;
        for (int i = 0; i < oxy.Length; i++)
        {
            oxyInt += ConvertToDecimal(int.Parse(oxy[i].ToString()), oxy.Length, i);
            c02Int += ConvertToDecimal(int.Parse(c02[i].ToString()), c02.Length, i);
        }
        return oxyInt * c02Int;
    }
}
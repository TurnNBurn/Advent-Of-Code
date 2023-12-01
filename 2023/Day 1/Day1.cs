using System.Security.Cryptography.X509Certificates;

public class AdventOfCode2023Day1
{

    public static void run()
    {
        string[] lines = System.IO.File.ReadAllLines("./2023/Day 1/Problem1Input.txt");
        int calibrationSum = Problem1(lines);
        Console.WriteLine("Day 1 - Problem 1: the sum of the calibration values is " + calibrationSum);
    }

    private static int Problem1(string[] lines)
    {
        int sum = 0;
        foreach (string line in lines)
        {
            string filteredLine = new string(line.Where(char.IsDigit).ToArray());
            string calibrationText = filteredLine.Length > 1 ? $"{filteredLine[0]}{filteredLine[^1]}" : new string(filteredLine[0], 2);
            sum += Convert.ToInt32(calibrationText);
        }
        return sum;
    }
}
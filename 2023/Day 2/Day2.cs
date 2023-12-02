public class AdventOfCode2023Day2
{

    public static void run()
    {
        string[] lines = System.IO.File.ReadAllLines("./2023/Day 2/Problem1Input.txt");
        int calibrationSum = Problem1(lines);
        Console.WriteLine("Day 1 - Problem 1: the sum of the calibration values is " + calibrationSum);
        //int realCalibrationSum = Problem2(lines);
        //Console.WriteLine("Day 1 - Problem 2: the correct sum of the calibration values is " + realCalibrationSum);
    }

    private static int Problem1(string[] lines)
    {
        int sum = 0;
        return sum;
    }
}
using System;
using System.Text;

public class AdventOfCode2022Day17
{
    public static void run()
    {
        string[] lines = System.IO.File.ReadAllLines("./2022/Day 17/Problem1Input.txt");

        int totalPressure = Problem1(lines);
        //long distressFrequency = Problem2(lines);

        Console.WriteLine("Day 17 - Problem 1: The most pressure that can be released is " + totalPressure + ".");
        //Console.WriteLine("Day 17 - Problem 2: The distress beacon's frequency is " + distressFrequency + ".");
    }

    private static int Problem1(string[] lines)
    {
        int[] floor = { 0, 0, 0, 0, 0, 0, 0 };
        for (int i = 0; i < 2022; i++)
        {
            switch ((i + 1) % 5)
            {
                case 1:

                    break;
                case 2:

                    break;
                case 3:

                    break;
                case 4:

                    break;
                default:

                    break;
            }
        }
        return -1;
    }

    private static int Problem2(string[] lines)
    {
        return -1;
    }

    public class Tetris
    {
        int left;
        int right;
        int bottom;

        public Tetris(int Left, int Right, int Bottom)
        {
            this.left = Left;
            this.right = Right;
            this.bottom = Bottom;
        }

        public bool IsResting()
        {
            return false;
        }

        public void MoveLeft()
        {

        }

        public void MoveRight()
        {

        }
        public void MoveDown()
        {

        }
    }
}
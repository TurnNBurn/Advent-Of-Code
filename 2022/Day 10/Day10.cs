using System;
using System.Text;

public class AdventOfCode2022Day10
{

    public static void run()
    {
        string[] lines = System.IO.File.ReadAllLines("./2022/Day 10/Problem1Input.txt");

        int signalStrength = Problem1(lines);
        //int ropeEndVisited = Problem2(lines);

        Console.WriteLine("Day 10 - Problem 1: The signal strength is " + signalStrength + ".");
        //Console.WriteLine("Day 10 - Problem 2: The rope end visits " + ropeEndVisited + " points.");
    }

    private static int Problem1(string[] lines)
    {
        CPU cpu = new CPU();
        foreach (string line in lines)
        {
            if (line.Equals("noop"))
            {
                cpu.noop();
            }
            else
            {
                cpu.AddX(Convert.ToInt32(line.Split(' ')[1]));
            }
        }

        int sum = 0;
        foreach (int i in cpu.signal)
        {
            sum += i;
        }
        return sum;
    }

    private static int Problem2(string[] lines)
    {
        return -1;
    }
}

public class CPU
{
    int clock;
    int register;
    public List<int> signal;
    public CPU()
    {
        clock = 0;
        register = 1;
        signal = new List<int>();
    }

    public void noop()
    {
        IncrementClock();
    }

    public void AddX(int x)
    {
        IncrementClock();
        IncrementClock();
        register += x;
    }

    private void IncrementClock()
    {
        int pixel = clock % 40;
        if (pixel == register || pixel == register - 1 || pixel == register + 1)
        {
            Console.Write('#');
        }
        else
        {
            Console.Write('.');
        }
        clock++;
        if (clock % 40 == 0)
        {
            Console.Write('\n');
        }
        if (clock == 20 || (clock - 20) % 40 == 0)
        {
            signal.Add(clock * register);
        }
    }
}
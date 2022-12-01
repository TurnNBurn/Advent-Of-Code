using System;

public class AdventOfCode2022Day1
{

    public static void run()
    {
        string[] lines = System.IO.File.ReadAllLines("./2022/Day 1/Problem1Input.txt");

        int elfWithMostCalories = Problem1(lines);
        int topThreeElvesCalories = Problem2(lines);

        Console.WriteLine("Day 1 - Problem 1: The elf with the most calories has " + elfWithMostCalories + " calories.");
        Console.WriteLine("Day 1 - Problem 2: The top three elves have " + topThreeElvesCalories + " calories.");
    }

    private static int Problem1(string[] lines)
    {
        int calories = 0;
        int maxCalories = 0;
        for (int i = 0; i < lines.Length; i++)
        {
            if (lines[i].Equals(String.Empty))
            {
                maxCalories = Math.Max(calories, maxCalories);
                calories = 0;
            }
            else
            {
                calories += Convert.ToInt32(lines[i]);
            }
        }
        if (calories != 0)
        {
            maxCalories = Math.Max(calories, maxCalories);
        }
        return maxCalories;
    }

    private static int Problem2(string[] lines)
    {
        int calories = 0;
        PriorityQueue<int, int> heap = new PriorityQueue<int, int>();
        for (int i = 0; i < lines.Length; i++)
        {
            if (lines[i].Equals(String.Empty))
            {
                if (heap.Count < 3 || calories > heap.Peek())
                {
                    heap.Enqueue(calories, calories);
                }
                calories = 0;
            }
            else
            {
                calories += Convert.ToInt32(lines[i]);
            }
        }
        if (calories != 0 && calories > heap.Peek())
        {
            heap.Enqueue(calories, calories);
        }
        while (heap.Count > 3)
        {
            heap.Dequeue();
        }
        int totalCalories = 0;
        while (heap.Count > 0)
        {
            totalCalories += heap.Dequeue();
        }
        return totalCalories;
    }
}
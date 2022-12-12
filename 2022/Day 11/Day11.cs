using System;
using System.Text;

public class AdventOfCode2022Day11
{

    public static void run()
    {
        string[] lines = System.IO.File.ReadAllLines("./2022/Day 11/Problem1Input.txt");

        int monkeyLevel = Problem1(lines);
        long longMonkeyLevel = Problem2(lines);

        Console.WriteLine("Day 11 - Problem 1: The monkey level is " + monkeyLevel + ".");
        Console.WriteLine("Day 11 - Problem 2: The monkey level after 10000 rounds is " + longMonkeyLevel + " points.");
    }

    private static int Problem1(string[] lines)
    {
        List<Monkey> monkeys = parseInput(lines);
        for (int i = 0; i < 20; i++)
        {
            for (int j = 0; j < monkeys.Count; j++)
            {
                monkeys[j].MonkeyBusiness(true);
            }
        }
        int most = 0;
        int secondMost = 0;
        foreach (Monkey monkey in monkeys)
        {
            if (monkey.count > most)
            {
                if (most > secondMost)
                {
                    secondMost = most;
                }
                most = monkey.count;
            }
            else if (monkey.count > secondMost)
            {
                secondMost = monkey.count;
            }
        }
        return most * secondMost;
    }

    private static long Problem2(string[] lines)
    {
        List<Monkey> monkeys = parseInput(lines);
        for (int i = 0; i < 10000; i++)
        {
            for (int j = 0; j < monkeys.Count; j++)
            {
                monkeys[j].MonkeyBusiness(false);
            }
        }
        long most = 0;
        long secondMost = 0;
        foreach (Monkey monkey in monkeys)
        {
            if (monkey.count > most)
            {
                if (most > secondMost)
                {
                    secondMost = most;
                }
                most = monkey.count;
            }
            else if (monkey.count > secondMost)
            {
                secondMost = monkey.count;
            }
        }
        return most * secondMost;
    }

    private static List<Monkey> parseInput(string[] lines)
    {
        List<Monkey> monkeys = new List<Monkey>();
        Dictionary<int, string[]> monkeyAssignment = new Dictionary<int, string[]>();
        for (int i = 0; i < lines.Length; i++)
        {
            if (lines[i].Split(' ')[0].Equals("Monkey"))
            {
                string items = lines[i + 1];
                Func<long, long> operation = BuildOperation(lines[i + 2]);
                int divisible = Convert.ToInt32(lines[i + 3].Split(' ')[5]);
                string ifTrue = lines[i + 4];
                string ifFalse = lines[i + 5];
                Monkey monkey = new Monkey(divisible, operation);
                GiveMonkeyItems(monkey, items);
                monkeyAssignment.Add(monkeys.Count, new string[] { ifTrue, ifFalse });
                monkeys.Add(monkey);
                i += 6;
            }
        }

        int supermod = 1;
        foreach (Monkey monkey in monkeys)
        {
            supermod *= monkey.divisibleTest;
        }
        foreach (Monkey monkey in monkeys)
        {
            monkey.supermod = supermod;
        }
        foreach (KeyValuePair<int, string[]> kvp in monkeyAssignment)
        {
            monkeys[kvp.Key].ifTrue = monkeys[kvp.Value[0][kvp.Value[0].Length - 1] - '0'];
            monkeys[kvp.Key].ifFalse = monkeys[kvp.Value[1][kvp.Value[1].Length - 1] - '0'];
        }
        return monkeys;
    }

    private static void GiveMonkeyItems(Monkey monkey, string items)
    {
        string[] splitItems = items.Split(':')[1].Split(',');
        foreach (string item in splitItems)
        {
            monkey.ReceiveItem(Convert.ToInt64(item));
        }
    }

    private static Func<long, long> BuildOperation(string line)
    {
        string[] operation = line.Split("=")[1].Split(' ');
        if (operation[3].Equals("old"))
        {
            return (x) => x * x;
        }
        else
        {
            int constant = Convert.ToInt32(operation[3]);
            if (operation[2].Equals("+"))
            {
                return (x) => x + constant;
            }
            else
            {
                return (x) => x * constant;
            }
        }
    }
}

public class Monkey
{
    public Queue<long> items;
    public Monkey? ifTrue;
    public Monkey? ifFalse;
    private Func<long, long> operation;
    public int divisibleTest;
    public int count;
    public int supermod;
    public Monkey(int divisible, Func<long, long> operate)
    {
        items = new Queue<long>();
        divisibleTest = divisible;
        operation = operate;
    }

    public void MonkeyBusiness(bool reduceWorry)
    {
        while (items.Count > 0)
        {
            long item = this.items.Dequeue();
            item = operation(item);
            if (reduceWorry)
            {
                item /= 3;
            }
            else
            {
                item %= supermod;
            }
            count++;
            if (item % divisibleTest == 0)
            {
                GiveItem(ifTrue, item);
            }
            else
            {
                GiveItem(ifFalse, item);
            }
        }
    }

    public void ReceiveItem(long item)
    {
        items.Enqueue(item);
    }

    private void GiveItem(Monkey? receiver, long item)
    {
        if (receiver != null)
        {
            receiver.ReceiveItem(item);
        }
    }
}
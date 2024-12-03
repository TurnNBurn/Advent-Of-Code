using System.Text.RegularExpressions;

public class AdventOfCode2023Day5
{

    public static void run()
    {
        string[] lines = System.IO.File.ReadAllLines("./2023/Day 5/Problem1Input.txt");
        long points = Problem1(lines);
        Console.WriteLine("Day 4 - Problem 1: the lowest seed location is " + +points);
        //long totalCards = Problem2(lines);
        //Console.WriteLine("Day 2 - The total number of cards is " + totalCards);
    }

    private static long Problem1(string[] lines)
    {
        long minLocation = 0;
        long[] seeds = lines[0].Split(':')[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(seed => Convert.ToInt64(seed)).ToArray();
        List<Range> knownRanges = new List<Range> { new Range(0, long.MaxValue, 0) };

        foreach (string line in lines)
        {
            if (line.Contains(':'))
                continue;
            if (string.IsNullOrEmpty(line))
                continue;
            string[] pieces = line.Split(" ");
            long min = Convert.ToInt64(pieces[0]);
            long max = Convert.ToInt64(pieces[2]);
            long transform = Convert.ToInt64(pieces[1]) - min;
            knownRanges = InsertRange(knownRanges, new Range(min, max, transform), transform);
        }

        foreach (long seed in seeds)
        {
            minLocation = Math.Min(minLocation, ConvertSeed(seed, knownRanges));
        }

        return minLocation;
    }

    private static long ConvertSeed(long seed, List<Range> knownRanges)
    {
        foreach (Range range in knownRanges)
        {
            if (range.Low <= seed && range.High >= seed)
            {
                return seed + range.offset;
            }
        }
        return 0;
    }

    private static List<Range> InsertRange(List<Range> knownRanges, Range newRange, long transform)
    {
        List<Range> newRanges = new List<Range>();
        foreach (Range range in knownRanges)
        {
            if (range.High + range.offset < newRange.Low)
            {
                newRanges.Add(range);
                continue;
            }

            if (range.Low + range.offset > newRange.High)
            {
                newRanges.Add(range);
                continue;
            }

            if (range.Low + range.offset < newRange.Low)
            {
                newRanges.Add(new Range(range.Low, newRange.Low - range.offset - 1, range.offset));
            }

            if (range.High + range.offset > newRange.High)
            {
                newRanges.Add(new Range(range.High + range.offset - newRange.High, range.High, range.offset));
            }

            if (range.Low + range.offset > newRange.Low && range.High + range.offset < newRange.High)
                continue;

        }
        newRanges.Add(new Range(newRange.Low, newRange.High, newRange.offset));
        return newRanges;
    }

    public class Range
    {
        public Range(long low, long high, long offset)
        {
            Low = low;
            High = high;
            this.offset = offset;
        }

        public long Low { get; set; }
        public long High { get; set; }
        public long offset { get; set; }
    }

}

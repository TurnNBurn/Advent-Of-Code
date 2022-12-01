using System;
using System.Text;

public class AdventOfCodeDay8
{

    public static void run()
    {
        string[] lines = System.IO.File.ReadAllLines("./2021/Day 8/Problem1Input.txt");
        int numUniqueDigits = Problem1(lines);
        int totalSum = Problem2(lines);
        Console.WriteLine("Day 8 - Problem 1: 1, 4, 7, and 8 appear " + numUniqueDigits + " times.");
        Console.WriteLine("Day 8 - Problem 2: The sum of all the outputs is " + totalSum);
    }

    private static int Problem1(string[] lines)
    {
        int numUniqueDigits = 0;
        foreach (string line in lines)
        {
            string[] digits = line.Split('|')[1].Split(' ');
            foreach (string digit in digits)
            {
                if (digit.Length == 2 || digit.Length == 3 || digit.Length == 4 || digit.Length == 7)
                {
                    numUniqueDigits++;
                }
            }
        }
        return numUniqueDigits;
    }

    private static int Problem2(string[] lines)
    {
        int totalSum = 0;
        foreach (string line in lines)
        {
            totalSum += ProcessOneLine(line);
        }
        return totalSum;
    }

    private static int ProcessOneLine(string line)
    {
        string[] uniqueDigits = line.Split('|')[0].Split(' ');
        string[] output = line.Split('|')[1].Split(' ');
        string[] digits = DecodeDigits(uniqueDigits);
        return DecodeOutput(digits, output);
    }

    private static string[] DecodeDigits(string[] uniqueDigits)
    {
        string[] digits = SortKnownDigits(uniqueDigits);
        digits = FindZeroSixAndNine(uniqueDigits, digits);
        digits = FindTwoThreeAndFive(uniqueDigits, digits);
        return digits;
    }

    private static string[] SortKnownDigits(string[] uniqueDigits)
    {
        string[] digits = new string[10];
        for (int i = 0; i < uniqueDigits.Length; i++)
        {
            switch (uniqueDigits[i].Length)
            {
                case 2:
                    digits[1] = SortDigit(uniqueDigits[i]);
                    break;
                case 3:
                    digits[7] = SortDigit(uniqueDigits[i]);
                    break;
                case 4:
                    digits[4] = SortDigit(uniqueDigits[i]);
                    break;
                case 7:
                    digits[8] = SortDigit(uniqueDigits[i]);
                    break;
                default:
                    break;
            }
        }
        return digits;
    }

    private static string[] FindZeroSixAndNine(string[] uniqueDigits, string[] digits)
    {
        for (int i = 0; i < uniqueDigits.Length; i++)
        {
            if (uniqueDigits[i].Length == 6)
            {
                if (IsSix(uniqueDigits[i], digits[7]))
                {
                    digits[6] = SortDigit(uniqueDigits[i]);
                }
                else if (IsZero(uniqueDigits[i], digits[4]))
                {
                    digits[0] = SortDigit(uniqueDigits[i]);
                }
                else
                {
                    digits[9] = SortDigit(uniqueDigits[i]);
                }
            }
        }
        return digits;
    }

    private static bool IsZero(string maybeZero, string four)
    {
        for (int i = 0; i < four.Length; i++)
        {
            if (!maybeZero.Contains(four[i]))
            {
                return true;
            }
        }
        return false;
    }

    private static bool IsSix(string maybeSix, string one)
    {
        for (int i = 0; i < one.Length; i++)
        {
            if (!maybeSix.Contains(one[i]))
            {
                return true;
            }
        }
        return false;
    }

    private static string[] FindTwoThreeAndFive(string[] uniqueDigits, string[] digits)
    {
        for (int i = 0; i < uniqueDigits.Length; i++)
        {
            if (uniqueDigits[i].Length == 5)
            {
                if (IsFive(uniqueDigits[i], digits[6]))
                {
                    digits[5] = SortDigit(uniqueDigits[i]);
                }
                else if (IsThree(uniqueDigits[i], digits[1]))
                {
                    digits[3] = SortDigit(uniqueDigits[i]);
                }
                else
                {
                    digits[2] = SortDigit(uniqueDigits[i]);
                }
            }
        }
        return digits;
    }

    private static bool IsFive(string maybeFive, string six)
    {
        for (int i = 0; i < maybeFive.Length; i++)
        {
            if (!six.Contains(maybeFive[i]))
            {
                return false;
            }
        }
        return true;
    }

    private static bool IsThree(string maybeThree, string one)
    {
        for (int i = 0; i < one.Length; i++)
        {
            if (!maybeThree.Contains(one[i]))
            {
                return false;
            }
        }
        return true;
    }

    private static int DecodeOutput(string[] digits, string[] output)
    {
        StringBuilder sb = new StringBuilder(String.Empty, 4);
        foreach (string outputDigit in output)
        {
            for (int i = 0; i < digits.Length; i++)
            {
                if (digits[i].Equals(SortDigit(outputDigit)))
                {
                    sb.Append(i.ToString());
                    break;
                }
            }
        }
        return Convert.ToInt32(sb.ToString());
    }

    private static string SortDigit(string digit)
    {
        char[] digitChars = digit.ToCharArray();
        Array.Sort(digitChars);
        return new string(digitChars);
    }
}
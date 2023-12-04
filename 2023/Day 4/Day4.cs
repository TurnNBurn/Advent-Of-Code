public class AdventOfCode2023Day4
{

    public static void run()
    {
        string[] lines = System.IO.File.ReadAllLines("./2023/Day 4/Problem1Input.txt");
        int points = Problem1(lines);
        Console.WriteLine("Day 4 - Problem 1: the sum of the card points is " + points);
        //int gearRatio = Problem2(lines);
        //Console.WriteLine("Day 4 - Problem 2: the sum of the gear ratios is " + gearRatio);
    }

    private static int Problem1(string[] lines)
    {
        int sum = 0;
        foreach (string line in lines)
        {
            HashSet<int> winningNums = new HashSet<int>();
            string[] pieces = line.Split("|");
            string[] winners = pieces[0].Split(":")[1].Trim().Split(" ", StringSplitOptions.RemoveEmptyEntries);
            foreach (string winner in winners)
            {
                winningNums.Add(Convert.ToInt32(winner.Trim()));
            }
            int cardPoints = 0;
            foreach (string num in pieces[1].Trim().Split(" ", StringSplitOptions.RemoveEmptyEntries))
            {
                if (winningNums.Contains(Convert.ToInt32(num.Trim())))
                {
                    if (cardPoints < 2)
                    {
                        cardPoints++;
                    }
                    else
                    {
                        cardPoints = 2 * cardPoints;
                    }
                }
            }
            sum += cardPoints;
        }
        return sum;
    }
}
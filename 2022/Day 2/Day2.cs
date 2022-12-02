using System;

public class AdventOfCode2022Day2
{

    public static void run()
    {
        string[] lines = System.IO.File.ReadAllLines("./2022/Day 2/Problem1Input.txt");

        int totalScore = Problem1(lines);
        int totalOptimizedScore = Problem2(lines);

        Console.WriteLine("Day 2 - Problem 1: My total score according to plan is " + totalScore + ".");
        Console.WriteLine("Day 2 - Problem 2: My total score with optimized play is " + totalOptimizedScore + ".");
    }

    private static int Problem1(string[] lines)
    {
        int myScore = 0;
        foreach (string line in lines)
        {
            myScore += ProcessOneGame(line[0], line[2]);
        }
        return myScore;
    }

    private static int ProcessOneGame(char opponentsMove, char myMove)
    {
        return GetScoreForMyMove(myMove) + DetermineOutcome(GetScoreForOpponentsMove(opponentsMove), GetScoreForMyMove(myMove));
    }

    private static int DetermineOutcome(int opponentsMove, int myMove)
    {
        if (opponentsMove == myMove)
        {
            return 3;
        }
        if (opponentsMove == 1 && myMove == 3)
        {
            return 0;
        }
        if (opponentsMove == 2 && myMove == 1)
        {
            return 0;
        }
        if (opponentsMove == 3 && myMove == 2)
        {
            return 0;
        }
        return 6;
    }

    //Duplicate code from function below because I'm betting part 2
    //Will need these separated
    private static int GetScoreForMyMove(char move)
    {
        if (move.Equals('X'))
        {
            return 1;
        }
        if (move.Equals('Y'))
        {
            return 2;
        }
        if (move.Equals('Z'))
        {
            return 3;
        }
        return -1;
    }

    private static int GetScoreForOpponentsMove(char move)
    {
        if (move.Equals('A'))
        {
            return 1;
        }
        if (move.Equals('B'))
        {
            return 2;
        }
        if (move.Equals('C'))
        {
            return 3;
        }
        return -1;
    }
    private static int Problem2(string[] lines)
    {
        int myScore = 0;
        foreach (string line in lines)
        {
            myScore += ProcessOneOptimizedGame(line[0], line[2]);
        }
        return myScore;
    }

    private static int ProcessOneOptimizedGame(char opponentsMove, char desiredOutcome)
    {
        int opponentsScore = GetScoreForOpponentsMove(opponentsMove);
        int myMove = DetermineMyMove(opponentsScore, desiredOutcome);
        return myMove + DetermineOutcome(opponentsScore, myMove);
    }

    private static int DetermineMyMove(int opponentsMove, char desiredOutcome)
    {
        if (desiredOutcome.Equals('Y'))
        {
            return opponentsMove;
        }
        if (desiredOutcome.Equals('X'))
        {
            if (opponentsMove == 1)
            {
                return 3;
            }
            else
            {
                return opponentsMove - 1;
            }
        }
        else
        {
            if (opponentsMove == 3)
            {
                return 1;
            }
            else
            {
                return opponentsMove + 1;
            }
        }
    }
}
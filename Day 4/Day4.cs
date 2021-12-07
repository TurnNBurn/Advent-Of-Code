using System;

public class AdventOfCodeDay4
{

    public static void run()
    {
        string[] lines = System.IO.File.ReadAllLines("./Day 4/Problem1Input.txt");
        int bingoScore = Problem1(lines);
        //int lifeSupport = Problem2(lines);

        Console.WriteLine("Day 4 - Problem 1: The bingo score of the first board to win is " + bingoScore);
    }

    private static int Problem1(string[] lines)
    {
        int bingoScore = 0;
        string[] bingoNumbers = lines[0].Split(',');
        List<Board> bingoBoards = ParseBingoBoards(lines);
        return bingoScore;
    }

    private static void PrintBoards(List<Board> bingoBoards)
    {
        Console.WriteLine("Test");
        foreach (Board board in bingoBoards)
        {
            Console.WriteLine("Board Solutions: ");
            foreach (List<int> solution in board.solutions)
            {
                Console.WriteLine("");
                foreach (int num in solution)
                {
                    Console.Write(num + " ");
                }
            }
        }
    }

    private static List<Board> ParseBingoBoards(string[] lines)
    {
        List<Board> bingoBoards = new List<Board>();
        List<string> boardContents = new List<string>();
        for (int i = 2; i < lines.Length; i++)
        {
            if (!lines[i].Equals(String.Empty))
            {
                boardContents.Add(lines[i]);
            }
            if ((i - 1) % 5 == 0)
            {
                bingoBoards.Add(BuildBoard(boardContents));
                boardContents.Clear();
            }
        }
        return bingoBoards;
    }

    private static Board BuildBoard(List<string> boardContents)
    {
        Board board = new Board();
        List<List<int>> columns = new List<List<int>>();
        for (int i = 0; i < 5; i++)
        {
            columns.Add(new List<int>());
        }
        for (int i = 0; i < boardContents.Count; i++)
        {
            string[] row = boardContents[i].Split(new string[] { "  ", " " }, StringSplitOptions.RemoveEmptyEntries);
            List<int> rowList = new List<int>();
            for (int j = 0; j < row.Length; j++)
            {
                int rowEntry = Convert.ToInt32(row[j]);
                rowList.Add(rowEntry);
                columns[j].Add(rowEntry);
            }
            board.solutions.Add(rowList);
        }
        foreach (List<int> column in columns)
        {
            board.solutions.Add(column);
        }
        return board;
    }

    private static int FindWinningBoard(string[] bingoNumbers, List<Board> bingoBoards)
    {
        int winningScore = 0;
        List<int> numbersCalled = new List<int>();
        for (int i = 0; i < bingoNumbers.Length; i++)
        {
            int newNumber = Convert.ToInt32(bingoNumbers[i]);
            numbersCalled.Add(newNumber);
            if (CheckForWinner(numbersCalled, bingoBoards, out winningScore))
            {
                return winningScore;
            }
        }
        return winningScore;
    }

    private static bool CheckForWinner(List<int> numbersCalled, List<Board> bingoBoards, out int winningScore)
    {
        winningScore = 0;
        for (int i = 0; i < bingoBoards.Count; i++)
        {
            if (IsBoardAWinner(numbersCalled, bingoBoards[i]))
            {
                winningScore = CalculateWinningScore(numbersCalled, bingoBoards[i]);
                return true;
            }
        }
        return false;
    }

    private static int CalculateWinningScore(List<int> numbersCalled, Board board)
    {
        int winningScore = 0;
        return winningScore;
    }

    private static bool IsBoardAWinner(List<int> numbersCalled, Board board)
    {
        foreach (List<int> solution in board.solutions)
        {
            if (SolutionHasBeenCalled(numbersCalled, solution))
            {
                return true;
            }
        }
        return false;
    }

    private static bool SolutionHasBeenCalled(List<int> numbersCalled, List<int> solution)
    {
        foreach (int num in solution)
        {
            if (!numbersCalled.Contains(num))
            {
                return false;
            }
        }
        return true;
    }

    public class Board
    {
        public Board()
        {
            solutions = new List<List<int>>();
        }
        public List<List<int>> solutions;

    }

}
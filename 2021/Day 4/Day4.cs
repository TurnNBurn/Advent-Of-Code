using System;

public class AdventOfCodeDay4
{

    public static void run()
    {
        string[] lines = System.IO.File.ReadAllLines("./2021/Day 4/Problem1Input.txt");
        int bingoScore = Problem1(lines);
        int losingBingoScore = Problem2(lines);

        Console.WriteLine("Day 4 - Problem 1: The bingo score of the first board to win is " + bingoScore);
        Console.WriteLine("Day 4 - Problem 2: The score of the last winning board is " + losingBingoScore);
    }

    private static int Problem1(string[] lines)
    {
        string[] bingoNumbers = lines[0].Split(',');
        List<Board> bingoBoards = ParseBingoBoards(lines);
        return FindWinningBoard(bingoNumbers, bingoBoards);
    }

    private static int Problem2(string[] lines)
    {
        string[] bingoNumbers = lines[0].Split(',');
        List<Board> bingoBoards = ParseBingoBoards(lines);
        return FindLastWinningBoard(bingoNumbers, bingoBoards);
    }

    private static List<Board> ParseBingoBoards(string[] lines)
    {
        List<Board> bingoBoards = new List<Board>();
        List<string> boardContents = new List<string>();
        for (int i = 2; i < lines.Length; i++)
        {
            if (lines[i].Equals(String.Empty))
            {
                bingoBoards.Add(BuildBoard(boardContents));
                boardContents.Clear();
            }
            else
            {
                boardContents.Add(lines[i]);
            }
        }
        bingoBoards.Add(BuildBoard(boardContents));
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
            numbersCalled.Add(Convert.ToInt32(bingoNumbers[i]));
            if (CheckForWinner(numbersCalled, bingoBoards, out winningScore))
            {
                return winningScore;
            }
        }
        return winningScore;
    }

    private static int FindLastWinningBoard(string[] bingoNumbers, List<Board> bingoBoards)
    {
        int winningScore = 0;
        List<int> numbersCalled = new List<int>();
        for (int i = 0; i < bingoNumbers.Length; i++)
        {
            numbersCalled.Add(Convert.ToInt32(bingoNumbers[i]));
            if (CheckForLastWinner(numbersCalled, bingoBoards, out winningScore))
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

    private static bool CheckForLastWinner(List<int> numbersCalled, List<Board> bingoBoards, out int winningScore)
    {
        winningScore = 0;
        List<Board> boardsToRemove = new List<Board>();
        for (int i = 0; i < bingoBoards.Count; i++)
        {
            if (IsBoardAWinner(numbersCalled, bingoBoards[i]))
            {
                if (bingoBoards.Count == 1)
                {
                    winningScore = CalculateWinningScore(numbersCalled, bingoBoards[i]);
                    return true;
                }
                else
                {
                    boardsToRemove.Add(bingoBoards[i]);
                }
            }
        }
        foreach (Board board in boardsToRemove)
        {
            bingoBoards.Remove(board);
        }
        return false;
    }

    private static int CalculateWinningScore(List<int> numbersCalled, Board board)
    {
        int winningScore = 0;
        HashSet<int> boardNumbers = new HashSet<int>();
        foreach (List<int> solution in board.solutions)
        {
            foreach (int num in solution)
            {
                if (!numbersCalled.Contains(num))
                {
                    boardNumbers.Add(num);
                }
            }
        }
        foreach (int num in boardNumbers)
        {
            winningScore += num;
        }
        return winningScore * numbersCalled[numbersCalled.Count - 1];
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
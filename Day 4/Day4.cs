using System;

public class AdventOfCodeDay4
{

    public static void run()
    {
        string[] lines = System.IO.File.ReadAllLines("./Day 3/Problem1Input.txt");
        int bingoScore = Problem1(lines);
        //int lifeSupport = Problem2(lines);

        Console.WriteLine("Day 4 - Problem 1: The bingo score of the first board to win is " + bingoScore);
    }

    private static int Problem1(string[] lines)
    {
        int bingoScore = 0;
        string[] bingoNumbers = lines[0].Split(',');
        return bingoScore;
    }

    private static List<Board> ParseBingoBoards(string[] lines)
    {
        List<Board> bingoBoards = new List<Board>();
        List<string> boardContents = new List<string>();
        for (int i = 2; i < lines.Length; i++)
        {
            if (lines[i].Equals(String.Empty))
            {
                Board temp = BuildBoard(boardContents);
                bingoBoards.Add(temp);
                boardContents.Clear();
            }
            else
            {
                boardContents.Add(lines[i]);
            }
        }
        return bingoBoards;
    }

    private static Board BuildBoard(List<string> boardContents)
    {
        Board board = new Board();
        List<List<int>> columns = new List<List<int>>();
        for (int i = 0; i < boardContents.Count; i++)
        {
            string[] row = boardContents[i].Split(',');
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

    public class Board
    {
        public Board()
        {
            solutions = new List<List<int>>();
        }
        public List<List<int>> solutions;

    }

}
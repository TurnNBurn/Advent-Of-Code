using System;

public class AdventOfCodeDay21
{

    public static void run()
    {
        string[] lines = System.IO.File.ReadAllLines("./Day 21/Problem1Input.txt");
        int product = Problem1(lines);
        long mostWins = Problem2(lines);
        Console.WriteLine("Day 21 - Problem 1: The product of the losers score and number of dice rolls is " + product);
        Console.WriteLine("Day 21 - Problem 2: The player with the most wins won in " + mostWins + " universes");
    }

    private static int Problem1(string[] lines)
    {
        int[] positions = GetStartingPositions(lines);
        int[] scores = { 0, 0 };
        int rolls = 0;
        while (scores[0] < 1000 && scores[1] < 1000)
        {
            rolls += 3;
            if (rolls % 2 == 1)
            {
                positions[0] = positions[0] + GetLastThreeRolls(rolls);
                positions[0] = ReducePosition(positions[0]);
                scores[0] += positions[0];
            }
            else
            {
                positions[1] = positions[1] + GetLastThreeRolls(rolls);
                positions[1] = ReducePosition(positions[1]);
                scores[1] += positions[1];
            }
        }
        return Math.Min(scores[0], scores[1]) * rolls;
    }

    private static long Problem2(string[] lines)
    {
        long[] wins = { 0, 0 };
        int[] positions = GetStartingPositions(lines);
        Dictionary<Game, long> games = new Dictionary<Game, long>();
        games.Add(new Game(positions[0], positions[1]), 1);
        ProcessRoll(games, 3, wins);
        return Math.Max(wins[0], wins[1]);
    }

    private static void ProcessRoll(Dictionary<Game, long> games, int rolls, long[] wins)
    {
        Dictionary<Game, long> newGames = new Dictionary<Game, long>();

        foreach (KeyValuePair<Game, long> game in games)
        {
            for (int i = 1; i < 4; i++)
            {
                for (int j = 1; j < 4; j++)
                {
                    for (int k = 1; k < 4; k++)
                    {
                        if (rolls % 2 == 1)
                        {
                            Game newGame = new Game(ReducePosition(game.Key.player1.position + i + j + k), game.Key.player2.position);
                            newGame.player1.score = game.Key.player1.score + newGame.player1.position;
                            newGame.player2.score = game.Key.player2.score;
                            if (newGame.player1.score > 20)
                            {
                                wins[0] += game.Value;
                            }
                            else
                            {
                                if (newGames.ContainsKey(newGame))
                                {
                                    newGames[newGame] += game.Value;
                                }
                                else
                                {
                                    newGames.Add(newGame, game.Value);
                                }
                            }
                        }
                        else
                        {
                            Game newGame = new Game(game.Key.player1.position, ReducePosition(game.Key.player2.position + i + j + k));
                            newGame.player2.score = game.Key.player2.score + newGame.player2.position;
                            newGame.player1.score = game.Key.player1.score;
                            if (newGame.player2.score > 20)
                            {
                                wins[1] += game.Value;
                            }
                            else
                            {
                                if (newGames.ContainsKey(newGame))
                                {
                                    newGames[newGame] += game.Value;
                                }
                                else
                                {
                                    newGames.Add(newGame, game.Value);
                                }
                            }
                        }
                    }
                }
            }
        }
        if (newGames.Count > 0)
        {
            ProcessRoll(newGames, rolls + 3, wins);
        }
    }

    private static int GetLastThreeRolls(int rolls)
    {
        if (rolls % 100 == 0)
        {
            return 297;
        }
        else if (rolls % 100 == 1)
        {
            return 200;
        }
        else if (rolls % 100 == 2)
        {
            return 103;
        }
        return (3 * (rolls % 100)) - 3;
    }

    private static int ReducePosition(int position)
    {
        if (position <= 10)
        {
            return position;
        }
        if (position % 10 == 0)
        {
            return 10;
        }
        return position % 10;
    }

    private static int[] GetStartingPositions(string[] lines)
    {
        int[] startingPositions = new int[2];
        startingPositions[0] = Convert.ToInt32(lines[0].Split(':')[1]);
        startingPositions[1] = Convert.ToInt32(lines[1].Split(':')[1]);
        return startingPositions;
    }
}

public class Game
{
    public Player player1;
    public Player player2;

    public Game(int player1Start, int player2Start)
    {
        player1 = new Player(player1Start);
        player2 = new Player(player2Start);
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as Game);
    }

    public bool Equals(Game? other)
    {
        return other != null && other.player1.position == player1.position
                            && other.player1.score == player1.score
                            && other.player2.position == player2.position
                            && other.player2.score == player2.score;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(player1.position, player1.score, player2.position, player2.score);
    }

}

public class Player
{
    public int position;
    public int score;
    public Player(int startPosition)
    {
        position = startPosition;
        score = 0;
    }
}
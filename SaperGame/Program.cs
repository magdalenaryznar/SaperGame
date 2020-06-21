using SaperGame.Core;
using System;
using System.Collections.Generic;

namespace SaperGame
{
    class Program
    {
        private static Game game;
        static void Main(string[] args)
        {
            Console.WriteLine("Wybierz poziom gry. Wpisz i zatwierdz:");
            Console.WriteLine("1. Easy");
            Console.WriteLine("2. Medium");
            Console.WriteLine("3. Hard");

            Level gameLevel = (Level)int.Parse(Console.ReadLine());

            game = new Game(gameLevel);

            List<string> moves = new List<string>();

            try
            {
                while (game.GameInProgress())
                {
                    Console.WriteLine("Poziom: " + gameLevel.ToString());
                    Console.WriteLine();

                    PrintGame();

                    Console.WriteLine("Podaj wspolrzedne w formie x y");
                    InformAboutFields(game.Level);
                    string field = Console.ReadLine();
                    moves.Add(field);

                    bool fieldResult = game.SetField(field);
                    if (!fieldResult)
                    {
                        Console.WriteLine("Try different field!");
                    }
                    Console.Clear();
                }
            }
            catch (Exception exception)
            {
                PrintGame();
                Console.ForegroundColor = ConsoleColor.Red;

                Console.WriteLine(exception.Message);

                Console.ForegroundColor = ConsoleColor.White;
            }

            var result = game.GetResult();
            if (result.Win)
            {
                Console.WriteLine("YOU WON!");
            }

            Console.WriteLine("Game results: ");
            Console.WriteLine("Score: " + result.Score);
            Console.WriteLine("Time in minutes: " + Math.Round(result.Time, 2));

            Console.WriteLine("Your moves:");
            foreach (string move in moves)
            {
                Console.WriteLine(move);
            }

            Console.ReadLine();
        }

        private static void InformAboutFields(Level level)
        {
            switch (level)
            {
                case Level.Easy:
                    Console.WriteLine("Put values between 0 to 5");
                    break;
                case Level.Medium:
                    Console.WriteLine("Put values between 0 to 8");
                    break;
                case Level.Hard:
                    Console.WriteLine("Put values between 0 to 11");
                    break;
            }
        }

        private static void PrintGame()
        {
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.White;

            var size = game.Size();

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (game.Fields[i, j].Visible)
                    {
                        Console.Write(game.Fields[i, j].Value);
                    }
                    else if (game.Fields[i, j].Visible && game.Fields[i, j].Value == 0)
                    {
                        Console.Write(" ");
                    }
                    else if (game.Fields[i, j].Visible && game.Fields[i, j].Value == 9)
                    {
                        Console.Write("#");
                    }
                    else
                    {
                        Console.Write("x");
                    }

                }

                Console.WriteLine();
            }

            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
        }
    }
}

using SaperGame.Core;
using SniperGame.Core;
using System;

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

            try
            {
                while (game.GameInProgress())
                {
                    Console.WriteLine("Poziom: " + gameLevel.ToString());
                    Console.WriteLine();

                    PrintGame();

                    Console.WriteLine("Podaj wspolrzedne w formie x y");
                    string field = Console.ReadLine();
                    game.SetField(field);

                    Console.Clear();
                }
            }
            catch(Exception exception)
            {
                PrintGame();

                Console.WriteLine(exception.Message);
                Console.ReadLine();
            }

            var result = game.Result();
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
                    if(game.Fields[i,j].Visible)
                    {
                        Console.Write(game.Fields[i, j].Value);
                    }
                    else if(game.Fields[i, j].Visible && game.Fields[i, j].Value == 0)
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

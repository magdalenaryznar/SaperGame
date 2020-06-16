using SniperGame.Core;
using System;

namespace SaperGame.Core
{
    public class Game
    {
        public SaperField[,] Fields { get; private set; }

        private int score;

        private bool gameInProgress;

        public Game(Level level)
        {
            switch (level)
            {
                case Level.Easy:
                    GenerateGame(6);
                    break;
                case Level.Medium:
                    GenerateGame(9);
                    break;
                case Level.Hard:
                    GenerateGame(12);
                    break;
            }
            gameInProgress = true;
        }

        private void GenerateGame(int size)
        {
            Fields = new SaperField[size, size];
            Random random = new Random();

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Fields[i, j] = new SaperField(random.Next(1, 8), false);
                }
            }
        }

        public GameResult Result()
        {
            return new GameResult(0, 0, false);
        }

        public bool GameInProgress()
        {
            return gameInProgress;
        }

        public int Size()
        {
            return (int)Math.Sqrt(Fields.Length);
        }
    }
}

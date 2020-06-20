using SaperGame.Core;
using System;
using System.Security.Cryptography.X509Certificates;

namespace SaperGame.Core
{
    public class Game
    {
        public SaperField[,] Fields { get; private set; }

        private const int Mine = 9;

        private int score;

        private bool gameInProgress;

        public Game(Level level)
        {
            switch (level)
            {
                case Level.Easy:
                    GenerateGame(6, 2);
                    break;
                case Level.Medium:
                    GenerateGame(9, 5);
                    break;
                case Level.Hard:
                    GenerateGame(12, 8);
                    break;
            }
            gameInProgress = true;
        }

        private void GenerateGame(int size, int mines)
        {
            Fields = new SaperField[size, size];
            Random random = new Random();

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Fields[i, j] = new SaperField(random.Next(0, 9), false);
                }
            }
            SetMines(size, mines);
        }

        public void SetField(string fieldValue)
        {
            var values = fieldValue.Split(' ');
            var x = int.Parse(values[0]);
            var y = int.Parse(values[1]);

            SetField(x, y);
            SetField(x - 1, y - 1);
            SetField(x - 1, y);
            SetField(x - 1, y + 1);
            SetField(x + 1, y - 1);
            SetField(x + 1, y);
            SetField(x + 1, y + 1);
            SetField(x, y - 1);
            SetField(x, y);
            SetField(x, y + 1);
        }

        public void SetField(int x, int y)
        {
            if (x < 0 || x > Size()) return; // poza tablicą wyjście
            if (y < 0 || y > Size()) return; // poza tablicą wyjście
           
            var field = Fields[x, y];
            if (field.Visible) return;

            field.Visible = true;

            if (field.Value == Mine)
            {
                field.Value = Mine;
                throw new Exception("Mine found! You lost :(");
            }
        }

        private void SetMines(int size, int mines)
        {
            Random random = new Random();

            while (mines > 0)
            {
                var x = random.Next(1, size - 1);
                var y = random.Next(1, size - 1);

                if (Fields[x, y].Value != Mine)
                {
                    SetMine(x, y);
                    mines--;
                }
            }
        }

        private bool SetMine(int x, int y)
        {
            if (Fields[x, y].Value != Mine)
            {
                Fields[x, y].Value = Mine;

                for (int k = -1; k < 2; k++)
                    for (int l = -1; l < 2; l++)
                    {
                        if ((x + l) < 0 || (y + k) < 0) continue; //wyjdz bo krawedz
                        if ((x + l) > 9 || (y + k) > 9) continue; //wyjdz bo krawedz

                        if (Fields[x + l, y + k].Value == Mine) continue; //wyjdz bo mina
                        Fields[x + l, y + k].Value += 1; //zwieksz o 1
                    }
            }

            return true;
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

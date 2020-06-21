using System;

namespace SaperGame.Core
{
    public class Game
    {
        public SaperField[,] Fields { get; private set; }
        public Level Level { get; set; }

        private const int Mine = 9;

        private int score = 0;

        private DateTime start;

        private bool gameInProgress;

        private bool hasWin = false;

        public Game(Level level)
        {
            start = DateTime.Now;

            Level = level;

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


        public GameResult GetResult()
        {
            return new GameResult(score, (DateTime.Now - start).TotalMinutes, hasWin);
        }

        public bool SetField(string fieldValue)
        {
            try
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

                return true;
            }
            catch (IndexOutOfRangeException)
            {
                return false;
            }
        }

        public int? SetField(int x, int y)
        {

            if (x < 0 || x > Size()) return null;
            if (y < 0 || y > Size()) return null;

            var field = Fields[x, y];
            if (field.Visible) return null;

            field.Visible = true;

            if (field.Value == Mine)
            {
                field.Value = Mine;
                throw new Exception("Mine found! You lost :(");
            }

            score += field.Value;

            return field.Value;

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
                        if ((x + l) < 0 || (y + k) < 0) continue;

                        if ((x + l) > 9 || (y + k) > 9) continue;

                        if (Fields[x + l, y + k].Value == Mine) continue;
                        Fields[x + l, y + k].Value += 1;
                    }
            }

            return true;
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

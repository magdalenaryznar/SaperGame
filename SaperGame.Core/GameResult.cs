using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SaperGame.Core
{
    public class GameResult
    {
        public int Score { get; }

        public bool Win { get; }

        public int Time { get;  }

        public GameResult(int score, int time, bool win)
        {
            Score = score;
            Win = win;
            Time = time;
        }
    }
}

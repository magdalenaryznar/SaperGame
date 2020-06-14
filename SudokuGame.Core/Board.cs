namespace SudokuGame.Core
{
    public class Board
    {
        public string[,] Fields { get; private set; }

        public Board(int size)
        {
            Fields = new string[size, size];
        }
    }
}

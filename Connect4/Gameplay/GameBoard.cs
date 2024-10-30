using System.Drawing;

namespace Connect4
{
    public class GameBoard
    {
        public const int height = 6;
        public const int width = 7;
        public static int[,] boardValues = new int[height, width];
                
        public static Color GetColour(int row, int column)
        {
            int player = boardValues[row, column];

            switch (player)
            {
                case 0:
                    return Color.White;
                case 1:
                    return Color.Yellow;
                case 2:
                    return Color.Red;
            }
            return Color.Black;
        }
    }
}

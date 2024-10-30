using System;
using System.Collections.Generic;
using System.Linq;

namespace Connect4
{
    internal class AI
    {
        int value = 0;
        bool[] validColumns = new bool[7];
        //consecutive scores
        int player2 = 2;
        int player3 = 5;
        int player4 = 1000000;
        int opponent3 = 6;

        public void RunAI(int[,] boardValues)
        {
            int winningCol = -1;
            int losingCol = -1;
            bool[] blockedCols = new bool[7];

            for (int col = 0; col < GameBoard.width; col++)
            {
                blockedCols[col] = false;
                int row = GetRow(boardValues, col);
                if (row >= 0)
                {
                    boardValues[row, col] = ColourSelect.playerColour;
                    if (GameScreen.CheckTerminalNode(boardValues, ColourSelect.playerColour, true))
                    {
                        losingCol = col;
                    }

                    boardValues[row, col] = GameScreen.aiColour;
                    if (GameScreen.CheckTerminalNode(boardValues, GameScreen.aiColour, true))
                    {
                        winningCol = col;
                    }

                    if (row - 1 >= 0)
                    {
                        boardValues[row - 1, col] = ColourSelect.playerColour;
                        if (GameScreen.CheckTerminalNode(boardValues, ColourSelect.playerColour, true))
                        {
                            blockedCols[col] = true;
                        }
                        boardValues[row - 1, col] = 0;
                    }

                    boardValues[row, col] = 0;

                }
            }
            if (winningCol >= 0)
            {
                int row = GetRow(boardValues, winningCol);
                boardValues[row, winningCol] = GameScreen.aiColour;
                GameScreen.CheckAI();
            }
            else if (losingCol >= 0)
            {
                int row = GetRow(boardValues, losingCol);
                boardValues[row, losingCol] = GameScreen.aiColour;
                GameScreen.CheckAI();
            }
            else
            {
                var moves = new List<Tuple<int, int>>();
                for (int col = 0; col < GameBoard.width; col++)
                {
                    int row = GetRow(boardValues, col);
                    if (row >= 0)
                    {
                        boardValues[row, col] = GameScreen.aiColour;
                        AI AI = new AI();
                        moves.Add(Tuple.Create(col, AI.MinMax(boardValues, 7, int.MinValue, int.MaxValue, false)));
                        boardValues[row, col] = 0;
                    }
                }
                
                moves.OrderBy(t => t.Item1);
                int[] columnsArray = new int[moves.Count];
                int[] scoresArray = new int[moves.Count];
                int maxScore = 0;
                int maxCol = columnsArray[0];
                if (moves.Count != 1)
                {
                    for (int col = 0; col < moves.Count; col++)
                    {
                        columnsArray[col] = moves[col].Item1;
                        scoresArray[col] = moves[col].Item2;
                        if (blockedCols[columnsArray[col]])
                        {
                            scoresArray[col] = int.MinValue;
                        }
                    }

                    for (int col = 0; col < moves.Count; col++)
                    {
                        if (scoresArray[col] == maxScore)
                        {
                            Random random = new Random();
                            int rand = random.Next(0, 2);
                            if (rand == 1)
                            {
                                maxScore = scoresArray[col];
                                maxCol = columnsArray[col];
                            }
                        }
                        if (scoresArray[col] > maxScore)
                        {
                            maxScore = scoresArray[col];
                            maxCol = columnsArray[col];
                        }
                    }
                    int randomColumn = RandomiseColumn(columnsArray, GameScreen.depth);
                    if (randomColumn >= 0)
                    {
                        maxCol = randomColumn;
                    }
                }
                int openCols = 0;
                for (int col = 0; col < GameBoard.width; col++)
                {
                    if (boardValues[0, col] == 0)
                    {
                        openCols++;
                    }
                }
                if (openCols == 1)
                {
                    for (int col = 0; col < GameBoard.width; col++)
                    {
                        if (boardValues[0, col] == 0)
                        {
                            maxCol = col;
                            break;
                        }
                    }
                }
                int row2 = GetRow(boardValues, maxCol);
                if (row2 == -1)
                {
                    for (int col = 0; col < GameBoard.width; col++)
                    {
                        if (boardValues[0,col] == 0)
                        {
                            maxCol = col;
                            break;
                        }
                    }
                    row2 = GetRow(boardValues, maxCol);
                }
                boardValues[row2, maxCol] = GameScreen.aiColour;
                GameScreen.CheckAI();
            }
        }

        private int MinMax(int[,] board, int depth, int alpha, int beta, bool maximisingPlayer)
        {
            validColumns = GetValidColumns(board);

            if (GameScreen.CheckTerminalNode(board, GameScreen.aiColour, true))
            {
                return int.MaxValue;
            }
            if (GameScreen.CheckTerminalNode(board, ColourSelect.playerColour, true))
            {
                return int.MinValue;
            }

            if (depth == 0)
            {
                return ScoreEvaluation(board);
            }

            int invalidCols = 0;
            for (int col = 0; col < GameBoard.width; col++)
            {
                if (!validColumns[col])
                {
                    invalidCols++;
                }
            }

            if (invalidCols == GameBoard.width)
            {
                return 0;
            }

            if (maximisingPlayer)
            {
                value = int.MinValue;
                for (int col = 0; col < GameBoard.width; col++)
                {
                    if (!validColumns[col])
                    {
                        break;
                    }
                    int row = GetRow(board, col);
                    int[,] boardCopy = (int[,])board.Clone();
                    boardCopy[row, col] = GameScreen.aiColour;
                    value = Math.Max(value, MinMax(boardCopy, depth - 1, alpha, beta, false));
                    alpha = Math.Max(alpha, value);
                    if (alpha >= beta)
                    {
                        break;
                    }
                }
                return value;
            }

            else
            {
                value = int.MaxValue;
                for (int col = 0; col < GameBoard.width; col++)
                {
                    if (!validColumns[col])
                    {
                        break;
                    }
                    int row = GetRow(board, col);
                    int[,] boardCopy = (int[,])board.Clone();
                    boardCopy[row, col] = ColourSelect.playerColour;
                    value = Math.Min(value, MinMax(boardCopy, depth - 1, alpha, beta, true));
                    beta = Math.Min(beta, value);
                    if (beta <= alpha)
                    {
                        break;
                    }
                }
                return value;
            }
        }

        private int ScoreEvaluation(int[,] board)
        {
            int testingColour = GameScreen.aiColour;
            int oppositeColour = ColourSelect.playerColour;
            int evaluationScore = 0;
            int consecutive = 0;
            for (int row = 0; row < 4; row++)
            {
                if (board[row, 3] == testingColour)
                {
                    evaluationScore++;
                }
            }

            evaluationScore *= 2;

            // Horizontal
            for (int col = 0; col < GameBoard.width - 3; col++)
            {
                for (int row = 0; row < GameBoard.height; row++)
                {
                    if (board[row, col] == testingColour)
                    {
                        consecutive++;
                        switch (consecutive)
                        {
                            case 2:
                                if (board[row, col + 1] == 0 && board[row, col + 2] == 0)
                                {
                                    evaluationScore += player2;
                                }
                                break;
                            case 3:
                                if (board[row, col + 1] == 0)
                                {
                                    evaluationScore += player3;
                                }
                                break;
                            case 4:
                                evaluationScore += player4;
                                break;
                        }
                    }
                    else if (board[row, col] == oppositeColour)
                    {
                        if (board[row, col + 1] == oppositeColour && board[row, col + 2] == oppositeColour && board[row, col + 3] == 0)
                        {
                            evaluationScore -= opponent3;
                        }

                        consecutive = 0;
                    }
                    else
                    {
                        consecutive = 0;
                    }
                }
            }

            // Vertical
            consecutive = 0;
            for (int col = 0; col < GameBoard.width; col++)
            {
                for (int row = GameBoard.height - 1; row > 3; row--)
                {
                    if (board[row, col] == testingColour)
                    {
                        consecutive++;
                        switch (consecutive)
                        {
                            case 2:
                                if (board[row - 1, col] == 0 && board[row - 2, col] == 0)
                                {
                                    evaluationScore += player2;
                                }
                                break;
                            case 3:
                                if (board[row - 1, col] == 0)
                                {
                                    evaluationScore += player3;
                                }
                                break;
                            case 4:
                                evaluationScore += player4;
                                break;
                        }
                    }
                    else if (board[row, col] == oppositeColour)
                    {
                        if (board[row - 1, col] == oppositeColour && board[row - 2, col] == oppositeColour && board[row - 3, col] == 0)
                        {
                            evaluationScore -= opponent3;
                        }

                        consecutive = 0;
                    }
                    else
                    {
                        consecutive = 0;
                    }
                }
            }

            //diagonal upward sloping
            for (int col = 0; col < GameBoard.width - 3; col++)
            {
                for (int row = 0; row < GameBoard.height - 3; row++)
                {
                    if (board[row, col] == testingColour && board[row + 1, col + 1] == testingColour && board[row + 2, col + 2] == testingColour && board[row + 3, col + 3] == testingColour)
                    {
                        evaluationScore += player4;
                    }
                    else if (board[row, col] == testingColour && board[row + 1, col + 1] == testingColour && board[row + 2, col + 2] == testingColour)
                    {
                        evaluationScore += player3;
                    }
                    else if (board[row, col] == oppositeColour && board[row + 1, col + 1] == oppositeColour && board[row + 2, col + 2] == oppositeColour)
                    {
                        evaluationScore -= opponent3;
                    }
                    else if (board[row, col] == testingColour && board[row + 1, col + 1] == testingColour)
                    {
                        evaluationScore += player2;
                    }
                }
            }
            //diagonal downward sloping
            for (int col = 0; col < GameBoard.width - 3; col++)
            {
                for (int row = 3; row < GameBoard.height; row++)
                {
                    if (board[row, col] == testingColour && board[row - 1, col + 1] == testingColour && board[row - 2, col + 2] == testingColour && board[row - 3, col + 3] == testingColour)
                    {
                        evaluationScore += player4;
                    }
                    else if (board[row, col] == testingColour && board[row - 1, col + 1] == testingColour && board[row - 2, col + 2] == testingColour)
                    {
                        evaluationScore += player3;
                    }
                    else if (board[row, col] == oppositeColour && board[row - 1, col + 1] == oppositeColour && board[row - 2, col + 2] == oppositeColour)
                    {
                        evaluationScore -= opponent3;
                    }
                    else if (board[row, col] == testingColour && board[row - 1, col + 1] == testingColour)
                    {
                        evaluationScore += player2;
                    }
                }
            }

            return evaluationScore;
        }

        private bool[] GetValidColumns(int[,] board)
        {
            for (int col = 0; col < GameBoard.width; col++)
            {
                validColumns[col] = false;
                if (board[0, col] == 0)
                {
                    validColumns[col] = true;
                }
                else
                {
                    validColumns[col] = false;
                }
            }
            return validColumns;
        }

        private static int GetRow(int[,] board, int col)
        {
            for (int row = 5; row > -1; row--)
            {
                if (board[row, col] == 0)
                {
                    return row;
                }
            }
            return -1;
        }

        private static int RandomiseColumn(int[] columnsArray, int depth)
        {
            int maxRandom = 0;
            switch (depth)
            {
                case 1:
                    maxRandom = 2;
                    break;
                case 2:
                    maxRandom = 4;
                    break;
                case 3:
                    maxRandom = 6;
                    break;
                case 4:
                    maxRandom = 8;
                    break;
                case 5:
                    return -1;
            }
            Random random = new Random();
            int rand = random.Next(maxRandom);
            if (rand == 1)//selected integer to continue with randomising process that works for every difficulty
            {
                Random random2 = new Random();
                int rand2 = random2.Next(columnsArray.Length);
                return rand2;
            }
            return -1;
        }
    }
}

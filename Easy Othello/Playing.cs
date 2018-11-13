
/// <summary>
/// 需要注意的事：
/// 在本代码（判定部分）中，x指棋盘的水平方向，即col
/// y指棋盘的竖直方向，即row
/// </summary>

namespace Easy_Othello
{
    class Playing
    {
        public const int N = 8;   //棋盘大小
        public int[,] ChessBoard = new int[N, N]{
            {0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0},
            {0,0,0,1,-1,0,0,0},
            {0,0,0,-1,1,0,0,0},
            {0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0} };
        public int blackCount = 2;
        public int whiteCount = 2;

        public Playing()
        {

        }

        // count the black chess
        public int countBlack()
        {
            int count = 0;
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    if (ChessBoard[i, j] == 1)
                        count++;
                }
            }
            return count;
        }

        // count the white chess
        public int countWhite()
        {
            int count = 0;
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    if (ChessBoard[i, j] == -1)
                        count++;
                }
            }
            return count;
        }

        // judge whether the chessboard can set chess
        public bool canSetChess(int color)
        {
            for (int i = 0; i < N; i++)
                for (int j = 0; j < N; j++)
                    if (ChessBoard[i, j] == 0)
                        if (canSetChess(i, j, 1))
                            return true;
            return false;
        }

        // judge whether the location can set chess
        public bool canSetChess(int x, int y, int color)
        {
            if (judgeUp(x, y, color) || judgeDown(x, y, color)
                    || judgeLeft(x, y, color) || judgeRight(x, y, color)
                    || judgeUpLeft(x, y, color) || judgeUpRight(x, y, color)
                    || judgeDownLeft(x, y, color) || judgeDownRight(x, y, color))
                return true;
            return false;
        }

        // when click the mouse change the chess color
        public void reverse(int x, int y, int color)
        {

            if (judgeLeft(x, y, color))
            {
                for (int i = x - 1; i >= 0; i--)
                {
                    if (ChessBoard[i, y] == -color)
                        ChessBoard[i, y] = color;
                    else
                        break;
                }
            }

            if (judgeRight(x, y, color))
            {
                for (int i = x + 1; i <= 7; i++)
                {
                    if (ChessBoard[i, y] == -color)
                        ChessBoard[i, y] = color;
                    else
                        break;
                }
            }

            if (judgeUp(x, y, color))
            {
                for (int i = y - 1; i >= 0; i--)
                {
                    if (ChessBoard[x, i] == -color)
                        ChessBoard[x, i] = color;
                    else
                        break;
                }
            }

            if (judgeDown(x, y, color))
            {
                for (int i = y + 1; i <= 7; i++)
                {
                    if (ChessBoard[x, i] == -color)
                        ChessBoard[x, i] = color;
                    else
                        break;
                }
            }

            if (judgeUpLeft(x, y, color))
            {
                if (x < y)
                    for (int i = x - 1; i >= 0; i--)
                    {
                        if (ChessBoard[i, i + y - x] == -color)
                            ChessBoard[i, i + y - x] = color;
                        else
                            break;
                    }
                else
                    for (int i = y - 1; i >= 0; i--)
                    {
                        if (ChessBoard[i + x - y, i] == -color)
                            ChessBoard[i + x - y, i] = color;
                        else
                            break;
                    }
            }

            if (judgeUpRight(x, y, color))
            {
                if ((7 - x) < y)
                    for (int i = 7 - x; i >= 2; i--)
                    {
                        if (ChessBoard[x + 1 + ((7 - x) - i), y - 1 - ((7 - x) - i)] == -color)
                            ChessBoard[x + 1 + ((7 - x) - i), y - 1 - ((7 - x) - i)] = color;
                        else
                            break;
                    }
                else
                    for (int i = y - 2; i >= 0; i--)
                    {
                        if (ChessBoard[x + 1 + ((y - 2) - i), y - 1 - ((y - 2) - i)] == -color)
                            ChessBoard[x + 1 + ((y - 2) - i), y - 1 - ((y - 2) - i)] = color;
                        else
                            break;
                    }
            }

            if (judgeDownLeft(x, y, color))
            {

                if ((7 - y) < x)
                    for (int i = 7 - y; i >= 2; i--)
                    {
                        if (ChessBoard[x - 1 - ((7 - y) - i), y + 1 + ((7 - y) - i)] == -color)
                            ChessBoard[x - 1 - ((7 - y) - i), y + 1 + ((7 - y) - i)] = color;
                        else
                            break;
                    }
                else
                    for (int i = x - 2; i >= 0; i--)
                    {
                        if (ChessBoard[x - 1 - ((x - 2) - i), y + 1 + ((x - 2) - i)] == -color)
                            ChessBoard[x - 1 - ((x - 2) - i), y + 1 + ((x - 2) - i)] = color;
                        else
                            break;
                    }
            }

            if (judgeDownRight(x, y, color))
            {

                if (x > y)
                    for (int i = x + 1; i <= 7; i++)
                    {
                        if (ChessBoard[x + 2 + (i - (x + 2)), y + 2 + (i - (x + 2))] == -color)
                            ChessBoard[x + 2 + (i - (x + 2)), y + 2 + (i - (x + 2))] = color;
                        else
                            break;
                    }
                else
                    for (int i = y + 1; i <= 7; i++)
                    {
                        if (ChessBoard[i + x - y, i] == -color)
                            ChessBoard[i + x - y, i] = color;
                        else
                            break;
                    }
            }

            ChessBoard[x, y] = color;
        }

        // judge whether the left will be changed if set a chess at a location
        private bool judgeLeft(int x, int y, int color)
        {
            if (x >= 2 && ChessBoard[x - 1, y] == -color)
            {
                for (int i = x - 2; i >= 0; i--)
                {
                    if (ChessBoard[i, y] == color)
                        return true;
                }
            }
            return false;
        }

        // judge whether the right will be changed if set a chess at a location
        private bool judgeRight(int x, int y, int color)
        {
            if (x <= 5 && ChessBoard[x + 1, y] == -color)
            {
                for (int i = x + 2; i <= 7; i++)
                {
                    if (ChessBoard[i, y] == color)
                        return true;
                }
            }
            return false;
        }

        // judge whether the up will be changed if set a chess at a location
        private bool judgeUp(int x, int y, int color)
        {
            if (y >= 2 && ChessBoard[x, y - 1] == -color)
            {
                for (int i = y - 2; i >= 0; i--)
                {
                    if (ChessBoard[x, i] == color)
                        return true;
                }
            }
            return false;
        }

        // judge whether the down will be changed if set a chess at a location
        private bool judgeDown(int x, int y, int color)
        {
            if (y <= 5 && ChessBoard[x, y + 1] == -color)
            {
                for (int i = y + 2; i <= 7; i++)
                {
                    if (ChessBoard[x, i] == color)
                        return true;
                }
            }
            return false;
        }

        // judge whether the up-left will be changed if set a chess at a location
        private bool judgeUpLeft(int x, int y, int color)
        {
            if (x >= 2 && y >= 2 && ChessBoard[x - 1, y - 1] == -color)
            {
                if (x < y)
                    for (int i = x - 2; i >= 0; i--)
                    {
                        if (ChessBoard[i, i + y - x] == color)
                            return true;
                    }
                else
                    for (int i = y - 2; i >= 0; i--)
                    {
                        if (ChessBoard[i + x - y, i] == color)
                            return true;
                    }
            }
            return false;
        }

        // judge whether the down-right will be changed if set a chess at a location
        private bool judgeDownRight(int x, int y, int color)
        {
            if (x <= 5 && y <= 5 && ChessBoard[x + 1, y + 1] == -color)
            {
                if (x > y)
                    for (int i = x + 2; i <= 7; i++)
                    {
                        if (ChessBoard[x + 2 + (i - (x + 2)), y + 2 + (i - (x + 2))] == color)
                            return true;
                    }
                else
                    for (int i = y + 2; i <= 7; i++)
                    {
                        if (ChessBoard[i + x - y, i] == color)
                            return true;
                    }
            }
            return false;
        }
        // judge whether the up-right will be changed if set a chess at a location
        private bool judgeUpRight(int x, int y, int color)
        {
            if (x <= 5 && y >= 2 && ChessBoard[x + 1, y - 1] == -color)
            {
                if ((7 - x) < y)
                    for (int i = 7 - x; i >= 2; i--)
                    {
                        if (ChessBoard[x + 2 + ((7 - x) - i), y - 2 - ((7 - x) - i)] == color)
                            return true;
                    }
                else
                    for (int i = y - 2; i >= 0; i--)
                    {
                        if (ChessBoard[x + 2 + ((y - 2) - i), y - 2 - ((y - 2) - i)] == color)
                            return true;
                    }
            }
            return false;
        }

        // judge whether the down-left will be changed if set a chess at a location
        private bool judgeDownLeft(int x, int y, int color)
        {
            if (y <= 5 && x >= 2 && ChessBoard[x - 1, y + 1] == -color)
            {
                if ((7 - y) < x)
                    for (int i = 7 - y; i >= 2; i--)
                    {
                        if (ChessBoard[x - 2 - ((7 - y) - i), y + 2 + ((7 - y) - i)] == color)
                            return true;
                    }
                else
                    for (int i = x - 2; i >= 0; i--)
                    {
                        if (ChessBoard[x - 2 - ((x - 2) - i), y + 2 + ((x - 2) - i)] == color)
                            return true;
                    }
            }
            return false;
        }

    }
}

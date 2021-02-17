using ChessBoard;

namespace Chess
{
    class King : Piece
    {
        public King(Board board, Color color) : base(board, color)
        {

        }

        public override bool[,] PossibleMoviments()
        {
            bool[,] mat = new bool[Board.Lines, Board.Columns];
            Position pos;
            for (int i = Position.Line - 1; i <= Position.Line + 1; i++)
            {
                for (int j = Position.Column - 1; j <= Position.Column + 1; j++)
                {
                    pos = new Position(i, j);
                    if (pos == Position)
                    {
                        continue;
                    }
                    else if (Board.IsPositionValid(pos) && IsAbleToMove(pos))
                    {
                        mat[i, j] = true;
                    }
                }
            }
            return mat;
        }

        public override string ToString()
        {
            return "K";
        }
    }
}

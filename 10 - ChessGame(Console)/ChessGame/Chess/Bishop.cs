using ChessBoard;

namespace Chess
{
    class Bishop : Piece
    {
        public Bishop(Board board, Color color) : base(board, color)
        {

        }


        public override string ToString()
        {
            return "B";
        }

        public override bool[,] PossibleMoviments()
        {
            bool[,] mat = new bool[Board.Lines, Board.Columns];
            
            Position pos = new Position(0, 0);
            //NO
            pos.SetValue(Position.Line - 1, Position.Column - 1);
            while (Board.IsPositionValid(pos) && IsAbleToMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
                if (Board.GetPiece(pos) != null && Board.GetPiece(pos).Color != Color) break;
                pos.SetValue(pos.Line - 1, pos.Column - 1);

            }

            //NE
            pos.SetValue(Position.Line - 1, Position.Column + 1);
            while (Board.IsPositionValid(pos) && IsAbleToMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
                if (Board.GetPiece(pos) != null && Board.GetPiece(pos).Color != Color) break;
                pos.SetValue(pos.Line - 1, pos.Column + 1);
            }

            //SE
            pos.SetValue(Position.Line + 1, Position.Column + 1);
            while (Board.IsPositionValid(pos) && IsAbleToMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
                if (Board.GetPiece(pos) != null && Board.GetPiece(pos).Color != Color) break;
                pos.SetValue(pos.Line + 1, pos.Column + 1);
            }

            //SO
            pos.SetValue(Position.Line + 1, Position.Column - 1);
            while (Board.IsPositionValid(pos) && IsAbleToMove(pos))
            {

                mat[pos.Line, pos.Column] = true;
                if (Board.GetPiece(pos) != null && Board.GetPiece(pos).Color != Color) break;
                pos.SetValue(pos.Line + 1, pos.Column - 1);
            }

            return mat;
        }
    }
}

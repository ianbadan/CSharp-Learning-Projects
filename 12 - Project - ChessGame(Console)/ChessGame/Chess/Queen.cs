using ChessBoard;

namespace Chess
{
    class Queen : Piece
    {
        public Queen(Board board, Color color) : base(board, color)
        {

        }

        

        public override string ToString()
        {
            return "Q";
        }

        public override bool[,] PossibleMoviments()
        {
            bool[,] mat = new bool[Board.Lines, Board.Columns];
            //UP
            Position pos = new Position(0, 0);

            pos.SetValue(Position.Line - 1, Position.Column);
            while (Board.IsPositionValid(pos) && IsAbleToMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
                if (Board.GetPiece(pos) != null && Board.GetPiece(pos).Color != Color) break;
                pos.Line--;

            }

            //DOWN
            pos.SetValue(Position.Line + 1, Position.Column);
            while (Board.IsPositionValid(pos) && IsAbleToMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
                if (Board.GetPiece(pos) != null && Board.GetPiece(pos).Color != Color) break;
                pos.Line++;
            }

            //LEFT
            pos.SetValue(Position.Line, Position.Column - 1);
            while (Board.IsPositionValid(pos) && IsAbleToMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
                if (Board.GetPiece(pos) != null && Board.GetPiece(pos).Color != Color) break;
                pos.Column--;
            }

            //RIGHT
            pos.SetValue(Position.Line, Position.Column + 1);
            while (Board.IsPositionValid(pos) && IsAbleToMove(pos))
            {

                mat[pos.Line, pos.Column] = true;
                if (Board.GetPiece(pos) != null && Board.GetPiece(pos).Color != Color) break;
                pos.Column++;
            }

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

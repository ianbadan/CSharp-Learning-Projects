using ChessBoard;

namespace Chess
{
    class Tower : Piece
    {
        public Tower(Board board, Color color) : base(board, color)
        {

        }

        public override bool[,] PossibleMoviments()
        {
            bool[,] mat = new bool[Board.Lines, Board.Columns];
            //UP
            Position pos = new Position(0,0);

            pos.SetValue(Position.Line - 1, Position.Column);
            while( Board.IsPositionValid(pos) && IsAbleToMove(pos))
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

            return mat;
        }

        public override string ToString()
        {
            return "T";
        }
    }
}

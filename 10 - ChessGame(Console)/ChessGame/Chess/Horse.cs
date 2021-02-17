using ChessBoard;

namespace Chess
{
    class Horse : Piece
    {
        public Horse(Board board, Color color) : base(board, color)
        {

        }


        public override string ToString()
        {
            return "H";
        }

        public override bool[,] PossibleMoviments()
        {
            bool[,] mat = new bool[Board.Lines, Board.Columns];

            Position pos = new Position(0, 0);

            pos.SetValue(Position.Line - 1, Position.Column - 2);
            if (Board.IsPositionValid(pos) && IsAbleToMove(pos)) mat[pos.Line, pos.Column] = true;

            pos.SetValue(Position.Line - 2, Position.Column - 1);
            if (Board.IsPositionValid(pos) && IsAbleToMove(pos)) mat[pos.Line, pos.Column] = true;

            pos.SetValue(Position.Line - 2, Position.Column + 1);
            if (Board.IsPositionValid(pos) && IsAbleToMove(pos)) mat[pos.Line, pos.Column] = true;

            pos.SetValue(Position.Line - 1, Position.Column + 2);
            if (Board.IsPositionValid(pos) && IsAbleToMove(pos)) mat[pos.Line, pos.Column] = true;

            pos.SetValue(Position.Line + 1, Position.Column + 2);
            if (Board.IsPositionValid(pos) && IsAbleToMove(pos)) mat[pos.Line, pos.Column] = true;

            pos.SetValue(Position.Line + 2, Position.Column + 1);
            if (Board.IsPositionValid(pos) && IsAbleToMove(pos)) mat[pos.Line, pos.Column] = true;

            pos.SetValue(Position.Line + 2, Position.Column - 1);
            if (Board.IsPositionValid(pos) && IsAbleToMove(pos)) mat[pos.Line, pos.Column] = true;

            pos.SetValue(Position.Line + 1, Position.Column - 2);
            if (Board.IsPositionValid(pos) && IsAbleToMove(pos)) mat[pos.Line, pos.Column] = true;

            return mat;
        }
    }
}

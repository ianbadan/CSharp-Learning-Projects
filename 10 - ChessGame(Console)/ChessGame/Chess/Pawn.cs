using ChessBoard;

namespace Chess
{
    class Pawn : Piece
    {
        public Pawn(Board board, Color color) : base(board, color)
        {

        }

        public override string ToString()
        {
            return "P";
        }

        private bool ExistsEnemy(Position pos)
        {
            Piece p = Board.GetPiece(pos);
            return p != null && p.Color != Color;
        }

        private bool IsPositionEmpty(Position pos)
        {
            return Board.GetPiece(pos) == null;
        }

        public override bool[,] PossibleMoviments()
        {
            bool[,] mat = new bool[Board.Lines, Board.Columns];

            Position pos = new Position(0, 0);

            if(Color == Color.White)
            {
                pos.SetValue(Position.Line - 1, Position.Column);
                if (Board.IsPositionValid(pos) && IsPositionEmpty(pos)) mat[pos.Line, pos.Column] = true;

                pos.SetValue(Position.Line - 2, Position.Column);
                if (Board.IsPositionValid(pos) && IsPositionEmpty(pos) && MovimentsQuantity == 0) mat[pos.Line, pos.Column] = true;

                pos.SetValue(Position.Line - 1, Position.Column - 1);
                if (Board.IsPositionValid(pos) && ExistsEnemy(pos)) mat[pos.Line, pos.Column] = true;

                pos.SetValue(Position.Line - 1, Position.Column + 1);
                if (Board.IsPositionValid(pos) && ExistsEnemy(pos)) mat[pos.Line, pos.Column] = true;
            }
            else
            {
                pos.SetValue(Position.Line + 1, Position.Column);
                if (Board.IsPositionValid(pos) && IsPositionEmpty(pos)) mat[pos.Line, pos.Column] = true;

                pos.SetValue(Position.Line + 2, Position.Column);
                if (Board.IsPositionValid(pos) && IsPositionEmpty(pos) && MovimentsQuantity == 0) mat[pos.Line, pos.Column] = true;

                pos.SetValue(Position.Line + 1, Position.Column - 1);
                if (Board.IsPositionValid(pos) && ExistsEnemy(pos)) mat[pos.Line, pos.Column] = true;

                pos.SetValue(Position.Line + 1, Position.Column + 1);
                if (Board.IsPositionValid(pos) && ExistsEnemy(pos)) mat[pos.Line, pos.Column] = true;
            }

            return mat;
        }
    }
}

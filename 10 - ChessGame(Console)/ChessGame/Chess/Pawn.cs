using ChessBoard;

namespace Chess
{
    class Pawn : Piece
    {

        private ChessMatch _match;

        public Pawn(Board board, Color color, ChessMatch match) : base(board, color)
        {
            _match = match;
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

            if (Color == Color.White)
            {
                pos.SetValue(Position.Line - 1, Position.Column);
                if (Board.IsPositionValid(pos) && IsPositionEmpty(pos)) mat[pos.Line, pos.Column] = true;

                pos.SetValue(Position.Line - 2, Position.Column);
                if (Board.IsPositionValid(pos) && IsPositionEmpty(pos) && MovimentsQuantity == 0) mat[pos.Line, pos.Column] = true;

                pos.SetValue(Position.Line - 1, Position.Column - 1);
                if (Board.IsPositionValid(pos) && ExistsEnemy(pos)) mat[pos.Line, pos.Column] = true;

                pos.SetValue(Position.Line - 1, Position.Column + 1);
                if (Board.IsPositionValid(pos) && ExistsEnemy(pos)) mat[pos.Line, pos.Column] = true;

                // #Special Move En Passant
                if (Position.Line == 3)
                {
                    Position left = new Position(Position.Line, Position.Column - 1);
                    if (Board.IsPositionValid(left) && ExistsEnemy(left) && Board.GetPiece(left) == _match.EnPassantVulnerable)
                    {
                        mat[left.Line - 1, left.Column] = true;
                    }
                    Position right = new Position(Position.Line, Position.Column + 1);
                    if (Board.IsPositionValid(right) && ExistsEnemy(right) && Board.GetPiece(right) == _match.EnPassantVulnerable)
                    {
                        mat[right.Line - 1, right.Column] = true;
                    }
                }
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

                // #SpecialMove En Passant
                if (Position.Line == 4)
                {
                    Position left = new Position(Position.Line, Position.Column - 1);
                    if (Board.IsPositionValid(left) && ExistsEnemy(left) && Board.GetPiece(left) == _match.EnPassantVulnerable)
                    {
                        mat[left.Line + 1, left.Column] = true;
                    }
                    Position right = new Position(Position.Line, Position.Column + 1);
                    if (Board.IsPositionValid(right) && ExistsEnemy(right) && Board.GetPiece(right) == _match.EnPassantVulnerable)
                    {
                        mat[right.Line + 1, right.Column] = true;
                    }
                }
            }

            return mat;
        }
    }
}

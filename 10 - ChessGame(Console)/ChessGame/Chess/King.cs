using ChessBoard;

namespace Chess
{
    class King : Piece
    {
        private ChessMatch _match;
        public King(Board board, Color color, ChessMatch match) : base(board, color)
        {
            _match = match;
        }

       
        public override string ToString()
        {
            return "K";
        }

        private bool VerifyRookToCastling(Position pos)
        {
            Piece p = Board.GetPiece(pos);
            return (p != null && p is Rook) && (p.Color == Color && p.MovimentsQuantity == 0);
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

            // Special Move Castling
            if(MovimentsQuantity == 0 && !_match.InCheck)
            {
                // #Castling Short
                Position posShortRook = new Position(Position.Line, Position.Column + 3);
                if (VerifyRookToCastling(posShortRook))
                {
                    Position P1 = new Position(Position.Line, Position.Column + 1);
                    Position P2 = new Position(Position.Line, Position.Column + 2);
                    if(Board.GetPiece(P1) == null & Board.GetPiece(P2) == null)
                    {
                        mat[Position.Line, Position.Column + 2] = true;
                    }
                }

                //#Castling Long
                Position posLongRook = new Position(Position.Line, Position.Column - 4);
                if (VerifyRookToCastling(posLongRook))
                {
                    Position P1 = new Position(Position.Line, Position.Column - 1);
                    Position P2 = new Position(Position.Line, Position.Column - 2);
                    Position P3 = new Position(Position.Line, Position.Column - 3);
                    if (Board.GetPiece(P1) == null & Board.GetPiece(P2) == null && Board.GetPiece(P3) == null)
                    {
                        mat[Position.Line, Position.Column - 2] = true;
                    }
                }
            }


            return mat;
        }

    }
}

﻿using ChessBoard;

namespace ChessPieces
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
    }
}

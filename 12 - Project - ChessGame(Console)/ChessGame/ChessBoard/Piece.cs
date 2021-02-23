using System;
using System.Collections.Generic;
using System.Text;

namespace ChessBoard
{
    abstract class Piece
    {
        public Position Position { get; set; }
        public Color Color { get; protected set; }
        public int MovimentsQuantity { get; protected set; }
        public Board Board { get; protected set; }

        public Piece(Board board, Color color)
        {
            Position = null;
            Board = board;
            Color = color;
            MovimentsQuantity = 0;
        }

        protected bool IsAbleToMove(Position position)
        {
            Piece p = Board.GetPiece(position);
            return p == null || p.Color != Color;
            ;
        }

        public bool ExistsPossibleMoviments()
        {
            bool[,] mat = PossibleMoviments();
            for(int i = 0; i < Board.Lines; i++)
            {
                for(int j = 0; j < Board.Columns; j++)
                {
                    if (mat[i, j])
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool CanMoveTo(Position pos)
        {
            return PossibleMoviments()[pos.Line, pos.Column];
        }

        public void IncrementMovimentsQuantity()
        {
            MovimentsQuantity++;
        }
        public void DecrementMovimentsQuantity()
        {
            MovimentsQuantity--;
        }

        public abstract bool[,] PossibleMoviments();
    }
}

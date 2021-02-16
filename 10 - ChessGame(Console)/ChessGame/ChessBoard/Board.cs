using System;
using System.Collections.Generic;
using System.Text;

namespace ChessBoard
{
    class Board
    {
        public int Lines { get; set; }
        public int Columns { get; set; }
        public Piece[,] Pieces { get; private set; }

        public Board(int lines, int columns)
        {
            Lines = lines;
            Columns = columns;
            Pieces = new Piece[lines, columns];
        }

        public Piece GetPiece(int line, int column)
        {
            return Pieces[line, column];
        }

        public Piece GetPiece(Position position)
        {
            return Pieces[position.Line, position.Column];
        }

        public bool ExistsPiece(Position position)
        {
            ValidatePosition(position);
            return GetPiece(position) != null;
        }

        public void InsertPiece(Piece piece, Position position)
        {
            if (ExistsPiece(position))
            {
                throw new ChessBoardException("Position is occupied by another piece!");
            }
            Pieces[position.Line, position.Column] = piece;
            piece.Position = position;
        }

        public Piece RemovePiece(Position position)
        {
            if(GetPiece(position) == null)
            {
                return null;
            }
            Piece aux = GetPiece(position);
            aux.Position = null;
            Pieces[position.Line, position.Column] = null;
            return aux;
        }

        public bool IsPositionValid(Position position)
        {
            if (position.Line < 0 || position.Line >= Lines || position.Column < 0 || position.Column >= Columns)
            {
                return false;
            }
            return true;
        }

        public void ValidatePosition(Position position)
        {
            if (!IsPositionValid(position))
            {
                throw new ChessBoardException("Position is not valid!");
            }
        }
    }
}

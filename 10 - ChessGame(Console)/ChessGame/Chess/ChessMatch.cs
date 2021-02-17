using System;
using System.Collections.Generic;
using ChessBoard;

namespace Chess
{
    class ChessMatch
    {
        public Board Board { get; private set; }
        public int Turn { get; private set; }
        public Color CurrentPlayer { get; private set; }
        public bool IsEnded { get; private set; }
        public bool InCheck { get; set; }
        public HashSet<Piece> Pieces { get; private set; }
        public HashSet<Piece> CapturedPieces { get; private set; }


        public ChessMatch()
        {
            Board = new Board(8, 8);
            Turn = 1;
            CurrentPlayer = Color.White;
            IsEnded = false;
            InCheck = false;
            Pieces = new HashSet<Piece>();
            CapturedPieces = new HashSet<Piece>();
            InitiateBoard();
        }

        public Piece ExecuteMoviment(Position origin, Position destination)
        {
            Piece piece = Board.RemovePiece(origin);
            piece.IncrementMovimentsQuantity();
            Piece captured = Board.RemovePiece(destination);
            Board.InsertPiece(piece, destination);
            if(captured != null)
            {
                CapturedPieces.Add(captured);
            }
            return captured;
        }

        public void UndoMove(Position origin, Position destination, Piece captured)
        {
            Piece p = Board.RemovePiece(destination);
            p.DecrementMovimentsQuantity();
            if(captured != null)
            {
                Board.InsertPiece(captured, destination);
                CapturedPieces.Remove(captured);
            }
            Board.InsertPiece(p, origin);
        }

        public void PerformsMove(Position origin, Position destination)
        {
            Piece captured = ExecuteMoviment(origin, destination);
            if (IsKingInCheck(CurrentPlayer))
            {
                UndoMove(origin, destination, captured);
                throw new ChessBoardException("You can't put yourself in check!");
            }

            if (IsKingInCheck(Adversary(CurrentPlayer)))
            {
                InCheck = true;
            }
            else
            {
                InCheck = false;
            }
            Turn++;
            ChangeCurrentPlayer();
        }

        public void ChangeCurrentPlayer()
        {
            if (CurrentPlayer == Color.White)
            {
                CurrentPlayer = Color.Black;
            }
            else
            {
                CurrentPlayer = Color.White;
            }
        }

        public void ValidateOriginPosition(Position position)
        {
            if (Board.GetPiece(position) == null)
            {
                throw new ChessBoardException("Selected Position is empty!");
            }
            if (CurrentPlayer != Board.GetPiece(position).Color)
            {
                throw new ChessBoardException("Select a chess piece that is yours!");
            }
            if (!Board.GetPiece(position).ExistsPossibleMoviments())
            {
                throw new ChessBoardException("There are no possible moviments to select chess piece!");
            }
        }

        public void ValidateDestinationPosition(Position origin, Position destination)
        {
            if (!Board.GetPiece(origin).CanMoveTo(destination))
            {
                throw new ChessBoardException("Position of destination is invalid!");
            }
        }

        public HashSet<Piece> GetCapturedPieces(Color color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach(Piece p in CapturedPieces)
            {
                if(p.Color == color)
                {
                    aux.Add(p);
                }
            }
            return aux;
        }

        public HashSet<Piece> GetPiecesInGame(Color color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach (Piece p in Pieces)
            {
                if (p.Color == color)
                {
                    aux.Add(p);
                }
            }
            aux.ExceptWith(GetCapturedPieces(color));
            return aux;
        }

        private Color Adversary(Color color)
        {
            if(color == Color.White)
            {
                return Color.Black;
            }
            else
            {
                return Color.White;
            }
        }

        private Piece GetKing(Color color)
        {
            foreach(Piece p in GetPiecesInGame(color))
            {
                if (p is King) return p;
            }
            return null;
        }

        public bool IsKingInCheck(Color color)
        {
            Piece king = GetKing(color);
            if(king == null)
            {
                throw new ChessBoardException($"There is no {color} king!");
            }
            foreach(Piece p in GetPiecesInGame(Adversary(color)))
            {
                bool[,] mat = p.PossibleMoviments();
                if (mat[king.Position.Line, king.Position.Column]) return true;
            }
            return false;
        }

        public void InsertNewPiece(char column, int line, Piece piece)
        {
            Board.InsertPiece(piece, new ChessPosition(column, line).ToPosition());
            Pieces.Add(piece);
        }

        public void InitiateBoard()
        {
            //Inserting Black Pieces
            InsertNewPiece('a', 8, new Tower(Board, Color.Black));
            //InsertNewPiece('b', 8, new Horse(Board, Color.Black));
            //InsertNewPiece('c', 8, new Bishop(Board, Color.Black));
            //InsertNewPiece('d', 8, new Queen(Board, Color.Black));
            InsertNewPiece('e', 8, new King(Board, Color.Black));
            //InsertNewPiece('f', 8, new Bishop(Board, Color.Black));
            //InsertNewPiece('g', 8, new Horse(Board, Color.Black));
            InsertNewPiece('h', 8, new Tower(Board, Color.Black));

            //InsertNewPiece('a', 7, new Pawn(Board, Color.Black));
            //InsertNewPiece('b', 7, new Pawn(Board, Color.Black));
            //InsertNewPiece('c', 7, new Pawn(Board, Color.Black));
            InsertNewPiece('d', 7, new Tower(Board, Color.Black));
            InsertNewPiece('e', 7, new Tower(Board, Color.Black));
            InsertNewPiece('f', 7, new Tower(Board, Color.Black));
            //InsertNewPiece('g', 7, new Pawn(Board, Color.Black));
            //InsertNewPiece('h', 7, new Pawn(Board, Color.Black));

            //Inserting White Pieces
            InsertNewPiece('a', 1, new Tower(Board, Color.White));
            //InsertNewPiece('b', 1, new Horse(Board, Color.White));
            //InsertNewPiece('c', 1, new Bishop(Board, Color.White));
            //InsertNewPiece('d', 1, new Queen(Board, Color.White));
            InsertNewPiece('e', 1, new King(Board, Color.White));
            //InsertNewPiece('f', 1, new Bishop(Board, Color.White));
            //InsertNewPiece('g', 1, new Horse(Board, Color.White));
            InsertNewPiece('h', 1, new Tower(Board, Color.White));

            //InsertNewPiece('a', 2, new Pawn(Board, Color.White));
            //InsertNewPiece('b', 2, new Pawn(Board, Color.White));
            //InsertNewPiece('c', 2, new Pawn(Board, Color.White));
            InsertNewPiece('d', 2, new Tower(Board, Color.White));
            InsertNewPiece('e', 2, new Tower(Board, Color.White));
            InsertNewPiece('f', 2, new Tower(Board, Color.White));
            //InsertNewPiece('g', 2, new Pawn(Board, Color.White));
            //InsertNewPiece('h', 2, new Pawn(Board, Color.White));

        }

    }
}

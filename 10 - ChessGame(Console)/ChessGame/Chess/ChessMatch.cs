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
        public bool IsMatchEnded { get; private set; }
        public bool InCheck { get; set; }
        public HashSet<Piece> Pieces { get; private set; }
        public HashSet<Piece> CapturedPieces { get; private set; }


        public ChessMatch()
        {
            Board = new Board(8, 8);
            Turn = 1;
            CurrentPlayer = Color.White;
            IsMatchEnded = false;
            InCheck = false;
            Pieces = new HashSet<Piece>();
            CapturedPieces = new HashSet<Piece>();
            InitiateBoard();
        }

        public Piece ExecuteMove(Position origin, Position destination)
        {
            Piece piece = Board.RemovePiece(origin);
            piece.IncrementMovimentsQuantity();
            Piece captured = Board.RemovePiece(destination);
            Board.InsertPiece(piece, destination);
            if (captured != null)
            {
                CapturedPieces.Add(captured);
            }

            // #Castling Short
            if(piece is King && destination.Column == origin.Column + 2)
            {
                Position RookOrigin = new Position(origin.Line, origin.Column + 3);
                Position RookDestination = new Position(origin.Line, origin.Column + 1);
                Piece Rook = Board.RemovePiece(RookOrigin);
                Rook.IncrementMovimentsQuantity();
                Board.InsertPiece(Rook, RookDestination);
            }
            // #Castling Long
            if (piece is King && destination.Column == origin.Column - 2)
            {
                Position RookOrigin = new Position(origin.Line, origin.Column - 4);
                Position RookDestination = new Position(origin.Line, origin.Column - 1);
                Piece Rook = Board.RemovePiece(RookOrigin);
                Rook.IncrementMovimentsQuantity();
                Board.InsertPiece(Rook, RookDestination);
            }

            return captured;
        }

        public void UndoMove(Position origin, Position destination, Piece captured)
        {
            Piece piece = Board.RemovePiece(destination);
            piece.DecrementMovimentsQuantity();
            if (captured != null)
            {
                Board.InsertPiece(captured, destination);
                CapturedPieces.Remove(captured);
            }
            Board.InsertPiece(piece, origin);

            // #Castling Short
            if (piece is King && destination.Column == origin.Column + 2)
            {
                Position RookOrigin = new Position(origin.Line, origin.Column + 3);
                Position RookDestination = new Position(origin.Line, origin.Column + 1);
                Piece Rook = Board.RemovePiece(RookDestination);
                Rook.DecrementMovimentsQuantity();
                Board.InsertPiece(Rook, RookOrigin);
            }

            // #Castling Long
            if (piece is King && destination.Column == origin.Column - 2)
            {
                Position RookOrigin = new Position(origin.Line, origin.Column - 4);
                Position RookDestination = new Position(origin.Line, origin.Column - 1);
                Piece Rook = Board.RemovePiece(RookDestination);
                Rook.DecrementMovimentsQuantity();
                Board.InsertPiece(Rook, RookOrigin);
            }
        }

        public void PerformsMove(Position origin, Position destination)
        {
            Piece captured = ExecuteMove(origin, destination);
            if (IsKingInCheck(CurrentPlayer))
            {
                UndoMove(origin, destination, captured);
                throw new ChessBoardException("You can't put yourself in check!");
            }

            if (IsKingInCheck(Adversary(CurrentPlayer)))
            {
                InCheck = true;
                if (IsCheckMate(Adversary(CurrentPlayer)))
                {
                    IsMatchEnded = true;
                    return;
                }

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
            foreach (Piece p in CapturedPieces)
            {
                if (p.Color == color)
                {
                    aux.Add(p);
                }
            }
            return aux;
        }

        public HashSet<Piece> GetInGamePieces(Color color)
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
            if (color == Color.White)
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
            foreach (Piece p in GetInGamePieces(color))
            {
                if (p is King) return p;
            }
            return null;
        }

        public bool IsKingInCheck(Color color)
        {
            Piece king = GetKing(color);
            if (king == null)
            {
                throw new ChessBoardException($"There is no {color} king!");
            }
            foreach (Piece p in GetInGamePieces(Adversary(color)))
            {
                bool[,] mat = p.PossibleMoviments();
                if (mat[king.Position.Line, king.Position.Column]) return true;
            }
            return false;
        }

        public bool IsCheckMate(Color color)
        {
            if (!IsKingInCheck(color)) return false;

            foreach (Piece p in GetInGamePieces(color))
            {
                bool[,] mat = p.PossibleMoviments();
                for (int i = 0; i < Board.Lines; i++)
                {
                    for (int j = 0; j < Board.Columns; j++)
                    {
                        if (mat[i, j])
                        {
                            Position origin = p.Position;
                            Position destination = new Position(i, j);
                            Piece captured = ExecuteMove(origin, destination);
                            bool testCheck = IsKingInCheck(color);
                            UndoMove(origin, destination, captured);
                            if (!testCheck) return false;
                        }
                    }
                }
            }
            return true;
        }

        public void InsertNewPiece(char column, int line, Piece piece)
        {
            Board.InsertPiece(piece, new ChessPosition(column, line).ToPosition());
            Pieces.Add(piece);
        }

        public void InitiateBoard()
        {
            //Inserting Black Pieces
            InsertNewPiece('a', 8, new Rook(Board, Color.Black));
            //InsertNewPiece('b', 8, new Horse(Board, Color.Black));
            //InsertNewPiece('c', 8, new Bishop(Board, Color.Black));
            //InsertNewPiece('d', 8, new Queen(Board, Color.Black));
            InsertNewPiece('e', 8, new King(Board, Color.Black, this));
            //InsertNewPiece('f', 8, new Bishop(Board, Color.Black));
            //InsertNewPiece('g', 8, new Horse(Board, Color.Black));
            InsertNewPiece('h', 8, new Rook(Board, Color.Black));

            InsertNewPiece('a', 7, new Pawn(Board, Color.Black));
            InsertNewPiece('b', 7, new Pawn(Board, Color.Black));
            InsertNewPiece('c', 7, new Pawn(Board, Color.Black));
            InsertNewPiece('d', 7, new Pawn(Board, Color.Black));
            InsertNewPiece('e', 7, new Pawn(Board, Color.Black));
            InsertNewPiece('f', 7, new Pawn(Board, Color.Black));
            InsertNewPiece('g', 7, new Pawn(Board, Color.Black));
            InsertNewPiece('h', 7, new Pawn(Board, Color.Black));

            //Inserting White Pieces
            InsertNewPiece('a', 1, new Rook(Board, Color.White));
            //InsertNewPiece('b', 1, new Horse(Board, Color.White));
            //InsertNewPiece('c', 1, new Bishop(Board, Color.White));
            //InsertNewPiece('d', 1, new Queen(Board, Color.White));
            InsertNewPiece('e', 1, new King(Board, Color.White, this));
           //InsertNewPiece('f', 1, new Bishop(Board, Color.White));
            //InsertNewPiece('g', 1, new Horse(Board, Color.White));
            InsertNewPiece('h', 1, new Rook(Board, Color.White));

            InsertNewPiece('a', 2, new Pawn(Board, Color.White));
            InsertNewPiece('b', 2, new Pawn(Board, Color.White));
            InsertNewPiece('c', 2, new Pawn(Board, Color.White));
            InsertNewPiece('d', 2, new Pawn(Board, Color.White));
            InsertNewPiece('e', 2, new Pawn(Board, Color.White));
            InsertNewPiece('f', 2, new Pawn(Board, Color.White));
            InsertNewPiece('g', 2, new Pawn(Board, Color.White));
            InsertNewPiece('h', 2, new Pawn(Board, Color.White));

        }

    }
}

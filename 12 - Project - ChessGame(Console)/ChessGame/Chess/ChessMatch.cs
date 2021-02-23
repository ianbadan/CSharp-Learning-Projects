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
        private HashSet<Piece> _pieces;
        private HashSet<Piece> _capturedPieces;
        public Piece EnPassantVulnerable { get; private set; }


        public ChessMatch()
        {
            Board = new Board(8, 8);
            Turn = 1;
            CurrentPlayer = Color.White;
            IsMatchEnded = false;
            InCheck = false;
            EnPassantVulnerable = null;
            _pieces = new HashSet<Piece>();
            _capturedPieces = new HashSet<Piece>();
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
                _capturedPieces.Add(captured);
            }

            // #SpecialMove Castling Short
            if (piece is King && destination.Column == origin.Column + 2)
            {
                Position RookOrigin = new Position(origin.Line, origin.Column + 3);
                Position RookDestination = new Position(origin.Line, origin.Column + 1);
                Piece Rook = Board.RemovePiece(RookOrigin);
                Rook.IncrementMovimentsQuantity();
                Board.InsertPiece(Rook, RookDestination);
            }
            // #SpecialMove Castling Long
            if (piece is King && destination.Column == origin.Column - 2)
            {
                Position RookOrigin = new Position(origin.Line, origin.Column - 4);
                Position RookDestination = new Position(origin.Line, origin.Column - 1);
                Piece Rook = Board.RemovePiece(RookOrigin);
                Rook.IncrementMovimentsQuantity();
                Board.InsertPiece(Rook, RookDestination);
            }

            // #SpecialMove En Passant
            if (piece is Pawn)
            {
                if (origin.Column != destination.Column && captured == null)
                {
                    Position pawnPosition;
                    if (piece.Color == Color.White)
                    {
                        pawnPosition = new Position(destination.Line + 1, destination.Column);
                    }
                    else
                    {
                        pawnPosition = new Position(destination.Line - 1, destination.Column);
                    }
                    captured = Board.RemovePiece(pawnPosition);
                    _capturedPieces.Add(captured);
                }
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
                _capturedPieces.Remove(captured);
            }
            Board.InsertPiece(piece, origin);

            // #SpecialMove Castling Short
            if (piece is King && destination.Column == origin.Column + 2)
            {
                Position RookOrigin = new Position(origin.Line, origin.Column + 3);
                Position RookDestination = new Position(origin.Line, origin.Column + 1);
                Piece Rook = Board.RemovePiece(RookDestination);
                Rook.DecrementMovimentsQuantity();
                Board.InsertPiece(Rook, RookOrigin);
            }

            // #SpecialMove Castling Long
            if (piece is King && destination.Column == origin.Column - 2)
            {
                Position RookOrigin = new Position(origin.Line, origin.Column - 4);
                Position RookDestination = new Position(origin.Line, origin.Column - 1);
                Piece Rook = Board.RemovePiece(RookDestination);
                Rook.DecrementMovimentsQuantity();
                Board.InsertPiece(Rook, RookOrigin);
            }

            // #SpecialMove En Passant
            if (piece is Pawn)
            {
                if (origin.Column != destination.Column && captured == EnPassantVulnerable)
                {
                    Piece pawn = Board.RemovePiece(destination);
                    Position pawnPosition;
                    if (piece.Color == Color.White)
                    {
                        pawnPosition = new Position(3, destination.Column);
                    }
                    else
                    {
                        pawnPosition = new Position(4, destination.Column);
                    }
                    Board.InsertPiece(pawn, pawnPosition);
                }
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

            Piece piece = Board.GetPiece(destination);

            // #SpecialMove Pawn Promotion
            if(piece is Pawn)
            {
                if ((piece.Color == Color.White && destination.Line == 0) || (piece.Color == Color.Black && destination.Line == 7))
                {
                    piece = Board.RemovePiece(destination);
                    _pieces.Remove(piece);
                    Console.WriteLine("\nPawn Promotion!");
                    Console.WriteLine("Queen(Q), Rook(R), Bishop(B),Horse (H)");
                    Console.Write("Choose a new piece (Q/R/B/H): ");
                    char pieceType = char.Parse(Console.ReadLine());
                    Piece newPiece;
                    if (pieceType == 'Q' || pieceType == 'q')
                    {
                        newPiece = new Queen(Board, piece.Color);
                    }
                    else if(pieceType == 'R' || pieceType == 'r')
                    {
                        newPiece = new Rook(Board, piece.Color);
                    }
                    else if(pieceType == 'B' || pieceType == 'b')
                    {
                        newPiece = new Bishop(Board, piece.Color);
                    } 
                    else if(pieceType == 'H' || pieceType == 'h')
                    {
                        newPiece = new Horse(Board, piece.Color);
                    }
                    else
                    {
                        newPiece = new Queen(Board, piece.Color);
                    }
                    
                    Board.InsertPiece(newPiece, destination);
                    _pieces.Add(newPiece);
                }
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

            piece = Board.GetPiece(destination);

            // #Special Move En Passant
            if (piece is Pawn && (destination.Line == origin.Line - 2 || destination.Line == origin.Line + 2) && piece.MovimentsQuantity == 1)
            {
                EnPassantVulnerable = piece;
            }
            else
            {
                EnPassantVulnerable = null;
            }
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
            foreach (Piece p in _capturedPieces)
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
            foreach (Piece p in _pieces)
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
            _pieces.Add(piece);
        }

        public void InitiateBoard()
        {
            //Inserting Black Pieces
            
            InsertNewPiece('a', 8, new Rook(Board, Color.Black));
            InsertNewPiece('b', 8, new Horse(Board, Color.Black));
            InsertNewPiece('c', 8, new Bishop(Board, Color.Black));
            InsertNewPiece('d', 8, new Queen(Board, Color.Black));
            InsertNewPiece('e', 8, new King(Board, Color.Black, this));
            InsertNewPiece('f', 8, new Bishop(Board, Color.Black));
            InsertNewPiece('g', 8, new Horse(Board, Color.Black));
            InsertNewPiece('h', 8, new Rook(Board, Color.Black));
            
            InsertNewPiece('a', 7, new Pawn(Board, Color.Black, this));
            InsertNewPiece('b', 7, new Pawn(Board, Color.Black, this));
            InsertNewPiece('c', 7, new Pawn(Board, Color.Black, this));
            InsertNewPiece('d', 7, new Pawn(Board, Color.Black, this));
            InsertNewPiece('e', 7, new Pawn(Board, Color.Black, this));
            InsertNewPiece('f', 7, new Pawn(Board, Color.Black, this));
            InsertNewPiece('g', 7, new Pawn(Board, Color.Black, this));
            InsertNewPiece('h', 7, new Pawn(Board, Color.Black, this));
            
            //Inserting White Pieces

            InsertNewPiece('a', 1, new Rook(Board, Color.White));
            InsertNewPiece('b', 1, new Horse(Board, Color.White));
            InsertNewPiece('c', 1, new Bishop(Board, Color.White));
            InsertNewPiece('d', 1, new Queen(Board, Color.White));
            InsertNewPiece('e', 1, new King(Board, Color.White, this));
            InsertNewPiece('f', 1, new Bishop(Board, Color.White));
            InsertNewPiece('g', 1, new Horse(Board, Color.White));
            InsertNewPiece('h', 1, new Rook(Board, Color.White));

            InsertNewPiece('a', 2, new Pawn(Board, Color.White, this));
            InsertNewPiece('b', 2, new Pawn(Board, Color.White, this));
            InsertNewPiece('c', 2, new Pawn(Board, Color.White, this));
            InsertNewPiece('d', 2, new Pawn(Board, Color.White, this));
            InsertNewPiece('e', 2, new Pawn(Board, Color.White, this));
            InsertNewPiece('f', 2, new Pawn(Board, Color.White, this));
            InsertNewPiece('g', 2, new Pawn(Board, Color.White, this));
            InsertNewPiece('h', 2, new Pawn(Board, Color.White, this));

        }

    }
}

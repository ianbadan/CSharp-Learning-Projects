using System;
using ChessBoard;

namespace Chess
{
    class ChessMatch
    {
        public Board Board { get; private set; }
        public int Turn { get; set; }
        public Color CurrentPlayer { get; set; }
        public bool isEnded { get; private set; }

        public ChessMatch()
        {
            Board = new Board(8,8);
            Turn = 1;
            CurrentPlayer = Color.White;
            isEnded = false;
            InitiateBoard();
        }

        public void ExecuteMoviment(Position origin, Position destination)
        {
            Piece piece = Board.RemovePiece(origin);
            piece.IncrementMovimentsQuantity();
            Piece capturedPiece = Board.RemovePiece(destination);
            Board.InsertPiece(piece, destination);

        }

        public void InitiateBoard()
        {
            Board.InsertPiece(new Tower(Board, Color.Black), new ChessPosition('a', 8).ToPosition());
            Board.InsertPiece(new Horse(Board, Color.Black), new ChessPosition('b', 8).ToPosition());
            Board.InsertPiece(new Bishop(Board, Color.Black), new ChessPosition('c', 8).ToPosition());
            Board.InsertPiece(new Queen(Board, Color.Black), new ChessPosition('d', 8).ToPosition());
            Board.InsertPiece(new King(Board, Color.Black), new ChessPosition('e', 8).ToPosition());
            Board.InsertPiece(new Bishop(Board, Color.Black), new ChessPosition('f', 8).ToPosition());
            Board.InsertPiece(new Horse(Board, Color.Black), new ChessPosition('g', 8).ToPosition());
            Board.InsertPiece(new Tower(Board, Color.Black), new ChessPosition('h', 8).ToPosition());
            Board.InsertPiece(new Pawn(Board, Color.Black), new ChessPosition('a', 7).ToPosition());
            Board.InsertPiece(new Pawn(Board, Color.Black), new ChessPosition('b', 7).ToPosition());
            Board.InsertPiece(new Pawn(Board, Color.Black), new ChessPosition('c', 7).ToPosition());
            Board.InsertPiece(new Pawn(Board, Color.Black), new ChessPosition('d', 7).ToPosition());
            Board.InsertPiece(new Pawn(Board, Color.Black), new ChessPosition('e', 7).ToPosition());
            Board.InsertPiece(new Pawn(Board, Color.Black), new ChessPosition('f', 7).ToPosition());
            Board.InsertPiece(new Pawn(Board, Color.Black), new ChessPosition('g', 7).ToPosition());
            Board.InsertPiece(new Pawn(Board, Color.Black), new ChessPosition('h', 7).ToPosition());

            Board.InsertPiece(new Tower(Board, Color.White), new ChessPosition('a', 1).ToPosition());
            Board.InsertPiece(new Horse(Board, Color.White), new ChessPosition('b', 1).ToPosition());
            Board.InsertPiece(new Bishop(Board, Color.White), new ChessPosition('c', 1).ToPosition());
            Board.InsertPiece(new Queen(Board, Color.White), new ChessPosition('d', 1).ToPosition());
            Board.InsertPiece(new King(Board, Color.White), new ChessPosition('e', 1).ToPosition());
            Board.InsertPiece(new Bishop(Board, Color.White), new ChessPosition('f', 1).ToPosition());
            Board.InsertPiece(new Horse(Board, Color.White), new ChessPosition('g', 1).ToPosition());
            Board.InsertPiece(new Tower(Board, Color.White), new ChessPosition('h', 1).ToPosition());
            Board.InsertPiece(new Pawn(Board, Color.White), new ChessPosition('a', 2).ToPosition());
            Board.InsertPiece(new Pawn(Board, Color.White), new ChessPosition('b', 2).ToPosition());
            Board.InsertPiece(new Pawn(Board, Color.White), new ChessPosition('c', 2).ToPosition());
            Board.InsertPiece(new Pawn(Board, Color.White), new ChessPosition('d', 2).ToPosition());
            Board.InsertPiece(new Pawn(Board, Color.White), new ChessPosition('e', 2).ToPosition());
            Board.InsertPiece(new Pawn(Board, Color.White), new ChessPosition('f', 2).ToPosition());
            Board.InsertPiece(new Pawn(Board, Color.White), new ChessPosition('g', 2).ToPosition());
            Board.InsertPiece(new Pawn(Board, Color.White), new ChessPosition('h', 2).ToPosition());

        }

    }
}

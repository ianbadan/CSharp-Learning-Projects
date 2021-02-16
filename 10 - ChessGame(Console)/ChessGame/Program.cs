using System;
using ChessBoard;
using Chess;

namespace ChessGame
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ChessMatch chessMatch = new ChessMatch();

                while (!chessMatch.isEnded)
                {
                    Console.Clear();
                    Screen.PrintBoard(chessMatch.Board);
                    Console.Write("Origin: ");
                    Position origin = Screen.ReadChessPosition().ToPosition();
                    Console.Write("Destionation: ");
                    Position destination = Screen.ReadChessPosition().ToPosition();
                    chessMatch.ExecuteMoviment(origin, destination);
                }
               
            }
            catch (ChessBoardException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}

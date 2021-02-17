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
                ChessMatch match = new ChessMatch();

                while (!match.IsEnded)
                {
                    try
                    {

                        Console.Clear();
                        Screen.PrintMatch(match);

                        Console.Write("\nOrigin: ");
                        Position origin = Screen.ReadChessPosition().ToPosition();
                        match.ValidateOriginPosition(origin);
                        bool[,] possiblePositions = match.Board.GetPiece(origin).PossibleMoviments();

                        Console.Clear();
                        Screen.PrintBoard(match.Board, possiblePositions);
                        Console.WriteLine("\nTurn: " + match.Turn);
                        Console.WriteLine("Waiting for move: " + match.CurrentPlayer);

                        Console.Write("\nDestination: ");
                        Position destination = Screen.ReadChessPosition().ToPosition();
                        match.ValidateDestinationPosition(origin, destination);

                        match.PerformsMove(origin, destination);
                    }
                    catch (ChessBoardException e)
                    {
                        Console.WriteLine(e.Message + " Press anything to continue!");
                        Console.ReadLine();
                    }
                    catch(IndexOutOfRangeException)
                    {
                        Console.WriteLine("Position out of bounds!");
                        Console.ReadLine();
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Invalid input format!");
                        Console.ReadLine();
                    }

                }

            }
            catch (ChessBoardException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}

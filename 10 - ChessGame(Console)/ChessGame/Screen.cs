using System;
using System.Collections.Generic;
using Chess;
using ChessBoard;


namespace ChessGame
{
    class Screen
    {
        public static void PrintMatch(ChessMatch match)
        {
            Screen.PrintBoard(match.Board);
            Console.WriteLine();
            PrintCapturedPieces(match);
            Console.WriteLine("\nTurn: " + match.Turn);
            Console.WriteLine("Waiting for move: " + match.CurrentPlayer);
            if (match.InCheck)
            {
                Console.WriteLine("CHECK!");
            }
        }

        public static void PrintCapturedPieces(ChessMatch match)
        {
            Console.WriteLine("Captured pieces: ");
            Console.Write("Whites: ");
            PrintSet(match.GetCapturedPieces(Color.White));
            Console.Write("Black: ");
            ConsoleColor aux = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            PrintSet(match.GetCapturedPieces(Color.Black));
            Console.ForegroundColor = aux;
        }
        
        public static void PrintSet(HashSet<Piece> set)
        {
            Console.Write("[");
            foreach(Piece p in set)
            {
                Console.Write(p + " ");
            }
            Console.Write("]\n");
        }

        public static void PrintBoard(Board board)
        {
            ConsoleColor aux = Console.ForegroundColor;
            for (int i = 0; i < board.Lines; i++)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(8 - i + " ");
                Console.ForegroundColor = aux;

                for (int j = 0; j < board.Columns; j++)
                {

                    PrintPiece(board.GetPiece(i, j));

                }
                Console.WriteLine();
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("  a b c d e f g h");
            Console.ForegroundColor = aux;

        }

        public static void PrintBoard(Board board, bool[,] possibleMoviments)
        {
            ConsoleColor auxForeground = Console.ForegroundColor;
            ConsoleColor OriginalBackground = Console.BackgroundColor;
            for (int i = 0; i < board.Lines; i++)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(8 - i + " ");
                Console.ForegroundColor = auxForeground;

                for (int j = 0; j < board.Columns; j++)
                {
                    if (possibleMoviments[i, j])
                    {
                        Console.BackgroundColor = ConsoleColor.DarkGray;
                    }
                    PrintPiece(board.GetPiece(i, j));
                    Console.BackgroundColor = OriginalBackground;
                }
                Console.WriteLine();
            }
            Console.BackgroundColor = OriginalBackground;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("  a b c d e f g h");
            Console.ForegroundColor = auxForeground;

        }


        //Method responsible for reading a input from keyboard and convert to a position in the chess board;
        public static ChessPosition ReadChessPosition()
        {
            string position = Console.ReadLine();
            char column = position.ToLower()[0];
            int line = int.Parse(position[1] + "");
            return new ChessPosition(column, line);
        }

        public static void PrintPiece(Piece piece)
        {
            if (piece == null)
            {
                Console.Write("- ");
            }
            else
            {
                if (piece.Color == Color.White)
                {
                    Console.Write(piece);
                }
                else
                {
                    ConsoleColor aux = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(piece);
                    Console.ForegroundColor = aux;
                }
                Console.Write(" ");
            }
        }



    }
}

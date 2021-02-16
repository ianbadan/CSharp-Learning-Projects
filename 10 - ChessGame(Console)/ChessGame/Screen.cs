﻿using System;
using Chess;
using ChessBoard;


namespace ChessGame
{
    class Screen
    {
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
                    if (board.GetPiece(i, j) == null)
                    {
                        Console.Write("- ");
                    }
                    else
                    {
                        PrintPiece(board.GetPiece(i, j));
                        Console.Write(" ");
                    }

                }
                Console.WriteLine();
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("  a b c d e f g h");
            Console.ForegroundColor = aux;

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
        }



    }
}

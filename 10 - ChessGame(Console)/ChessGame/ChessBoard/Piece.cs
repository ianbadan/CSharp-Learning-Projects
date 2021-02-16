﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ChessBoard
{
    class Piece
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

        public void IncrementMovimentsQuantity()
        {
            MovimentsQuantity++;
        }
    }
}

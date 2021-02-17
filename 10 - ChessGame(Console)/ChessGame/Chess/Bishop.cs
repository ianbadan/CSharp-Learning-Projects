using ChessBoard;

namespace Chess
{
    class Bishop : Piece
    {
        public Bishop(Board board, Color color) : base(board, color)
        {

        }

        public override bool[,] PossibleMoviments()
        {
            throw new System.NotImplementedException();
        }

        public override string ToString()
        {
            return "B";
        }
    }
}

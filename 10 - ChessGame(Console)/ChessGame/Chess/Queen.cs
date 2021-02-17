using ChessBoard;

namespace Chess
{
    class Queen : Piece
    {
        public Queen(Board board, Color color) : base(board, color)
        {

        }

        public override bool[,] PossibleMoviments()
        {
            throw new System.NotImplementedException();
        }

        public override string ToString()
        {
            return "Q";
        }
    }
}

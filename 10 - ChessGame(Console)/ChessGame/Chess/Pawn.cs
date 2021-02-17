using ChessBoard;

namespace Chess
{
    class Pawn : Piece
    {
        public Pawn(Board board, Color color) : base(board, color)
        {

        }

        public override bool[,] PossibleMoviments()
        {
            throw new System.NotImplementedException();
        }

        public override string ToString()
        {
            return "P";
        }
    }
}

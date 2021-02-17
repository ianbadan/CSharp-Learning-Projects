using ChessBoard;

namespace Chess
{
    class Horse : Piece
    {
        public Horse(Board board, Color color) : base(board, color)
        {

        }

        public override bool[,] PossibleMoviments()
        {
            throw new System.NotImplementedException();
        }

        public override string ToString()
        {
            return "H";
        }
    }
}

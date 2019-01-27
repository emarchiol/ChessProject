namespace SolarWinds.MSP.Chess.Pieces
{
    public class ChessPiece
    {
        protected int xCoordinate = -1;
        protected int yCoordinate = -1;
        protected char indentifierOnBoard;
        protected PieceColor pieceColor;
        protected int maxAmountOfPieces;

        public int XCoordinate
        {
            get { return xCoordinate; }
            set { xCoordinate = value; }
        }

        public int YCoordinate
        {
            get { return yCoordinate; }
            set { yCoordinate = value; }
        }

        public PieceColor PieceColor
        {
            get { return this.pieceColor; }
            private set { pieceColor = value; }
        }

        public int MaxAmountOfPieces
        {
            get { return this.maxAmountOfPieces; }
        }

        public char IndentifierOnBoard
        {
            get { return this.indentifierOnBoard; }
        }
    }
}

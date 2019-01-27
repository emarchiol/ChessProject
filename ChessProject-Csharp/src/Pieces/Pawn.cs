using System;

namespace SolarWinds.MSP.Chess.Pieces
{
    public class Pawn : ChessPiece, IPieceMovement
    {
        public Pawn(PieceColor pieceColor)
        {
            this.maxAmountOfPieces = 8;
            this.pieceColor = pieceColor;
            this.indentifierOnBoard = 'P';
        }

        public void Move(int newX, int newY)
        {
            MovementType movementType = GetMovementType(newX, newY);

            switch (movementType)
            {
                case MovementType.Move:
                    if (this.xCoordinate == newX && 
                        this.yCoordinate != newY && 
                        ChessBoard.Instance.IsLegalMovement(newX, newY, false))
                    {
                        int distance = 1;
                        if (this.yCoordinate == 1 && this.pieceColor == PieceColor.White)
                        {
                            distance = 2;
                        }

                        if (this.yCoordinate == 6 && this.pieceColor == PieceColor.Black)
                        {
                            distance = 2;
                        }

                        // is he moving like a pawn?
                        bool validYMovement = false;
                        
                        if (this.pieceColor == PieceColor.White)
                        {
                            int userDistance = (newY - this.yCoordinate);
                            validYMovement = userDistance > 0 && userDistance <= distance ? true : false;
                        }
                        else
                        {
                            int userDistance = (newY - this.yCoordinate);
                            validYMovement = userDistance < 0 && userDistance >= (-distance) ? true : false;
                        }

                        if (validYMovement)
                        {
                            this.yCoordinate = newY;
                            return;
                        }
                    }

                    break;

                case MovementType.Capture:
                    ChessBoard.Instance.UpdateCapturePiece(newX, newY);
                    this.xCoordinate = newX;
                    this.yCoordinate = newY;
                    return;
            }

            throw new ArgumentException("Invalid coordinates for this Pawn.");
        }

        public MovementType GetMovementType(int newX, int newY)
        {
            if (this.xCoordinate != newX &&
                        this.yCoordinate != newY &&
                        ChessBoard.Instance.IsLegalMovement(newX, newY, true))
            {
                // is he capturing like a pawn?
                bool validYMovement = false;
                bool validXMovement = false;

                if (this.pieceColor == PieceColor.White)
                {
                    validYMovement = (newY - this.yCoordinate) == 1 ? true : false;
                }
                else
                {
                    validYMovement = (newY - this.yCoordinate) == -1 ? true : false;
                }

                validXMovement = Math.Abs(newX - this.xCoordinate) == 1;

                if (validYMovement && validXMovement)
                {
                    return MovementType.Capture;
                }
            }

            return MovementType.Move;
        }

        public override string ToString()
        {
            return this.CurrentPositionAsString();
        }

        public string CurrentPositionAsString()
        {
            return string.Format("Current X: {1}{0}Current Y: {2}{0}Piece Color: {3}", Environment.NewLine, XCoordinate, YCoordinate, PieceColor);
        }
    }
}

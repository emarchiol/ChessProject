using System;

namespace SolarWinds.MSP.Chess.Pieces
{
    public class Pawn : ChessPiece, IPieceMovement
    {
        // TODO: future implementation.
        public delegate void SwitchPawnForCapturedPiece();
        public SwitchPawnForCapturedPiece changePiece;

        public Pawn(PieceColor pieceColor)
        {
            this.maxAmountOfPieces = 8;
            this.pieceColor = pieceColor;
            this.indentifierOnBoard = 'P';
        }

        public void Move(int newX, int newY)
        {
            MovementType movementType = DefineMovementType(newX, newY);

            switch (movementType)
            {
                case MovementType.Move:
                    this.yCoordinate = newY;
                    if (this.YCoordinate == 0 || this.yCoordinate == 7)
                    {
                        // Call for a Game method to ask the user the piece.
                        // Then the method will remove the pawn from the board and change it for the other piece.
                        this.changePiece();
                    }
                    return;

                case MovementType.Capture:
                    ChessBoard.Instance.UpdateCapturePiece(newX, newY);
                    this.xCoordinate = newX;
                    this.yCoordinate = newY;
                    return;
            }

            throw new ArgumentException("Invalid coordinates for this Pawn.");
        }

        public MovementType DefineMovementType(int newX, int newY)
        {
            if (this.xCoordinate != newX &&
                this.yCoordinate != newY &&
                ChessBoard.Instance.IsLegalMovement(newX, newY, true))
            {
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
                    return MovementType.Move;
                }
            }

            return MovementType.InvalidMovement;
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

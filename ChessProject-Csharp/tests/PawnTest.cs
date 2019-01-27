using Microsoft.VisualStudio.TestTools.UnitTesting;
using SolarWinds.MSP.Chess.Pieces;
using System;

namespace SolarWinds.MSP.Chess
{
    [TestClass]
	public class PawnTest
	{
		private ChessBoard chessBoard;
		private Pawn pawnEater;
        private Pawn pawnOwned;

        [TestInitialize]
		public void SetUp()
		{
            chessBoard = ChessBoard.Instance;
            chessBoard.ClearBoard();
            pawnEater = new Pawn(PieceColor.Black);
            pawnOwned = new Pawn(PieceColor.White);
        }

        // State_Action_Result

		[TestMethod]
		public void PawnLegalCoordinates_MoveForward_DoesMove()
		{
            chessBoard.Add(pawnEater, 6, 2);

            pawnEater.Move(6, 1);

			Assert.AreEqual(pawnEater.XCoordinate, 6);
            Assert.AreEqual(pawnEater.YCoordinate, 1);
		}

        [TestMethod]
        public void PawnLegalCoordinates_CaptureLeft_CapturePiece()
        {
            chessBoard.Add(pawnEater, 6, 2);
            chessBoard.Add(pawnOwned, 5, 1);

            pawnEater.Move(5, 1);

            Assert.AreEqual(pawnEater.XCoordinate, 5);
            Assert.AreEqual(pawnEater.YCoordinate, 1);
            Assert.IsFalse(chessBoard.isPiecePresentOnBoard(pawnOwned));
        }

        [TestMethod]
        public void PawnLegalCoordinates_CaptureRight_CapturePiece()
        {
            chessBoard.Add(pawnEater, 6, 2);
            chessBoard.Add(pawnOwned, 7, 1);

            pawnEater.Move(7, 1);

            Assert.AreEqual(pawnEater.XCoordinate, 7);
            Assert.AreEqual(pawnEater.YCoordinate, 1);
            Assert.IsFalse(chessBoard.isPiecePresentOnBoard(pawnOwned));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Invalid coordinates for a Pawn.")]
        public void PawnIllegalCoordinates_MoveForward_DoesNotMove()
        {
            chessBoard.Add(pawnEater, 6, 2);

            pawnEater.Move(6, -1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Invalid coordinates for a Pawn.")]
        public void PawnIllegalCoordinates_CaptureRight_DoesNotCapturePiece()
        {
            chessBoard.Add(pawnEater, 6, 2);

            pawnEater.Move(5, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Invalid coordinates for a Pawn.")]
        public void PawnIllegalCoordinates_CaptureLeft_DoesNotCapturePiece()
        {
            chessBoard.Add(pawnEater, 6, 2);

            pawnEater.Move(7, 0);
        }

        [TestMethod]
        public void PawnLegalCoordinate_GetMovementType_TypeCapture()
        {
            chessBoard.Add(pawnEater, 6, 2);
            chessBoard.Add(pawnOwned, 5, 1);

            MovementType movementType = pawnEater.DefineMovementType(5, 1);

            Assert.AreEqual(MovementType.Capture, movementType);
        }

        [TestMethod]
        public void PawnIlegalCoordinate_MovingBackward_GetMovementType_TypeIllegal()
        {
            chessBoard.Add(pawnEater, 6, 2);

            MovementType movementType = pawnEater.DefineMovementType(6, 3);

            Assert.AreEqual(MovementType.InvalidMovement, movementType);
        }

        [TestMethod]
        public void PawnLegalCoordinate_MovingForward_GetMovementType_TypeMove()
        {
            chessBoard.Add(pawnEater, 6, 2);

            MovementType movementType = pawnEater.DefineMovementType(6, 1);

            Assert.AreEqual(MovementType.Move, movementType);
        }
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using SolarWinds.MSP.Chess.Pieces;
using System;

namespace SolarWinds.MSP.Chess
{
    [TestClass]
	public class ChessBoardTest
	{
		private ChessBoard chessBoard;

        [TestInitialize]
		public void SetUp()
		{
            chessBoard = ChessBoard.Instance;
            chessBoard.ClearBoard();
		}

        [TestMethod]
		public void Has_MaxBoardWidth_of_7()
		{
			Assert.AreEqual(ChessBoard.MaxBoardWidth, 7);
		}

        [TestMethod]
		public void Has_MaxBoardHeight_of_7()
		{
			Assert.AreEqual(ChessBoard.MaxBoardHeight, 7);
		}

        [TestMethod]
		public void IsLegalBoardPosition_True_X_equals_0_Y_equals_0()
		{
			var isValidPosition = chessBoard.IsLegalBoardPosition(0, 0);
			Assert.IsTrue(isValidPosition);
		}

        [TestMethod]
		public void IsLegalBoardPosition_True_X_equals_5_Y_equals_5()
		{
			var isValidPosition = chessBoard.IsLegalBoardPosition(5, 5);
            Assert.IsTrue(isValidPosition);
		}

        [TestMethod]
		public void IsLegalBoardPosition_False_X_equals_11_Y_equals_5()
		{
			var isValidPosition = chessBoard.IsLegalBoardPosition(11, 5);
            Assert.IsFalse(isValidPosition);
		}

        [TestMethod]
		public void IsLegalBoardPosition_False_X_equals_0_Y_equals_9()
		{
			var isValidPosition = chessBoard.IsLegalBoardPosition(0, 9);
            Assert.IsFalse(isValidPosition);
		}

        [TestMethod]
		public void IsLegalBoardPosition_False_X_equals_11_Y_equals_0()
		{
			var isValidPosition = chessBoard.IsLegalBoardPosition(11, 0);
            Assert.IsFalse(isValidPosition);
		}

        [TestMethod]
		public void IsLegalBoardPosition_False_For_Negative_X_Values()
		{
			var isValidPosition = chessBoard.IsLegalBoardPosition(-1, 5);
            Assert.IsFalse(isValidPosition);
		}

        [TestMethod]
		public void IsLegalBoardPosition_False_For_Negative_Y_Values()
		{
			var isValidPosition = chessBoard.IsLegalBoardPosition(5, -1);
            Assert.IsFalse(isValidPosition);
		}

        [TestMethod]
        public void Avoids_Duplicate_Positioning()
		{
			Pawn firstPawn = new Pawn(PieceColor.Black);
			Pawn secondPawn = new Pawn(PieceColor.Black);

			chessBoard.Add(firstPawn, 6, 3);
			chessBoard.Add(secondPawn, 6, 3);

			Assert.AreEqual(firstPawn.XCoordinate, 6);
            Assert.AreEqual(firstPawn.YCoordinate, 3);
            Assert.IsFalse(chessBoard.isPiecePresentOnBoard(secondPawn));
		}

        [TestMethod]
		public void Limits_The_Number_Of_Pawns()
		{
			for (int i = 0; i < 10; i++)
			{
				Pawn pawn = new Pawn(PieceColor.Black);
				int row = i / (ChessBoard.MaxBoardWidth+1);
                int col = i % (ChessBoard.MaxBoardWidth + 1);

                chessBoard.Add(pawn, 6 + row, col);
				if (row < 1)
				{
					Assert.AreEqual(pawn.XCoordinate, (6 + row));
					Assert.AreEqual(pawn.YCoordinate, col);
				}
				else
				{
                    Assert.AreEqual(pawn.XCoordinate, -1);
                    Assert.AreEqual(pawn.YCoordinate, -1);
                    Assert.IsFalse(chessBoard.isPiecePresentOnBoard(pawn));
				}
			}
		}
	}
}

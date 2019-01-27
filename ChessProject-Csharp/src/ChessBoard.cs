using System;
using System.Collections.Generic;
using System.Linq;
using SolarWinds.MSP.Chess.Pieces;

namespace SolarWinds.MSP.Chess
{
    public class ChessBoard
    {
        public static readonly int MaxBoardWidth = 7;
        public static readonly int MaxBoardHeight = 7;

        private List<ChessPiece> onBoardPieces;
        private List<ChessPiece> outsiteBoardPieces;

        private static ChessBoard instance = null;

        private ChessBoard()
        {
            this.onBoardPieces = new List<ChessPiece>();
            this.outsiteBoardPieces = new List<ChessPiece>();
        }

        public static ChessBoard Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ChessBoard();
                }

                return instance;
            }
        }

        public void Add(ChessPiece newChessPiece, int xCoordinate, int yCoordinate)
        {
            int[] coordinates = new int[2] { xCoordinate, yCoordinate };

            if (this.IsLegalBoardPosition(xCoordinate, yCoordinate) && this.GetPieceFromBoard(coordinates) == null)
            {
                int maxAmount = this.onBoardPieces.Where(x => x.GetType().Name == newChessPiece.GetType().Name && x.PieceColor == newChessPiece.PieceColor).Count();

                if (maxAmount < newChessPiece.MaxAmountOfPieces)
                {
                    newChessPiece.XCoordinate = xCoordinate;
                    newChessPiece.YCoordinate = yCoordinate;

                    this.onBoardPieces.Add(newChessPiece);
                }
            }
        }

        public void ClearBoard()
        {
            this.onBoardPieces.Clear();
            this.outsiteBoardPieces.Clear();
        }

        public bool IsLegalBoardPosition(int xCoordinate, int yCoordinate)
        {
            bool result = true;
            // It's inside the board?
            if ((xCoordinate > 7 || xCoordinate < 0) || (yCoordinate > 7 || yCoordinate < 0))
            {
                result = false;
            }

            return result;
        }

        public bool IsLegalMovement(int xCoordinate, int yCoordinate, bool isCapturing)
        {
            bool result = true;
            bool onTopOfAnotherPiece = onBoardPieces.Where(piece => piece.XCoordinate == xCoordinate && piece.YCoordinate == yCoordinate).Count() > 0;

            // It's on top of another piece?
            if ((onTopOfAnotherPiece && !isCapturing) || (!onTopOfAnotherPiece && isCapturing))
            {
                result = false;
            }

            // It's inside the board? 
            if (!this.IsLegalBoardPosition(xCoordinate, yCoordinate))
            {
                result = false;
            }

            return result;
        }

        public ChessPiece GetPieceFromBoard(int[] coordinatesOfPiece)
        {
            ChessPiece result = null;

            result = onBoardPieces.FirstOrDefault(piece => piece.XCoordinate == coordinatesOfPiece[0] &&
                                            piece.YCoordinate == coordinatesOfPiece[1]);

            return result;
        }

        public bool isPiecePresentOnBoard(ChessPiece pieceToFind)
        {
            if(this.onBoardPieces.Contains(pieceToFind))
            {
                return true;
            }

            return false;
        }

        public void UpdateCapturePiece(int xCoordinate, int yCoordinate)
        {
            var pieceToRemove = this.onBoardPieces.FirstOrDefault(x => x.XCoordinate == xCoordinate && x.YCoordinate == yCoordinate);
            if(pieceToRemove == null)
            {
                throw new ArgumentException("Piece not found on board.");
            }

            outsiteBoardPieces.Add(pieceToRemove);
            onBoardPieces.Remove(pieceToRemove);
        }
    }
}

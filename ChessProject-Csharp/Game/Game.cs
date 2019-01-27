using SolarWinds.MSP.Chess;
using SolarWinds.MSP.Chess.Pieces;
using System;
using System.Linq;

namespace Game
{
    public class Game
    {
        public static void Main()
        {
            Game newGame = new Game();
        }

        public Game()
        {
            // Add some pieces as example on the board.
            ChessBoard chessBoard = ChessBoard.Instance;
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.ForegroundColor = ConsoleColor.Black;

            Pawn pawn1 = new Pawn(PieceColor.Black);
            Pawn pawn2 = new Pawn(PieceColor.White);
            string warnUser = string.Empty;

            chessBoard.Add(pawn1, 1, 6);
            chessBoard.Add(pawn2, 0, 1);

            do
            {
                Console.Clear();
                Console.Out.WriteLine(warnUser);

                PrintBoard();
                Console.Out.Write("Select the next movement (i.e. B3:B4):");
                string input = Console.In.ReadLine();

                try
                {
                    int[,] coordinatesOfPiece = TransformInputToCoordinates(input);

                    ChessPiece chessPiece = ChessBoard.Instance.GetPieceFromBoard(new int[] { coordinatesOfPiece[0,0], coordinatesOfPiece[0,1] });

                    if (chessPiece == null)
                    {
                        throw new ArgumentException("No piece found at: " + input.Substring(0, 2));
                    }

                    // move it.
                    ((IPieceMovement)chessPiece).Move(coordinatesOfPiece[1, 0], coordinatesOfPiece[1, 1]);
                }
                catch(ArgumentException e)
                {
                    warnUser = e.Message;
                }

            } while (true); // TODO break with something.
        }
        private int[,] TransformInputToCoordinates(string input)
        {
            // Format: A4:B5 where A4 is current position and B5 is where he want to move.
            int[,] result = new int[2,2];
            if (input.Count() != 5)
            {
                throw new ArgumentException("Invalid coordinates.");
            }

            string[] results = input.Split(':');

            if(results.Count() != 2)
            {
                throw new ArgumentException("Invalid coordinates.");
            }

            string currentPiecePosition = results[0];
            string nextPiecePosition = results[1];

            // Coordinate from letter.
            result[0, 0] = GetIntegerCoordinateFromLetter(currentPiecePosition[0]);
            result[1, 0] = GetIntegerCoordinateFromLetter(nextPiecePosition[0]);

            if(!int.TryParse(currentPiecePosition[1].ToString(), out int currentY) ||
               !int.TryParse(nextPiecePosition[1].ToString(), out int nextY))
            {
                throw new ArgumentException("Invalid coordinates.");
            }

            // Rest of the coordinate.
            //int currentY = Convert.ToInt32(currentPiecePosition[1].ToString());
            //int nextY = Convert.ToInt32(nextPiecePosition[1].ToString());

            if (!ChessBoard.Instance.IsLegalBoardPosition(result[0,0], currentY) || 
                !ChessBoard.Instance.IsLegalBoardPosition(result[1, 0], nextY) )
            {
                throw new ArgumentException("Invalid coordinates.");
            }

            result[0, 1] = currentY;
            result[1, 1] = nextY;

            return result;
        }

        private int GetIntegerCoordinateFromLetter(char coordinate)
        {
            // TODO: improve this.
            int result;

            switch(coordinate)
            {
                case 'a':
                case 'A':
                    result = 0;
                    break;
                case 'b':
                case 'B':
                    result = 1;
                    break;
                case 'c':
                case 'C':
                    result = 2;
                    break;
                case 'd':
                case 'D':
                    result = 3;
                    break;
                case 'e':
                case 'E':
                    result = 4;
                    break;
                case 'f':
                case 'F':
                    result = 5;
                    break;
                case 'g':
                case 'G':
                    result = 6;
                    break;
                case 'h':
                case 'H':
                    result = 7;
                    break;
                default:
                    throw new ArgumentException();
            }

            return result;
        }

        public void PrintBoard()
        {
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.ForegroundColor = ConsoleColor.Black;

            string line = string.Empty;
            string emptySpace = " ";
            ChessBoard chessBoard = ChessBoard.Instance;
            Console.WriteLine("|{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}|", "A","B","C","D","E","F","G","H");

            for (int y = ChessBoard.MaxBoardHeight; y >= 0; y--)
            {
                for (int x = 0; x <= ChessBoard.MaxBoardWidth; x++) 
                {
                    var piece = chessBoard.GetPieceFromBoard(new int[] { x, y });
                    if(piece == null)
                    {
                        Console.Write("|"+emptySpace);
                    }    
                    else
                    {
                        Console.Write("|");

                        if (piece.PieceColor == PieceColor.White)
                        {
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        Console.Write(piece.IndentifierOnBoard);
                        Console.ForegroundColor = ConsoleColor.Black;
                    }
                }
                
                Console.WriteLine("|" + y);
                line = string.Empty;
            }
        }
    }
}

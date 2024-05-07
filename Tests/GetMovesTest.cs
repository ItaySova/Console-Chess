using Console_Chess.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Chess.Tests
{
    internal class GetMovesTest:ChessGame
    {
        public GetMovesTest(Board board):base(board) { }

        public static void GetMovesTestPawm()
        {
            Board board = new Board();
            Piece blackPawn = new Pawn(false, new Position(1,4));
            Piece blackNearWhitePawn = new Pawn(false, new Position(5, 0));
            Piece blackOnEdge = new Pawn(false, new Position(7, 7));
            Piece blackRow2 = new Pawn(false, new Position(6, 6));
            Piece whitePawn = new Pawn(true, new Position(6, 1));
            Piece whiteNearBlackPawn = new Pawn(true, new Position(2, 5));
            Piece whiteOnEdge = new Pawn(true, new Position(0, 0));
            Piece whiteRow7 = new Pawn(true, new Position(1, 6));
            if (!board.AddPiece(whitePawn))
            {
                Console.WriteLine("white pawn not added");
            }
            if (!board.AddPiece(blackPawn))
            {
                Console.WriteLine("black pawn not added");
            }
            if (!board.AddPiece(blackNearWhitePawn))
            {
                Console.WriteLine("blackNearWhitePawn not added");
            }
            if (!board.AddPiece(whiteNearBlackPawn))
            {
                Console.WriteLine("whiteNearBlackPawn not added");
            }
            if (!board.AddPiece(whiteOnEdge))
            {
                Console.WriteLine("whiteOnEdge not added");
            }
            if (!board.AddPiece(blackOnEdge))
            {
                Console.WriteLine("blackOnEdge not added");
            }
            if (!board.AddPiece(blackRow2))
            {
                Console.WriteLine("blackRow2 not added");
            }
            if (!board.AddPiece(whiteRow7))
            {
                Console.WriteLine("whiteRow7 not added");
            }

            board.Print();
            ChessGame testGame = new GetMovesTest(board);
            testGame.Play();
        }

        public static void GetMovesTestking()
        {
            //
        }

        public static void GetMovesTestQueen()
        {
            //
        }

        public static void GetMovesTestBishop()
        {
            //
        }

        public static void GetMovesTestRook()
        {
            //
        }

        public static void GetMovesTestKnight()
        {
            //
        }

        public override void Play()
        {
            Console.WriteLine("playing from moves test!");
            Board testBoard = GetBoard();
            bool Player = GetTurnPlayer();
            bool gameOver = false;
            string CurrentPieceMoves = "";
            while (!gameOver)
            {
                // printing
                testBoard.Print();

                // taking user input
                Move playerMove = UserInput();
                Piece Chosen = testBoard.GetPositionPiece(playerMove.GetFromPos());
                Console.WriteLine("move is : " + playerMove.ToString() + "\n" +
                    "piece chosen: " + Chosen);
                //testBoard.RemovePiece(playerMove.GetFromPos()); // - later for moving
                CurrentPieceMoves = Chosen.GetMoves(testBoard);
                int[] coords = playerMove.GetFromPos().GetCoordinates();
                Console.WriteLine("player " + (Player ? "white" : "black") + " moves for chosen piece at:" + playerMove.GetFromPos() + " or " +
                    coords[0] +", " + coords[1] + " is: " + CurrentPieceMoves);

                // print the board with the position available for chosen piece:
                testBoard.Print(CurrentPieceMoves);

                Console.WriteLine("press enter to continue to new turn:");
                Console.ReadLine();
                Console.Clear();
                // testing the input for valid input - rulewise

                // executing the input

                // change the turn player and incrementing turn count:
                Player = !Player;
                SetTurnPlayer(Player);
            }
        }
    }
}

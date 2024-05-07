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
            Piece whitePawn = new Pawn(true, new Position(6, 1));
            if (!board.AddPiece(whitePawn))
            {
                Console.WriteLine("white pawn not added");
            }
            if (!board.AddPiece(blackPawn))
            {
                Console.WriteLine("black pawn not added");
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
            while (!gameOver)
            {
                // printing
                testBoard.Print();

                // taking user input
                Move playerMove = UserInput();
                Piece Chosen = testBoard.GetPositionPiece(playerMove.GetFromPos());
                Console.WriteLine("move is : " + playerMove.ToString() + "\n" +
                    "piece chosen: " + Chosen);
                testBoard.RemovePiece(playerMove.GetFromPos());

                // testing the input for valid input - rulewise

                // executing the input

                // change the turn player and incrementing turn count:
                Player = !Player;
                SetTurnPlayer(Player);
            }
        }
    }
}

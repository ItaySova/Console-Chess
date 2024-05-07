using Console_Chess.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Chess.Tests
{
    internal class GetAllMovesAndExecuteTest: ChessGame
    {
        public GetAllMovesAndExecuteTest(Board board) : base(board) { }

        public GetAllMovesAndExecuteTest() { }
        public static void GetAllMovesTest()
        {
            Board board = new Board();
            Piece blackPawn = new Pawn(false, new Position(1, 4));
            Piece blackNearWhitePawn = new Pawn(false, new Position(5, 0));
            Piece blackOnEdge = new Pawn(false, new Position(7, 7));
            Piece blackRow2 = new Pawn(false, new Position(6, 6));
            Piece whitePawn = new Pawn(true, new Position(6, 1));
            Piece whiteNearBlackPawn = new Pawn(true, new Position(2, 5));
            Piece whiteOnEdge = new Pawn(true, new Position(0, 0));
            Piece whiteRow7 = new Pawn(true, new Position(1, 6));
            board.AddPiece(blackPawn);
            board.AddPiece(blackNearWhitePawn);
            board.AddPiece(blackOnEdge);
            board.AddPiece(blackRow2);
            board.AddPiece(whitePawn);
            board.AddPiece(whiteNearBlackPawn);
            board.AddPiece(whiteOnEdge);
            board.AddPiece(whiteRow7);
            board.Print();
            ChessGame testGame = new GetAllMovesAndExecuteTest(board);
            testGame.Play();
        }

        public static void IsMoveInListAutomation()
        {
            ChessGame testGame = new GetAllMovesAndExecuteTest();
            testGame.Play();
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

                int[] coords = playerMove.GetFromPos().GetCoordinates();
                Console.WriteLine("player " + (Player ? "white" : "black") + " moves for chosen piece at:" + playerMove.GetFromPos() + " or " +
                    coords[0] + ", " + coords[1] + " is: ");
                Console.WriteLine();
                bool isMoveValid = IsMoveInAllPlayerMoves(playerMove);
                Console.WriteLine((isMoveValid?"valid move": "invalid move"));
                // print the board with the position available for chosen piece:
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

        // simulate game with only valid moves from input regard
        public void GameSimulation(string[] InputMoves)
        {
            Console.WriteLine("playing from moves test!");
            Board testBoard = GetBoard();
            bool Player = GetTurnPlayer();
            bool gameOver = false;
            int moveCounter = 0;
            while (!gameOver)
            {

                // printing
                testBoard.Print();

                // taking user input
                Move playerMove = ConvertInputToMove(InputMoves[moveCounter]);
                Console.WriteLine(playerMove.ToString());

                // testing the input for valid input - rulewise
                Piece[] allPlayerPieces = testBoard.GetAllPiecesForPlayer(Player);
                bool isMoveValid = IsMoveInAllPlayerMoves(playerMove);
                Console.WriteLine((isMoveValid ? "valid move" : "invalid move"));
                // executing the input

                // change the turn player and incrementing turn count:
                moveCounter ++;
                Player = !Player;
                SetTurnPlayer(Player);
            }
        }
    }
}

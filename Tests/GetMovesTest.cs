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
            /*list of moves for future tests: 
             b2b3-> a3a2-> f6f7 -> e7e6 -> a8a7 -> h1h2 -> g7g8 -> g2g1
             */
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
            Board board = new Board();
            Piece BlackKing = new King(false, new Position(0, 4));
            Piece whiteKing = new King(true, new Position(7, 4));
            Piece WhiteNearBlackKing = new Pawn(true , new Position(1, 3));
            Piece whiteNearWhiteKing = new Pawn(true, new Position(6, 3));
            Piece blackNearBlackKing = new Pawn(false, new Position(1, 5));
            Piece blackNearWhiteKing = new Pawn(false, new Position(6, 5));
            Piece WhiteKingNoSorround = new King(true , new Position(4, 2));
            board.AddPiece(BlackKing);
            board.AddPiece(whiteKing);
            board.AddPiece(WhiteNearBlackKing);
            board.AddPiece(whiteNearWhiteKing);
            board.AddPiece(blackNearBlackKing);
            board.AddPiece(blackNearWhiteKing);
            board.AddPiece(WhiteKingNoSorround);
            board.Print();
            ChessGame testGame = new GetMovesTest(board);
            testGame.Play();

        }

        public static void GetMovesTestQueen()
        {
            Board board = new Board();
            Piece whiteQueen = new Queen(true, new Position(7, 3));
            Piece blackQueen = new Queen(false, new Position(1, 3));
            board.AddPiece(blackQueen);
            board.AddPiece(whiteQueen);
            ChessGame testGame = new GetMovesTest(board);
            testGame.Play();
        }

        public static void GetMovesTestBishop()
        {
            Board board = new Board();
            Piece whiteBishop = new Bishop(true, new Position(4, 3));
            Piece blackBishop = new Bishop(false, new Position(0, 2));
            Piece blackPawn = new Pawn(false, new Position(2, 0));
            board.AddPiece(blackBishop);
            board.AddPiece(whiteBishop);
            board.AddPiece(blackPawn);
            ChessGame testGame = new GetMovesTest(board);
            testGame.Play();
        }

        public static void GetMovesTestRook()
        {
            Board board = new Board();
            Piece whiteRook = new Rook(true, new Position(7, 7));
            Piece WhiteInMiddleBoard = new Rook(true, new Position(4, 3));
            Piece blackRook = new Rook(false, new Position(0, 0));
            Piece blackRookOnWhitePath = new Rook(false, new Position(1, 7));
            Piece whitePawn = new Pawn(true, new Position(7,1));
            board.AddPiece(whiteRook);
            board.AddPiece(WhiteInMiddleBoard);
            board.AddPiece(blackRook);
            board.AddPiece(blackRookOnWhitePath);
            board.AddPiece(whitePawn);
            ChessGame testGame = new GetMovesTest(board);
            testGame.Play();
        }

        public static void GetMovesTestKnight()
        {
            Board board = new Board();
            Piece whiteKnight = new Knight(true, new Position(3, 3));
            Piece blackKnight = new Knight(false, new Position(1, 0));
            Piece whitePawn = new Pawn(true, new Position(2, 2));
            board.AddPiece(whiteKnight);
            board.AddPiece(blackKnight);
            board.AddPiece(whitePawn);

            ChessGame testGame = new GetMovesTest(board);
            testGame.Play();
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
                string[] MovesArr = CurrentPieceMoves.Split(',');
                int[] coords = playerMove.GetFromPos().GetCoordinates();
                Console.WriteLine("player " + (Player ? "white" : "black") + " moves for chosen piece at:" + playerMove.GetFromPos() + " or " +
                    coords[0] +", " + coords[1] + " is: ");
                for(int i=0; i< MovesArr.Length; i++)
                {
                    if (MovesArr[i] != "")
                    {
                        Console.Write(MovesArr[i] + ",");
                    }
                }
                Console.WriteLine();

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

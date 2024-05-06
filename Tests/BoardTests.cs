using Console_Chess.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Chess.Tests
{
    internal class BoardTests: ChessGame
    {
        public static void GetPositionTest()
        {
            Board board = new Board();
            board.InitBoard();
            board.Print();
            Piece p1 = board.GetPositionPiece(new Position(0,0));
            Piece p2 = board.GetPositionPiece(new Position(7, 7));
            Piece p3 = board.GetPositionPiece(new Position(4, 4));
            Piece p4 = board.GetPositionPiece(new Position(1, 2));
            Piece p5 = board.GetPositionPiece(new Position(1, 7));

            bool expected = p3 == null && p1 is Rook && p2 is Rook && p4 is Pawn && p5 is Pawn;
            Console.WriteLine("p1: "+p1);
            Console.WriteLine("p2: " + p2);
            Console.WriteLine("p3: " + p3);
            Console.WriteLine("p4: " + p4);
            Console.WriteLine("p5: " + p5);
        }

        public override void Play()
        {
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
                Console.WriteLine("move is : "+ playerMove.ToString()+ "\n" +
                    "piece chosen: "+ Chosen);
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

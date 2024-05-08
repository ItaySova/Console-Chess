using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Chess.Tests
{
    internal class NewGameLogicTest: ChessGame
    {
        bool MovesMarked;

        public NewGameLogicTest(Board board, bool markFlag) : base(board)
        {
            this.MovesMarked = markFlag;
        }
        public NewGameLogicTest(Board board) : base(board)
        {
            this.MovesMarked = false;
        }

        public NewGameLogicTest(bool markFlag)
        {
            this.MovesMarked = markFlag;
        }
        public NewGameLogicTest():this(false)
        {
        }

        public static void NewLogic()
        {
            /*bool flag = false;
            NewGameLogicTest game = new NewGameLogicTest(flag);
            game.Play();*/
            NewLogic(false);
        }

        public static void NewLogic(bool markFlag)
        {
            NewGameLogicTest game = new NewGameLogicTest(markFlag);
            game.Play();
        }

        public override void Play()
        {
            while (!IsGameOver)
            {

                // get all available moves
                string[] MovesAvailable = GetAllPlayerMoves();
                if (MovesMarked)
                {
                    string[] MovesCopyForPrint = new string[MovesAvailable.Length];
                    for (int i = 0; i < MovesAvailable.Length; i++)
                    {
                        MovesCopyForPrint[i] = MovesAvailable[i];
                    }
                    // print with marking:
                    board.Print(MovesCopyForPrint);
                }
                else
                {
                    // print with no mark
                    board.Print();
                }

                // is Check test - will be complete gamestate check later 
                bool check = isPlayerInCheck();
                Console.WriteLine(check? "CHECK":"");
                bool isMoveValid = false;
                Move playerMove = null;
                // taking user input
                while (!isMoveValid)
                {
                    playerMove = UserInput();
                    isMoveValid = Array.IndexOf(MovesAvailable, playerMove.ToString()) != -1;
                    if (!isMoveValid)
                    {
                        Console.WriteLine("move is not valid - try again");
                        isMoveValid = IsMoveInAllPlayerMoves(playerMove);
                    }
                }             
                // testing the input for valid input - rulewise
                // add validation that the move doesnt leave a player in check

                // executing the input
                ExecuteMove(playerMove);

                // change the turn player and incrementing turn count:
                TurnPlayer = !TurnPlayer;
                TurnCount++;
            };
        }
    }
}

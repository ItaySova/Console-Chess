using Console_Chess.Pieces;
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
            Console.WriteLine("playing from new logic design");
            GameState state = new GameState();
            DisplayRules();
            state.UpdateMoveslist(board);
            while (!state.GetGameOver())
            {

                // get all available moves
                string[] MovesAvailable = state.GetAllPossibleMoves(board);
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
                
                Console.WriteLine(state.GetCheckStatus() ? "CHECK":"");

                // remove in the end:
                /*string[] LegalMoves = state.GetLegalMoves(board, MovesAvailable);
                Console.WriteLine("is legal == possible " + IsLegalMovesEqualPossible(state.GetMovesList(), LegalMoves));
                Console.WriteLine(LegalMoves.Length + " legal moves available");*/

                bool isMoveValid = false;
                Move playerMove = null;
                // taking user input
                while (!isMoveValid)
                {
                    playerMove = UserInput();

                    if (playerMove == null)
                    {
                        // means player resigned
                        state.UpdateGameOver("RESIGN");
                        break;
                    }

                    isMoveValid = Array.IndexOf(state.GetMovesList(), playerMove.ToString()) != -1;
                    if (!isMoveValid)
                    {
                        Console.WriteLine("move is not valid - try again");
                    }                    
                }             

                if(playerMove == null)
                {
                    continue;
                }

                // executing the input
                ExecuteMove(board, playerMove, state);

                // change the turn player and incrementing turn count:
                state.UpdateGameState(board);
                TurnPlayer = state.GetPlayer();
                TurnCount = state.GetTurnCount();
            };
            board.Print();
            string res = state.GetResult();
            Console.WriteLine("GAME OVER BY " + res);
            if (res == "CHECKMATE")
            {
                Console.WriteLine("the {0} player won!", (TurnPlayer?"black":"white"));
            }
        }

        private bool IsLegalMovesEqualPossible(string[] possibleMoves, string[] legalMoves)
        {
            for (int possibleCounter = 0, legalCounter=0; possibleCounter < possibleMoves.Length; possibleCounter++)
            {
                if (possibleMoves[possibleCounter] != "") // if the possible move is not empty
                {
                    if(possibleMoves[possibleCounter] != legalMoves[legalCounter]) // check if it is same as the legal in the same respective place
                    {
                        return false;
                    }
                    // the moves in the respective position is the same
                    legalCounter++; // only increment if a comparison was done
                }
            }
            return true;
        }

        public override Move UserInput() // virtual for tests
        {
            bool isValid = false;
            string input = "";
            Move PlayerMove = null;
            while (!isValid)
            {
                Console.WriteLine("what will be your move, " + (TurnPlayer ? "white" : "black"));
                input = Console.ReadLine();
                if (input == "RESIGN")
                {
                    return null;
                }
                if(input == "RULES")
                {
                    DisplayRules();
                    continue;
                }

                if (ValidateInput(input))
                {
                    // if the input is valid - convert to move and proceed
                    PlayerMove = ConvertInputToMove(input);
                    Position FromPos = PlayerMove.GetFromPos();
                    Piece Chosen = board.GetPositionPiece(FromPos);
                    // validation for choosing a piece which belong to turns player and not an empty square
                    isValid = Chosen != null && IsPieceBelongToPlayer(Chosen);
                    if (!isValid)
                    {
                        HandleInvalidInput(Chosen);
                    }
                }                                    
            }
            return PlayerMove;
        }

    }
}

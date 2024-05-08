using Console_Chess.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Chess.Tests
{
    internal class GameSimulations : ChessGame
    {
        public GameSimulations(Board board) : base(board) { }

        public GameSimulations() { }

        public static void Simulation1(string option)
        {
            //ChessGame testGame = new GameSimulations();
            string[] inputs = { "e2e4", "f7f5", "b1c3", "b8c6" ,"d1h5","g7g6","h5f5","g6f5"};
            string[] inputsForQuickCheckmate = { "e2e4", "f7f5", "d2d3", "g7g5", "d1h5"};

            string[] inputsOfChoice = null;
            switch (option)
            {
                case "1":
                    Console.WriteLine("running random simulation");
                    inputsOfChoice = inputs;
                    break;
                case "2":
                    Console.WriteLine("running quick checkmate simulation");
                    inputsOfChoice = inputsForQuickCheckmate;
                    break;
                case "3":
                    Console.WriteLine(" not implemented yet");
                    break;
                default:
                    break;
            }
            //testGame.GameSimulation(inputs);
            GameSimulations simu = new GameSimulations();
            simu.GameSimulationWithInputCheck(inputsOfChoice);
        }

        public override void GameSimulation(string[] InputMoves)
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
                bool checkTest = isPlayerInCheck();

                // taking user input
                Move playerMove = ConvertInputToMove(InputMoves[moveCounter]);
                Console.WriteLine(playerMove.ToString() +" by player: " + (Player?"white":"black"));
                Console.WriteLine((checkTest?"player is in Check":"player is not in check!"));

                // testing the input for valid input - rulewise
                bool isMoveValid = IsMoveInAllPlayerMoves(playerMove);
                Console.WriteLine((isMoveValid ? "execute input:" : "invalid move"));
                // executing the input
                ExecuteMove(testBoard,playerMove);
                // change the turn player and incrementing turn count:
                Console.WriteLine("press enter to continue");
                Console.ReadLine();
                moveCounter++;
                if(moveCounter == InputMoves.Length)
                {
                    testBoard.Print();
                    Console.WriteLine("ran out of automatic inputs");
                    gameOver = true;
                }
                Player = !Player;
                SetTurnPlayer(Player);
            }
        }

        // adding simulation with mistakes
        public void GameSimulationWithInputCheck(string[] inputs)
        {
            Console.WriteLine("playing from moves test!");
            Board testBoard = GetBoard();
            bool gameOver = false;
            int moveCounter = 0;
            GameState state = new GameState();
            bool ContinueManually = false;
            while (!state.GetGameOver())
            {
                // check if simulation run out of moves
                if (moveCounter == inputs.Length && ContinueManually == false)
                {
                    testBoard.Print();
                    Console.WriteLine("ran out of automatic inputs, would you like to continue manually?");
                    string answer = Console.ReadLine();
                    switch (answer)
                    {
                        case "y":
                            Console.WriteLine("will be implemented");
                            ContinueManually = true;
                            break;
                        case "n":
                            Console.WriteLine("ending simulation");
                            break;
                    }
                    if (!ContinueManually)
                    {
                        break;
                    }
                }


                bool Player = state.GetPlayer();
                string[] MovesAvailable = GetAllPlayerMoves();
                // printing
                testBoard.Print();
                Console.WriteLine(state.GetCheckStatus() ? "CHECK" : "");
                string[] LegalMoves = state.GetLegalMoves(testBoard, MovesAvailable);

                bool isMoveValid = false;
                Move playerMove = null;
                // taking user input
                while (!isMoveValid)
                {
                    playerMove = ValidateAutomationInput(inputs[moveCounter]);
                    isMoveValid = Array.IndexOf(MovesAvailable, playerMove.ToString()) != -1;
                    if (!isMoveValid)
                    {
                        Console.WriteLine("input not valid - enter new input: ");
                        changeMoveIfNotInMovesList(inputs, moveCounter);
                        continue;
                    }
                    if (isMoveValid)
                    {
                        Console.WriteLine("move is in all possible - check for legal moves");
                        isMoveValid = Array.IndexOf(LegalMoves, playerMove.ToString()) != -1;
                    }
                }
                    

                // executing the input
                ExecuteMove(testBoard,playerMove);
                // change the turn player and incrementing turn count:
                Console.WriteLine((state.GetPlayer() ? "white" : "black") + " chose the move " + playerMove.ToString());
                Console.WriteLine("press enter to continue");
                Console.ReadLine();
                state.UpdateGameState(testBoard);
                moveCounter++;
                
                // Player = !Player;
                SetTurnPlayer(state.GetPlayer());
            }
        }

        public override Move UserInput()
        {
            Board testBoard = GetBoard();
            bool Player = GetTurnPlayer();
            bool isValid = false;
            string input = "";
            Move PlayerMove = null;
            while (!isValid)
            {
                Console.WriteLine("what will be your move, " + (Player ? "white" : "black"));
                input = Console.ReadLine();
                if (ValidateInput(input))
                {
                    // if the input is valid - convert to move and proceed
                    PlayerMove = ConvertInputToMove(input);
                    Position FromPos = PlayerMove.GetFromPos();
                    Piece Chosen = testBoard.GetPositionPiece(FromPos);
                    // validation for choosing a piece which belong to turns player and not an empty square
                    isValid = Chosen != null && IsPieceBelongToPlayer(Chosen);
                }
                // massage for invalid move - 
                if (!isValid)
                    Console.WriteLine("failed input validation check - either the from spot is empty or contains enemt piece:");
            }
            return PlayerMove;
        }

        public Move ValidateAutomationInput(string input)
        {
            Board testBoard = GetBoard();
            bool Player = GetTurnPlayer();
            bool isValid = false;
            Move PlayerMove = null;
            while (!isValid)
            {
                if (ValidateInput(input))
                {
                    PlayerMove = ConvertInputToMove(input);
                    Position FromPos = PlayerMove.GetFromPos();
                    Piece Chosen = testBoard.GetPositionPiece(FromPos);
                    // validation for choosing a piece which belong to turns player and not an empty square
                    isValid = Chosen != null && IsPieceBelongToPlayer(Chosen);
                }
                if (!isValid)
                {
                    Console.WriteLine("automation input invalid - please give input manually:");
                    input = Console.ReadLine();
                }                
            }
            return PlayerMove;
        }

        public void changeMoveIfNotInMovesList(string[] inputs, int counter)
        {
            string userInput = "";
            userInput = Console.ReadLine();
            inputs[counter] = userInput;
        }
    }
}

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

        public static void Simulation1()
        {
            //ChessGame testGame = new GameSimulations();
            string[] inputs = { "e2e4", "f7f5", "b1c3", "b8c6" ,"d1h5","g7g6","h5f5","g6f5"};
            //testGame.GameSimulation(inputs);
            GameSimulations simu = new GameSimulations();
            simu.GameSimulationWithInputCheck(inputs);
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
                ExecuteMove(playerMove);
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
            bool Player = GetTurnPlayer();
            bool gameOver = false;
            int moveCounter = 0;
            while (!gameOver)
            {

                // printing
                testBoard.Print();
                bool checkTest = isPlayerInCheck();

                // taking user input
                Move playerMove = ValidateAutomationInput(inputs[moveCounter]);
                Console.WriteLine(playerMove.ToString() + " by player: " + (Player ? "white" : "black"));
                Console.WriteLine((checkTest ? "player is in Check" : "player is not in check!"));

                // testing the input for valid input - rulewise
                bool isMoveValid = IsMoveInAllPlayerMoves(playerMove);
                Console.WriteLine((isMoveValid ? "execute input:" : "invalid move, repeat with new input"));
                if(!isMoveValid)
                {
                    Console.WriteLine("enter new input: ");
                    changeMoveIfNotInMovesList(inputs, moveCounter);
                    continue;
                }
                // executing the input
                ExecuteMove(playerMove);
                // change the turn player and incrementing turn count:
                Console.WriteLine("press enter to continue");
                Console.ReadLine();
                moveCounter++;
                if (moveCounter == inputs.Length)
                {
                    testBoard.Print();
                    Console.WriteLine("ran out of automatic inputs");
                    gameOver = true;
                }
                Player = !Player;
                SetTurnPlayer(Player);
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

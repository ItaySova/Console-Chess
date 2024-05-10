﻿using Console_Chess.Pieces;
using System;
using System.Collections.Generic;
using System.Data;
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
            string[] inputsForQuickBlackCheckMate = { "F2F4", "E7E5","G2G4","D8H4", "F4F5"};
            string[] EnPassantInputs = { "b2b4","g7g5","b2b4","a7a5"}; // Todo continue later
            string[] CastlingInputs = { };
            string[] ThreeFoldInput = { "g1h3", "g8h6", "h3g1", "h6g8", "g1h3", "g8h6", "h3g1", "h6g8", "g1h3", "g8h6" };
            string[] ThreeFoldIncorrect = { "g1h3", "g8h6", "h1g1", "h6g8", "g1h1", "g8h6", "h3g1", "h6g8",
             "g1h3", "g8h6", "h3g1", "h6g8",
             "g1h3", "g8h6", "h3g1", "h6g8"
            , "g1h3", "g8h6", "h3g1", "h6g8"
            , "g1h3", "g8h6", "h3g1", "h6g8"
            , "g1h3", "g8h6", "h3g1", "h6g8"}; //
            string[] FullGaryKasparovGame = { "e2e4","e7e5","g1f3","b8c6","d2d4","e5d4","f3d4", "g8f6","d4c6","b7c6","e4e5", "d8e7"
            , "d1e2", "f6d5", "c2c4", "e7b4","b1d2", "d5f4","e2e3", "f4g6","f1d3","f8c5","e3g3","e8g8" // castling here
            , "e1g1" /*also castling*/, "d7d6", "d2b3", "g6e5", "a2a3", "b4b6", "b3b5", "b6c5", "c1e3","c5a5", "b2b4",
            "a5a4", "e3d4", "f7f6", "d4e5","f6e5","f2f4","c8f5", "f6e5", "f5d3", "g3d3", "d6e5", "d3d7", "a4b3","d7c6",
            "b3e3" /*check*/, "g1h1"}; // started at 11 stopped befor move 20 of kasparov 

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
                    Console.WriteLine("running quick black checkmate simulation");
                    inputsOfChoice = inputsForQuickBlackCheckMate;
                    break;
                case "4":
                    Console.WriteLine("running en passant simulation");
                    inputsOfChoice = EnPassantInputs;
                    break;
                case "5":
                    Console.WriteLine("running castling simulation");
                    inputsOfChoice = CastlingInputs;
                    break;
                case "6":
                    Console.WriteLine("Pawm promotion simulation");
                    break;
                case "7":
                    Console.WriteLine("running ThreeFoldInput simulation");
                    inputsOfChoice = ThreeFoldInput;
                    break;
                case "8":
                    Console.WriteLine("running ThreeFoldInput WRONG simulation");
                    inputsOfChoice = ThreeFoldIncorrect;
                    break;
                case "9":
                    Console.WriteLine("recreating Garry Kasparov VS Jorden van Forees");
                    inputsOfChoice = FullGaryKasparovGame;
                    break;
                default:
                    break;
            }
            ChessGame simu = new GameSimulations();
            simu.GameSimulation(inputsOfChoice);
            //testGame.GameSimulation(inputsOfChoice);
            //GameSimulations simu = new GameSimulations();
            //simu.GameSimulationWithInputCheck(inputsOfChoice);
        }

        public override void GameSimulation(string[] InputMoves)
        {
            GameState state = new GameState();
            int moveCounter = 0;
            bool SkipNext3Turns = GameSimulations.SkipNext3Turns();
            while (!state.GetGameOver())
            {
                string[] MovesAvailable = state.GetAllPossibleMoves(board);
                // printing
                board.Print();
                Console.WriteLine(state.GetCheckStatus() ? "CHECK" : "");
                string[] LegalMoves = state.GetLegalMoves(board, MovesAvailable);
                bool isMoveValid = false;
                Move playerMove = null;
                // taking user input
                while (!isMoveValid)
                {
                    playerMove = ValidateAutomationInput(InputMoves[moveCounter]);
                    isMoveValid = Array.IndexOf(MovesAvailable, playerMove.ToString()) != -1;
                    if (!isMoveValid)
                    {
                        Console.WriteLine("move is not valid - try again");
                        isMoveValid = IsMoveInAllPlayerMoves(playerMove);
                    }
                    if (isMoveValid)
                    {
                        Console.WriteLine("move is in all possible - check for legal moves");
                        isMoveValid = Array.IndexOf(LegalMoves, playerMove.ToString()) != -1;
                    }
                }
                if (SkipNext3Turns)
                {
                    Console.WriteLine("execute move number {0}: {1}", moveCounter, InputMoves[moveCounter]);
                }else
                {
                    Console.WriteLine("press enter to continue execute move number " + moveCounter + " " + InputMoves[moveCounter]);                    
                    Console.ReadLine();
                }
                
                ExecuteMove(board, playerMove, state);

                // change the turn player and incrementing turn count:
                state.UpdateGameState(board);
                TurnPlayer = state.GetPlayer();
                //TurnPlayer = !TurnPlayer;
                TurnCount++;
                moveCounter++;
                if(moveCounter == InputMoves.Length)
                {
                    Console.WriteLine("input ended");
                    break;
                }
            }
            board.Print();
            Console.WriteLine("GAME OVER BY " + state.GetResult());
        }

        // adding simulation with mistakes
        public void GameSimulationWithInputCheck(string[] inputs)
        {
            Console.WriteLine("playing from moves test!");
            Board testBoard = GetBoard();
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
                ExecuteMove(testBoard,playerMove, state);
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

        public static bool SkipNext3Turns()
        {
            Console.WriteLine("would you like to skip the next 3 turnes? enter y for yes");
            string input = Console.ReadLine();
            return input == "y" || input == "Y";
        }

        public void changeMoveIfNotInMovesList(string[] inputs, int counter)
        {
            string userInput = "";
            userInput = Console.ReadLine();
            inputs[counter] = userInput;
        }
    }
}

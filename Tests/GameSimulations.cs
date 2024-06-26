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
            // contains castle queen side, castle king side, en passant in the middle of the board, en passant in coloumn from col g to h
            // and en passant from a to b
            string[] PawnPromotionInputs = { "g2g4", "a7a5", "g4g5", "a5a4", "b2b4", "a4b3", "e2e4", "h7h5", "g5h6", "g7h6", "a2b3", "f8g7", "c1a3", "g8f6",
                "b1c3", "e8g8", "d1e2", "g7h8", "e1c1", "a8a4", "b3b4", "a4a7", "f1h3", "a7a4", "b4b5", "a4a6", "b5a6", "b8c6", "a6a7", "b7b5", "a7a8", "e7e6", "e4e5",
                "c6a5", "e2e4", "c8a6", "g1e2", "a6c8", "h1g1", "h8g7", "g1g3", "c8a6", "d1g1", "a6c8", "g3g7", "g8h8", "e4h7", "f6h7", "RESIGN" };

            // contain en passant from h to g, en passant from b to a and en passant to the west and east in the 
            // middle of the board
            string[] EnPassantInputs = { "h2h4", "b7b5", "h4h5", "g7g5", "h5g6", "b5b4", "a2a4", "b4a3", "c2c4",
                "f7f5", "c4c5", "d7d5", "c5d6", "f5f4", "e2e4", "f4e3", "RESIGN"}; 

            string[] StalemateInputs = { "c2c4", "h7h5", "h2h4", "a7a5", "d1a4", "a8a6", "a4a5", "a6h6", "a5c7", "f7f6",
                "c7d7", "e8f7", "d7b7", "d8d3", "b7b8", "d3h7", "b8c8", "f7g6", "c8e6" };
            string[] ThreeFoldInput = { "g1h3", "g8h6", "h3g1", "h6g8", "g1h3", "g8h6", "h3g1", "h6g8", "g1h3", "g8h6" };
            string[] ThreeFoldIncorrect = { "g1h3", "g8h6", "h1g1", "h6g8", "g1h1", "g8h6", "h3g1", "h6g8",
             "g1h3", "g8h6", "h3g1", "h6g8",
             "g1h3", "g8h6", "h3g1", "h6g8"
            , "g1h3", "g8h6", "h3g1", "h6g8"
            , "g1h3", "g8h6", "h3g1", "h6g8"
            , "g1h3", "g8h6", "h3g1", "h6g8"}; //
            string[] FullGaryKasparovGame = { "e2e4","e7e5","g1f3","b8c6","d2d4","e5d4","f3d4", "g8f6","d4c6","b7c6","e4e5", "d8e7"
            , "d1e2", "f6d5", "c2c4", "e7b4","b1d2", "d5f4","e2e3", "f4g6","f1d3","f8c5","e3g3","e8g8" // castling here
            , "e1g1" /*also castling*/, "d7d6", "d2b3", "g6e5", "a2a3", "b4b6", "b3c5", "b6c5", "c1e3","c5a5", "b2b4",
            "a5a4", "e3d4", "f7f6", "d4e5","f6e5","f2f4","c8f5", "f4e5", "f5d3", "g3d3", "d6e5", "d3d7", "a4b3","d7c6",
            "b3e3" /*check*/, "g1h1", "g8h8", "f1e1", "e3c3","c6c7","a8c8", "c7a7", "c8c4", "h2h3", "c4f4",
            "a7c5", "c3b2","c5e5", "b2b3", "e5e3", "b3c4", "a1c1", "c4f7", "e3g3", "h7h6","b4b5","f7d5", "a3a4",
            "f4a4", "c1b1", "f8f5","b5b6", "f5g5","b6b7", "d5b7", "g3g5"}; // started at 11 stopped befor move 32 of kasparov 
            string[] FiftyRulesInputs = { "e2e4", "e7e6", "f1e2", "f8e7", "e2f1", "e7f6", "f1d3", "f6e7", "d3c4", "e7f8", "c4d3",
                "d8f6", "g1h3", "f8d6", "e1g1", "d6a3", "b1c3", "g8h6", "g1h1", "e8g8", "h1g1", "a3b4", "c3b1", "b4c5", "d1e1", "g8h8", "e1e2", "h8g8",
                "e2e3", "b8a6", "e3f3", "f8e8", "f3g3", "c5b6", "g1h1", "b6a5", "g3f4", "a5b6", "f4e5", "g8h8", "e5d4", "h6g8", "b1a3", "f6h6", "a3c4",
                "h6h5", "h3g1", "a6b8", "f1d1", "h5h4", "d1e1", "h4h3", };
            string[] fullMagnusCarlsenVSLoekVanWely = {"e2e4","c7c5", "g1f3", "e7e6", "d2d4","c5d4", "f3d4", "a7a6", "b1c3", "d8c7", "f1d3", "g8f6",
                "e1g1" /*castling*/ ,"f8c5", "d4b3","c5e7", "f2f4", "d7d6", "a2a4", "b8c6", "a4a5", "b7b5", "a5b6" /*en passant capture*/,"c7b6" /*check*/,
                "g1h1", "e8g8" /*castling*/, "d1e2", "a6a5", "c1e3", "b6c7", "c3b5", "c7b8", "c2c3", "d6d5" /*move number 18*/,"e4e5","f6e4", "d3e4",
                "d5e4", "b3c5", "e7c5", "e3c5", "c8a6", "c3c4", "f8d8", "b5d6","f7f5", "e5f6" /*en passant capture*/,"d8d6", "e2e4", "a6b7",
                "c5d6", "b8d6", "a1d1", "c6d8", "f6f7" /*check*/, "g8f7", "e4h7", "d6c6", "f1f2", "c6e4", "f4f5","e6e5","f2d2", "b7c6" ,
                "h7g6" /*check*/,"f7e7", "d2d7", "RESIGN"};
                
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
                    Console.WriteLine("running stalemate simulation");
                    inputsOfChoice = StalemateInputs;
                    break;
                case "6":
                    Console.WriteLine("Pawm promotion simulation");
                    inputsOfChoice = PawnPromotionInputs;
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
                case "10":
                    Console.WriteLine("running the fifty moves rule inputs");
                    inputsOfChoice = FiftyRulesInputs;
                    break;
                case "11":
                    Console.WriteLine("recreating MagnusCarlsenVSLoekVanWely ");
                    inputsOfChoice = fullMagnusCarlsenVSLoekVanWely;
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
            state.UpdateMoveslist(board);
            bool SkipNext3Turns = GameSimulations.SkipNext3Turns();
            while (!state.GetGameOver())
            {
                if (moveCounter == InputMoves.Length)
                {
                    Console.WriteLine("input ended");
                    break;
                }

                // printing
                board.Print();
                Console.WriteLine(state.GetCheckStatus() ? "CHECK" : "");

                bool isMoveValid = false;
                Move playerMove = null;

                if (SkipNext3Turns)
                {
                    Console.WriteLine("execute move number {0}: {1}", moveCounter, InputMoves[moveCounter]);
                    SkipNext3Turns = !(moveCounter == 6); // for testing purposes skip to the last time the board was correct
                }
                else
                {
                    Console.WriteLine("press enter to continue execute move number " + moveCounter + " " + InputMoves[moveCounter]);
                    Console.ReadLine();
                }

                // taking user input
                while (!isMoveValid)
                {
                    playerMove = ValidateAutomationInput(InputMoves[moveCounter]);
                    if (playerMove == null)
                    {
                        // means player resigned
                        state.UpdateGameOver("RESIGN");
                        break;
                    }
                    isMoveValid = Array.IndexOf(state.GetMovesList(), playerMove.ToString()) != -1;
                    if (!isMoveValid)
                    {
                        Console.WriteLine("move {0} is not valid - try again", playerMove.ToString());
                        Console.ReadLine();
                    }
                }

                if (playerMove == null)
                {
                    continue;
                }

                ExecuteMove(board, playerMove, state);

                // change the turn player and incrementing turn count:
                state.UpdateGameState(board);
                TurnPlayer = state.GetPlayer();
                //TurnPlayer = !TurnPlayer;
                TurnCount = state.GetTurnCount();
                moveCounter++;
                
            }
            board.Print();
            Console.WriteLine("GAME OVER BY " + state.GetResult());
        }
        

        public static bool SkipNext3Turns()
        {
            Console.WriteLine("would you like to skip the next 3 turnes? enter y for yes");
            string input = Console.ReadLine();
            return input == "y" || input == "Y";
        }

    }
}

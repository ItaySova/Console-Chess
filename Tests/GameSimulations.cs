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
            ChessGame testGame = new GameSimulations();
            string[] inputs = { "e2e4", "e7e5", "b1c3", "b8c6" };
            testGame.GameSimulation(inputs);
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

                // taking user input
                Move playerMove = ConvertInputToMove(InputMoves[moveCounter]);
                Console.WriteLine(playerMove.ToString());

                // testing the input for valid input - rulewise
                bool isMoveValid = IsMoveInAllPlayerMoves(playerMove);
                Console.WriteLine((isMoveValid ? "execute input:" : "invalid move"));
                // executing the input
                // change the turn player and incrementing turn count:
                Console.WriteLine("press enter to continue");
                Console.ReadLine();
                moveCounter++;
                if(moveCounter == InputMoves.Length)
                {
                    Console.WriteLine("ran out of automatic inputs");
                    gameOver = true;
                }
                Player = !Player;
                SetTurnPlayer(Player);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Chess.Tests
{
    internal class UserInputTest: ChessGame
    {
        public override void Play()
        {
            string[] inputsForUserInputFunction = new string[] { "a1a2", "b1b2", "bbbb", "A1a2", "", "H1H2", "a8a7", "h8h7" };
            Move[] moves = new Move[10];

            for (int i = 0; i < 6; i++)
            {
                moves[i] = UserTextToMoveInput(inputsForUserInputFunction);
            }
            Console.WriteLine();
            string MoveList = "";
            for (int i = 0;i < moves.Length; i++)
            {
                if (moves[i] != null)
                {
                    MoveList += moves[i] + ",";
                }
            }
            Console.WriteLine("the following list of moves: \n" +
                "a1a2,b1b2,bbbb,A1a2,'',H1H2,a8a7,h8h7\nyielded the following moves:\n" +MoveList);
        }

        // uset input
        public Move UserTextToMoveInput(string[] TestInputs)
        {            
            bool isValid = false;
            int counter = 0;
            Move PlayerMove = null;
            while (!isValid)
            {
                if (ValidateInput(TestInputs[counter]))
                {
                    isValid = true; // if the input is valid - convert to move and then check from Move class if it is legal
                    PlayerMove = ConvertInputToMove(TestInputs[counter]);
                    // add validation for the move itself - such as if the move is legal for the piece or if it leaves the player in check
                }
                // massage for invalid move - 
                if (!isValid)
                {
                    counter++;
                }
                else if (isValid)
                {
                    // if is valid remove input from the array
                    TestInputs[counter] = "";
                }
            }
            return PlayerMove;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Chess
{
    internal class ChessGame
    {
        Board board;
        bool TurnPlayer;
        bool IsGameOver;
        public ChessGame()
        {
            board = new Board();
            board.InitBoard();
            TurnPlayer = true;
            IsGameOver = false;
        }

        public void Play()
        {
            // printing
            board.Print();

            // taking user input
            string input = UserInput();

            // testing the input for valid input

            // executing the input
        }
        public string UserInput()
        {
            bool isValid = false;
            string input = "";
            while (!isValid)
            {
                Console.WriteLine("what will be your move, " + (TurnPlayer?"white":"black"));
                input = Console.ReadLine();
                if(ValidateInput(input))
                {
                    isValid = true;
                }
            }
            return input;
        }

        public bool ValidateInput(string input) // validate that the input itself is valid
        {
            // valid move input is A2A3 - 4 chars when 1,3 positions are numbers and 0,2 are letters
            if (input == null || input.Length != 4)
            {
                return false;
            }

            string ValidNumbers = "1,2,3,4,5,6,7,8";
            string ValidChars = "A,B,C,D,E,F,G,H,a,b,c,d,e,f,g,h";
            bool CharsValid = ValidChars.Contains(input[0] + "") && ValidChars.Contains(input[2] + "");
            bool NumbersValid = ValidNumbers.Contains(input[1] + "") && ValidNumbers.Contains(input[3] + "");
            return CharsValid && NumbersValid;
        }
    }
}

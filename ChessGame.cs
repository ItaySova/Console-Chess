using Console_Chess.Pieces;
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
        int TurnCount;
        public ChessGame()
        {
            board = new Board();
            board.InitBoard();
            TurnPlayer = true;
            IsGameOver = false;
            TurnCount = 0;
        }

        public virtual void Play() // virtual for tests
        {
            while (!IsGameOver)
            {
                // printing
                board.Print();

                // taking user input
                Move playerMove = UserInput();
                Console.WriteLine(playerMove.ToString());

                // testing the input for valid input - rulewise

                // executing the input

                // change the turn player and incrementing turn count:
                TurnPlayer = !TurnPlayer;
                TurnCount++;
            }
        }
        public virtual Move UserInput() // virtual for tests
        {
            bool isValid = false;
            string input = "";
            Move PlayerMove = null;
            while (!isValid)
            {
                Console.WriteLine("what will be your move, " + (TurnPlayer?"white":"black"));
                input = Console.ReadLine();
                if(ValidateInput(input))
                {
                    // if the input is valid - convert to move and proceed
                    PlayerMove = ConvertInputToMove(input);
                    Position FromPos = PlayerMove.GetFromPos();
                    Piece Chosen = board.GetPosition(FromPos);
                    // validation for choosing a piece which belong to turns player and not an empty square
                    isValid = Chosen != null && IsPieceBelongToPlayer(Chosen);
                    // add validation for the move itself - such as if the move is legal for the piece or if it leaves the player in check
                }
                // massage for invalid move - 
                if(!isValid)
                    Console.WriteLine("that was an invalid move - try again:");
            }
            return PlayerMove;
        }

        public bool ValidateInput(string input) // validate that the input itself is valid in regard to form (a-Z,1-8,a-Z,1-8)
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

        public Position ConvertInputToPosition(char letter, char number)
        {
            // the input already passed validation - letter will be column and number will be row
            string lettersDict = "abcdefgh";
            string numbersDict = "87654321";
            int col = lettersDict.IndexOf((letter+ "").ToLower());
            int row = numbersDict.IndexOf(number);

            Position pos = new Position(row, col);
            return pos;
        }

        public Move ConvertInputToMove(string input)
        {
            Position fromPos = ConvertInputToPosition(input[0], input[1]);
            Position toPose = ConvertInputToPosition(input[2], input[3]);

            return new Move(fromPos, toPose);
        }

        public bool IsPieceBelongToPlayer(Piece piece)
        {
            bool isPieceBelongToPlayer = piece.GetPlayer() == TurnPlayer;
            return isPieceBelongToPlayer;
        }
        // getters:
        public bool GetTurnPlayer()
        {
            return TurnPlayer;
        }
    }
}

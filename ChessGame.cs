using Console_Chess.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Console_Chess
{
    internal class ChessGame
    {
        protected Board board;
        protected bool TurnPlayer;
        protected bool IsGameOver;
        protected int TurnCount;
        public ChessGame()
        {
            board = new Board();
            board.InitBoard();
            TurnPlayer = true;
            IsGameOver = false;
            TurnCount = 0;
        }

        // constructor for testing different board stated
        public ChessGame(Board board)
        {
            this.board = board;
            TurnPlayer = true;
            IsGameOver=false;
            TurnCount = 0;
        }

        public virtual void Play() // virtual for tests
        {
            GameState state = new GameState();
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
                    playerMove = UserInput();
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
                // add validation that the move doesnt leave a player in check

                // executing the input
                ExecuteMove(board, playerMove,state);

                // change the turn player and incrementing turn count:
                state.UpdateGameState(board);
                TurnPlayer = state.GetPlayer();
                //TurnPlayer = !TurnPlayer;
                TurnCount++;
            }
            Console.WriteLine("GAME OVER BY " + state.GetResult());
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
                    Piece Chosen = board.GetPositionPiece(FromPos);
                    // validation for choosing a piece which belong to turns player and not an empty square
                    isValid = Chosen != null && IsPieceBelongToPlayer(Chosen);
                }
                // massage for invalid move - 
                if(!isValid)
                    Console.WriteLine("failed input validation check - either the from spot is empty or contains enemt piece:");
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

        //TODO - refactor 
        public bool IsMoveInAllPlayerMoves(Move move)
        {
            // helpers - get all pieces for player
            Piece[] allPlayerPieces = board.GetAllPiecesForPlayer(TurnPlayer);
            // get moves for all pieces
            string allPossibleMoves = board.GetAllMovesForPieces(allPlayerPieces);
            string[] movesArr = allPossibleMoves.Split(',');
            // then check if the move in list
            return Array.IndexOf(movesArr, move.ToString()) != -1;
            //return true;
        }

        // get ALL moves for optimisation and debugging purpose
        // TODO - replace the function with the one in gameState
        public string[] GetAllPlayerMoves()
        {
            Piece[] allPlayerPieces = board.GetAllPiecesForPlayer(TurnPlayer);
            // get moves for all pieces
            string allPossibleMoves = board.GetAllMovesForPieces(allPlayerPieces);
            string[] movesArr = allPossibleMoves.Split(',');
            return movesArr;
        }

        public virtual void GameSimulation(string[] inputs)
        {
            Console.WriteLine("virtual function for tests classes");
        }

        public bool isPlayerInCheck()
        {
            // get all opponent pieces
            Piece[] allOpponentPieces = board.GetAllPiecesForPlayer(!TurnPlayer);
            // get the current player king for its stored position 
            Piece currentPlayerKing = board.FindPlayerKing(TurnPlayer);
            // check if there is no king return false for tests 
            if(currentPlayerKing == null)
            {
                this.IsGameOver = true;
                return false;
            }
            // check for every enemy piece if at least one can capture the current king
            foreach (Piece piece in allOpponentPieces)
            {
                if(piece == null)
                {
                    continue;
                }
                if (piece.CanCaptureKing(board, currentPlayerKing))
                {
                    return true;
                }
            }
            return false;
        }
        // getters:
        public bool GetTurnPlayer()
        {
            return TurnPlayer;
        }

        public Board GetBoard()
        {
            return board;
        }

        public void SetTurnPlayer(bool turnPlayer)
        {
            this.TurnPlayer = turnPlayer;
        }

        public static bool IsMoveEnPassant(Board board, Move move, GameState state)
        {
            // any move that reach here is already gone through validations:
            string[] enPassantMoves = state.GetEnpassantMoves(board).Split(',');
            if(enPassantMoves.Length == 1)
            {
                return false;
            }
            if(Array.IndexOf(enPassantMoves, move.ToString()) != -1)
            {
                Console.WriteLine("en passant capture");
                return true;
            }
            return false;
        }
        public static bool ExecuteMove(Board board, Move move, GameState state)
        {
            // check if move is a special move - for now en passant
            if (IsMoveEnPassant(board, move, state))
            {
                Console.WriteLine("now remove the piece!");
                
                board.RemovePiece(state.GetEnPassantCapturePosition());
            }

            if (move == null)
            {
                Console.WriteLine("got empty move");
                return false;
            }
            Piece pieceCopy = board.RemovePiece(move.GetFromPos());
            Piece ToPosCopy = board.RemovePiece(move.GetToPosition());
            pieceCopy.SetPiecePosition(move.GetToPosition());
            pieceCopy.SetHasMoved(true);

            SendEnPassantToState(board, pieceCopy, move, state);


            return board.AddPiece(pieceCopy); // change later to reset 50 move rule
        }

        public static void SendEnPassantToState(Board board, Piece piece, Move move, GameState state)
        {
            // setting and removing en passant rights for the NEXT move
            if (piece is Pawn)
            {
                // checking the distance between from and to and if == 2 update middle position for en passant
                // distance between positions is only on the row difference
                int fromRow = move.GetFromPos().GetRow();
                int toRow = move.GetToPosition().GetRow();
                int rowDistance = fromRow > toRow ? fromRow - toRow : toRow - fromRow;
                if (rowDistance == 2)
                {
                    Position enPassantPos = new Position((fromRow + toRow) / 2, move.GetFromPos().GetColumn());
                    state.UpdateEnPassantPosition(board, enPassantPos, move.GetToPosition());
                }
                else
                {
                    state.UpdateEnPassantPosition(board, null, null);
                }
            }
            else
            {
                // set the en passant position to null since no pawn moved 
                state.UpdateEnPassantPosition(board, null, null);
            }
        }

    }
}


/* old play funciotn:
 *         public virtual void Play() // virtual for tests
        {
            while (!IsGameOver)
            {
                // printing
                board.Print();

                // taking user input
                Move playerMove = UserInput();
                Console.WriteLine(playerMove.ToString());

                // testing the input for valid input - rulewise
                bool isMoveValid = IsMoveInAllPlayerMoves(playerMove);
                if (!isMoveValid)
                {
                    Console.WriteLine("move is not in all moves list - try again");
                    continue;
                }
                // add validation that the move doesnt leave a player in check

                // executing the input
                ExecuteMove(playerMove);

                // change the turn player and incrementing turn count:
                TurnPlayer = !TurnPlayer;
                TurnCount++;
            }
        }
 
 */
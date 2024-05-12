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
            // remove later - log of moves for repeating in tests:
            string movesLog = "";
            GameState state = new GameState();
            DisplayRules();
            state.UpdateMoveslist(board);
            state.UpdateHistory(board);
            while (!state.GetGameOver())
            {
                // printing
                board.Print();
                Console.WriteLine(state.GetCheckStatus() ? "CHECK" : "");
                // check if the moves int the gamestate is identical to the legal moves done manually"                

                bool isMoveValid = false;
                Move playerMove = null;
                // taking user input
                while (!isMoveValid)
                {
                    playerMove = UserInput();
                    if(playerMove == null)
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
                movesLog += playerMove.ToString() + ',';

                // executing the input
                ExecuteMove(board, playerMove,state);

                // change the turn player and incrementing turn count:
                state.UpdateGameState(board);
                TurnPlayer = state.GetPlayer();
                //TurnPlayer = !TurnPlayer;
                TurnCount = state.GetTurnCount();
            }
            board.Print();
            Console.WriteLine("GAME OVER BY " + state.GetResult());
            if(state.GetResult() == "CHECKMATE")
            {
                Console.WriteLine("the {0} player won!", (TurnPlayer?"Black":"White"));
            }
            //fConsole.WriteLine("moves log:\n" + movesLog);
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
                if (input == "RESIGN")
                {
                    return null;
                }

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

        // function to be called only on invalid input
        public void HandleInvalidInput(Piece chosenPiece)
        {
            if (chosenPiece == null)
            {
                Console.WriteLine("the spot chosen int the start of the move is an empty spoy");
            } else
            {
                Console.WriteLine("the piece chosen is not the current player Piece");
            }
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
        

        public virtual void GameSimulation(string[] inputs)
        {
            Console.WriteLine("virtual function for tests classes");
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

        public static bool IsMoveCastling(Board board, Move move)
        {
            // will get the move and return if its castling of any type 
            // make sure that the origin position of the move contains a king:
            return move.ToString() == "e1g1" || move.ToString() == "e1c1" ||
                move.ToString() == "e8g8" || move.ToString() == "e8c8";
        }

        private static void SetCastlingRook(Board board, Move move)
        {
            Move rookMove;
            Position toPosition = null;
            Position rookOriginalPos = null;
            int MoveRow = move.GetFromPos().GetRow();
            if(move.ToString() == "e1g1" || move.ToString() == "e8g8")
            {
                // rook does h1f1 or rook does h8f8 (column h-> f or 7 - 5 
                toPosition = new Position(MoveRow, 5);
                rookOriginalPos = new Position(MoveRow, 7);
            }
            if (move.ToString() == "e1c1" || move.ToString() == "e8c8")
            {
                // rook does a1d1 or rook does a8d8 (column a-> d or 0 -> 3
                toPosition = new Position(MoveRow, 3);
                rookOriginalPos = new Position(MoveRow, 0);
            }

            rookMove = new Move(rookOriginalPos, toPosition);
            // remove the rook from its position and place it instead in its place
            Piece rookCopy = board.RemovePiece(rookOriginalPos);
            rookCopy.SetPiecePosition(toPosition);
            board.AddPiece(rookCopy);
        }
        public static bool ExecuteMove(Board board, Move move, GameState state)
        {
            // check if move is a special move - for now en passant
            if (IsMoveEnPassant(board, move, state))
            {
                //Console.WriteLine("now remove the piece!");
                
                board.RemovePiece(state.GetEnPassantCapturePosition());
            }

            if (move == null)
            {
                Console.WriteLine("got empty move");
                return false;
            }
            // normal move - remove the piece in from, remove the piece to,
            Piece pieceCopy = board.RemovePiece(move.GetFromPos());
            Piece ToPosCopy = board.RemovePiece(move.GetToPosition());

            // if a piece is captured - clean all history in state:
            if(ToPosCopy != null || pieceCopy is Pawn)
            {
                state.SetTurnCount(0);
                state.ClearHistory();
            }

            // check if a move is castling
            if (IsMoveCastling(board, move) && pieceCopy is King)
            {
                //Console.WriteLine("castling execute: " + move.ToString());

                SetCastlingRook(board, move);
            }
                        
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

        public virtual Move ValidateAutomationInput(string input)
        {
            bool isValid = false;
            Move PlayerMove = null;
            while (!isValid)
            {
                if(input == "RESIGN")
                {
                    return null;
                }
                if (ValidateInput(input))
                {
                    PlayerMove = Move.ConvertStringToMove(input);
                    Position FromPos = PlayerMove.GetFromPos();
                    Piece Chosen = board.GetPositionPiece(FromPos);
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

        public void DisplayRules()
        {
            Console.WriteLine("hello and welcome to my chess console game!\nhere are the following rules:\n" +
                "1. at the begining of the turn, the turn player choose a move in the format of 1 letter for the column and a number,\nrepresenting" +
                " the piece on the board he choose to make a move with, and then 1 letter and one number for representing the place the piece" +
                " moves to. for example 'a2a4' or 'e2e4.'" +
                "a player must choose its own pieces.\n2. a player can forfeit the match by entering 'RESIGN' instead of his move, resulting" +
                "in a loss.\n3. if a player want to look againg at those rules - type 'RULES' instead of the move");
        }

    }
}


/* old play funciotn:
  
            // remove later - log of moves for repeating in tests:
            string movesLog = "";
            GameState state = new GameState();
            state.UpdateMoveslist(board);
            while (!state.GetGameOver())
            {
                string[] MovesAvailable = state.GetAllPossibleMoves(board);
                // printing
                board.Print();
                Console.WriteLine(state.GetCheckStatus() ? "CHECK" : "");
                string[] LegalMoves = state.GetLegalMoves(board, MovesAvailable);
                // check if the moves int the gamestate is identical to the legal moves done manually"
                Console.WriteLine("manuall legal moves == state.getMoves: " + (state.GetMovesList().ToString() == LegalMoves.ToString()));
                Console.WriteLine("turn number: " + state.GetTurnCount());
                bool isMoveValid = false;
                Move playerMove = null;
                // taking user input
                while (!isMoveValid)
                {
                    playerMove = UserInput();
                    if(playerMove == null)
                    {
                        // means player resigned
                        state.UpdateGameOver("RESIGN");
                        break;
                    }

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
                if(playerMove == null)
                {
                    continue;
                }
                movesLog += playerMove.ToString() + ',';

                // executing the input
                ExecuteMove(board, playerMove,state);

                // change the turn player and incrementing turn count:
                state.UpdateGameState(board);
                TurnPlayer = state.GetPlayer();
                //TurnPlayer = !TurnPlayer;
                TurnCount++;
            }
            Console.WriteLine("GAME OVER BY " + state.GetResult());
            Console.WriteLine("moves log:\n" + movesLog);
 */
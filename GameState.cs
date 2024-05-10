using Console_Chess.Pieces;
using Console_Chess.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Chess
{
    internal class GameState // will be created once every start of the game in the Play function
    {
        bool TurnPlayer; // 
        bool GameOver; // is the game over
        bool IsCheck; // check for turn player
        int TurnCount; //
        string[] MovesList;
        string BoardHistoryFirstTime;
        string BoardHistorySecondTime;
        string BoardHistoryThirdTime;
        string Result = "";
        Position EnPassantPosition = null;
        Position WestToEnpassantPawn;
        Position EastToEnpassantPawn;

        public GameState(bool turnPlayer, bool gameOver, bool isCheck, int turnCount,
            string boardHistoryFirstTime, string boardHistorySecondTime,
            string boardHistoryThirdTime)
        {
            TurnPlayer = turnPlayer;
            GameOver = gameOver;
            IsCheck = isCheck;
            TurnCount = turnCount;
            
            BoardHistoryFirstTime = boardHistoryFirstTime;
            BoardHistorySecondTime = boardHistorySecondTime;
            BoardHistoryThirdTime = boardHistoryThirdTime;
        }

        public GameState(): this(true,false,false,0,"","","")
        {          
        }

        public void SetTurnCount(int number)
        {
            TurnCount = number;
        }

        public int GetTurnCount()
        {
            return TurnCount;
        }
        public bool IsPlayerInCheck(Board board)
        {
            // get all opponent pieces
            Piece[] allOpponentPieces = board.GetAllPiecesForPlayer(!TurnPlayer);
            // get the current player king for its stored position 
            Piece currentPlayerKing = board.FindPlayerKing(TurnPlayer);
            
            // check for every enemy piece if at least one can capture the current king
            foreach (Piece piece in allOpponentPieces)
            {
                if (piece == null)
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

        public Position GetEnPassantPosition()
        {
            return EnPassantPosition;
        }

        public void UpdateEnPassantPosition(Board board, Position EnPassantPos, Position enPassantPawnPosition)
        {
            //Console.WriteLine("en passant updated in position " + EnPassantPos);
            EnPassantPosition = EnPassantPos;
            if (EnPassantPos == null)
            {
                WestToEnpassantPawn = null;
                EastToEnpassantPawn = null;
            } else
            {
                WestToEnpassantPawn = Direction.PositionAfterStepInDirection(enPassantPawnPosition, Direction.West);
                EastToEnpassantPawn = Direction.PositionAfterStepInDirection(enPassantPawnPosition, Direction.East);
                // if either position is outside the board - change them to null
                if (!Board.IsPositionInBoard(WestToEnpassantPawn))
                {
                    WestToEnpassantPawn = null;
                }
                if (!Board.IsPositionInBoard(EastToEnpassantPawn))
                {
                    EastToEnpassantPawn = null;
                }
            }
        }
        public bool GetPlayer()
        {
            return TurnPlayer;
        }

        public bool GetCheckStatus()
        {
            return IsCheck;
        }
        public bool IsCheckMate(Board board)
        {
            // first implement Checkmate game over only!
            return false;
        }

        public void UpdateGameOver(string input)
        {
            // if a player resigned on its own
            if(input == "RESIGN")
            {
                GameOver = true;
                Result = "Win by resign";
            }
            // check if the player has any legal moves left to do
            // helper - get legal moves
        }

        public bool GetGameOver()
        {
            return GameOver;
        }

        public string GetResult()
        {
            return Result;
        }

        // gets a list of all possible moves and return a list of only legal moves - which dont leave the player in check 
        // is called in the begining of the turn so the player is updated for IsCheck
        public string[] GetLegalMoves(Board board, string[] allPlayerMoves)
        {
            string LegalMovesList = "";
            for (int moveCount = 0;  moveCount < allPlayerMoves.Length; moveCount++)
            {
                // for every valid move check if after executed leaves player in check
                if (allPlayerMoves[moveCount] != "")
                {
                    Move playerMoveAsMove = Move.ConvertStringToMove(allPlayerMoves[moveCount]);
                    Board copy = board.Copy();
                    GameState stateCopy = this.Copy();
                    // small test: 
                    //Console.WriteLine(board.ToString() == copy.ToString());
                    //NewGameLogicTest.ExecuteMove(copy, playerMoveAsMove);
                    ChessGame.ExecuteMove(copy, playerMoveAsMove, stateCopy);
                    bool IsCopyCheck = IsPlayerInCheck(copy); // update 
                    if (IsCopyCheck == false)
                    {
                        LegalMovesList += allPlayerMoves[moveCount] + ",";
                    }
                }
            }

            return LegalMovesList.Split(',');
        }

        public string[] GetAllPossibleMoves(Board board)
        {
            Piece[] allPlayerPieces = board.GetAllPiecesForPlayer(TurnPlayer);
            // get moves for all pieces
            string allPossibleMoves = board.GetAllMovesForPieces(allPlayerPieces);
            // append en passant manually if not null 
            string EnPassantMoves = GetEnpassantMoves(board);
            string CastlingMoves = GetCastlingMoves(board);
            allPossibleMoves += "," + EnPassantMoves;
            allPossibleMoves += "," + CastlingMoves;
            string[] movesArr = allPossibleMoves.Split(',');
            return movesArr;
        }

        public string GetEnpassantMoves(Board board)
        {
            string move = "";
            Piece[] pawns = board.GetAllPawnsForPlayer(TurnPlayer);
            if(EnPassantPosition != null)
            {
                if (WestToEnpassantPawn != null && Board.IsPositionInBoard(WestToEnpassantPawn))
                {
                    Piece pieceWestToEN = board.GetPositionPiece(WestToEnpassantPawn);
                    // go through the player pawns and check if they are equal to pieceWestToEN
                    for (int i = 0; i < pawns.Length && pieceWestToEN != null; i++)
                    {
                        if (pawns[i] != null && pawns[i].Equals(pieceWestToEN))
                        {
                            move += WestToEnpassantPawn.ToString() + EnPassantPosition.ToString() + ",";
                        }
                    }
                }
                if (EastToEnpassantPawn != null && Board.IsPositionInBoard(EastToEnpassantPawn))
                {
                    Piece pieceEastToEN = board.GetPositionPiece(EastToEnpassantPawn);
                    for (int i = 0; i < pawns.Length && pieceEastToEN != null; i++)
                    {
                        if (pawns[i] != null && pawns[i].Equals(pieceEastToEN))
                        {
                            move += EastToEnpassantPawn.ToString() + EnPassantPosition.ToString() + ",";
                        }
                    }
                }

            }

            return move;
        }

        public void HandlePawnPromotion(Board board, Piece pawnToPromote)
        {
            string choice = "";
            bool validChoice = false;
            while (!validChoice)
            {
                Console.WriteLine("Choose a piece to replace the pawn:\nq for queen\nb for bishop\nr for rook\nn for knight:");
                choice = Console.ReadLine();
                switch (choice)
                {
                    case "q":
                    case "b":
                    case "r":
                    case "n":
                        validChoice = true;
                        break;
                    default:
                        Console.WriteLine("not a valid choice please try again");
                        break;
                }
            }

            board.RemovePiece(pawnToPromote.GetPosition());

            switch (choice)
            {
                case "q":
                    //pawnToPromote = new Queen(GetPlayer(), pawnToPromote.GetPosition());                    
                    board.AddPiece(new Queen(GetPlayer(), pawnToPromote.GetPosition()));
                    break;
                case "b":
                    board.AddPiece(new Bishop(GetPlayer(), pawnToPromote.GetPosition()));
                    break;
                case "r":
                    board.AddPiece(new Rook(GetPlayer(), pawnToPromote.GetPosition()));
                    break;
                case "n":
                    board.AddPiece(new Knight(GetPlayer(), pawnToPromote.GetPosition()));
                    break;
                default:
                    break;
            }
            board.GetPositionPiece(pawnToPromote.GetPosition()).SetHasMoved(true);
        }
        public string GetCastlingMoves(Board board)
        {
            // castling is either from col e to col g ("king side) or col e to col c (queen side)
            string moves = "";
            // if a player is in check - return empty string:
            if (IsPlayerInCheck(board))
            {
                return moves;
            }
            // 2 helpers - get castle from the king side - e8g8 for black or e1g1 for white,
            // and queen side e8c8 for black and e1c1 for whites
            if (CanCastleKingSide(board))
            {
                moves += TurnPlayer ? "e1g1" : "e8g8";
                moves += ',';
            }
            if (CanCastleQueenSide(board))
            {
                moves += TurnPlayer ? "e1c1" : "e8c8";
                moves += ',';
            }
            return moves;
        }

        // helpers for the right to castle at the current moment
        private bool CanCastleKingSide(Board board)
        {
            // turn player = true => white player => row is 7, otherwise 0. also used for the rook pos
            int KingRow = TurnPlayer ? 7 : 0;
            Position AssumedKingPos = new Position(KingRow, 4);
            Position AssumedRookPos = new Position(KingRow, 7);
            bool unMoved = IskingAndRookUnmoved(board, AssumedKingPos, AssumedRookPos);
            //bool IsSpacesBetweenKingAndRookEmpty = IsSpacesBetweenPositionsEmpty(board, AssumedKingPos, AssumedRookPos);
            return unMoved && IsSpacesBetweenPositionsEmpty(board, AssumedKingPos, AssumedRookPos);
        }

        private bool CanCastleQueenSide(Board board)
        {
            int KingRow = TurnPlayer ? 7 : 0;
            Position AssumedKingPos = new Position(KingRow, 4);
            Position AssumedRookPos = new Position(KingRow, 0);
            bool unMoved = IskingAndRookUnmoved(board, AssumedKingPos, AssumedRookPos);
            //bool IsSpacesBetweenKingAndRookEmpty = IsSpacesBetweenPositionsEmpty(board, AssumedKingPos, AssumedRookPos);
            return unMoved && IsSpacesBetweenPositionsEmpty(board, AssumedKingPos, AssumedRookPos);
        }

        public bool IskingAndRookUnmoved(Board board, Position kingPos, Position rookPos)
        {
            Piece king = board.GetPositionPiece(kingPos);
            Piece rook = board.GetPositionPiece(rookPos);
            // if both position contain a piece - and both pieces has not moved - the pieces is necessarly a king and a rook
            return king != null && rook != null &&
                king.GetHasMoved() == false && rook.GetHasMoved() == false;
        }

        // will check for 2 positions in the same row - not including the given positions
        private bool IsSpacesBetweenPositionsEmpty(Board board, Position from, Position to)
        {
            int ColDelta = from.GetColumn() - to.GetColumn();
            if (ColDelta == 0) return false; // the same position was given twice
            // if the delta is negative - we step to the east - otherwise to the west
            Direction FromToDirection = ColDelta > 0 ? Direction.West : Direction.East;
            Position nextPos = Direction.PositionAfterStepInDirection(from, FromToDirection); // set next position
            while (!nextPos.Equals(to))
            {
                // if there is a non empty position -  return false
                if (board.GetPositionPiece(nextPos) != null) return false;
                // check if the current position is threatened - copy the board and check for IsPlayerInCheck
                Board copy = board.Copy();
                Piece copyKing = copy.RemovePiece(from);
                copyKing.SetPiecePosition(nextPos);
                copy.AddPiece(copyKing);
                bool IsCopyCheck = IsPlayerInCheck(copy);
                if (IsCopyCheck)
                {
                    return false;
                }

                nextPos = Direction.PositionAfterStepInDirection(nextPos,FromToDirection);
            }
            return true;
        }

        public GameState Copy()
        {
            GameState copy = new GameState();
            copy.TurnPlayer = this.TurnPlayer; // 
            copy.GameOver = this.GameOver; // is the game over
            copy.IsCheck = this.IsCheck; // check for turn player
            copy.TurnCount = this.TurnCount; //
            copy.MovesList = this.MovesList;
            copy.BoardHistoryFirstTime = this.BoardHistoryFirstTime;
            copy.BoardHistorySecondTime = this.BoardHistorySecondTime;
            copy.BoardHistoryThirdTime = this.BoardHistoryThirdTime;
            copy.Result = this.Result;
            copy.EnPassantPosition = this.EnPassantPosition;
            copy.WestToEnpassantPawn = this.WestToEnpassantPawn;
            copy.EastToEnpassantPawn = this.EastToEnpassantPawn;

            return copy;
        }
        public void UpdateGameState(Board board)
        {
            // before anything check if there is a need to promote a pawn and do it
            Piece[] CurrentPlayerPawns = board.GetAllPawnsForPlayer(TurnPlayer);
            for (int i= 0; i<CurrentPlayerPawns.Length; i++)
            {
                if(CurrentPlayerPawns[i] != null && (CurrentPlayerPawns[i].GetPosition().GetRow() == 7 || CurrentPlayerPawns[i].GetPosition().GetRow() == 0))
                {
                    HandlePawnPromotion(board, CurrentPlayerPawns[i]);
                }
            }
            // update history from function - check for threefoldrepetition
            if (UpdateHistory(board))
            {
                GameOver = true;
                ComputeResult("DRAW BY THREEFOLD REPETITION");
                return;
            }
            TurnPlayer = !TurnPlayer;
            // update if the player of the next turn is in check
            IsCheck = IsPlayerInCheck(board);
            // update if the game is over
            // get all possibleMoves for next player: 
            string[] PossibleMoves = GetAllPossibleMoves(board);
            // extract all the legal ones
            MovesList = GetLegalMoves(board, PossibleMoves);
            // in this case no legal moves left - the game is over
            if(MovesList.Length == 1 && MovesList[0] == "")
            {
                GameOver = true;
                ComputeResult();
            }
            if(IsFiftyMovesRule())
            {
                GameOver = true;
                ComputeResult("Fifty-Moves-Rule");
            }
            TurnCount++;
        }

        // update legal moves for the begining of play
        public void UpdateMoveslist(Board board)
        {
            string[] PossibleMoves = GetAllPossibleMoves(board);
            MovesList = GetLegalMoves(board, PossibleMoves);
        }

        public string[] GetMovesList()
        {
            return MovesList;
        }

        // function that will return the position of the Piece that can capture the en passant:
        public Position GetEnPassantCapturePosition()
        {
            if (WestToEnpassantPawn != null)
            {
                return Direction.PositionAfterStepInDirection(WestToEnpassantPawn, Direction.East);
            }
            if( EastToEnpassantPawn != null)
            {
                return Direction.PositionAfterStepInDirection(EastToEnpassantPawn, Direction.West);
            }
            return null;
        }

        public bool UpdateHistory(Board board)
        {
            string HistorySnap = HistoryBuilder.SnapshotBoard(board,this);
            if (BoardHistoryFirstTime.IndexOf(HistorySnap) == -1)
            {
                BoardHistoryFirstTime += HistorySnap + "|";
            } else if(BoardHistorySecondTime.IndexOf(HistorySnap) == -1)
            {
                BoardHistorySecondTime += HistorySnap + "|";
            } else
            {
                return true;
            }
            return false;
        }

        public void ClearHistory()
        {
            BoardHistoryFirstTime = "";
            BoardHistorySecondTime = "";
        }

        public bool IsFiftyMovesRule()
        {
            return TurnCount == 50;
        }

        public bool IsInsufficientMaterial(Board board)
        {
            // use a couple of helper functions:
            return false;
        }

        // overload for case of having no legal moves left
        public void ComputeResult()
        {
            // is called in the end of the turn - if the next player is in check - its a win for !player
            if (IsCheck)
            {
                Console.WriteLine("the player:" + (TurnPlayer ? "black" : "white") + " has won the match");
                Result = "CHECKMATE";
            } else
            {
                Result = "Stalemate";
            }
        }

        public void ComputeResult(string reason)
        {
            Result = reason;
        }
    }
}

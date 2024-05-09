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

        public void UpdateEnPassantPosition(Board board, Position EnPassantPos, Position enPassantPawnPosition)
        {
            Console.WriteLine("en passant updated in position " + EnPassantPos);
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

        public void UpdateGameOver(Board board, string[] allPlayerMoves)
        {
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
                    // small test: 
                    //Console.WriteLine(board.ToString() == copy.ToString());
                    NewGameLogicTest.ExecuteMove(copy, playerMoveAsMove);
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
            allPossibleMoves += "," + EnPassantMoves;
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


        public string GetCastlingMoves(Board board)
        {
            string moves = "";
            // 2 helpers - get castle from the king side - e8g8 for black or e1h1 for white,
            // and queen side e8c8 for black and e1c1 for whites

            return moves;
        }

        // helpers for the right to castle a certain way at any point from now in in the game
        private bool CanCastleKingSide(Board board)
        {
            // turn player = true => white player => row is 7, otherwise 0. also used for the rook pos
            int KingRow = TurnPlayer ? 7 : 0;
            Position AssumedKingPos = new Position(KingRow, 4);
            Position AssumedRookPos = new Position(KingRow, 7);
            bool unMoved = IskingAndRookUnmoved(board, AssumedKingPos, AssumedRookPos);
            bool IsSpacesBetweenKingAndRookEmpty = IsSpacesBetweenPositionsEmpty(board, AssumedKingPos, AssumedRookPos);
            return unMoved && IsSpacesBetweenKingAndRookEmpty;
        }

        private bool CanCastleQueenSide(Board board)
        {
            int KingRow = TurnPlayer ? 7 : 0;
            Position AssumedKingPos = new Position(KingRow, 4);
            Position AssumedRookPos = new Position(KingRow, 0);
            bool unMoved = IskingAndRookUnmoved(board, AssumedKingPos, AssumedRookPos);
            bool IsSpacesBetweenKingAndRookEmpty = IsSpacesBetweenPositionsEmpty(board, AssumedKingPos, AssumedRookPos);
            return unMoved && IsSpacesBetweenKingAndRookEmpty;
        }

        private bool IskingAndRookUnmoved(Board board, Position kingPos, Position rookPos)
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
            while (nextPos != to)
            {
                // if there is a non empty position -  return false
                if (board.GetPositionPiece(nextPos) != null) return false;

                nextPos = Direction.PositionAfterStepInDirection(nextPos,FromToDirection);
            }
            return true;
        }
        public void UpdateGameState(Board board)
        {
            TurnPlayer = !TurnPlayer;
            // update if the player of the next turn is in check
            IsCheck = IsPlayerInCheck(board);
            // update if the game is over
            // get all possibleMoves for next player: 
            string[] PossibleMoves = GetAllPossibleMoves(board);
            // extract all the legal ones
            MovesList = GetLegalMoves(board, PossibleMoves);
            if(MovesList.Length == 1 && MovesList[0] == "")
            {
                GameOver = true;
                ComputeResult();
            }
            TurnCount++;
        }

        // function that will return the position of the Piece that can be captured:
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

        public void ComputeResult()
        {
            // is called in the end of the turn - if the next player is in check - its a win for !player
            if (IsCheck)
            {
                Console.WriteLine("the player:" + (TurnPlayer ? "black" : "white") + " has won the match");
                Result = "CHECKMATE";
            }
        }
    }
}

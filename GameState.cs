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
            string[] movesArr = allPossibleMoves.Split(',');
            return movesArr;
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

        public void ComputeResult()
        {
            // is called in the end of the turn - if the next player is in check - its a win for !player
            if (IsCheck)
            {
                Console.WriteLine("the player:" + (TurnPlayer ? "white" : "black") + "has won the match");
                Result = "CHECKMATE";
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Chess.Pieces
{
    internal class Piece
    {
        protected bool IsWhite;
        protected bool HasMoved;
        protected Position PiecePosition;
        public Piece(bool player,Position pos) 
        {
            IsWhite = player;
            HasMoved = false;
            PiecePosition = pos;
        }

        public bool GetPlayer()
        {
            return IsWhite;
        }

        public bool GetHasMoved()
        {
            return HasMoved;
        }
        public void SetHasMoved(bool move)
        {
            HasMoved = move;
        }

        public Position GetPosition()
        {
            return PiecePosition;
        }

        public bool SetPiecePosition(Position pos)
        {
            if (pos == null ) return false;
            this.PiecePosition = pos;
            return true;
        }

        public virtual string GetMoves(Board board)
        {
            return "implementation from base class";
        }

        public string GetMovesInDir(Board board, Direction dir)
        {
            string MoveList = "";
            Position NextStep = Direction.PositionAfterStepInDirection(PiecePosition, dir);
            while(Board.IsPositionInBoard(NextStep))
            {
                // position is inside the board - if null add it to the list - else check if it is enemy 
                Piece pieceOnStepPosition = board.GetPositionPiece(NextStep);
                if(pieceOnStepPosition == null)
                {
                    MoveList += PiecePosition.ToString() + NextStep.ToString() + ",";
                    NextStep = Direction.PositionAfterStepInDirection(NextStep, dir);
                } 
                else if(pieceOnStepPosition.GetPlayer() != GetPlayer())
                {
                    MoveList += PiecePosition.ToString() + NextStep.ToString() + ",";
                    break;
                } 
                else
                {
                    // means there is a piece and its the same players piece:
                    break;
                }
            }
            return MoveList;
        }

        public virtual bool CanCaptureKing(Board board, King OpponentKing)
        {
            return true;
        }
        public override string ToString()
        {
            return IsWhite ? "w" : "b";
        }
    }
}

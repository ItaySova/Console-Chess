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

        public virtual bool CanCaptureKing(Board board, Piece opponentKing)
        {
            // create a move from the piece position and check if it is legal
            // if so - return true - otherwise false
            Position fromPosition = PiecePosition;
            Position toPosition = opponentKing.PiecePosition;
            Move checkMove = new Move(fromPosition, toPosition);
            // check only if the move is in the move list:
            string[] MoveArr = (GetMoves(board)).Split(',');
            // Todo - change for full legality check later

            return Array.IndexOf(MoveArr, checkMove.ToString()) != -1;
        }

        public virtual Piece Copy()
        {
            Piece pieceCopy = new Piece(this.IsWhite, this.PiecePosition);
            pieceCopy.HasMoved = this.HasMoved;
            return pieceCopy;
        }
        public override string ToString()
        {
            return IsWhite ? "w" : "b";
        }
    }
}

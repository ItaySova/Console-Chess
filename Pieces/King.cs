using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Chess.Pieces
{
    internal class King: Piece
    {
        private Direction[] directions;
        public King(bool player, Position pos) :base(player, pos) 
        {
            directions = new Direction[8];
            directions[0] = Direction.North;
            directions[1] = Direction.South;
            directions[2] = Direction.West;
            directions[3] = Direction.East;
            directions[4] = Direction.NorthWest;
            directions[5] = Direction.NorthEast;
            directions[6] = Direction.SouthWest;
            directions[7] = Direction.SouthEast;
        }

        // add promption later
        public override string GetMoves(Board board)
        {
            // loping over directions, creating a position for step, checking if insidethe board, if so add it
            string MoveList = "";
            Position StepInDir;
            for (int i = 0; i< directions.Length; i++)
            {
                StepInDir = Direction.PositionAfterStepInDirection(PiecePosition, directions[i]);
                if (Board.IsPositionInBoard(StepInDir))
                {
                    Piece PieceOnStepPosition = board.GetPositionPiece(StepInDir);
                    // if the position is null OR its occupied by an enemy - add the move
                    if(PieceOnStepPosition == null || PieceOnStepPosition.GetPlayer() != GetPlayer())
                    {
                        MoveList += PiecePosition.ToString() + StepInDir.ToString() + ",";
                    }
                }
            }
            return MoveList;
        }

        public override Piece Copy()
        {
            King copy = new King(this.IsWhite, this.PiecePosition);
            copy.HasMoved = this.HasMoved;
            return copy;
        }
        public override string ToString()
        {
            return base.ToString() + "k";
        }
    }
}

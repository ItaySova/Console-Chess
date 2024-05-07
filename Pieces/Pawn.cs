using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Chess.Pieces
{
    internal class Pawn:Piece
    {
        private Direction Forward;
        private Direction[] Diagonals;
        public Pawn(bool player, Position pos):base(player, pos) 
        {
            Diagonals = new Direction[2];
            if (player)
            {
                Forward = Direction.North;
                Diagonals[0] = Direction.NorthEast;
                Diagonals[1] = Direction.NorthWest;
            } else
            {
                Forward = Direction.South;
                Diagonals[0] = Direction.SouthEast;
                Diagonals[1] = Direction.SouthWest;
            }
        }

        public override string GetMoves(Board board)
        {
            string MovesList = "";
            MovesList += GetForwardMoves(board);
            return MovesList;
        }

        public string GetForwardMoves(Board board)
        {
            string MoveList = "";
            // compute 1 step position and check if its in the board borders:
            Position singleForwardStep = Direction.PositionAfterStepInDirection(PiecePosition, Forward);
            if(Board.IsPositionInBoard(singleForwardStep) && board.GetPositionPiece(singleForwardStep) == null)
            {
                MoveList += PiecePosition.ToString() + singleForwardStep.ToString() + ",";
            }
            Position twoStepPosition = Direction.PositionAfterStepInDirection(singleForwardStep, Forward);
            if (!HasMoved && Board.IsPositionInBoard(twoStepPosition)  && board.GetPositionPiece(twoStepPosition) == null)
            {
                MoveList += PiecePosition.ToString() + twoStepPosition.ToString();
            }
            return MoveList;
        }
        public override string ToString()
        {
            return base.ToString() + "p";
        }
    }
}

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

        // ad en passant and promotion
        public override string GetMoves(Board board)
        {
            string MovesList = "";
            MovesList += GetForwardMoves(board);
            MovesList += GetDiagonalMoves(board);
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
                MoveList += PiecePosition.ToString() + twoStepPosition.ToString() + ",";
            }
            return MoveList;
        }

        public string GetDiagonalMoves(Board board)
        {
            string MoveList = "";
            Position diagonalStep;
            for (int i = 0;i < Diagonals.Length; i++)
            {
                diagonalStep = Direction.PositionAfterStepInDirection(PiecePosition, Diagonals[i]);
                
                if (Board.IsPositionInBoard(diagonalStep)) // of the position is inside the board
                {
                    Piece pieceInDiagonal = board.GetPositionPiece(diagonalStep);
                    if (pieceInDiagonal != null && pieceInDiagonal.GetPlayer() != this.GetPlayer()) // if there is an oponent piece
                    {
                        MoveList += PiecePosition.ToString() + diagonalStep.ToString() + ",";
                    }                    
                }
            }

            return MoveList;
        }


        public override Piece Copy()
        {
            Pawn copy = new Pawn(this.IsWhite, this.PiecePosition);
            copy.HasMoved = this.HasMoved;
            return copy;
        }

        public override string ToString()
        {
            return base.ToString() + "p";
        }
    }
}

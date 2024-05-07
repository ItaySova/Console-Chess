using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Chess.Pieces
{
    internal class Queen:Piece
    {
        private Direction[] directions;
        public Queen(bool player, Position pos) :base(player, pos)
        {
            directions = new Direction[8];
            directions[0] = Direction.East;
            directions[1] = Direction.North;
            directions[2] = Direction.South;
            directions[3] = Direction.West;
            directions[4] = Direction.NorthEast;
            directions[5] = Direction.SouthEast;
            directions[6] = Direction.SouthWest;
            directions[7] = Direction.NorthWest;
        }

        public override string GetMoves(Board board)
        {
            string MoveList = "";
            for (int i = 0;i< directions.Length; i++)
            {
                MoveList += GetMovesInDir(board, directions[i]) + ",";
            }
            return MoveList;
        }
        public override string ToString()
        {
            return base.ToString() + "q";
        }
    }
}

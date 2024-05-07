using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Chess.Pieces
{
    internal class Bishop:Piece
    {
        private Direction[] directions; 
        public Bishop(bool player, Position pos) :base(player,pos) 
        {
            directions = new Direction[4];
            directions[0] = Direction.NorthEast;
            directions[1] = Direction.SouthWest;
            directions[2] = Direction.SouthEast;
            directions[3] = Direction.NorthWest;
        }

        public override string GetMoves(Board board)
        {
            string MoveList = "";
            for(int i = 0;i < directions.Length; i++)
            {
                MoveList += GetMovesInDir(board, directions[i]) + ",";
            }
            return MoveList;
        }

        public override string ToString()
        {
            return base.ToString() + "b";
        }
    }
}

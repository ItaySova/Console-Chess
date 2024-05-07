using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Chess.Pieces
{
    internal class Rook: Piece
    {
        private Direction[] directions;
        public Rook(bool player, Position pos) :base(player,pos) 
        {
            directions = new Direction[4];
            directions[0] = Direction.North;
            directions[1] = Direction.South;
            directions[2] = Direction.West;
            directions[3]= Direction.East;
        }

        public override string GetMoves(Board board)
        {
            string MoveList = "";
            //loop over direction - and for each of them get all the steps until his a border or piece
            for (int i = 0;i< directions.Length; i++)
            {
                MoveList += GetMovesInDir(board, directions[i]) + ",";
            }
            return MoveList;
        }

        public override string ToString()
        {
            return base.ToString() + "r";
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Chess.Pieces
{
    internal class Rook: Piece
    {
        public Rook(bool player, Position pos) :base(player,pos) { }

        public override string GetMoves(Board board)
        {
            return base.GetMoves(board);
        }

        public override string ToString()
        {
            return base.ToString() + "r";
        }
    }
}

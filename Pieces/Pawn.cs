using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Chess.Pieces
{
    internal class Pawn:Piece
    {
        public Pawn(bool player):base(player) { }

        public override string ToString()
        {
            return base.ToString() + "p";
        }
    }
}

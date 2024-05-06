using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Chess.Pieces
{
    internal class Knight :Piece
    {
        public Knight(bool player) : base(player) { }
        public override string ToString()
        {
            return base.ToString() + "n";
        }
    }
}

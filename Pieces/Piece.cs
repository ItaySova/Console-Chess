using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Chess.Pieces
{
    internal class Piece
    {
        public bool IsWhite;
        public Piece(bool player) 
        {
            IsWhite = player;        
        }

        public override string ToString()
        {
            return IsWhite ? "w" : "b";
        }
    }
}

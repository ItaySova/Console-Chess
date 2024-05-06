using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Chess.Pieces
{
    internal class Piece
    {
        protected bool IsWhite;
        protected bool HasMoved;
        protected Position PiecePosition;
        public Piece(bool player,Position pos) 
        {
            IsWhite = player;
            HasMoved = false;
            PiecePosition = pos;
        }

        public bool GetPlayer()
        {
            return IsWhite;
        }

        public bool GetHasMoved()
        {
            return HasMoved;
        }

        public virtual string GetMoves(Board board)
        {
            return "implementation from base class";
        }
        public override string ToString()
        {
            return IsWhite ? "w" : "b";
        }
    }
}

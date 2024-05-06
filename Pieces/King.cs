﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Chess.Pieces
{
    internal class King: Piece
    {
        public King(bool player, Position pos) :base(player, pos) { }

        public override string ToString()
        {
            return base.ToString() + "k";
        }
    }
}

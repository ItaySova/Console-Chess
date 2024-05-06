using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Chess
{
    internal class Move
    {
        private Position FromPosition;
        private Position ToPosition;

        public Move(Position fromPosition, Position toPosition)
        {
            FromPosition = fromPosition;
            ToPosition = toPosition;
        }

        public override string ToString()
        {
            return FromPosition.ToString() + ToPosition.ToString();
        }
    }
}

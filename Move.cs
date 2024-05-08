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

        public Position GetFromPos()
        {
            return FromPosition;
        }

        public Position GetToPosition() { return ToPosition; }

        public static Move ConvertStringToMove(string moveString)
        {
            Position fromPos = Position.ConvertCharsToPosition(moveString[0], moveString[1]);
            Position toPose = Position.ConvertCharsToPosition(moveString[2], moveString[3]);

            return new Move(fromPos, toPose);
        }

        public override bool Equals(object? obj)
        {
            Move other = obj as Move;
            if (other == null) return false;
            return other.FromPosition == this.FromPosition && other.ToPosition == this.ToPosition;
        }
        public override string ToString()
        {
            return FromPosition.ToString() + ToPosition.ToString();
        }
    }
}

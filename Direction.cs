using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Chess
{
    internal class Direction
    {
        int RowDelta;
        int ColumnDelta;

        public Direction(int rowDelta, int columnDelta)
        {
            RowDelta = rowDelta;
            ColumnDelta = columnDelta;
        }
        public static Direction North = new Direction(-1,0);
        public static Direction South = new Direction(1, 0);
        public static Direction East = new Direction(0, 1);
        public static Direction West = new Direction(0, -1);

        public static Direction NorthEast = new Direction(-1, 1);
        public static Direction NorthWest = new Direction(-1, -1);
        public static Direction SouthEast = new Direction(1, 1);
        public static Direction SouthWest = new Direction(1, -1);

        public static Position PositionAfterStepInDirection(Position pos,Direction direction)
        {
            int newRow = pos.GetRow() + direction.RowDelta;
            int newColumn = pos.GetColumn() + direction.ColumnDelta;

            return new Position(newRow, newColumn);
        }

    }
}

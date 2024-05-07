using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Chess
{
    internal class Position
    {
        private int Row;
        private int Column;

        public Position(int row, int column)
        {
            this.Row = row;
            this.Column = column;
        }

        public int GetRow() { return Row; }
        public int GetColumn() { return Column; }

        public int[] GetCoordinates()
        {
            return new[] { Row, Column };
        }

        public override bool Equals(object? obj)
        {
            Position other = obj as Position;
            if (other == null) return false;
            return this.Row == other.Row && this.Column == other.Column;
        }
        public override string ToString()
        {
            //  hgfedcba
            string lettersDict = "abcdefgh";
            string numbersDict = "87654321";
            char letter = lettersDict[Column];
            char number = numbersDict[Row];
            return "" + letter + number;
        }

        // consider removing!
        public static bool ISStringPosEqualToPos(string str, Position pos)
        {
            if(str.Length != 2)
            {
                return false;
            }
            // copy from convert input to position
            string lettersDict = "abcdefgh";
            string numbersDict = "87654321";
            int col = lettersDict.IndexOf((str[0] + "").ToLower());
            int row = numbersDict.IndexOf(str[1]);

            return row == pos.Row && col == pos.Column;
        }
    }
}

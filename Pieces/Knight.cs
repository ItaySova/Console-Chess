using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Chess.Pieces
{
    internal class Knight :Piece
    {
        private Direction[] directions;
        public Knight(bool player, Position pos) : base(player, pos) 
        {
            directions = new Direction[4];
            directions[0] = Direction.NorthEast;
            directions[1] = Direction.NorthWest;
            directions[2] = Direction.SouthEast;
            directions[3] = Direction.SouthWest;
        }

        public override string GetMoves(Board board)
        {
            // each move a knight can go consists of 2 steps in horizontal and one in vertical and vice versa
            // there are 2 moves in any diagonal direction - the move described above is equal to 1 step diagonal + horizontal/vertical 
            string MoveList = "";
            // attempt at hard coding
            Position[] positions = new Position[8];
            int piecePosRow = PiecePosition.GetRow();
            int piecePosCol = PiecePosition.GetColumn();
            // 2 steps north 1 sideways
            positions[0] = new Position(piecePosRow - 2, piecePosCol + 1);
            positions[1] = new Position(piecePosRow - 2, piecePosCol - 1);
            // two steps south 1 step sideways
            positions[2] = new Position(piecePosRow + 2, piecePosCol + 1);
            positions[3] = new Position(piecePosRow + 2, piecePosCol - 1);
            // two steps east one step vertical
            positions[4] = new Position(piecePosRow + 1, piecePosCol + 2);
            positions[5] = new Position(piecePosRow - 1, piecePosCol + 2);
            // 2 steps west one step vertical:
            positions[6] = new Position(piecePosRow + 1, piecePosCol - 2);
            positions[7] = new Position(piecePosRow - 1, piecePosCol - 2);

            // loop through positions and check if they are inside the board or containing the same player piece:
            for (int i = 0;i<positions.Length; i++)
            {
                if (Board.IsPositionInBoard(positions[i]))
                {
                    Piece pieceInStepPosition = board.GetPositionPiece(positions[i]);
                    if (pieceInStepPosition == null || pieceInStepPosition.GetPlayer() != GetPlayer())
                    {
                        MoveList += PiecePosition.ToString() + positions[i].ToString() + ",";
                    }
                }
            }
            return MoveList;
        }
        public override string ToString()
        {
            return base.ToString() + "n";
        }
    }
}

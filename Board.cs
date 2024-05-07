using Console_Chess.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Chess
{
    internal class Board
    {
        private Piece[,] Pieces;

        public Board()
        {
            Pieces = new Piece[8, 8];
        }

        // init standart board
        public void InitBoard()
        {
            // init black pieces:
            Pieces[0, 0] = new Rook(false, new Position(0, 0));
            Pieces[0, 1] = new Knight(false, new Position(0, 1));
            Pieces[0, 2] = new Bishop(false, new Position(0, 2));
            Pieces[0, 3] = new Queen(false, new Position(0, 3));
            Pieces[0, 4] = new King(false, new Position(0, 4));
            Pieces[0, 5] = new Bishop(false, new Position(0, 5));
            Pieces[0, 6] = new Knight(false, new Position(0, 6));
            Pieces[0, 7] = new Rook(false, new Position(0, 7));

            // init white pieces:
            Pieces[7, 0] = new Rook(true, new Position(7, 0));
            Pieces[7, 1] = new Knight(true, new Position(7, 1));
            Pieces[7, 2] = new Bishop(true, new Position(7, 2));
            Pieces[7, 3] = new Queen(true, new Position(7, 3));
            Pieces[7, 4] = new King(true, new Position(7, 4));
            Pieces[7, 5] = new Bishop(true, new Position(7, 5));
            Pieces[7, 6] = new Knight(true, new Position(7, 6));
            Pieces[7, 7] = new Rook(true, new Position(7, 7));

            // init both sides pawns:

            for(int col = 0; col < 8; col++)
            {
                Pieces[1, col] = new Pawn(false, new Position(1, col));
                Pieces[6, col] = new Pawn(true, new Position(6, col));
            }
        }

        public void Print()
        { //|br||bn||bb||bq||bk||bb||bn||br|
            Console.WriteLine("  | a|| b|| c|| d|| e|| f|| g|| h|");
            for (int row = 0; row < 8; row++)
            {
                Console.Write(8 - row + " ");
                for (int col = 0;col < 8; col++)
                {
                    if (Pieces[row, col] != null)
                    {
                        Console.Write("|"+Pieces[row, col]+"|");
                    }
                    else
                    {
                        Console.Write("|  |");
                    }                    
                }
                Console.WriteLine();
            }
        }
        // override print with string of moves.

        public void Print(string moves)
        { //|br||bn||bb||bq||bk||bb||bn||br|
            // converting the string of moves to an array of to positions
            string[] movesArr = moves.Split(',');
            for (int i = 0; i < movesArr.Length; i++)
            {
                movesArr[i] = movesArr[i].Substring(2);
            }
            // check in the loop if the position given is equal to the [r,c]
            Position currentPos;
            Console.WriteLine("  | a|| b|| c|| d|| e|| f|| g|| h|");
            for (int row = 0; row < 8; row++)
            {
                Console.Write(8 - row + " ");
                for (int col = 0; col < 8; col++)
                {
                    currentPos = new Position(row, col); // to check if it appears in the moves Arr
                    int IndexPosInArr = Array.IndexOf(movesArr, currentPos.ToString());
                    if (Pieces[row, col] != null)
                    {
                        Console.Write("|" + Pieces[row, col] + "|");
                    }
                    else if(IndexPosInArr != -1)
                    {
                        Console.Write("|^^|");
                    }
                    else
                    {
                        Console.Write("|  |");
                    }
                }
                Console.WriteLine();
            }
        }


        public Piece GetPositionPiece(Position pos)
        {
            return Pieces[pos.GetRow(),pos.GetColumn()];
        }

        public static bool IsPositionInBoard(Position pos)
        {
            return (pos.GetRow() >= 0 &&
                pos.GetColumn() >= 0 &&
                pos.GetRow() <=7 &&
                pos.GetColumn() <=7);
        }

        public Piece RemovePiece(Position pos)
        {
            Piece pieceToRemove = Pieces[pos.GetRow(), pos.GetColumn()];
            if (pieceToRemove != null)
            {
                Pieces[pos.GetRow(), pos.GetColumn()] = null;
            }
            return pieceToRemove;
        }

        // function for adding piece - mainly for tests
        public bool AddPiece(Piece piece)
        {
            // invalid piece to add to board
            if (piece == null  || piece.GetPosition() == null)
            {
                return false;
            }
            // check if position is occupied 
            Position targetPos = piece.GetPosition();
            Piece pieceAtTargetPos = GetPositionPiece(targetPos);
            if (pieceAtTargetPos != null)
            {
                return false;
            }
            // if position is empty:
            Pieces[targetPos.GetRow(), targetPos.GetColumn()] = piece;
            return true;
        }
    }
}

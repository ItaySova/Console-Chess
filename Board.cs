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
            Pieces[0, 0] = new Rook(false);
            Pieces[0, 1] = new Knight(false);
            Pieces[0, 2] = new Bishop(false);
            Pieces[0, 3] = new Queen(false);
            Pieces[0, 4] = new King(false);
            Pieces[0, 5] = new Bishop(false);
            Pieces[0, 6] = new Knight(false);
            Pieces[0, 7] = new Rook(false);

            // init white pieces:
            Pieces[7, 0] = new Rook(true);
            Pieces[7, 1] = new Knight(true);
            Pieces[7, 2] = new Bishop(true);
            Pieces[7, 3] = new Queen(true);
            Pieces[7, 4] = new King(true);
            Pieces[7, 5] = new Bishop(true);
            Pieces[7, 6] = new Knight(true);
            Pieces[7, 7] = new Rook(true);

            // init both sides pawns:

            for(int col = 0; col < 8; col++)
            {
                Pieces[1, col] = new Pawn(false);
                Pieces[6, col] = new Pawn(true);
            }
        }

        public void Print()
        {
            for(int row = 0; row < 8; row++)
            {
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
    }
}

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
                if (movesArr[i] != "")
                {
                    movesArr[i] = movesArr[i].Substring(2);
                }
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
                        if ((IndexPosInArr != -1))
                        {
                            Console.Write("(" + Pieces[row, col] + ")");
                            continue;
                        }
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

        public void Print(string[] movesArr)
        { //|br||bn||bb||bq||bk||bb||bn||br|
            // converting the string of moves to an array of to positions
            for (int i = 0; i < movesArr.Length; i++)
            {
                if (movesArr[i] != "")
                {
                    movesArr[i] = movesArr[i].Substring(2);
                }
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
                        if ((IndexPosInArr != -1))
                        {
                            Console.Write("(" + Pieces[row, col] + ")");
                            continue;
                        }
                        Console.Write("|" + Pieces[row, col] + "|");
                    }
                    else if (IndexPosInArr != -1)
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

        //get all pieces for player: 
        public Piece[] GetAllPiecesForPlayer(bool player)
        {
            // each player has maximum 16 pieces
            Piece[] playerPieces = new Piece[16];
            int PiecesIndex = 0;
            for(int row=0; row<8; row++)
            {
                for(int col=0; col<8; col++)
                {
                    if (Pieces[row,col] != null)
                    {
                        if(Pieces[row, col].GetPlayer() == player)
                        {
                            playerPieces[PiecesIndex] = Pieces[row, col];
                            PiecesIndex++;
                        }                        
                    }
                }
            }
            return playerPieces;
        }

        // function for En passant implementation:
        public Piece[] GetAllPawnsForPlayer(bool player)
        {
            Piece[] pawns = new Piece[8]; // will be maimum 8
            Piece[] allPieces = GetAllPiecesForPlayer(player);
            for(int i=0, pawnsIndex=0; i< allPieces.Length; i++)
            {
                if (allPieces[i] != null && allPieces[i] is Pawn)
                {
                    pawns[pawnsIndex] = allPieces[i];
                    pawnsIndex++;
                }
            }
            return pawns;
        }

        public string GetAllMovesForPieces(Piece[] playerPieces)
        {
            string UnitedMoveList = "";
            for (int i=0;i<playerPieces.Length; i++)
            {
                if (playerPieces[i] != null)
                {
                    UnitedMoveList += playerPieces[i].GetMoves(this);
                }
                // for debugging and some optimizations sake:
                else
                {
                    break;
                }
            }
            return UnitedMoveList;
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

        public Piece FindPlayerKing(bool player)
        {
            for (int r=0;r <8;r++)
            {
                for (int c=0;c<8;c++)
                {
                    if (Pieces[r,c] != null)
                    {
                        if(Pieces[r, c] is King && Pieces[r, c].GetPlayer() == player)
                        {
                            return Pieces[r, c];
                        }
                    }
                }
            }
            return null;
        }

        public Board Copy()
        {
            Board copy = new Board();
            for (int r=0;r<8;r++)
            {
                for(int c=0;c<8; c++)
                {
                    if (Pieces[r,c] != null)
                    {
                        copy.Pieces[r,c] = Pieces[r,c].Copy();
                    }
                }
            }
            return copy;
        }

        public override string ToString()
        {
            // will be represented as a 64 chars string where '_' represent emtpy space and each piece by its ToString
            string BoardRepresentation = "";
            for (int r=0;r<8; r++)
            {
                for(int col = 0; col < 8; col++)
                {
                    if (Pieces[r, col] != null)
                    {
                        BoardRepresentation += Pieces[r, col].ToString();
                        continue;
                    }
                    BoardRepresentation += "_";
                }
            }
            return BoardRepresentation;
        }
    }
}

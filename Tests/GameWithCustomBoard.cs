using Console_Chess.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Chess.Tests
{
    internal class GameWithCustomBoard: ChessGame
    {
        public GameWithCustomBoard(Board board):base(board) { }

        public static void CastlingBoard()
        {
            Board board = new Board();
            board.AddPiece(new King(false, new Position(0, 4)));
            board.AddPiece(new King(true, new Position(7, 4)));
            board.AddPiece(new Rook(false, new Position(0, 7)));
            board.AddPiece(new Rook(false, new Position(0, 0)));
            board.AddPiece(new Rook(true, new Position(7, 7)));
            board.AddPiece(new Rook(true, new Position(7, 0)));
            ChessGame game = new ChessGame(board);
            game.Play();
        }

        public static void EnPassantBoard()
        {
            Board board = new Board();
            // add pieces later
            ChessGame game = new ChessGame(board);
            game.Play();
        }

        public static void PawnPromotionBoard()
        {
            Board board = new Board();
            // add relevant pawns
            ChessGame game = new ChessGame(board);
            game.Play();
        }
    }
}

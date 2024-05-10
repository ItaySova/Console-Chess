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
            board.AddPiece(new King(false, new Position(0, 4)));
            board.AddPiece(new King(true, new Position(7, 4)));
            // add pieces later
            board.AddPiece(new Pawn(false, new Position(2, 0)));
            board.AddPiece(new Pawn(false, new Position(1, 1)));
            board.AddPiece(new Pawn(false, new Position(2, 2)));
            board.AddPiece(new Pawn(false, new Position(1, 3)));
            board.AddPiece(new Pawn(false, new Position(2, 4)));
            board.AddPiece(new Pawn(false, new Position(1, 5)));
            board.AddPiece(new Pawn(false, new Position(2, 6)));
            board.AddPiece(new Pawn(false, new Position(1, 7)));
            // white
            board.AddPiece(new Pawn(true, new Position(5, 0)));
            board.AddPiece(new Pawn(true, new Position(4, 1)));
            board.AddPiece(new Pawn(true, new Position(5, 2)));
            board.AddPiece(new Pawn(true, new Position(4, 3)));
            board.AddPiece(new Pawn(true, new Position(5, 4)));
            board.AddPiece(new Pawn(true, new Position(4, 5)));
            board.AddPiece(new Pawn(true, new Position(5, 6)));
            board.AddPiece(new Pawn(true, new Position(4, 7)));
            ChessGame game = new ChessGame(board);
            game.Play();
        }

        public static void PawnPromotionBoard()
        {
            Board board = new Board();
            board.AddPiece(new King(false, new Position(0, 4)));
            board.AddPiece(new King(true, new Position(7, 4)));
            // add relevant pawns
            board.AddPiece(new Pawn(false, new Position(4, 4)));
            board.AddPiece(new Pawn(false, new Position(4, 5)));
            board.AddPiece(new Pawn(true, new Position(3, 0)));
            board.AddPiece(new Pawn(true, new Position(3, 1)));
            ChessGame game = new ChessGame(board);
            game.Play();
        }

        public static void GameOfInsufficientMaterial()
        {
            Board board = new Board();
            board.AddPiece(new King(false, new Position(0, 4)));
            board.AddPiece(new King(true, new Position(7, 4)));
            ChessGame game = new ChessGame(board);
            game.Play();
        }
    }
}

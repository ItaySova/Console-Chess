using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Chess
{
    internal class HistoryBuilder
    {
        public static string SnapshotBoard(Board board, GameState state)
        {
            string Snapshot = "";
            // add board raw representation
            Snapshot += board.ToString();
            // add castling rights for both players as KSW KSB - king side white, king side black            
            //  QSW QSB - queen side white, queen side black
            Snapshot += CastlingRights(board, state);
            // add En Passant Position as tostring position or nothing 
            Snapshot += EnPassantRights(board, state);
            return Snapshot;
        }

        // helper for KSW KSB QSW QSB
        public static string CastlingRights(Board board, GameState state)
        {
            string whiteRights = "";
            string blackRights = "";
            // black castling rights - init position for king is 0,4 and rooks in 0,7 & 0,0
            bool IsKSB = state.IskingAndRookUnmoved(board, new Position(0, 4), new Position(0, 7));
            bool IsQSB = state.IskingAndRookUnmoved(board, new Position(0, 4), new Position(0, 0));
            if (IsKSB)
            {
                blackRights += "KSB";
            }
            if (IsQSB)
            {
                blackRights += "QSB";
            }

            bool IsKSW = state.IskingAndRookUnmoved(board, new Position(7, 4), new Position(7, 7));
            bool IsQSW = state.IskingAndRookUnmoved(board, new Position(7, 4), new Position(7, 0));

            if (IsKSW)
            {
                whiteRights += "KSW";
            }

            if (IsQSW)
            {
                whiteRights += "QSW";
            }
            return whiteRights + blackRights;
        }

        // add the current value of the en passant option - regardless if there is a pawn to catch it
        public static string EnPassantRights(Board board, GameState state)
        {
            string EnpassantRights = "";
            Position pos = state.GetEnPassantPosition();
            if (pos != null)
            {
                EnpassantRights += pos.ToString();
            }
            return EnpassantRights;
        }
    }
}

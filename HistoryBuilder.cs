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
            
            // add castling rights for both players as QSW QSB - queen side white, queen side black

            // add En Passant Position as tostring position or nothing 
            return Snapshot;
        }

        // helper for KSW KSB QSW QSB
        public static string CastlingRights(Board board, GameState state)
        {
            string whiteRights = "";
            string blackRights = "";

            return whiteRights + blackRights;
        }
    }
}

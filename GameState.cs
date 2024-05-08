using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Chess
{
    internal class GameState
    {
        bool TurnPlayer; // 
        bool GameOver; // is the game over
        bool IsCheck; // check for turn player
        int turnCount; //
        string BoardHistoryFirstTime;
        string BoardHistorySecondTime;
        string BoardHistoryThirdTime;

        public GameState() { }

        public bool IsGameOver(Board board)
        {
            // first implement Checkmate game over only!
            return false;
        }
    }
}

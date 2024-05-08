using Console_Chess.Tests;

namespace Console_Chess
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // tests
            if (args.Length != 0)
            {
                TestManager.TestDistribute(args[0]);                
            } else
            {
                ChessGame game = new ChessGame();
                game.Play();
            }                                    
        }
    }
}

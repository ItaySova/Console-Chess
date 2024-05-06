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
                InputTests.TestUserInputs();

                Console.WriteLine("press enter for next tests (UserInputtest):");
                Console.ReadLine();
                UserInputTest t2 = new UserInputTest();
                t2.Play();
            } else
            {
                ChessGame game = new ChessGame();
                game.Play();
            }                                    
        }
    }
}

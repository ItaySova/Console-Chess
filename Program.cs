using Console_Chess.Tests;

namespace Console_Chess
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // tests
            InputTests.TestUserInputs();

            Console.WriteLine("press enter for next tests (UserInputtest):");
            Console.ReadLine();
            UserInputTest t2 = new UserInputTest();
            t2.Play();

            //ChessGame game = new ChessGame();
            //game.Play();
        }
    }
}

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
                if (args[0] == "input")
                {
                    InputTests.TestUserInputs();

                    Console.WriteLine("press enter for next tests (UserInputtest):");
                    Console.ReadLine();
                    UserInputTest t2 = new UserInputTest();
                    t2.Play();
                } else if (args[0] == "board")
                {
                    //BoardTests.GetPositionTest();
                    Console.WriteLine("Board test!");
                    BoardTests tb1 = new BoardTests();
                    tb1.Play();
                } else if (args[0] == "pieces")
                {
                    Console.WriteLine("pieces get moves test - which would you like to check:");
                    string inpuy = Console.ReadLine();
                    switch (inpuy)
                    {
                        case "Queen":
                            Console.WriteLine("testing queen:");
                            break;
                        default:
                            Console.WriteLine("testing pawn:");
                            GetMovesTest.GetMovesTestPawm();
                            break;
                    }
                }
                
            } else
            {
                ChessGame game = new ChessGame();
                game.Play();
            }                                    
        }
    }
}

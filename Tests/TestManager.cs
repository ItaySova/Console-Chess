using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Chess.Tests
{
    internal class TestManager
    {
        public static void TestDistribute(string arg)
        {
            switch (arg)
            {
                case "input":
                    InputTests.TestUserInputs();

                    Console.WriteLine("press enter for next tests (UserInputtest):");
                    Console.ReadLine();
                    UserInputTest t2 = new UserInputTest();
                    t2.Play();
                    break;
                case "board":
                    //BoardTests.GetPositionTest();
                    Console.WriteLine("Board test!");
                    BoardTests tb1 = new BoardTests();
                    tb1.Play();
                    break;
                case "pieces":
                    PieceDestribute();
                    break;
                case "getallmoves":
                    Console.WriteLine("testing get all moves:");
                    GetAllMovesAndExecuteTest.GetAllMovesTest();
                    break;
                case "simulation":
                    SimulationDestribute();
                    break;
                case "logic":
                    Console.WriteLine("testing new logic, enter 1 for all position marked mode:");
                    string input = Console.ReadLine();
                    switch (input)
                    {
                        case "1":
                            NewGameLogicTest.NewLogic(true);
                            break;
                        default:
                            NewGameLogicTest.NewLogic();
                            break;
                    }
                    break;
                case "custom":                    
                    CustomDistrubute();
                    break;
                default:
                    Console.WriteLine("option chosen not available");
                    break;
            }
        }

        public static void PieceDestribute()
        {
            Console.WriteLine("pieces get moves test - which would you like to check:");
            string inpuy = Console.ReadLine();
            switch (inpuy)
            {
                case "queen":
                    Console.WriteLine("testing queen:");
                    GetMovesTest.GetMovesTestQueen();
                    break;
                case "bishop":
                    Console.WriteLine("testing bishop:");
                    GetMovesTest.GetMovesTestBishop();
                    break;
                case "king":
                    Console.WriteLine("testing king:");
                    GetMovesTest.GetMovesTestking();
                    break;
                case "rook":
                    Console.WriteLine("testing rook:");
                    GetMovesTest.GetMovesTestRook();
                    break;
                case "knight":
                    Console.WriteLine("testing knight:");
                    GetMovesTest.GetMovesTestKnight();
                    break;
                default:
                    Console.WriteLine("testing pawn:");
                    GetMovesTest.GetMovesTestPawm();
                    break;
            }
        }

        public static void SimulationDestribute()
        {
            Console.WriteLine("which simulation to run? \n1.random simulation\n2.quick checkmate\n3.inputsForQuickBlackCheckMate" +
                "\n4.EnPassantInputs\n5.CastlingInputs\n6.PawnPromotionInputs\n7.ThreeFoldInput\n8.ThreeFoldInput second\n9." +
                "Garry Kasparov VS Jorden van Forees\n10.fifty-moves-rule");
            string simulation = Console.ReadLine();
            switch (simulation)
            {
                case "1":
                case "2":
                case "3":
                case "4":
                case "5":
                case "6":
                case "7":
                case "8":
                case "9":
                case "10":
                    GameSimulations.Simulation1(simulation);
                    break;
                default:
                    Console.WriteLine("not valid running manuall");
                    GameSimulations.Simulation1("1");
                    break;
            }
        }

        public static void CustomDistrubute()
        {
            Console.WriteLine("choose from the following Boards:\n1.Castling\n2.EnPassant\n3.PawnPromotion");
            string option = Console.ReadLine();
            switch (option)
            {
                case "1":
                    GameWithCustomBoard.CastlingBoard();
                    break;
                case "2":
                    GameWithCustomBoard.EnPassantBoard();
                    break;
                case "3":
                    GameWithCustomBoard.PawnPromotionBoard();
                    break;
                default:
                    Console.WriteLine("not a valid option - starting a standart game: ");
                    ChessGame game = new ChessGame();
                    game.Play();
                    break;
            }
        }
    }
}

/* historic from program - remove at the end of project
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
                        case "queen":
                            Console.WriteLine("testing queen:");
                            GetMovesTest.GetMovesTestQueen();
                            break;
                        case "bishop":
                            Console.WriteLine("testing bishop:");
                            GetMovesTest.GetMovesTestBishop();
                            break;
                        case "king":
                            Console.WriteLine("testing king:");
                            GetMovesTest.GetMovesTestking();
                            break;
                        case "rook":
                            Console.WriteLine("testing rook:");
                            GetMovesTest.GetMovesTestRook();
                            break;
                        case "knight":
                            Console.WriteLine("testing knight:");
                            GetMovesTest.GetMovesTestKnight();
                            break;
                        default:
                            Console.WriteLine("testing pawn:");
                            GetMovesTest.GetMovesTestPawm();
                            break;
                    }
                } else if (args[0] == "getallmoves")
                {
                    Console.WriteLine("testing get all moves:");
                    GetAllMovesAndExecuteTest.GetAllMovesTest();
                } else if (args[0] == "simulation")
                {
                    Console.WriteLine("which simulation to run? ");
                    string simulation = Console.ReadLine();
                    switch (simulation)
                    {
                        case "1":
                            Console.WriteLine("running simulation number 1");
                            GameSimulations.Simulation1();
                            break;
                        default :
                            Console.WriteLine("invalid input - running simulation number 1");
                            GameSimulations.Simulation1();
                            break;
                    }
                } else if (args[0] == "logic")
                {
                    Console.WriteLine("testing new logic");
                    NewGameLogicTest.NewLogic();
                }
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Chess.Tests
{
    internal class InputTests
    {
        // for the user valid input only:
        public static void TestUserInputs()
        {
            ChessGame game = new ChessGame();
            string[] inputs = new string[10];
            inputs[0] = "";
            inputs[1] = "AAB2";
            inputs[2] = "aaaa";
            inputs[3] = "2a2B";
            inputs[4] = "22222";
            inputs[5] = "2222";
            inputs[6] = "s2j2";
            inputs[7] = "A9B0";
            inputs[8] = "a2b2";
            inputs[9] = "A2b3";
            for (int i = 0; i < inputs.Length; i++)
            {
                Console.WriteLine(inputs[i] + " test yielded: " + (game.ValidateInput(inputs[i])));
            }
           
        }
    }
}

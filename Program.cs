namespace Console_Chess
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Board board = new Board();
            board.InitBoard();
            board.Print();
        }
    }
}

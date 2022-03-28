using BlackjackMechanics.GameTools;
using BlackjackMechanics.Players;


namespace BlackjackGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Player player = new Player(10000);
            Game game = new Game(player);
            game.CreateGame(8);
            game.StartGame();
        }
    }
}
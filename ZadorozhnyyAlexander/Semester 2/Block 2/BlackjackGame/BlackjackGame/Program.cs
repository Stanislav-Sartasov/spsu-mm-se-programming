using BlackjackMechanics.GameTools;
using BlackjackMechanics.Players;
using BlackjackBots;

namespace BlackjackGame
{
    class Program
    {
        static void Main(string[] args)
        {
            int sum = 0;
            int loseAllBalanceGames = 0;
            for (int i = 0; i < 50000; i++)
            {
                PrimitiveManchetanStrategyBot bot = new PrimitiveManchetanStrategyBot(1000, 50);
                Game game = new Game(bot);
                game.CreateGame(8);
                game.StartGame();
                sum += (int)bot.Money;
                if (bot.Money <= 0)
                    loseAllBalanceGames++;
            }
            Console.WriteLine(sum / 50000);
            Console.WriteLine(loseAllBalanceGames);

            sum = 0;
            loseAllBalanceGames = 0;
            for (int i = 0; i < 50000; i++)
            {
                MartingaleBot bot = new MartingaleBot(1000, 50);
                Game game = new Game(bot);
                game.CreateGame(8);
                game.StartGame();
                sum += (int)bot.Money;
                if (bot.Money <= 0)
                    loseAllBalanceGames++;
            }
            Console.WriteLine(sum / 50000);
            Console.WriteLine(loseAllBalanceGames);

            sum = 0;
            loseAllBalanceGames = 0;
            for (int i = 0; i < 50000; i++)
            {
                OneThreeTwoSixBot bot = new OneThreeTwoSixBot(1000, 50);
                Game game = new Game(bot);
                game.CreateGame(8);
                game.StartGame();
                sum += (int)bot.Money;
                if (bot.Money <= 0)
                    loseAllBalanceGames++;
            }
            Console.WriteLine(sum / 50000);
            Console.WriteLine(loseAllBalanceGames);
        }
    }
}
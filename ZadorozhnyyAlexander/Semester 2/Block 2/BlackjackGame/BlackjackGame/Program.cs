using BlackjackMechanics.GameTools;
using BlackjackBots;

namespace BlackjackGame
{
    class Program
    {
        static void Main(string[] agrs)
        {
            Console.WriteLine("This program show what balance you'll have after play Blackjack" +
                "with one of the strategies: \n");

            int countGames = 50000;
            int startMoney = 10000;
            int startRate = 500;
            int sumAfterGame = 0;
            int countLoseAllBalanceGames = 0;

            // PrimitiveManchetanStrategyBot statistic

            for (int i = 0; i < countGames; i++)
            {
                PrimitiveManchetanStrategyBot bot = new PrimitiveManchetanStrategyBot(startMoney, startRate);
                Game game = new Game(bot);
                game.CreateGame(8);
                game.StartGame();
                sumAfterGame += (int)bot.Money;
                if (bot.Money <= 0)
                    countLoseAllBalanceGames++;
            }
            Console.WriteLine("1) PrimitiveManchetanStrategyBot statistic: ");
            Console.WriteLine($"Average winnings over 40 games ---> {sumAfterGame / 50000}/{startMoney} <--- Your start balance");
            Console.WriteLine("The number of games when a player loses his entire balance ---> " + countLoseAllBalanceGames);

            // MartingaleBot statistic
            sumAfterGame = 0;
            countLoseAllBalanceGames = 0;

            for (int i = 0; i < countGames; i++)
            {
                MartingaleBot bot = new MartingaleBot(startMoney, startRate);
                Game game = new Game(bot);
                game.CreateGame(8);
                game.StartGame();
                sumAfterGame += (int)bot.Money;
                if (bot.Money <= 0)
                    countLoseAllBalanceGames++;
            }
            Console.WriteLine("2) MartingaleBot statistic: ");
            Console.WriteLine($"Average winnings over 40 games ---> {sumAfterGame / 50000}/{startMoney} <--- Your start balance");
            Console.WriteLine("The number of games when a player loses his entire balance ---> " + countLoseAllBalanceGames);

            // OneThreeTwoSixBot statistic
            sumAfterGame = 0;
            countLoseAllBalanceGames = 0;

            for (int i = 0; i < countGames; i++)
            {
                OneThreeTwoSixBot bot = new OneThreeTwoSixBot(startMoney, startRate);
                Game game = new Game(bot);
                game.CreateGame(8);
                game.StartGame();
                sumAfterGame += (int)bot.Money;
                if (bot.Money <= 0)
                    countLoseAllBalanceGames++;
            }
            Console.WriteLine("3) OneThreeTwoSixBot statistic: ");
            Console.WriteLine($"Average winnings over 40 games ---> {sumAfterGame / 50000}/{startMoney} <--- Your start balance");
            Console.WriteLine("The number of games when a player loses his entire balance ---> " + countLoseAllBalanceGames);

            Console.WriteLine("<----------------------------------------------->");
            return;
        }
    }
}
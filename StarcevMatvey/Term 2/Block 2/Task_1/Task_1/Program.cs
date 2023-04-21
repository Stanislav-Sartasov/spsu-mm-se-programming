using BlackJack;
using Bots;
using static System.Console;

namespace Task_1
{
    class Progaram
    {
        static void Main(string[] args)
        {
            int counfOfGames = 100;

            int rndBotSum = 0;
            int dblBotSum = 0;
            int goodBotSum = 0;

            for (int _ = 0; _ < counfOfGames; _++)
            {
                RandomBot rndBot = new RandomBot("Rnd2000", 1000);
                DoubleBot dblBot = new DoubleBot("dbl2222", 1000);
                GoodBot goodBot = new GoodBot("NotVeryGoodBot", 1000);
                List<Player> players = new List<Player> { rndBot, dblBot, goodBot };
                Game blackJack = new Game(players);
                blackJack.StartGame(40);
                rndBotSum += rndBot.Balance;
                dblBotSum += dblBot.Balance;
                goodBotSum += goodBot.Balance;
            }

            ReadKey();

            Clear();

            WriteLine("Random bot: " + rndBotSum / counfOfGames);
            WriteLine("Double bot: " + dblBotSum / counfOfGames);
            WriteLine("HasStrategy bot: " + goodBotSum / counfOfGames);

            ReadKey();
        }
    }
}
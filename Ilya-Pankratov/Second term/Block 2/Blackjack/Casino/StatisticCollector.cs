using Player;
using Blackjack;

namespace Casino
{
    internal class StatisticCollector
    {
        private int gamePlayed;
        private int cashSum;
        private readonly int initialBotCash;
        public IPlayer Player { get; private set;}
        public string StrategyName { get; private set; }

        public StatisticCollector(IPlayer player, string strategyName)
        {
            Player = player;
            StrategyName = strategyName;
            cashSum = 0;
            gamePlayed = 0;
            initialBotCash = player.Cash;
        }

        public void Collect(int gameCounter)
        {
            int initialCash = Player.Cash;
            var gameTable = new GameTable(Player);

            for (int i = 0; i < gameCounter; i++)
            {
                gameTable.Play();
                gamePlayed++;
                cashSum += Player.Cash;

                // update

                gameTable.ResetGameDeck();
                Player.Cash = initialCash;
                Player.Flag = PlayerState.Play;
                Player.GamePlayed = 0;
            }
        }

        public void Show()
        {
            double average = (double)cashSum / gamePlayed;
            Console.WriteLine($"Strategy name: {StrategyName}\nInitial player cash: {initialBotCash}\n" +
                              $"GamePlayed: {gamePlayed}\nArithmetical mean: {average}\nResult: -{Math.Round(1 - average/initialBotCash, 4)*100}%\n");
        }
    }
}

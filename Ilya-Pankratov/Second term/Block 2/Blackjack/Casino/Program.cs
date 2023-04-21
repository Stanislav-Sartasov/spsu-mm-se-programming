using Bots;

namespace Casino
{ 
    public static class Program
    {
        static void Main(string[] agrs)
        {
            Console.WriteLine("The program demonstrates how different " +
                              "blackjack strategies affect a player's balance.\n");

            int gameCounter = 10000;

            // WinningStrategyBot statistic

            var winningBot = new WinningStrategyBot(x => x > 40, 1000);
            var collector = new StatisticCollector(winningBot, "Winning strategy");
            collector.Collect(gameCounter);
            collector.Show();

            // HiLowStrategyBot statistic

            var hiLowBot = new HiLowStrategyBot((x) => x > 40, 1000);
            collector = new StatisticCollector(hiLowBot, "HiLow strategy");
            collector.Collect(gameCounter);
            collector.Show();

            // HiOptIIStrategyBot statistic

            var hiOptIIBot = new HiOptIIStrategyBot((x) => x > 40, 1000);
            collector = new StatisticCollector(hiOptIIBot, "HiOptIIStrategyBot");
            collector.Collect(gameCounter);
            collector.Show();

            Console.WriteLine("That's all! Thank you!");
            return;
        }
    }
}
using GameTable.BotStructure;
using GameTable;

namespace Task4
{
    public class StartCasino
    {
        public IBot bot;
        private int startBalance = 0;
        private int counter = 0;
        private int minCash = 0;
        private int maxCash = 0;
        private int percentGain = 0;
        public StartCasino(IBot bot)
        {
            this.bot = bot;
        }

        public void Launch()
        {
            Roulette roulette = new Roulette(bot);
            startBalance = bot.Balance;
            counter = bot.GameCounter;

            roulette.Game();
            minCash = Math.Min(startBalance, bot.Balance);
            maxCash = Math.Max(startBalance, bot.Balance);
            bot.Gain = bot.Balance - startBalance;
            percentGain = bot.Gain / (startBalance / 100);
            
            PrintInfo();
        }

        public void PrintInfo() => Console.WriteLine($"Name bot: {bot.Name}, balance: {bot.Balance}, games won: {bot.WinsCounter} from {counter - bot.GameCounter - 1}.\nMinimum balance: {minCash},  maximum balance: {maxCash}, gain: {bot.Gain}, percentage of gain: {percentGain} %.");
    }
}

using Plugin;

namespace Task4
{
    public class  Program
    {
        public static void Main()
        {
            Console.WriteLine("This program shows the implementation of the Casino game European Roulette: ");

            int startBalance = 10000, startCounterGame = 40;
            string name = "";
            object[] launchArgs = { name,  startBalance, startCounterGame };
            var bots = new BotLoader().BotsLoader("../../../../Plugin/Plugins/", launchArgs );
            StartCasino casino;

            if (bots is not null)
            {
                for (int i = 0; i < bots.Count; i++)
                {
                    casino = new StartCasino(bots[i]);
                    casino.Launch();
                }
            }    

        }
    }
}
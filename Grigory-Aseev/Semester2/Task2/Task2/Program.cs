using Bots;

namespace Task2
{
    public class Program
    {
        public static void Main()
        {
            Console.WriteLine("this program is an imitation of blackjack with basic American rules.\nIt displays the statistics of 1000 games with 40 bot bets with different strategies on the console.\n\n");

            CasinoLaunch casino = new CasinoLaunch(new StandartBot(), "StandartBot");
            casino.StartCasino();
            casino.PrintInfo();

            casino = new CasinoLaunch(new PlusMinusBot(), "PlusMinusBot");
            casino.StartCasino();
            casino.PrintInfo();

            casino = new CasinoLaunch(new HalvesBot(), "HalvesBot");
            casino.StartCasino();
            casino.PrintInfo();
        }
    }
}
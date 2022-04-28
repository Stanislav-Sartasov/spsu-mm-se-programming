using PlayerStructure;
using GameTable;

namespace Task2
{
    internal class CasinoLaunch
    {
        private string name;
        private IPlayer player;
        private int minCash = 0, maxCash = 0, averageCash = 0;
        private int initialBalance;

        internal CasinoLaunch(IPlayer player, string name)
        {
            this.name = name;
            this.player = player;
        }

        internal void StartCasino()
        {
            initialBalance = player.Balance;
            PlayingField field = new PlayingField(player);

            for (int i = 0; i < 1000; i++)
            {
                field.Play();
                minCash = Math.Min(minCash, player.Balance);
                maxCash = Math.Max(maxCash, player.Balance);
                averageCash += player.Balance;

                field.UpdateShoe();
                player.ResetBalance(initialBalance);
                player.State = PlayerState.Playing;
                player.GameCounter = 40;
                player.StateOfShoe = ShoeState.Reset;
            }
        }

        internal void PrintInfo() => Console.WriteLine($"Bot name : {name}, initial balance : {initialBalance}.\nIn 1000 games with 40 stakes remaining balance in average : {averageCash / 1000}, in minimum : {minCash}, in maximum {maxCash}\n");
    }
}
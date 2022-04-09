using System;
using CasinoLib;

namespace CasinoLib
{
    public abstract class BotPlayer : Player
    {
        public abstract Int64 BaseBetValue { get; }
        public Int64 LastBetValue { get; protected set; }
        public bool IsLastWon { get; protected set; }

        public BotPlayer(Int64 money) : base(money) { }

        //Баланс бота меняется на тот, который в среднем станет через betCount ставок
        public void PlayAverageBetsForBot(Roulette game, Int64 betCount)
        {
            Int64 startBalance = Balance;
            Double averageBalance = 0;
            for (int i = 0; i < 4000; ++i)
            {
                Balance = startBalance;
                for (int j = 0; j < betCount && Balance > 0; ++j)
                {
                    PlaceBetAndPlay(game);
                }
                averageBalance += (Double)Balance / (Double)4000;
            }
            Balance = (Int64)averageBalance;
        }
    }
}

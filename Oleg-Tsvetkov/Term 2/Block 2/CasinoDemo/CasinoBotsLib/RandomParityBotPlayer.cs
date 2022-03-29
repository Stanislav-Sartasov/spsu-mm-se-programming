using CasinoLib;

namespace CasinoBotsLib
{
    //Бот, стратегия которого заключается в ставках на случайную четность
    //Все ставки одинаковы и равны 20, если баланс меньше 20 - ставка равна балансу.
    public class RandomParityBotPlayer : BotPlayer
    {
        public override Int64 BaseBetValue
        {
            get
            {
                return 20;
            }
        }
        public RandomParityBotPlayer(Int64 money) : base(money) { }
        
        public override bool PlaceBetAndPlay(Roulette game)
        {
            if (Balance == 0)
            {
                return false;
            }
            LastBetValue = Balance >= BaseBetValue ? BaseBetValue : Balance;
            Int64 betNumber = new Random().NextInt64(0, 1);
            RouletteBet bet = new(LastBetValue, BetType.Parity);
            bet.SetParity(betNumber);
            Int64 betResult = game.Play(bet);
            Balance += betResult;
            IsLastWon = betResult > 0;
            return true;
        }
    }
}
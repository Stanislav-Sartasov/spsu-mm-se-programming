using CasinoLib;

namespace CasinoBotsLib
{
    //Стратегия бота заключается в ставке одного и того же количества денег на одно и то же число до победы.
    //После победы бот снова выбирает случайное число.
    public class YoloBotPlayer : BotPlayer
    {
        public override Int64 BaseBetValue
        {
            get
            {
                return 25;
            }
        }
        public Int64 LastNumber { get; private set; }

        public YoloBotPlayer(Int64 money) : base(money)
        {
            IsLastWon = true;
            LastNumber = new Random().NextInt64(0, 36);
        }

        public override bool PlaceBetAndPlay(Roulette game)
        {
            if (Balance == 0)
            {
                return false;
            }
            if (IsLastWon)
            {
                LastNumber = new Random().NextInt64(0, 36);
            }
            Int64 betResult = game.Play(BetType.Single, LastNumber, BaseBetValue);
            Balance += betResult;
            IsLastWon = betResult > 0;
            return true;
        }
    }
}

using CasinoLib;

namespace CasinoBotsLib
{
    //Бот работает по стратегии Мартингейла. Делаются ставки только на красное или чёрное.
    //Если произошла победа, следующая ставка равна базовой, цвет ставки меняется на противоположный
    //Если проигрыш, след. ставка повышается в два раза, цвет не меняется. 
    //В реальности стратегия в среднем делает выигрыши частыми, но маленькими, проигрыши редкими, но большими
    //Бот хранит прошлую ставку, чтобы на её основании делать следующую
    public class MartingaleBotPlayer : BotPlayer
    {
        public override Int64 BaseBetValue {
            get 
            {
                return 10;
            }
        }
        public Int64 LastColor { get; private set; }

        public MartingaleBotPlayer(Int64 money) : base(money) 
        {
            LastBetValue = BaseBetValue;
            IsLastWon = true;
            LastColor = new Random().NextInt64(0, 1);
        }

        public override bool PlaceBetAndPlay(Roulette game)
        {
            if (Balance == 0)
            {
                return false;
            }
            if (IsLastWon)
            {
                LastColor = LastColor == 0 ? 1 : 0;
                LastBetValue = BaseBetValue;
                Int64 betValue = Balance >= LastBetValue ? LastBetValue : Balance;
                RouletteBet bet = new(betValue, BetType.Color);
                bet.SetColor(LastColor);
                Int64 betResult = game.Play(bet);
                Balance += betResult;
                IsLastWon = betResult > 0;
            }
            else
            {
                LastBetValue = Balance >= LastBetValue*2 ? LastBetValue*2 : Balance;
                RouletteBet bet = new(LastBetValue, BetType.Color);
                bet.SetColor(LastColor);
                Int64 betResult = game.Play(bet);
                Balance += betResult;
                IsLastWon = betResult > 0;
            }
            return true;
        }
    }
}

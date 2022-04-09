using CasinoLib;

namespace MartingaleBotLib
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
        public BetColorType LastColor { get; private set; }

        public MartingaleBotPlayer(Int64 money) : base(money) 
        {
            LastBetValue = BaseBetValue;
            IsLastWon = true;
            LastColor = (BetColorType)new Random().NextInt64(0, 1);
        }

        public override bool PlaceBetAndPlay(Roulette game)
        {
            if (Balance == 0)
            {
                return false;
            }
            if (IsLastWon)
            {
                LastColor = LastColor == BetColorType.Black ? BetColorType.Red : BetColorType.Black;
                LastBetValue = BaseBetValue;
                Int64 betValue = Balance >= LastBetValue ? LastBetValue : Balance;
                ColorRouletteBet bet = new(betValue);
                bet.SetColor(LastColor);
                Int64 betResult = game.Play(bet);
                Balance += betResult;
                IsLastWon = betResult > 0;
            }
            else
            {
                LastBetValue = Balance >= LastBetValue*2 ? LastBetValue*2 : Balance;
                ColorRouletteBet bet = new(LastBetValue);
                bet.SetColor(LastColor);
                Int64 betResult = game.Play(bet);
                Balance += betResult;
                IsLastWon = betResult > 0;
            }
            return true;
        }
    }
}

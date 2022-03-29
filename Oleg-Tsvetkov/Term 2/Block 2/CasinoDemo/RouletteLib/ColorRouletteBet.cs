namespace CasinoLib
{
    public class ColorRouletteBet : RouletteBet
    {
        public BetColorType? Color { get; private set; }

        // Задаём размер ставки и её тип. 
        public ColorRouletteBet(Int64 money) : base(money)
        {
            Type = BetType.Color;
        }

        public void SetColor(BetColorType color)
        {
            if (Enum.IsDefined(typeof(BetColorType), color))
            {
                Color = color;
            }
            else
            {
                throw new ArgumentException("Invalid color argument");
            }
        }

    }
}
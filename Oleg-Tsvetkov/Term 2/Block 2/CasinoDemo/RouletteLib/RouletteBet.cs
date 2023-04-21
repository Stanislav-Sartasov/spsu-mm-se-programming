namespace CasinoLib
{
    public abstract class RouletteBet
    {
        public Int64 Value { get; set; }
        public BetType Type { get; set; }

        // Задаём размер ставки и её тип. 
        public RouletteBet(Int64 money)
        {
            Value = money;
        }

    }
}
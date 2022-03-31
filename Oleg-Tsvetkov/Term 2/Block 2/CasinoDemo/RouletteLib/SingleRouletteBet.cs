namespace CasinoLib
{
    public class SingleRouletteBet : RouletteBet
    {
        public Int64? Single { get; private set; }

        // Задаём размер ставки и её тип. 
        public SingleRouletteBet(Int64 money) : base(money)
        {
            Type = BetType.Single;
        }

        public void SetSingle(Int64 single)
        {
            if (single >= 0 && single <= 36)
            {
                Single = single;
            }
            else
            {
                throw new ArgumentException("Argument can be between 0 and 36");
            }
        }

    }
}
namespace CasinoLib
{
    public class ParityRouletteBet : RouletteBet
    {
        public BetParityType? Parity { get; private set; }

        // Задаём размер ставки и её тип. 
        public ParityRouletteBet(Int64 money) : base(money)
        {
            Type = BetType.Parity;
        }

        public void SetParity(BetParityType parity)
        {
            if (Enum.IsDefined(typeof(BetParityType), parity))
            {
                Parity = parity;
            }
            else
            {
                throw new ArgumentException("Invalid parity argument");
            }
        }

    }
}
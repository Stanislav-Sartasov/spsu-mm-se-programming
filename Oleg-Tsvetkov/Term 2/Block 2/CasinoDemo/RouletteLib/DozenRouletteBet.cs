namespace CasinoLib
{
    public class DozenRouletteBet : RouletteBet
    {
        public BetDozenType? Dozen { get; private set; }

        // Задаём размер ставки и её тип. 
        public DozenRouletteBet(Int64 money) : base(money)
        {
            Type = BetType.Dozen;
        }

        public void SetDozen(BetDozenType dozen)
        {
            if (Enum.IsDefined(typeof(BetDozenType), dozen))
            {
                Dozen = dozen;
            }
            else
            {
                throw new ArgumentException("Invalid dozen argument");
            }
        }

    }
}
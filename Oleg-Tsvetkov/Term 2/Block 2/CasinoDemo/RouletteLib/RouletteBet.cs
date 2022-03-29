namespace CasinoLib
{
    public class RouletteBet
    {
        public Int64 Value { get; set; }
        public BetType Type { get; set; }

        public Int64? Parity { get; private set; }
        public Int64? Color { get; private set; }
        public Int64? Single { get; private set; }
        public Int64? Dozen { get; private set; }

        // Задаём размер ставки и её тип. 
        public RouletteBet(Int64 money, BetType type)
        {
            Value = money;
            Type = type;
        }

        public void SetParity(Int64 parity)
        {
            if (parity == 0 || parity == 1)
            {
                Parity = parity;
            }
            else
            {
                throw new ArgumentException("Argument can be either 0 or 1");
            }
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

        public void SetColor(Int64 color)
        {
            if (color >= 0 && color <= 2)
            {
                Color = color;
            }
            else
            {
                throw new ArgumentException("Argument can be between 0 and 2");
            }
        }

        public void SetDozen(Int64 dozen)
        {
            if (dozen >= 1 && dozen <= 3)
            {
                Dozen = dozen;
            }
            else
            {
                throw new ArgumentException("Argument can be between 1 and 3");
            }
        }

    }
}
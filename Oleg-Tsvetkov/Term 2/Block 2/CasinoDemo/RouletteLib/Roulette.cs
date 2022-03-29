namespace CasinoLib
{
    public class Roulette
    {
        private static Int64[] RedNumbers = {32, 19, 21, 25, 34, 27, 36, 30, 23, 5, 16, 1, 14, 9, 18, 7, 12, 3};
        private static Int64[] BlackNumbers = {15, 4, 2, 17, 6, 13, 11, 8, 10, 24, 33, 20, 31, 22, 29, 28, 35, 26};
        public Int64 LastNumber { get; private set; }

        public Roulette()
        {
            LastNumber = -1;
        }
        private void SpinRoulette()
        {
            LastNumber = new Random().Next(0, 36);
        }

        public Int64 GetColorCode(Int64 number)
        {
            if (number == 0)
            {
                return 2;
            }
            if (RedNumbers.Contains(number))
            {
                return 1;
            }
            if (BlackNumbers.Contains(number))
            {
                return 0;
            }
            throw new ArgumentOutOfRangeException("Ошибка! Тип ставки за пределом разрешённых значений.");
        }
        //Вернёт изменение баланса после ставки
        public Int64 Play(RouletteBet bet)
        {
            SpinRoulette();
            switch (bet.Type)
            {
                //0 - чёрный, 1 - красный, 2 - зелёный 
                case BetType.Color: 
                    if (bet.Color is null)
                    {
                        throw new ArgumentNullException("Color is null");
                    }
                    if (GetColorCode(LastNumber) == bet.Color && bet.Color != 2)
                    {
                        return bet.Value;
                    }
                    else if (GetColorCode(LastNumber) == bet.Color && bet.Color == 2)
                    {
                        return bet.Value * 35;
                    }
                    else
                    {
                        return -bet.Value;
                    }
                    break;
                //0 - чётный, 1 - нечётный
                case BetType.Parity:
                    if (bet.Parity is null)
                    {
                        throw new ArgumentNullException("Parity is null");
                    }
                    if (bet.Parity % 2 != LastNumber % 2 || LastNumber == 0)
                    {
                        return -bet.Value;
                    }
                    else
                    {
                        return bet.Value;
                    }
                    break;
                //Числа от 1 до 3 обозначают соотв. дюжину чисел (1-12, 13-24, 25-36) 
                case BetType.Dozen:
                    if (bet.Dozen is null)
                    {
                        throw new ArgumentNullException("Dozen is null");
                    }
                    if ((LastNumber <= 12 && LastNumber > 0 && bet.Dozen == 1) || (13 <= LastNumber && LastNumber <= 24 && bet.Dozen == 2) || (LastNumber > 24 && bet.Dozen == 3))
                    {
                        return bet.Value*2;
                    }
                    else
                    {
                        return -bet.Value;
                    }
                    break;
                case BetType.Single:
                    if (bet.Single is null)
                    {
                        throw new ArgumentNullException("Single is null");
                    }
                    if (LastNumber == bet.Single)
                    {
                        return bet.Value * 35;
                    }
                    else
                    {
                        return -bet.Value;
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException("Bet type is out of allowed range");
            }
        }
    }
}
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
        public Int64 Play(BetType type, Int64 number, Int64 bet)
        {
            SpinRoulette();
            switch (type)
            {
                //0 - чёрный, 1 - красный, 2 - зелёный 
                case BetType.Color: 
                    if (number < 0 || number > 2)
                    {
                        throw new ArgumentOutOfRangeException("Ошибка! При ставке на цвет число выбор должен равняться 1 или 0.");
                    }
                    if (GetColorCode(LastNumber) == number && number != 2)
                    {
                        return bet;
                    }
                    else if (GetColorCode(LastNumber) == number && number == 2)
                    {
                        return bet * 35;
                    }
                    else
                    {
                        return -bet;
                    }
                    break;
                //0 - чётный, 1 - нечётный
                case BetType.Parity:
                    if (number != 0 && number != 1)
                    {
                        throw new ArgumentOutOfRangeException("Ошибка! При ставке на чётность выбор должен равняться 1 или 0.");
                    }
                    if (number % 2 != LastNumber % 2 || LastNumber == 0)
                    {
                        return -bet;
                    }
                    else
                    {
                        return bet;
                    }
                    break;
                //Числа от 1 до 3 обозначают соотв. дюжину чисел (1-12, 13-24, 25-36) 
                case BetType.Dozen:
                    if (number < 1 || number > 3)
                    {
                        throw new ArgumentOutOfRangeException("Ошибка! При ставке на дюжину выбор должен быть от 1 до 3.");
                    }
                    if ((LastNumber <= 12 && LastNumber != 0 && number == 1) || (13 <= LastNumber && LastNumber <= 24 && number == 2) || (LastNumber > 24 && number == 3))
                    {
                        return bet*2;
                    }
                    else
                    {
                        return -bet;
                    }
                    break;
                case BetType.Single:
                    if (number < 0 || number > 36)
                    {
                        throw new ArgumentOutOfRangeException("Ошибка! При ставке на одно число выбор должен быть от 0 до 36.");
                    }
                    if (LastNumber == number)
                    {
                        return bet * 35;
                    }
                    else
                    {
                        return -bet;
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException("Ошибка! Тип ставки за пределом разрешённых значений.");
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasinoLib
{
    public class ConsolePlayer : Player
    {
        public ConsolePlayer(Int64 money) : base(money) { }

        private Int64 AskConsoleForNumber(Int64 from, Int64 to)
        {
            Int64 result = Int64.MinValue;
            while (result < from || result > to)
            {
                result = Convert.ToInt64(Console.ReadLine());
                if (result < from || result > to)
                {
                    Console.WriteLine("Недопустимое число! Попробуйте снова!");
                }
            }
            return result;
        }

        public override bool PlaceBetAndPlay(Roulette game)
        {
            Console.WriteLine("Введите тип ставки: ");
            Console.WriteLine("0 - Цвет");
            Console.WriteLine("1 - Чётность");
            Console.WriteLine("2 - Дюжина");
            Console.WriteLine("3 - Конкретное число");
            Int64 betOption = AskConsoleForNumber(0, 3);
            BetType betType = (BetType)betOption;
            Console.WriteLine("Ваш текущий баланс: "+Balance.ToString());
            Console.WriteLine("Введите размер ставки: ");
            Int64 betValue = AskConsoleForNumber(1, Balance);
            Int64 betNumber = -1;
            switch (betType)
            {
                case BetType.Color:
                    Console.WriteLine("Введите 0 для ставки на чёрное, 1 - на красное, 2 - на зелёное.");
                    betNumber = AskConsoleForNumber(0, 2);
                    break;
                case BetType.Parity:
                    Console.WriteLine("Введите 1 для ставки на нечётное, 2 - на чётное.");
                    betNumber = AskConsoleForNumber(1, 2);
                    break;
                case BetType.Dozen:
                    Console.WriteLine("Введите число от 1 до 3, отвечающее за соотв. дюжину:");
                    betNumber = AskConsoleForNumber(1, 3);
                    break;
                case BetType.Single:
                    Console.WriteLine("Введите число от 0 до 36:");
                    betNumber = AskConsoleForNumber(0, 36);
                    break;
            }
            Int64 betResult = game.Play(betType, betNumber, betValue);
            Balance += betResult;
            return true;
        }
    }
}

using System.Collections.Generic;
using AbstractClasses;

namespace BlackjackMechanics.Players
{
    public class Player : AParticipant
    {
        public double Money;
        public double Multiplier = 1;
        public double Rate;
        public bool IsWantNextCard = false;

        public Player(int money)
        {
            Money = money;
            CardsInHand = new List<ACard>();
        }

        public void SetRate(double rate)
        {
            Rate = rate;
        }

        public override bool GetNextCard()
        {
            IsWantNextCard = !IsWantNextCard;
            return !IsWantNextCard;
        }

        public void DoubleBet()
        {
            Rate *= 2;
        }

        public override void Win()
        {
            Money += Rate * Multiplier;
            CountGames++;
            CountWinGames++;
            Console.WriteLine("Вы забрали выигрыш. Его сумма составляет: " + Rate * Multiplier + "\n\n");
            Console.WriteLine("Ваш баланс: " + Money +"\n\n");
            Multiplier = 1;
            Console.WriteLine("<----------------------------------------------->\n\n");

        }

        public override void Lose()
        {
            Money -= Rate;
            CountGames++;
            Console.WriteLine("В данной игре игрок проиграл и победило казино. Сумма проигранной ставки: " + Rate + "\n\n");
            Console.WriteLine("Ваш баланс: " + Money + "\n\n");
            Console.WriteLine("<----------------------------------------------->\n\n");
        }
    }
}

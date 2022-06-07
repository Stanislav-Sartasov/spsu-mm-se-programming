using Game.Players;


namespace Bots
{
    public class SmartBot : Player
    {
        public readonly int InitialMoney;
        public int PlayersBet;
        public int LastBet;

        public SmartBot(string name, int money) : base(name, money)
        {
            InitialMoney = money;
            PlayersBet = 0;
        }

        public override int Bet()
        {
            if (Money > InitialMoney)
            {
                PlayersBet = (int)Math.Floor(LastBet * 1.3);
            }

            else
            {
                PlayersBet = (int)Math.Floor(Money * 0.14);
            }

            LastBet = PlayersBet;
            Money -= PlayersBet;

            return PlayersBet;

        }

        public override PlayerAction Move()
        {
            if (GetValue() <= 12)
            {
                return PlayerAction.Double;
            }

            else if ((GetValue() >= 13) && (GetValue() <= 17))
            {
                return PlayerAction.Hit;
            }

            return PlayerAction.Stand;

        }
    }
}
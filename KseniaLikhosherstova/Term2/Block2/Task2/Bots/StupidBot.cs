using Game.Players;


namespace Bots
{
    public class StupidBot : Player
    {
        public readonly int InitialMoney;
        public int PlayersBet;

        public StupidBot(string name, int money) : base(name, money)
        {
            InitialMoney = money;
            PlayersBet = 0;
        }

        public override int Bet()
        {
            if (Money == InitialMoney)
            {
                PlayersBet = (int)Math.Floor(Money * 0.2);
            }

            else
            {
                PlayersBet = (int)Math.Floor(Money * 0.14);
            }

            Money -= PlayersBet;
            return PlayersBet;
        }

        public override PlayerAction Move()
        {
            if (GetValue() >= 12)
            {
                return PlayerAction.Stand;
            }

            return PlayerAction.Hit;
        }
    }
}
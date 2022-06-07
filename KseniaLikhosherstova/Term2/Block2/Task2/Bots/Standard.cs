using Game.Players;



namespace Bots
{

    public class StandardBot : Player
    {

        public readonly int InitialMoney;
        public int PlayersBet;


        public StandardBot(string name, int money) : base(name, money)
        {
            InitialMoney = money;
            PlayersBet = 0;
        }

        public override int Bet()
        {

            if (Money / 2 > InitialMoney)
            {
                PlayersBet = (int)Math.Floor(Money * 0.4);
            }

            else if (Money % 5 == 0)
            {
                PlayersBet = Math.Min((int)Math.Floor(Money * 0.25), (int)Math.Floor(InitialMoney * 0.125));
            }

            else
            {
                PlayersBet = 50;
            }

            Money -= PlayersBet;

            return PlayersBet;
        }

        public override PlayerAction Move()
        {
            if ((GetValue() > 8) && (GetValue() < 13))
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
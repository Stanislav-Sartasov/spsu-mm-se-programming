using Game.Players;


namespace Bots
{

    public class StandardBot : Player
    {

        private readonly int initialMoney;
        private int playersBet;


        public StandardBot(string name, int money) : base(name, money)
        {
            initialMoney = money;
            playersBet = 0;
        }

        public override int Bet()
        {

            if (Money / 2 > initialMoney)
            {
                playersBet = (int)Math.Floor(Money * 0.4);
            }

            else if (Money % 5 == 0)
            {
                playersBet = Math.Min((int)Math.Floor(Money * 0.25), (int)Math.Floor(initialMoney * 0.125));
            }

            else
            {
                playersBet = 50;
            }

            Money -= playersBet;

            return playersBet;
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

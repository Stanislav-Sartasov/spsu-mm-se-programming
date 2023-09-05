using Game.Players;


namespace Bots
{
    public class SmartBot : Player
    {
        private readonly int initialMoney;
        private int playersBet;
        private int lastBet;

        public SmartBot(string name, int money) : base(name, money)
        {
            initialMoney = money;
            playersBet = 0;
        }

        public override int Bet()
        {
            if (Money > initialMoney)
            {
                playersBet = (int)Math.Floor(lastBet * 1.3);
            }

            else
            {
                playersBet = (int)Math.Floor(Money * 0.14);
            }

            lastBet = playersBet;
            Money -= playersBet;

            return playersBet;

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
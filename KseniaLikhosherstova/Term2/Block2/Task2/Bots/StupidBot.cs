using Game.Players;


namespace Bots
{
    public class StupidBot : Player
    {
        private readonly int initialMoney;
        private int playersBet;

        public StupidBot(string name, int money) : base(name, money)
        {
            initialMoney = money;
            playersBet = 0;
        }

        public override int Bet()
        {
            if (Money == initialMoney)
            {
                playersBet = (int)Math.Floor(Money * 0.2);
            }

            else
            {
                playersBet = (int)Math.Floor(Money * 0.14);
            }

            Money -= playersBet;
            return playersBet;
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
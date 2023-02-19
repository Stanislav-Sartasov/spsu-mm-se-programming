using BlackJackEnumerations;
using BlackJack;

namespace Bots
{
    public class DoubleBot : Player
    {
        private int oldBalance = 0;
        private int oldBet;
        private int normalBet = 0;

        public DoubleBot(string name, int balance) : base(name, balance)
        {
            oldBet = (int)Math.Floor(balance * 0.1);
            normalBet = oldBet;
        }

        public override int GetNewBet()
        {
            if (oldBalance > Balance)
            {
                return 2 * oldBet;
            }
            else
            {
                return normalBet;
            }
        }

        public override PlayerMove GetMove()
        {
            if (Enumerable.Range(1, 17).Contains(GetScore()))
            {
                return PlayerMove.Hit;
            }
            else
            {
                oldBalance = Balance;
                oldBet = Bet;
                return PlayerMove.Stand;
            }
        }
    }
}

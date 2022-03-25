using BlackJack;

namespace Bots
{
    public class GoodBot : Player
    {
        private int startBalance;
        private int standartBet;

        public GoodBot(string name, int balance) : base(name, balance)
        {
            startBalance = balance;
            standartBet = (int)Math.Floor(0.1 * balance);
        }

        public override int GetNewBet()
        {
            return standartBet;
        }

        public override string GetMove()
        {
            int score = GetScore();
            if (score == 11)
            {
                if (Balance > startBalance / 2)
                {
                    return "double";
                }
                else
                {
                    return "hit";
                }

            }
            else if (Enumerable.Range(1, 17).Contains(score))
            {
                return "hit";
            }
            else
            {
                return "stand";
            }
        }
    }
}

using BlackJackEnumerations;
using BlackJack;

namespace Bots
{
    public class RandomBot : Player
    {
        private Random random;

        public RandomBot(string name, int balance) : base(name, balance)
        {
            random = new Random(DateTime.Now.Millisecond);
        }

        public override int GetNewBet()
        {
            return random.Next(Balance);
        }

        public override PlayerMove GetMove()
        {
            return (PlayerMove)random.Next(3);
        }

    }
}

using BlackJack;

namespace Bots
{
    public class RandomBot : Player
    {
        private Random random;

        public RandomBot(string name, int balance) : base(name, balance)
        {
            random = new Random();
        }

        public override int GetNewBet()
        {
            return random.Next(Balance);
        }

        public override string GetMove()
        {
            string[] possibleOptions = { "hit", "double", "stand" };
            return possibleOptions[random.Next(possibleOptions.Length)];
        }

    }
}

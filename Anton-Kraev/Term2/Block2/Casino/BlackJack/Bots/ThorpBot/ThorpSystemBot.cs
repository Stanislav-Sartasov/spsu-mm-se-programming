using BotStructure;
using Cards;

namespace ThorpBot
{
    public sealed class ThorpSystemBot : Bot, IBot
    {
        public ThorpSystemBot(int balance) : base(balance) { }

        protected override int NewBet(Shoes shoes)
        {
            int realScore = CardsCounting(shoes) / (int)Math.Ceiling(shoes.AllCardsCount / 52.0);
            realScore /= 4;
            if (realScore < -10)
                return MinBet;
            if (realScore > 10)
                return MinBet * 21;
            return MinBet * (11 + realScore);
        }

        private int CardsCounting(Shoes shoes)
        {
            int count = 0;
            count += 8 * (32 - shoes.Decks["2"]);
            count += 9 * (32 - shoes.Decks["3"]);
            count += 12 * (32 - shoes.Decks["4"]);
            count += 15 * (32 - shoes.Decks["5"]);
            count += 9 * (32 - shoes.Decks["6"]);
            count += 5 * (32 - shoes.Decks["7"]);
            count -= 32 - shoes.Decks["8"];
            count -= 5 * (32 - shoes.Decks["9"]);
            count -= 10 * (128 - shoes.Decks["10"]);
            count -= 12 * (32 - shoes.Decks["A"]);
            return count;
        }
    }
}
using BotStructure;
using Cards;

namespace KellyBot
{
    public sealed class KellyCriterionBot : Bot, IBot
    {
        public KellyCriterionBot(int balance) : base(balance) { }

        protected override int NewBet(Shoes shoes)
        {
            int realScore = (int)(CardsCounting(shoes) / Math.Ceiling(shoes.AllCardsCount / 52.0));
            if (realScore < 0)
                return 0;
            if (realScore == 0)
              return MinBet;
            return (int)Round(Balance * (realScore / 100.0));
        }

        private double CardsCounting(Shoes shoes)
        {
            double count = 0;
            count += 0.5 * (64 - (shoes.Decks["2"] + shoes.Decks["7"]));
            count += 96 - (shoes.Decks["3"] + shoes.Decks["4"] + shoes.Decks["6"]);
            count += 1.5 * (32 - shoes.Decks["5"]);
            count -= 0.5 * (32 - shoes.Decks["9"]);
            count -= 160 - (shoes.Decks["10"] + shoes.Decks["A"]);
            return count;
        }

        private static double Round(double x) => x - x % 2 + (x >= 0 ? x % 2 <= 1 ? 0 : 2 : 0);
    }
}
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
            count += 0.5 * (64 - (shoes.Decks[CardRank.Two] + shoes.Decks[CardRank.Seven]));
            count += 96 - (shoes.Decks[CardRank.Three] + shoes.Decks[CardRank.Four] + shoes.Decks[CardRank.Six]);
            count += 1.5 * (32 - shoes.Decks[CardRank.Five]);
            count -= 0.5 * (32 - shoes.Decks[CardRank.Nine]);
            count -= 160 - (shoes.Decks[CardRank.Ten] + shoes.Decks[CardRank.Jack] + shoes.Decks[CardRank.Queen] + shoes.Decks[CardRank.King] + shoes.Decks[CardRank.Ace]);
            return count;
        }

        private static double Round(double x) => x - x % 2 + (x >= 0 ? x % 2 <= 1 ? 0 : 2 : 0);
    }
}
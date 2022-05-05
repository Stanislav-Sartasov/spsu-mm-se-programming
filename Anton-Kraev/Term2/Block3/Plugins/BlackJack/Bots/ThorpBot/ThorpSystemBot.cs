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
            count += 8 * (32 - shoes.Decks[CardRank.Two]);
            count += 9 * (32 - shoes.Decks[CardRank.Three]);
            count += 12 * (32 - shoes.Decks[CardRank.Four]);
            count += 15 * (32 - shoes.Decks[CardRank.Five]);
            count += 9 * (32 - shoes.Decks[CardRank.Six]);
            count += 5 * (32 - shoes.Decks[CardRank.Seven]);
            count -= 32 - shoes.Decks[CardRank.Eight];
            count -= 5 * (32 - shoes.Decks[CardRank.Nine]);
            count -= 10 * (128 - shoes.Decks[CardRank.Ten] - shoes.Decks[CardRank.Jack] - shoes.Decks[CardRank.Queen] - shoes.Decks[CardRank.King]);
            count -= 12 * (32 - shoes.Decks[CardRank.Ace]);
            return count;
        }
    }
}
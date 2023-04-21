using PlayerStructure;
using ToolKit;

namespace Bots
{
    public abstract class ACountingBot : ABasicBot
    {
        protected int currentAccount;
        protected double realAccount;
        protected int countOfCards = 8 * 52;
        protected int discardedСards = 0;
        protected int countingBet = 0;

        public ACountingBot(int money = 10000, int games = 40) : base(money, games)
        {
            currentAccount = 0;
            realAccount = 0;
            StateOfShoe = ShoeState.Calculated;
        }

        public void ThinkOver(List<Card> dealerHand)
        {
            if (StateOfShoe == ShoeState.Reset)
            {
                currentAccount = 0;
                realAccount = 0;
                discardedСards = 0;
                StateOfShoe = ShoeState.Calculated;
            }

            foreach (var hand in Hands)
            {
                CountHand(hand);
            }
            foreach (var card in dealerHand)
            {
                Count(card);
            }
        }

        private protected abstract int CountCard(Card card);

        private void CountHand(Hand hand)
        {
            foreach (var card in hand.Cards)
            {
                Count(card);
            }
        }

        private void Count(Card card)
        {
            discardedСards++;
            currentAccount += CountCard(card);
            realAccount = (double)currentAccount * discardedСards / countOfCards;
        }
    }
}
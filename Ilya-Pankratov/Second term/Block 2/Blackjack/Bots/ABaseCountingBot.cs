using GameTools;
using Player;

namespace Bots
{
    public abstract class ABaseCountingBot : ABaseBot
    {
        protected int cardGone;
        protected int totalCards;
        protected int score;
        protected double realScore;

        public ABaseCountingBot(int cash = 1000, int numberOfDecks = 8, int sizeOfDeck = 52) : base(cash)
        {
            cardGone = 0;
            totalCards = numberOfDecks * sizeOfDeck;
            score = 0;
            realScore = 0;
        }

        public ABaseCountingBot(Func<int, bool> leaveFunction, int cash = 1000, int numberOfDecks = 8, int sizeOfDeck = 52) : base(leaveFunction, cash)
        {
            cardGone = 0;
            totalCards = numberOfDecks * sizeOfDeck;
            score = 0;
            realScore = 0;
        }

        public override HandState Play(Hand hand, List<Card> dealerCards)
        {
            if (Flag == PlayerState.Stop)
            {
                if (Hands[0] == hand)
                {
                    CountCards(dealerCards);
                }

                CountCards(hand);

                return HandState.Done;
            }
            else if (Flag == PlayerState.DeckReset)
            {
                cardGone = 0;
                score = 0;
                realScore = 0;

                return HandState.Done;
            }

            return base.Play(hand, dealerCards);
        }

        protected abstract int CountCardValue(Card card);

        private void CountCard(Card card)
        {
            cardGone++;
            score += CountCardValue(card);  
            realScore = (double)score * cardGone / totalCards;
        }

        private void CountCards(Hand hand)
        { 
            foreach (var card in hand.Cards)
            {
                CountCard(card);
            }
        }

        private void CountCards(List<Card> cards)
        {
            foreach (var card in cards)
            {
                CountCard(card);
            }
        }
    }
}
using AbstractClasses;
using BlackjackMechanics.Cards;

namespace BlackjackMechanics.Players
{
    internal class Dealer : AParticipant
    {
        public ACard VisibleCard;

        private void TakeCard(ACard card)
        {
            if (VisibleCard == null)
                VisibleCard = card;
            this.CardsInHand.Add(card);
        }

        public override bool GetNextCard()
        {
            return this.GetSumOfCards() < 17;
        }

        public void HandOutCards(DeckOfCards deck, AParticipant player)
        {
            for (int i = 0; i < 2; i++)
            {
                player.CardsInHand.Add(deck.GetOneCard(player));
                TakeCard(deck.GetOneCard(this));
            }
        }
    }
}

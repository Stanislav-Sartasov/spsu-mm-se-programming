using AbstractClasses;
using BlackjackMechanics.Cards;

namespace BlackjackMechanics.Players
{
    public class Dealer : AParticipant
    {
        public ACard VisibleCard;

        public Dealer()
        {
            CardsInHand = new List<ACard>();
        }

        public override bool GetNextCard()
        {
            return this.GetSumOfCards() < 17;
        }

        public void TakeCard(ACard card)
        {
            if (VisibleCard == null)
                VisibleCard = card;
            this.CardsInHand.Add(card);
        }

        public void GiveCard(ACard card, AParticipant player)
        {
            player.CardsInHand.Add(card);
        }

        public void HandOutCards(DeckOfCards deck, AParticipant player)
        {
            for (int i = 0; i < 2; i++)
            {
                GiveCard(deck.GetOneCard(player), player);
                TakeCard(deck.GetOneCard(this));
            }
        }
    }
}

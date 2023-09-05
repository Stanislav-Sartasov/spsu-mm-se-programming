﻿using AbstractClasses;
using BlackjackMechanics.Cards;

namespace BlackjackMechanics.Players
{
    public class Dealer : AParticipant
    {
        public ACard VisibleCard { get; private set; }

        public Dealer()
        {
            CardsInHand = new List<ACard>();
            PlayerTurnNow = PlayerTurn.NotPlayer;
        }

        public override bool IsNeedNextCard()
        {
            return this.GetSumOfCards() < 17;
        }

        public override void TakeCard(ACard card)
        {
            if (VisibleCard == null)
                VisibleCard = card;
            this.CardsInHand.Add(card);
        }

        public void GiveCard(ACard card, AParticipant player)
        {
            player.TakeCard(card);
        }

        public void HandOutCards(DeckOfCards deck, AParticipant player)
        {
            for (int i = 0; i < 2; i++)
            {
                GiveCard(deck.GetOneCard(player), player);
                TakeCard(deck.GetOneCard(this));
            }
        }

        public void ClearHands()
        {
            CardsInHand.Clear();
            VisibleCard = null;
        }
    }
}

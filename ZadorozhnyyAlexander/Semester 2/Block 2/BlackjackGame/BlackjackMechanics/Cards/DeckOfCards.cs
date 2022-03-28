using AbstractClasses;


namespace BlackjackMechanics.Cards
{
    public class DeckOfCards
    {
        public List<ACard> Deck;
        public int CountDecksInOne;

        public DeckOfCards(int data)
        {
            CountDecksInOne = data;
            Deck = new List<ACard>();

            CreateCardNewDeckOfCard();
        }

        private void CreateCardNewDeckOfCard()
        {
            string[] CardSuits = { "Heart", "Diamond", "Club", "Spade" };

            foreach (var suit in CardSuits)
            {
                for (int i = 0; i < 9 * CountDecksInOne; i++)
                {
                    Deck.Add(new NumberCard(i % 9 + 2, suit));
                }

                for (int i = 0; i < 3 * CountDecksInOne; i++)
                {
                    Deck.Add(new FaceCard(i % 3, suit));
                }

                for (int i = 0; i < CountDecksInOne; i++)
                {
                    Deck.Add(new AceCard(suit));
                }
            }
                
        }

        public void ShuffleDeck()
        {
            Random rnd = new Random();
            Deck = Deck.OrderBy(x => rnd.Next()).ToList();
        }

        private void RemoveCard(ACard card)
        {
            this.Deck.Remove(card);
        }

        public ACard GetOneCard(AParticipant player)
        {
            ACard card = this.Deck.First();
            if (card.CardName == "Ace")
                ((AceCard)card).CheckIsMoreThenTwentyOne(player.CardsInHand);
            RemoveCard(card);
            return card;
        }

        public void ResetDeckOfCards()
        {
            Deck.Clear();
            CreateCardNewDeckOfCard();
            ShuffleDeck();
        }
    }
}

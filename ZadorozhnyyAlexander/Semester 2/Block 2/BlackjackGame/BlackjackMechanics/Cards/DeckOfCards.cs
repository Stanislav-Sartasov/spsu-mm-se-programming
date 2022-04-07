using AbstractClasses;


namespace BlackjackMechanics.Cards
{
    public class DeckOfCards
    {
        public List<ACard> Deck { get; private set; }
        private int countDecksInOne;

        public DeckOfCards(int data)
        {
            countDecksInOne = data;
            Deck = new List<ACard>();

            CreateNewDeckOfCard();
        }

        private void CreateNewDeckOfCard()
        {
            for (int deck = 0; deck < countDecksInOne; deck++)
            {
                for (int suit = 0; suit < 4; suit++)
                {
                    for (int i = 0; i < 12; i++)
                    {
                        Deck.Add(new UsualCard((CardNames)i, (CardSuits)suit));
                    }

                    Deck.Add(new AceCard((CardSuits)suit));
                }
            }
        }

        private void RemoveCard(ACard card)
        {
            this.Deck.Remove(card);
        }

        public ACard GetOneCard(AParticipant player)
        {
            ACard card = this.Deck.First();
            if (card.CardName == CardNames.Ace)
                ((AceCard)card).CheckIsMoreThenTwentyOne(player.CardsInHand);
            RemoveCard(card);
            return card;
        }

        public void ShuffleDeck()
        {
            Random rnd = new Random();
            Deck = Deck.OrderBy(x => rnd.Next()).ToList();
        }

        public void ResetDeckOfCards()
        {
            Deck.Clear();
            CreateNewDeckOfCard();
        }
    }
}

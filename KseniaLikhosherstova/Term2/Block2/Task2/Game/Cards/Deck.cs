namespace Game.Cards
{
    public class Deck
    {
        public List<Card> Cards { get; private set; }

        private Deck()
        {
            Cards = new List<Card>();

            foreach (var rank in Enum.GetValues<Rank>())
                foreach (var suit in Enum.GetValues<Suit>())
                    Cards.Add(new Card(rank, suit));
        }

        public static Deck CreateDeck(int standardDecksCount)
        {
            if (standardDecksCount < 1)
                throw new ArgumentException(nameof(standardDecksCount));

            Deck deck = new Deck();

            for (int i = 0; i < standardDecksCount - 1; i++)
            {
                deck.Cards.AddRange(new Deck().Cards);
            }

            return deck;
        }

        public void Shuffle()
        {
            Random rnd = new Random();

            Cards = Cards.OrderBy(x => rnd.Next()).ToList();
        }

        public Card PickCard()
        {
            if (!Cards.Any())
                throw new Exception("There are no more cards in the decks");

            var cardToPick = Cards.First();
            Cards.Remove(cardToPick);

            return cardToPick;
        }
    }
}

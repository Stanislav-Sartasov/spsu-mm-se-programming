namespace ToolKit
{
    public class Deck
    {
        private protected List<Card> cards { get; set; }
        public int CountOfCards
        {
            get { return cards.Count; }
        }
        private static Random Randomizer = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);

        public Deck()
        {
            cards = Enumerable.Range(1, 4).SelectMany(suit => Enumerable.Range(1, 13).Select(point => new Card((Suits)suit, (CardPoints)point))).ToList();
        }

        public void Shuffle()
        {
            int n = cards.Count;
            while (n-- > 1)
            {
                int k = Randomizer.Next(n + 1);
                Card value = cards[k];
                cards[k] = cards[n];
                cards[n] = value;
            }
        }

        public Card TakeCard()
        {
            var card = cards.FirstOrDefault();

            if (card is null || !cards.Remove(card))
            {
                throw new ArgumentException("There are not enough cards in this deck.");
            }

            return card;
        }

        public List<Card> TakeCards(int count)
        {
            List<Card> takenCards = new List<Card>();

            if (count <= 0)
            {
                throw new ArgumentOutOfRangeException("Count must be a natural number.");
            }

            while (count > 0)
            {
                takenCards.Add(TakeCard());
                count--;
            }

            return takenCards;
        }
    }
}
namespace GameTools
{
    public class Deck
    {
        public List<Card> Cards { get; set; }

        public Deck()
        {
            Cards = new List<Card>();
            Reset();
        }

        public void Shuffle()
        {
            Cards = Cards.OrderBy(c => Guid.NewGuid())
                .ToList();
        }

        public virtual void Reset()
        {
            Cards = Enumerable.Range(0, 4)
                .SelectMany(s => Enumerable.Range(1, 13)
                    .Select(r => new Card(s, r))
                            )
                .ToList();
        }

        public Card TakeCard()
        {
            var card = Cards.FirstOrDefault();
            Cards.Remove(card);

            return card;
        }

        public List<Card> TakeCards (int numberOfCards)
        {
            var cards = Cards.Take(numberOfCards);

            var takeCards = cards as Card[] ?? cards.ToArray();
            Cards.RemoveAll(takeCards.Contains);

            return takeCards.ToList();
        }
    }
}
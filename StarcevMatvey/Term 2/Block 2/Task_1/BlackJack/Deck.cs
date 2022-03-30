namespace BlackJack
{
    public class Deck
    {
        public List<Card> Cards { get; }

        public Deck()
        {
            Cards = new List<Card>();
            for (int i = 0; i < 52; i++)
            {
                Cards.Add(new Card(i % 4, i % 13 + 1));
            }
        }
    }
}

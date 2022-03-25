namespace BlackJack
{
    public class Deck
    {
        public List<Card> Cards { get; }

        public Deck()
        {
            Cards = new List<Card>();
            string[] suits = { "H", "D", "P", "B" };
            string[] values = { "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A" };
            for (int i = 0; i < 52; i++)
            {
                Cards.Add(new Card(suits[i % 4], values[i % 13]));
            }
        }
    }
}

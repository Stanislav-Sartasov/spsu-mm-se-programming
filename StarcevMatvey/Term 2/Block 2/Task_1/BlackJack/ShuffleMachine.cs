namespace BlackJack
{
    public class ShuffleMachine
    {
        public List<Card> Cards { get; private set; }
        private int index = 0;

        public ShuffleMachine(int countOfDecks = 1)
        {
            Cards = new List<Card>();
            for (int _ = 0; _ < countOfDecks; _++)
            {
                Cards = Cards.Concat(new Deck().Cards).ToList();
            }
        }

        public void Shuffle()
        {
            Random random = new Random(DateTime.Now.Millisecond);
            for (int i = 0; i < Cards.Count; i++)
            {
                (Cards[i], Cards[random.Next(i)]) = (Cards[random.Next(i)], Cards[i]);
            }
        }

        public Card TrowCard()
        {
            if (index > Cards.Count - 26)
            {
                index = 0;
                Shuffle();
            }

            return Cards[index++]; 
        }
    }
}

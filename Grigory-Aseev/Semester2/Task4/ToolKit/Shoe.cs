namespace ToolKit
{
    public class Shoe : Deck
    {
        public int NumberOfDecks { get; private set; }

        public Shoe(int numberOfDecks = 8)
        {
            if (numberOfDecks <= 0 || numberOfDecks >= 8)
            {
                numberOfDecks = 8;
            }

            NumberOfDecks = numberOfDecks;
            InitializeShoe();
        }

        private void InitializeShoe()
        {
            cards = new List<Card>();
            int countOfDecks = NumberOfDecks;

            while (countOfDecks-- > 0)
            {
                Deck deck = new();
                cards.AddRange(deck.TakeCards(deck.CountOfCards));
            }

            Shuffle();
        }

        public bool CheckUpdate()
        {
            return CountOfCards < NumberOfDecks * 52 / 3;
        }
    }
}
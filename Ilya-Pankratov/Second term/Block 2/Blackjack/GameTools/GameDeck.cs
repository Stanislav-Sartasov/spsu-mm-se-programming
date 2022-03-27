namespace GameTools
{
    public class GameDeck : Deck
    {
        public GameDeck(int numberOfDecks)
        {
            Cards = new List<Card>();
            Reset(numberOfDecks);
        }

        public void Reset(int numberOfDecks)
        {
            Cards = new List<Card>();

            for (int i = 0; i < numberOfDecks; i++)
            {
                Cards.AddRange(new Deck().Cards);
            }

            Shuffle();
        }

        public override void Reset()
        {
            Reset(8);
        }
    }
}
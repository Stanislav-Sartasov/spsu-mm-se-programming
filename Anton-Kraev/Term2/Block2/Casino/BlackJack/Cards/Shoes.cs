namespace Cards
{
    public class Shoes
    {
        public int AllCardsCount;

        public Dictionary<string, int> Decks;
        
        public Shoes()
        {
            AllCardsCount  = 416;
            Decks = new()
            {
                { "A", 32 },
                { "10", 128 },
                { "9", 32 },
                { "8", 32 },
                { "7", 32 },
                { "6", 32 },
                { "5", 32 },
                { "4", 32 },
                { "3", 32 },
                { "2", 32 },
            };
        }

        public Card GetCard()
        {
            Random randomizer = new Random();
            int randomNumber = randomizer.Next(1, AllCardsCount);
            int currentCard = 0;

            foreach (var cards in Decks)
            {
                currentCard += cards.Value;
                if (currentCard >= randomNumber)
                {
                    Decks[cards.Key]--;
                    AllCardsCount--;
                    return new Card(cards.Key);
                }
            }

            return new Card("0");
        }
    }
}
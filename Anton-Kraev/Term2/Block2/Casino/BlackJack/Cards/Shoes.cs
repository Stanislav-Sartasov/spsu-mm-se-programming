namespace Cards
{
    public class Shoes
    {
        public int AllCardsCount;

        public Dictionary<CardRank, int> Decks;
        
        public Shoes()
        {
            AllCardsCount  = 416;
            Decks = new()
            {
                { CardRank.Ace, 32 },
                { CardRank.King, 32 },
                { CardRank.Queen, 32 },
                { CardRank.Jack, 32 },
                { CardRank.Ten, 32 },
                { CardRank.Nine, 32 },
                { CardRank.Eight, 32 },
                { CardRank.Seven, 32 },
                { CardRank.Six, 32 },
                { CardRank.Five, 32 },
                { CardRank.Four, 32 },
                { CardRank.Three, 32 },
                { CardRank.Two, 32 },
            };
        }

        public Card GetCard()
        {
            Random randomizer = new Random();
            int randomNumber = randomizer.Next(1, AllCardsCount);
            int currentCard = 0;
            CardRank? rank = null;

            foreach (var cards in Decks)
            {
                currentCard += cards.Value;
                if (currentCard >= randomNumber)
                {
                    Decks[cards.Key]--;
                    AllCardsCount--;
                    rank = cards.Key;
                    break;
                }
            }

            return new Card((CardRank)rank);
        }
    }
}
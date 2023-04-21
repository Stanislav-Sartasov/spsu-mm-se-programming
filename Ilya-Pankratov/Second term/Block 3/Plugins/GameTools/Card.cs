namespace GameTools
{
    public class Card
    {
        public Visibility Flag { get; set; }
        public CardSuit Suit { get; set; }
        public CardRank Rank { get; set; }

        public Card(int suit, int rank)
        {
            Suit = (CardSuit)suit;
            Rank = (CardRank)rank;
            Flag = Visibility.Visible;
        }

        public Card(CardSuit suit, CardRank rank)
        {
            Suit = suit;
            Rank = rank;
            Flag = Visibility.Visible;
        }

        public int GetPoints(int playerPoints)
        {
            int cardPoints;

            if (Rank == CardRank.Ace)
            {
                if (playerPoints + 11 < 21)
                {
                    cardPoints = 11;
                }
                else
                {
                    cardPoints = (int)CardRank.Ace;
                }
            }
            else if (Rank < CardRank.Ten)
            {
                cardPoints = (int)Rank;
            }
            else
            {
                cardPoints = 10;
            }

            return cardPoints;
        }
    }
}

namespace BasicLibrary
{

    public class Card
    {
        private CardSuit suit;
        private CardRank rank;
        private int value;

        public Card(CardSuit suit, CardRank rank)
        {
            this.suit = suit;
            this.rank = rank;
            value = CalculateValue((int)rank);
        }
        public int CalculateValue(int rank)
        {
            if (rank > 10) return 10;
            else return rank;

        }
        public static bool operator ==(Card firstCard, Card secondCard)
        {
            if (firstCard.rank != secondCard.rank ||
                firstCard.suit != secondCard.suit ||
                firstCard.value != secondCard.value)
                return false;
            else return true;


        }
        public static bool operator !=(Card firstCard, Card secondCard)
        {
            return !(firstCard == secondCard);
        }

        public int GetCardValue()
        {
            return value;
        }
    }
}

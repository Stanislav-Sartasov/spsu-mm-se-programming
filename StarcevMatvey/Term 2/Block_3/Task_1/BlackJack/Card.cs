using BlackJackEnumerations;

namespace BlackJack
{
    public class Card
    {
        public CardSuit Suit { get; private set; }
        public CardValue Value { get; private set; }

        public Card(int suit, int value)
        {
            Suit = (CardSuit)suit;
            Value = (CardValue)value;
        }

        public int GetValue()
        {
            if (Value < CardValue.Jack)
            {
                return (int)Value;
            }
            else
            {
                return 10;
            }
        }

        public static bool operator ==(Card LeftCard, Card RightCard)
        {
            return LeftCard.Suit == RightCard.Suit && LeftCard.Value == RightCard.Value;
        }

        public static bool operator !=(Card LeftCard, Card RightCard)
        {
            return !(LeftCard == RightCard);
        }
    }
}
namespace BlackJack
{
    public class Card
    {
        public string Suit { get; private set; }
        public string Value { get; private set; }

        public Card(string suit, string value)
        {
            Suit = suit;
            Value = value;
        }

        public int GetValue()
        {
            if (Value == "A")
            {
                return 1;
            }
            else if (Value.ToCharArray().All(x => Char.IsDigit(x)))
            {
                return Int32.Parse(Value);
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
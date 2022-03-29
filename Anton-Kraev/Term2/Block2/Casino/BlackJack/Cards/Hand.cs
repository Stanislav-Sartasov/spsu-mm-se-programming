namespace Cards
{
    public class Hand : ICloneable
    {
        public List<Card> Cards;

        public int Bet;

        private int acesCount;

        public Hand(int bet)
        {
            Bet = bet;
            Cards = new List<Card>();
        }

        public object Clone()
        {
            var copy = new Hand(Bet);
            copy.Cards.Add(Cards[0]);
            copy.acesCount = acesCount;
            return copy;
        }

        public int GetHandValue()
        {
            int handValue = 0;
            acesCount = 0;

            foreach (var card in Cards)
            {
                if (card.Name == "A")
                    acesCount++;
                handValue += card.GetCardValue();
            }

            foreach (var card in Cards)
            {
                if (card.Name == "A" && handValue > 21)
                {
                    handValue -= 10;
                    acesCount--;
                }
            }

            return handValue;
        }

        public bool ContainsAce()
        {
            GetHandValue();
            if (acesCount == 0)
                return false;
            return true;
        }
    }
}
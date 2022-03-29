using Cards;

namespace Casino
{
    public class Croupier
    {
        public Card OpenCard;

        public List<Card> Hand;
        public int HandValue { get; private set; }

        public Croupier()
        {
            HandValue = 0;
            Hand = new List<Card>();
            OpenCard = new Card("0");
        }

        public void Play(Shoes shoes)
        {
            foreach (var card in Hand)
            {
                HandValue += card.GetCardValue();
            }

            while (HandValue < 17)
            {
                Card newCard = shoes.GetCard();
                Hand.Add(newCard);
                HandValue += newCard.GetCardValue();
            }

            foreach (var card in Hand)
            {
                if (card.Name == "A" && HandValue > 21)
                    HandValue -= 10;
            }
        }
    }
}
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
        }

        public void Play(Shoes shoes)
        {
            HandValue = 0;

            foreach (var card in Hand)
            {
                HandValue += card.GetCardRank();
            }

            while (HandValue < 17)
            {
                Card newCard = shoes.GetCard();
                Hand.Add(newCard);
                HandValue += newCard.GetCardRank();
                foreach (var card in Hand)
                {
                    if (card.Rank == CardRank.Ace && HandValue > 21)
                        HandValue -= 10;
                }
            }
        }
    }
}
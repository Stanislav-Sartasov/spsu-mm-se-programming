using Game.Cards;


namespace Game.Players
{
    public abstract class Hand
    {
        public const int BlackJackHandValue = 21;

        public abstract string Name { get; }
        public List<Card> Cards { get; }


        public Hand()
        {
            Cards = new List<Card>();
        }


        public virtual void ClearHand()
        {
            Cards.Clear();
        }

        public virtual void Hit(Card card)
        {
            AddCard(card);
        }

        public void AddCard(Card card)
        {
            Cards.Add(card);
        }

        public int GetValue()
        {
            int totalValue = 0;

            foreach (var card in Cards)
                totalValue += card.GetValueOfCard(totalValue);

            return totalValue;
        }
    }
}
